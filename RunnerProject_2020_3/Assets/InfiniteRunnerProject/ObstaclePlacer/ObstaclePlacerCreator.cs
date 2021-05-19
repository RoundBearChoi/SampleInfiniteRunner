using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class ObstaclePlacerCreator : UnitCreator
    {
        private Transform _parentTransform = null;
        private Unit _runner = null;
        private GameStage _gameStage = null;

        public ObstaclePlacerCreator(Unit runner, GameStage gameStage)
        {
            _runner = runner;
            _parentTransform = gameStage.transform;
            _gameStage = gameStage;
        }

        public override Unit GetUnit()
        {
            GameObject obj = new GameObject("ObstaclePlacer (Clone)");
            Unit placer = obj.AddComponent<ObstaclePlacer>();
            placer.stateController = new StateController(StateFactory.Create_ObstaclePlacer_Repeat(placer.unitData, _runner, _gameStage));
            placer.transform.parent = _parentTransform;
            placer.transform.localPosition = Vector3.zero;
            placer.transform.localRotation = Quaternion.identity;
            placer.SetUpdater(new PlacerUpdater(placer.stateController, _runner));

            placer.listSpriteAnimations = new List<SpriteAnimation>();

            return placer;
        }
    }
}