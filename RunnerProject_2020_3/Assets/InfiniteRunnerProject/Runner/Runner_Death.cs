using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class Runner_Death : State
    {
        private static SpriteAnimationSpec _animationSpec = null;

        public static void SetAnimationSpec()
        {
            _animationSpec = UnitCreator.currentSpec;
        }

        public Runner_Death(Unit unit)
        {
            Debugger.Log("runner is dead");
            _unit = unit;
        }
        public override void OnEnter()
        {
            IMessage message = new UIMessage("runner is dead");
            message.Register();

            _unit.unitData.unitTransform.position = _unit.unitData.unitTransform.position + (Vector3.back * 1f);
            _unit.unitData.rigidBody2D.velocity = new Vector3(0f, 6f, 0f);
            _unit.unitData.boxCollider2D.enabled = false;
        }

        public override void OnFixedUpdate()
        {
            if (_unit.unitData.unitTransform.position.y <= -20f)
            {
                _unit.unitData.rigidBody2D.Sleep();
            }
        }

        public override SpriteAnimationSpec GetSpriteAnimationSpec()
        {
            return _animationSpec;
        }
    }
}