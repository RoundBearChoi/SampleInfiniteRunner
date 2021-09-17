using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Server
{
    [System.Serializable]
    public class ServerTCP
    {
        [SerializeField]
        private int _id;

        public System.Net.Sockets.TcpClient socket;

        int _dataBufferSize = 0;
        System.Net.Sockets.NetworkStream stream;
        RB.Network.Packet receivedData;
        byte[] receiveBuffer;

        public ServerTCP(int id)
        {
            _id = id;
        }

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public void Connect(System.Net.Sockets.TcpClient incomingSocket, int dataBufferSize)
        {
            socket = incomingSocket;
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            stream = socket.GetStream();

            _dataBufferSize = dataBufferSize;

            receivedData = new RB.Network.Packet();
            receiveBuffer = new byte[_dataBufferSize];

            stream.BeginRead(receiveBuffer, 0, _dataBufferSize, ReceiveCallback, null);

            ServerManager.CURRENT.serverSend.Welcome(_id, "Welcome to the server!");
            ServerManager.CURRENT.serverSend.ClientsConnectionStatus();
        }

        public void SendData(RB.Network.Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log($"Error sending data to player {_id} via TCP: {e}");
            }
        }

        private void ReceiveCallback(System.IAsyncResult result)
        {
            try
            {
                int byteLength = stream.EndRead(result);
                if (byteLength <= 0)
                {
                    ClientData clientData = ServerManager.CURRENT.serverController.connectedClients.GetClientData(_id);
                    clientData.Disconnect();
                    return;
                }

                byte[] data = new byte[byteLength];
                System.Array.Copy(receiveBuffer, data, byteLength);

                receivedData.Reset(HandleData(data)); // Reset receivedData if all data was handled
                stream.BeginRead(receiveBuffer, 0, _dataBufferSize, ReceiveCallback, null);
            }
            catch (System.Exception e)
            {
                Debug.Log($"Error receiving TCP data: {e}");

                ClientData clientData = ServerManager.CURRENT.serverController.connectedClients.GetClientData(_id);
                clientData.Disconnect();
            }
        }

        private bool HandleData(byte[] data)
        {
            int packetLength = 0;

            receivedData.SetBytes(data);

            if (receivedData.UnreadLength() >= 4)
            {
                // If client's received data contains a packet
                packetLength = receivedData.ReadInt();
                if (packetLength <= 0)
                {
                    // If packet contains no data
                    return true; // Reset receivedData instance to allow it to be reused
                }
            }

            while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
            {
                // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
                byte[] _packetBytes = receivedData.ReadBytes(packetLength);
                RB.Network.ThreadControl.ExecuteOnMainThread(() =>
                {
                    using (RB.Network.Packet _packet = new RB.Network.Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        ServerManager.CURRENT.serverController.packetHandlers[_packetId](_id, _packet); // Call appropriate method to handle the packet
                    }
                });

                packetLength = 0; // Reset packet length
                if (receivedData.UnreadLength() >= 4)
                {
                    // If client's received data contains another packet
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        // If packet contains no data
                        return true; // Reset receivedData instance to allow it to be reused
                    }
                }
            }

            if (packetLength <= 1)
            {
                return true; // Reset receivedData instance to allow it to be reused
            }

            return false;
        }

        public void Disconnect()
        {
            socket.Close();

            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;
        }
    }
}