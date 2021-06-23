using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class FrontEnemyCreator : UnitCreator
    {
        private Transform _parentTransform;

        public FrontEnemyCreator(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        public override Unit GetUnit()
        {
            SampleLeftEnemy enemy = GameObject.Instantiate(ResourceLoader.GetResource(typeof(SampleLeftEnemy))) as SampleLeftEnemy;
            enemy.unitData = new UnitData(enemy.transform);
            enemy.attackData = new AttackData();

            enemy.stateController = new StateController(
                new FrontEnemy_Idle(enemy.unitData),
                enemy.unitData);
            enemy.transform.parent = _parentTransform;
            enemy.transform.localRotation = Quaternion.identity;
            enemy.SetUpdater(new DefaultUpdater(enemy.stateController));

            enemy.unitData.spriteAnimations = new SpriteAnimations(enemy.stateController);
            //enemy.InitSpriteAnimations();

            enemy.unitData.spriteAnimations.Add("front enemy idle animation",
                new SpriteAnimationSpecs(
                    "Texture_Front_Enemy_Sample",
                    10,
                    new Vector2(2.33f, 2.57f),
                    OffsetType.BOTTOM_CENTER,
                    Vector2.zero),
                enemy.transform);

            return enemy;
        }
    }
}