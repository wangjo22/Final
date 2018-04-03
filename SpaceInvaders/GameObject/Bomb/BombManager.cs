using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombManager
    {
        public enum BombType
        {
            Straight,
            ZigZag,
            Cross
        }
       
        private BombManager()
        {
            this.pSB_Bombs = SpriteBatchManager.Find(SpriteBatch.Name.BombSprites);
            Debug.Assert(this.pSB_Bombs != null);

            this.pSB_BombCollisionBox = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
            Debug.Assert(this.pSB_BombCollisionBox != null);

            this.pStraight = this.CreateBomb(BombType.Straight);
            Debug.Assert(this.pStraight != null);

            this.pZigZag = this.CreateBomb(BombType.ZigZag);
            Debug.Assert(this.pZigZag != null);
            
            this.pCross = this.CreateBomb(BombType.Cross);
            Debug.Assert(this.pCross != null);

            this.pBombGroup = null;
        }

        private Bomb CreateBomb(BombType type)
        {
            Bomb pBomb = null;
            switch(type)
            {
                case BombType.Straight:
                    pBomb = new Bomb(GameObject.Name.BombStraight, GameSprite.Name.BombStraight, 0.0f, 0.0f);
                    break;
                case BombType.ZigZag:
                    pBomb = new Bomb(GameObject.Name.BombZigZag, GameSprite.Name.BombZigZag, 0.0f, 0.0f);
                    break;
                case BombType.Cross:
                    pBomb = new Bomb(GameObject.Name.BombCross, GameSprite.Name.BombCross, 0.0f, 0.0f);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            Debug.Assert(pBomb != null);
            return pBomb;
        }


        static public void Create(BombGroup pGroup)
        {
            if (pInstance == null)
            {
                pInstance = new BombManager();
            }
            pInstance.pBombGroup = pGroup;
            Debug.Assert(pInstance != null);
        }

        private static BombManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static Bomb GetStraightFall(float posX, float posY)
        {
            BombManager pMan = BombManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            //if (pMan.pStraight.isDead == true)
            //{
                //pMan.pStraight.ResetPosition(posX, posY);
                pMan.pStraight = new Bomb(GameObject.Name.BombStraight, GameSprite.Name.BombStraight, posX, posY);
                pMan.pSB_Bombs.Attach(pMan.pStraight.pProxySprite);
                pMan.pSB_BombCollisionBox.Attach(pMan.pStraight.pColObject.pBoxSprite);
                pMan.pBombGroup.Add(pMan.pStraight);
                pMan.pStraight.isDead = false;
            //}
            return pMan.pStraight;
        }

        public static Bomb GetZigZagFall(float posX, float posY)
        {
            BombManager pMan = BombManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            //if (pMan.pZigZag.isDead == true)
            //{
                //pMan.pZigZag.ResetPosition(posX, posY);
                pMan.pZigZag = new Bomb(GameObject.Name.BombZigZag, GameSprite.Name.BombZigZag, posX, posY);
                pMan.pSB_Bombs.Attach(pMan.pZigZag.pProxySprite);
                pMan.pSB_BombCollisionBox.Attach(pMan.pZigZag.pColObject.pBoxSprite);
                pMan.pBombGroup.Add(pMan.pZigZag);
                pMan.pZigZag.isDead = false;
            //}
            return pMan.pZigZag;
        }
        public static Bomb GetCrossFall(float posX, float posY)
        {
            BombManager pMan = BombManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            //if (pMan.pCross.isDead == true)
            //{
                //pMan.pCross.ResetPosition(posX, posY);
                pMan.pCross = new Bomb(GameObject.Name.BombCross, GameSprite.Name.BombCross, posX, posY);
                pMan.pSB_Bombs.Attach(pMan.pCross.pProxySprite);
                pMan.pSB_BombCollisionBox.Attach(pMan.pCross.pColObject.pBoxSprite);
                pMan.pBombGroup.Add(pMan.pCross);
                pMan.pCross.isDead = false;
            //}
            return pMan.pCross;
        }

        public static void DeactiveBomb(Bomb b)
        {
            Debug.Assert(b != null);

            BombManager pMan = BombManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            
            if(b.pColObject.pBoxSprite.GetBackToSBNode() != null)
            {
                b.RemoveFromSpriteBatch();
            }
            pMan.pBombGroup.RemoveFromHeadAndLast(b);
        }

        public static void RemoveBombs()
        {
            BombGroup pGroup = (BombGroup)GameObjectManager.Find(GameObject.Name.BombGroup);
            Bomb pBomb = (Bomb)pGroup.poHead;
            Bomb pNextBomb;
            while(pBomb != null)
            {
                pNextBomb = (Bomb)pBomb.pNext;

                BombManager.DeactiveBomb(pBomb);
                pBomb = pNextBomb;
            }
        }

        private static BombManager pInstance;
        private SpriteBatch pSB_Bombs;
        private SpriteBatch pSB_BombCollisionBox;

        private Bomb pStraight;
        private Bomb pZigZag;
        private Bomb pCross;

        private BombGroup pBombGroup;
    }
}
