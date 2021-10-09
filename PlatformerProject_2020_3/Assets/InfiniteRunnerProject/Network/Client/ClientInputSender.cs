using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Client
{
    public class ClientInputSender : MonoBehaviour
    {
        [SerializeField]
        protected bool[] _inputs = null;

        public virtual void SendInputToServer()
        {
            if (_inputs == null || _inputs.Length == 0)
            {
                _inputs = new bool[7];
            }

            _inputs[0] = ContainsPress(CommandType.MOVE_UP);
            _inputs[1] = ContainsPress(CommandType.MOVE_DOWN);
            _inputs[2] = ContainsPress(CommandType.MOVE_LEFT);
            _inputs[3] = ContainsPress(CommandType.MOVE_RIGHT);
            _inputs[4] = ContainsPress(CommandType.JUMP);
            _inputs[5] = ContainsPress(CommandType.ATTACK_A);
            _inputs[6] = ContainsPress(CommandType.SHIFT);

            RB.Client.ClientSend.SendClientInput(_inputs);
        }

        bool ContainsPress(CommandType commandType)
        {
            InputController inputController = GameInitializer.current.GetStage().GetInputController();
            UserInput latestInput = inputController.GetLatestUserInput();

            if (latestInput.commands.ContainsPress(commandType, false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}