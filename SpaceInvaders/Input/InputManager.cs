using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputManager
    {
        private InputManager()
        {
            this.pSubjectMoveLeft = new InputSubject();
            this.pSubjectMoveRight = new InputSubject();
            this.pSubjectShootMissile = new InputSubject();
            this.pSubjectToggleShield = new InputSubject();
            this.pSubjectToggleCollisionBox = new InputSubject();

            this.isSpaceKeyPressedPrev = false;
            this.isCKeyPressedPrev = false;
            this.isSKeyPressedPrev = false;
        }
        
        public static void Create()
        {
            if(pInstance == null)
            {
                pInstance = new InputManager();
            }
            Debug.Assert(pInstance != null);
        }
        
        private static InputManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static InputSubject GetMoveRightSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectMoveRight;
        }

        public static InputSubject GetMoveLeftSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectMoveLeft;
        }

        public static InputSubject GetShootMissileSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectShootMissile;
        }

        public static InputSubject GetOnOffShieldSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectToggleShield;
        }

        public static InputSubject GetOnOffCollisionBoxSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectToggleCollisionBox;
        }


        public static void Update()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Pressed Space Bar : Shoot Missile
            bool isSpaceKeyPressedNow = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
            bool isCKeyPressedNow = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_C);
            bool isSKeyPressedNow = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_S);

            if (isSpaceKeyPressedNow && pMan.isSpaceKeyPressedPrev == false)
            {
                pMan.pSubjectShootMissile.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT))
            {
                pMan.pSubjectMoveLeft.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT))
            {
                pMan.pSubjectMoveRight.Notify();
            }

            if (isCKeyPressedNow && pMan.isCKeyPressedPrev == false )
            {
                pMan.pSubjectToggleCollisionBox.Notify();
            }

            if (isSKeyPressedNow && pMan.isSKeyPressedPrev == false )
            {
                pMan.pSubjectToggleShield.Notify();
            }

            if(Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) && GameScene.GetCurrentGameSceneState() == GameState.SceneName.Select)
            {
                GameScene.Set1PlayerMode();
                GameScene.Handle();
            }

            //if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) && GameScene.GetCurrentGameSceneState() == GameState.SceneName.Select)
            //{
            //    GameScene.Set2PlayerMode();
            //    GameScene.Handle();
            //}

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_R) && GameScene.GetCurrentGameSceneState() == GameState.SceneName.GameOver)
            {
                GameScene.Handle();
            }

            pMan.isSpaceKeyPressedPrev = isSpaceKeyPressedNow;
            pMan.isCKeyPressedPrev = isCKeyPressedNow;
            pMan.isSKeyPressedPrev = isSKeyPressedNow;
        }

        private static InputManager pInstance = null;
        private bool isSpaceKeyPressedPrev;
        private bool isCKeyPressedPrev;
        private bool isSKeyPressedPrev;

        private InputSubject pSubjectMoveRight;
        private InputSubject pSubjectMoveLeft;
        private InputSubject pSubjectShootMissile;

        private InputSubject pSubjectToggleShield;
        private InputSubject pSubjectToggleCollisionBox;
    }
}
