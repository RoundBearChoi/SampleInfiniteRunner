using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class Runner_Slide_GetUp : UnitState
    {
        public static SpriteAnimationSpec animationSpec = null;

        public Runner_Slide_GetUp(Unit unit)
        {
            ownerUnit = unit;
            _listStateComponents.Add(new LerpRunSpeedOnFlatGround(ownerUnit, 0f, 0.05f));
            _listStateComponents.Add(new UpdateCollider2DSize(ownerUnit, new Vector2(0.8f, 3.4f)));
        }

        public override SpriteAnimationSpec GetSpriteAnimationSpec()
        {
            return animationSpec;
        }

        public override void OnFixedUpdate()
        {
            FixedUpdateComponents();

            if (ownerUnit.unitData.spriteAnimations.GetCurrentAnimation().IsOnEnd())
            {
                ownerUnit.unitData.listNextStates.Add(new Runner_NormalRun(ownerUnit));
            }
        }
    }
}