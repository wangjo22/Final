using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameScene
    {
        private GameScene() 
        {
            this.player1_Lives = 0;
            this.player2_Lives = 0;
            this.Is2PlayerMode = false;
        }

        public static void Create()
        {
            Debug.Assert(pInstance == null);
            if(pInstance == null)
            {
                pInstance = new GameScene();
            }
            Debug.Assert(pInstance != null);
        }

        private static GameScene PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static void Handle()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.pState.Handle();
        }

        public static void LoadContent()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.pState.LoadContent();
        }

        public static void Update(float getTime)
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.pState.Update(getTime);
        }

        public static void Draw()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.pState.Draw();
        }

        public static void SetGameScene(GameState.SceneName name)
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            switch(name)
            {
                case GameState.SceneName.Select:
                    pScene.pState = GameSceneManager.GetSelectSceneState();
                    break;
                case GameState.SceneName.Player1:
                    pScene.pState = GameSceneManager.GetPlayer1SceneState();
                    break;
                case GameState.SceneName.Player2:
                    pScene.pState = GameSceneManager.GetPlayer2SceneState();
                    break;
                case GameState.SceneName.GameOver:
                    pScene.pState = GameSceneManager.GetGameOverState();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public static GameState.SceneName GetCurrentGameSceneState()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);
            return pScene.pState.GetStateName();
        }


        public static void Set1PlayerMode()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.player1_Lives = 3;
            pScene.player2_Lives = 0;
            pScene.Is2PlayerMode = false;
        }


        public static void Set2PlayerMode()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.player1_Lives = 3;
            pScene.player2_Lives = 3;
            pScene.Is2PlayerMode = true;
        }

        public static void Player1Dead()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.player1_Lives--;
            if (pScene.player1_Lives < 0)
            {
                GameScene.Player1GameOver();
            }
        }

        public static void Player2Dead()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.player2_Lives--;
        }

        public static int Get1PlayerLive()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            return pScene.player1_Lives;
        }

        public static int Get2PlayerLives()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            return pScene.player2_Lives;
        }

        public static bool CheckIs2PlayerMode()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            return pScene.Is2PlayerMode;
        }

        public static void MoveToNextStage()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            pScene.pState.MoveToNextStage();
        }

        public static void Player1GameOver()
        {
            GameScene pScene = GameScene.PrivGetInstance();
            Debug.Assert(pScene != null);

            if(pScene.pState is Player1Scene)
            {
                Player1Scene pp1s = (Player1Scene)pScene.pState;
                pp1s.GameOver();
            }
        }




        private static GameScene pInstance;
        private GameState pState;
        private int player1_Lives;
        private int player2_Lives;
        private bool Is2PlayerMode;
    }       
}
