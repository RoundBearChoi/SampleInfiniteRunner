using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class IntroStageTransition : IStageTransition
    {
        private GameInitializer _gameInitializer = null;

        public IntroStageTransition(GameInitializer initializer)
        {
            _gameInitializer = initializer;
        }

        public Stage MakeTransition()
        {
            //temp - will no longer use prefabs
            Stage introStage = GameObject.Instantiate(ResourceLoader.Get(typeof(IntroStage))) as Stage;
            introStage.SetInitializer(_gameInitializer);
            introStage.transform.parent = _gameInitializer.transform;
            introStage.transform.localPosition = Vector3.zero;
            introStage.transform.localRotation = Quaternion.identity;

            introStage.Init();

            return introStage;
        }
    }
}