using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class LittleRed_Run : UnitState
    {
        public LittleRed_Run(Unit unit)
        {
            ownerUnit = unit;

            float runspeed = BaseInitializer.current.fighterDataSO.DefaultRunSpeed;

            if (!ownerUnit.unitData.facingRight)
            {
                runspeed *= -1f;
            }

            _listStateComponents.Add(new LerpHorizontalSpeed_FlatGround(ownerUnit, runspeed, BaseInitializer.current.fighterDataSO.RunSpeedLerpPercentage));
            _listStateComponents.Add(new TriggerLittleRedAttackA(ownerUnit));
            _listStateComponents.Add(new TriggerFallState(ownerUnit));
            _listStateComponents.Add(new CreateStepDust(ownerUnit));

            _listMatchingSpriteTypes.Add(SpriteType.LITTLE_RED_RUN);
        }

        public override void OnFixedUpdate()
        {
            FixedUpdateComponents();

            //speed up
            if (fixedUpdateCount > 50)
            {
                Debugger.Log("start speeding");
            }

            //when touching ground
            if (ownerUnit.unitData.collisionStays.IsTouchingGround(CollisionType.BOTTOM) ||
                ownerUnit.unitData.collisionEnters.IsTouchingGround(CollisionType.BOTTOM))
            {
                if (ownerUnit.USER_INPUT.commands.ContainsHoldOrPress(CommandType.JUMP))
                {
                    BaseMessage jumpDustMessage = new Message_ShowJumpDust(true, ownerUnit.transform.position);
                    jumpDustMessage.Register();

                    //multiply/divide runspeed on jump
                    ownerUnit.unitData.rigidBody2D.velocity = new Vector2(ownerUnit.unitData.rigidBody2D.velocity.x * GameInitializer.current.fighterDataSO.HorizontalMomentumMultiplierOnRunningJump, ownerUnit.unitData.rigidBody2D.velocity.y);
                    ownerUnit.unitData.airControl.SetMomentum(ownerUnit.unitData.rigidBody2D.velocity.x);
                    ownerUnit.unitData.listNextStates.Add(new LittleRed_Jump_Up(ownerUnit, BaseInitializer.current.fighterDataSO.VerticalJumpForce, 0));
                }

                if (!ownerUnit.USER_INPUT.commands.ContainsHold(CommandType.MOVE_RIGHT) && ownerUnit.unitData.facingRight)
                {
                    ownerUnit.unitData.listNextStates.Add(new LittleRed_Idle(ownerUnit));
                }

                if (!ownerUnit.USER_INPUT.commands.ContainsHold(CommandType.MOVE_LEFT) && !ownerUnit.unitData.facingRight)
                {
                    ownerUnit.unitData.listNextStates.Add(new LittleRed_Idle(ownerUnit));
                }
            }
        }
    }
}