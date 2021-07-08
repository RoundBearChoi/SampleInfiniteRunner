using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class Golem_Creator : UnitCreator
    {
        private Transform _parentTransform;

        public Golem_Creator(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        public override Unit GetUnit()
        {
            Unit golem = GameObject.Instantiate(ResourceLoader.unitLoader.GetObj(UnitType.GOLEM)) as Unit;
            golem.transform.parent = _parentTransform;
            golem.transform.localRotation = Quaternion.identity;

            golem.unitData = new UnitData(golem.transform);
            golem.attackData = new AttackData();

            golem.iStateController = new StateController(
                new Golem_Idle(golem),
                golem.unitData);

            golem.InitBoxCollider(StaticRefs.golemSpriteData.GolemBoxColliderSize);
            golem.InitCollisionChecker();

            golem.unitData.spriteAnimations = new SpriteAnimations(golem.iStateController);
            golem.unitData.faceRight = false;

            SetIdle(golem);

            return golem;
        }

        public override void AddUnits(List<Unit> listUnits)
        {
            listUnits.Add(GetUnit());
        }

        void SetIdle(Unit unit)
        {
            unit.unitData.spriteAnimations.AddSpriteAnimation(
                "golem idle animation",
                new SpriteAnimationSpecs(
                    StaticRefs.golemSpriteData.Golem_SpriteName,
                    StaticRefs.golemSpriteData.Golem_SpriteInterval,
                    StaticRefs.golemSpriteData.Golem_SpriteSize,
                    OffsetType.BOTTOM_CENTER,
                    Vector2.zero),
                unit.transform);
        }
    }
}