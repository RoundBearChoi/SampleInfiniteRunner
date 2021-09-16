using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Client
{
    public class ClientControl : BaseClientControl
    {
        public static FighterClient fighterClient = null;

        [SerializeField]
        ClientConnection[] _clientConnections = null;

        public override void ConnectToServer()
        {
            _connectionFailed = false;
            _clientConnections = new[] {
                new ClientConnection (999, false),
                new ClientConnection (999, false),
                new ClientConnection (999, false),};

            if (fighterClient == null)
            {
                fighterClient = Instantiate(ResourceLoader.etcLoader.GetObj(etcType.FIGHTER_CLIENT)) as FighterClient;
                fighterClient.transform.SetParent(this.transform, true);
            }

            string hostIP = GetHostIP();
            Client.instance.ConnectToServer(hostIP);
        }

        public override void ShowEnterIPUI()
        {
            BaseInitializer.current.stageTransitioner.AddNextStage(BaseStage.InstantiateNewStage(StageType.ENTER_IP_STAGE));
        }

        public override void QueueConnectionFailedMessage()
        {
            _connectionFailed = true;
        }

        public override int GetClientIndex()
        {
            return Client.instance.myId;
        }

        public override void UpdateClientConnectionStatus(ClientConnection[] arr)
        {
            _clientConnections = arr;
        }

        public override ClientConnection[] GetClientConnectionStatus()
        {
            return _clientConnections;
        }
    }
}