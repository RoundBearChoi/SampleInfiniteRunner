using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class Runner_Jump_Fall : State
    {
        private UserInput _userInput = null;
        private static SpriteAnimationSpec _animationSpec = null;

        public static void SetAnimationSpec()
        {
            _animationSpec = UnitCreator.currentSpec;
        }

        public Runner_Jump_Fall(Unit unit, UserInput input)
        {
            _unit = unit;
            _userInput = input;
        }

        public override void OnEnter()
        {

        }

        public override void OnFixedUpdate()
        {
            if (_unit.unitData.collisionStays.IsTouchingGround(CollisionType.BOTTOM))
            {
                _unit.unitData.listNextStates.Add(new Runner_NormalRun(_unit, _userInput));

                //Units.instance.AddCreator(new LandingDust_Creator(Stage.currentStage.transform));
                //Units.instance.ProcessCreators();
                //Units.instance.GetUnit<LandingDust>().transform.position = _unit.transform.position;
            }

            UpdateComponents();
        }

        public override SpriteAnimationSpec GetSpriteAnimationSpec()
        {
            return _animationSpec;
        }
    }
}