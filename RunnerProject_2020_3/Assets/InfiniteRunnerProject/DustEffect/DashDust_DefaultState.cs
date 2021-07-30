using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class DashDust_DefaultState : UnitState
    {
        public static SpriteAnimationSpec animationSpec;

        public DashDust_DefaultState(Unit unit)
        {
            ownerUnit = unit;

            _listMatchingSpriteTypes.Add(SpriteType.DUST_DASH);
        }

        public override SpriteAnimationSpec GetSpriteAnimationSpec()
        {
            return animationSpec;
        }

        public override void OnFixedUpdate()
        {
            if (ownerUnit.unitData.spriteAnimations.GetCurrentAnimation().IsOnEnd())
            {
                ownerUnit.destroy = true;
            }
        }
    }
}