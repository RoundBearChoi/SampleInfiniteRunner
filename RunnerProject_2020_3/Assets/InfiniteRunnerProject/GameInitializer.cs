using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class GameInitializer : MonoBehaviour
    {
        GameStage game = null;
        IntroStage intro = null;

        private void Start()
        {
            ResourceLoader.Init();

            StartNewGame();
        }

        private void StartNewGame()
        {
            game = Instantiate(ResourceLoader.Get(typeof(GameStage))) as GameStage;
            game.Init();
            game.transform.parent = this.transform;
            game.transform.localPosition = Vector3.zero;
            game.transform.localRotation = Quaternion.identity;
        }

        private void StartIntro()
        {
            intro = Instantiate(ResourceLoader.Get(typeof(IntroStage))) as IntroStage;
        }

        private void Update()
        {
            if (game != null)
            {
                game.OnUpdate();

                if (game.RestartGame())
                {
                    Destroy(game.gameObject);
                    game = null;
                    StartNewGame();
                }

                if (game.ReturnToIntro())
                {
                    Destroy(game.gameObject);
                    game = null;
                    StartIntro();
                }
            }

            if (intro != null)
            {
                if (intro.EnterPressed)
                {
                    Destroy(intro.gameObject);
                    intro = null;
                    StartNewGame();
                }
            }
        }

        private void FixedUpdate()
        {
            if (game != null)
            {
                game.OnFixedUpdate();
            }
        }
    }
}