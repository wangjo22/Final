using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    public class GameSceneManager
    {
        private GameSceneManager() 
        {
            this.pSelect = new SelectScene();
            Debug.Assert(this.pSelect != null);

            this.p1Scene = new Player1Scene();
            Debug.Assert(this.p1Scene != null);

            this.p2Scene = new Player2Scene();
            Debug.Assert(this.p2Scene != null);

            this.pOver = new GameOverState();
            Debug.Assert(this.pOver != null);

        }
        public static void Create()
        {
            Debug.Assert(pInstance == null);
            if(pInstance == null)
            {
                pInstance = new GameSceneManager();
            }
            Debug.Assert(pInstance != null);
        }

        private static GameSceneManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static GameState GetSelectSceneState()
        {
            GameSceneManager pMan = GameSceneManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSelect;
        }

        public static GameState GetPlayer1SceneState()
        {
            GameSceneManager pMan = GameSceneManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.p1Scene;
        }

        public static GameState GetPlayer2SceneState()
        {
            GameSceneManager pMan = GameSceneManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.p2Scene;
        }

        public static GameState GetGameOverState()
        {
            GameSceneManager pMan = GameSceneManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pOver;
        }


        private static GameSceneManager pInstance;
        private SelectScene pSelect;
        private Player1Scene p1Scene;
        private Player2Scene p2Scene;
        private GameOverState pOver;
    }       
}
