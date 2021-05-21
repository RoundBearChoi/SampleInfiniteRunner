using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class GameStage : Stage
    {
        private UI ui = null;
        private FixedUpdateCounter fixedUpdateCounter = new FixedUpdateCounter();
        private UpdateCounter updateCounter = new UpdateCounter();
        private UserInput userInput = new UserInput();

        [SerializeField]
        private GameData gameDataScriptableObj = null;

        public override void Init()
        {
            StaticRefs.gameData = gameDataScriptableObj;

            _listUnitCreators.Add(new RunnerCreator(userInput, this.transform));
            CreateUnits();

            _listUnitCreators.Add(new CameraControllerCreator(this.transform, units.GetUnit(0), FindObjectOfType<Camera>()));
            _listUnitCreators.Add(new ObstaclePlacerCreator(units.GetUnit(0), this));
            CreateUnits();

            ui = Instantiate(ResourceLoader.Get(typeof(UI))) as UI;
            ui.SetCounters(fixedUpdateCounter, updateCounter);
            ui.transform.parent = this.transform;
            ui.transform.localPosition = Vector3.zero;
            ui.transform.localRotation = Quaternion.identity;
        }

        public override void OnUpdate()
        {
            updateCounter.OnUpdate();
            userInput.OnUpdate();
            ui.OnUpdate();

            CreateUnits();
        }

        public override void OnFixedUpdate()
        {
            fixedUpdateCounter.OnFixedUpdate();
            units.OnFixedUpdate();

            foreach (KeyPress press in userInput.listPresses)
            {
                if (press.keyCode == KeyCode.F5)
                {
                    _gameIntializer.stageTransition = new GameStageTransition(_gameIntializer);
                    break;
                }

                if (press.keyCode == KeyCode.F6)
                {
                    _gameIntializer.stageTransition = new IntroStageTransition(_gameIntializer);
                    break;
                }
            }

            userInput.listPresses.Clear();
        }
    }
}