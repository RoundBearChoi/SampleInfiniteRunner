using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public static class ResourceLoader
    {
        static Dictionary<System.Type, Object> dicResources = new Dictionary<System.Type, Object>();
        static Dictionary<SpriteType, Object> dicSprites = new Dictionary<SpriteType, Object>();

        public static void Init()
        {
            GameStage game = Resources.Load("GameStage", typeof(GameStage)) as GameStage;
            dicResources.Add(game.GetType(), game);

            IntroStage intro = Resources.Load("IntroStage", typeof(IntroStage)) as IntroStage;
            dicResources.Add(intro.GetType(), intro);

            Runner runner = Resources.Load("Prefab_Runner", typeof(Runner)) as Runner;
            dicResources.Add(runner.GetType(), runner);

            Obstacle obstacle = Resources.Load("Obstacle", typeof(Obstacle)) as Obstacle;
            dicResources.Add(obstacle.GetType(), obstacle);

            CollisionDetector collisionDetector = Resources.Load("CollisionDetector", typeof(CollisionDetector)) as CollisionDetector;
            dicResources.Add(collisionDetector.GetType(), collisionDetector);

            UI ui = Resources.Load("UI", typeof(UI)) as UI;
            dicResources.Add(ui.GetType(), ui);

            DefaultUIBlock defaultUIBlock = Resources.Load("DefaultUIBlock", typeof(DefaultUIBlock)) as DefaultUIBlock;
            dicResources.Add(defaultUIBlock.GetType(), defaultUIBlock);

            RunnerDeathNotification deathNotification = Resources.Load("RunnerDeathNotification", typeof(RunnerDeathNotification)) as RunnerDeathNotification;
            dicResources.Add(deathNotification.GetType(), deathNotification);

            dicSprites.Add(SpriteType.WHITE_BOX, Resources.Load("Sprite_WhiteBox") as GameObject);
        }

        public static Object Get(System.Type _type)
        {
            if (dicResources.ContainsKey(_type))
            {
                return dicResources[_type];
            }
            else
            {
                return null;
            }
        }

        public static Object GetSprite(SpriteType spriteType)
        {
            if (dicSprites.ContainsKey(spriteType))
            {
                return dicSprites[spriteType];
            }
            else
            {
                return null;
            }
        }
    }
}