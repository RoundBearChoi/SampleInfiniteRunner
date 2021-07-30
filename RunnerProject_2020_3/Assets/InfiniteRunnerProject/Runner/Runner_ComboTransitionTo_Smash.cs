using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class Runner_ComboTransitionTo_Smash : UnitState
    {
        public static SpriteAnimationSpec animationSpec = null;

        public Runner_ComboTransitionTo_Smash(Unit unit)
        {
            ownerUnit = unit;
            _listStateComponents.Add(new LerpHorizontalSpeed_Air(ownerUnit, 0.01f, 0.05f));
            _listStateComponents.Add(new LerpVerticalSpeed_Air(ownerUnit, -0.1f, 0.15f));

            _listMatchingSpriteTypes.Add(SpriteType.RUNNER_COMBOTRANSITIONTO_SMASH);
        }

        public override SpriteAnimationSpec GetSpriteAnimationSpec()
        {
            return animationSpec;
        }

        public override void OnFixedUpdate()
        {
            FixedUpdateComponents();

            if (ownerUnit.unitData.rigidBody2D.velocity.y <= 0f)
            {
                ownerUnit.unitData.listNextStates.Add(new Runner_Smash_Air_Fall(ownerUnit));
            }
        }
    }
}