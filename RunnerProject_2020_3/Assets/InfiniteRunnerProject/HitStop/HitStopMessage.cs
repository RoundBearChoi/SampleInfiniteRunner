using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class HitStopMessage : BaseMessage
    {
        uint _totalHitStopFrames = 0;
        Unit _unit = null;

        public HitStopMessage(uint totalHitStopFrames, Unit unit, MessageType messageType)
        {
            _totalHitStopFrames = totalHitStopFrames;
            mMessageType = messageType;
            _unit = unit;
        }

        public override void Register()
        {
            GameInitializer.current.RunCoroutine(_register());
        }

        IEnumerator _register()
        {
            Debugger.Log("waiting 1 frame before register");
        
            yield return new WaitForEndOfFrame();

            Stage.currentStage.units.unitsMessageHandler.RegisterMessage(this);
        
            Debugger.Log("hitstop message registered.. " + "spriteindex: " + _unit.unitData.spriteAnimations.currentAnimation.SPRITE_INDEX);
        }

        public override uint GetUnsignedIntMessage()
        {
            return _totalHitStopFrames;
        }
    }
}