using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpawnUFO : Command
    {
        public SpawnUFO()
        {
            // initialized the sprite animation is attached to
        }

        public override void Execute(float deltaTime)
        {
            if(GameScene.GetCurrentGameSceneState() == GameState.SceneName.Player1 || 
                GameScene.GetCurrentGameSceneState() == GameState.SceneName.Player2)
            {
                UFOManager.ActivateUFO();
            }
        }


        public void Dump()
        {
            Debug.WriteLine("MovementSprite.Dump()... No Idea what to show now...");
        }
        // Data: ---------------
    }
}
