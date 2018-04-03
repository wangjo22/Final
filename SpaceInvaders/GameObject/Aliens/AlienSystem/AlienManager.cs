using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienManager_Link : Manager
    {
        public AlienNode_Link pActive = null;
        public AlienNode_Link pReserve = null;
    }
    public class AlienManager : AlienManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private AlienManager(int numReserve = 66, int reserveGrow = 10)
            : base()
        {
            this.numAliveAliens = 55;
            this.originDeltaTime = 1.0f;

            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new AlienNode();
        }

        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void Create(int reserveNum = 66, int reserveGrow = 10)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // Initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new AlienManager(reserveNum, reserveGrow);
            }
        }

        public static void ResetNumAliens()
        {
            AlienManager pMan = AlienManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.numAliveAliens = 55;
        }

        public static void OneAlienDead()
        {
            AlienManager pMan = AlienManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.numAliveAliens--;
        }

        public static void CheckDeltaTime()
        {
            AlienManager pMan = AlienManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            if (40 < pMan.numAliveAliens)
            {
                TimerManager.SetAlienDeltaTime(pMan.originDeltaTime * 1.0f);
            }
            else if (30 < pMan.numAliveAliens && pMan.numAliveAliens <= 40)
            {
                TimerManager.SetAlienDeltaTime(pMan.originDeltaTime * 0.7f);
            }
            else if (20 < pMan.numAliveAliens && pMan.numAliveAliens <= 30)
            {
                TimerManager.SetAlienDeltaTime(pMan.originDeltaTime * 0.4f);
            }
            else if (8 < pMan.numAliveAliens && pMan.numAliveAliens <= 20)
            {
                TimerManager.SetAlienDeltaTime(pMan.originDeltaTime * 0.2f);
            }
            else if (3 < pMan.numAliveAliens && pMan.numAliveAliens <= 8)
            {
                TimerManager.SetAlienDeltaTime(pMan.originDeltaTime * 0.1f);
            }
            else if(pMan.numAliveAliens <= 3)
            {
                TimerManager.SetAlienDeltaTime(pMan.originDeltaTime * 0.05f);
            }
        }

        public static void ResetAllAlienGrid()
        {
            AlienManager pMan = AlienManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            AlienNode pCurr = (AlienNode)pMan.BaseGetActive();
            AlienNode pNext;
            GameObject pParent;
            GameObject pObj;
            AlienCategory pAlien;
            while (pCurr != null)
            {
                pNext = (AlienNode)pCurr.pNext;

                // Get Alien object and its parent. The parent will be AlienGroup or AlienColumn.
                pObj = pCurr.GetAlienObject();

                pParent = (GameObject)pCurr.GetAlienObject().pParent;

                pObj.isDead = false;

                pObj.pPrev = null;
                pObj.pNext = null;

                pParent.Add(pObj);

                if (pObj.type == Component.Container.LEAF)
                {
                    pAlien = (AlienCategory)pObj;

                    pObj.x = pAlien.originX;
                    pObj.y = pAlien.originY;
                    SpriteBatch pSB_GameSprite = SpriteBatchManager.Find(SpriteBatch.Name.GameSprites);
                    Debug.Assert(pSB_GameSprite != null);

                    pSB_GameSprite.Attach(pObj.pProxySprite);
                    // pMan.pSB_GameSprite.Attach(pObj.pProxySprite);
                }

                SpriteBatch pSB_Collsion = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
                pSB_Collsion.Attach(pObj.pColObject.pBoxSprite);
                // pMan.pSB_CollisionSprite.Attach(pObj.pColObject.pBoxSprite);

                AlienManager.Remove(pCurr);

                DLink p = pMan.BaseGetActive();

                pCurr = pNext;
            }

            // Checked new Alien Hierarchy is good
            //  pMan.pSB_GameSprite.Dump();
            //  pMan.pSB_CollisionSprite.Dump();
        }


        public static void ResetAlienPoints(float offY = 0.0f)
        {
            GameObject pGroup = GameObjectManager.Find(GameObject.Name.AlienGroup);

            GameObject pColumn = Iterator.GetChildGameObject(pGroup);
            float offset = Constant.ALIEN_OFFSET_X;
            int i = 0;
            while (pColumn != null)
            {
                GameObject pAlien = Iterator.GetChildGameObject(pColumn);

                pAlien.x = 100.0f + offset * i;
                pAlien.y = Constant.WINDOW_HEIGHT - 200.0f - offY;

                pAlien = (GameObject)pAlien.pNext;
                pAlien.x = 100.0f + offset * i;
                pAlien.y = Constant.WINDOW_HEIGHT - 240.0f - offY;

                pAlien = (GameObject)pAlien.pNext;
                pAlien.x = 100.0f + offset * i;
                pAlien.y = Constant.WINDOW_HEIGHT - 280.0f - offY;

                pAlien = (GameObject)pAlien.pNext;
                pAlien.x = 100.0f + offset * i;
                pAlien.y = Constant.WINDOW_HEIGHT - 320.0f - offY;

                pAlien = (GameObject)pAlien.pNext;
                pAlien.x = 100.0f + offset * i;
                pAlien.y = Constant.WINDOW_HEIGHT - 360.0f - offY;

                i++;
                pColumn = (GameObject)pColumn.pNext;
            }
        }

        public static void PrintMe()
        {
            AlienManager pMan = AlienManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            AlienNode pNode = (AlienNode)pMan.BaseGetActive();
            while (pNode != null)
            {
                pNode.PrintMe();
                pNode = (AlienNode)pNode.pNext;
            }
        }

        public static AlienNode Add(GameObject pObj)
        {
            Debug.Assert(pObj != null);


            AlienManager pMan = AlienManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            if(pObj.type == Component.Container.LEAF)
            {
                pMan.numAliveAliens--;
            }

            // Take a node out from Reserve list. 
            // This node is washed in BasePopNode().

            AlienNode pNode = (AlienNode)pMan.BasePopNode();
            Debug.Assert(pNode != null);

            pNode.Set(pObj);
            pMan.BaseSortedAdd(pNode);
            return pNode;
        }

        public static AlienNode Find(int _Index)
        {
            //AlienManager pMan = AlienManager.PrivGetInstance();
            //Debug.Assert(pMan != null);
            //// So:  Use the Compare Node - as a reference
            ////      use in the Compare() function
            //pMan.poNodeCompare. = name;
            //TimeEvent pData = (TimeEvent)pMan.BaseFind(pInstance.poNodeCompare);
            //return pData;
            return null;
        }

        public static void Remove(AlienNode pNode)
        {
            AlienManager pMan = AlienManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        protected override DLink DerivedCreateNode()
        {

            DLink pNode = new AlienNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            AlienNode pDataA = (AlienNode)pLinkA;
            AlienNode pDataB = (AlienNode)pLinkB;

            Boolean status = false;

            if (pDataA.index == pDataB.index)
            {
                status = true;
            }

            return status;
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            AlienNode pNode = (AlienNode)pLink;
            pNode.Wash();
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            AlienNode pData = (AlienNode)pLink;
            //pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static AlienManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static AlienManager pInstance;
        private int numAliveAliens;
        private float originDeltaTime;
        private AlienNode poNodeCompare;

    }
}
