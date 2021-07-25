using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class RunnerStageTransition : IStageTransition
    {
        private GameInitializer _gameInitializer = null;

        public RunnerStageTransition(GameInitializer initializer)
        {
            _gameInitializer = initializer;
        }

        public BaseStage MakeTransition()
        {
            BaseStage runnerStage = GameObject.Instantiate(ResourceLoader.stageLoader.GetObj(StageType.RUNNER_STAGE)) as BaseStage;
            runnerStage.SetInitializer(_gameInitializer);
            runnerStage.transform.parent = _gameInitializer.transform;
            runnerStage.transform.localPosition = new Vector3(0f, -3f, 0f); //testing stage position
            runnerStage.transform.localRotation = Quaternion.identity;

            return runnerStage;
        }
    }
}