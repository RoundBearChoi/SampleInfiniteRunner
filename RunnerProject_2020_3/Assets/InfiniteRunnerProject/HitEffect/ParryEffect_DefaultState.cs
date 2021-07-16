using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class ParryEffect_DefaultState : State
    {
        public static SpriteAnimationSpec animationSpec;

        public ParryEffect_DefaultState(Unit unit)
        {
            _unit = unit;
        }

        public override void OnFixedUpdate()
        {
            if (_unit.unitData.spriteAnimations.currentAnimation.IsOnEnd())
            {
                _unit.destroy = true;
            }
        }

        public override SpriteAnimationSpec GetSpriteAnimationSpec()
        {
            return animationSpec;
        }
    }
}