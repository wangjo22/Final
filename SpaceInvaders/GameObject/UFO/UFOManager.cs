using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOManager
    {
        private UFOManager()
        {
            this.pSB_Sprite = SpriteBatchManager.Find(SpriteBatch.Name.GameSprites);
            Debug.Assert(this.pSB_Sprite != null);

            this.pSB_CollisionBox = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
            Debug.Assert(this.pSB_CollisionBox != null);

            this.pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 0.0f, -100.0f);
            this.pSB_Sprite.Attach(pUFO.pProxySprite);
            this.pSB_CollisionBox.Attach(pUFO.pColObject.pBoxSprite);
            this.pRandom = new Random();
        }


        static public void Create(UFOGroup pGroup)
        {
            if (pInstance == null)
            {
                pInstance = new UFOManager();
            }
            pInstance.pUFOGroup = pGroup;
            pInstance.pUFOGroup.Add(pInstance.pUFO);
            Debug.Assert(pInstance != null);
        }

        private static UFOManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }


        public static void ActivateUFO()
        {
            UFOManager pMan = UFOManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.pUFO.isDead = false;

            int lottery = pMan.pRandom.Next(2);
            if (lottery == 0)
            {
                pMan.pUFO.SetPos(40.0f, Constant.WINDOW_HEIGHT - 130);
                pMan.pUFO.SetMoveToRight();
            }
            else
            {
                pMan.pUFO.SetPos(Constant.WINDOW_WIDTH - 40.0f, Constant.WINDOW_HEIGHT - 130);
                pMan.pUFO.SetMoveToLeft();
            }
            SoundManager.Play(Sound.Name.UFO_Low);

        }

        public static void DeactiveUFO()
        {
            UFOManager pMan = UFOManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.pUFO.isDead = true;
            SoundManager.Play(Sound.Name.UFO_High);
            pMan.pUFO.SetPos(0.0f, -50);
        }

        public static bool CheckIsUFODead()
        {
            UFOManager pMan = UFOManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pUFO.isDead;
        }

        private static UFOManager pInstance;
        private SpriteBatch pSB_Sprite;
        private SpriteBatch pSB_CollisionBox;

        public UFOGroup pUFOGroup;
        public UFO pUFO;
        private Random pRandom;
    }
}
