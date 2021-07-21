using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class Swamp_BackTrees_DefaultState : UnitState
    {
        public static SpriteAnimationSpec animationSpec;

        public Swamp_BackTrees_DefaultState(Unit unit)
        {
            ownerUnit = unit;

            _listStateComponents.Add(new HorizontalParallax(unit, unit.transform.position, GameInitializer.current.swampParallaxSO.Swamp_BackTrees_ParallaxPercentage));
        }

        public override SpriteAnimationSpec GetSpriteAnimationSpec()
        {
            return animationSpec;
        }

        public override void OnFixedUpdate()
        {
            FixedUpdateComponents();
        }
    }
}