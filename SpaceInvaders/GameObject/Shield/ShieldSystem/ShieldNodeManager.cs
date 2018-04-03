using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShieldNodeManager_Link : Manager
    {
        public ShieldNode_Link pActive = null;
        public ShieldNode_Link pReserve = null;
    }
    public class ShieldNodeManager : ShieldNodeManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private ShieldNodeManager(int numReserve = 66, int reserveGrow = 10)
            : base()
        {
           
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new ShieldNode();

          //  this.pSB_GameSprite = SpriteBatchManager.Find(SpriteBatch.Name.GameSprites);
           // this.pSB_CollisionSprite = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
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
                pInstance = new ShieldNodeManager(reserveNum, reserveGrow);
            }
        }

   

        public static void ResetAllShieldGrid()
        {
            ShieldNodeManager pMan = ShieldNodeManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            ShieldNode pCurr = (ShieldNode)pMan.BaseGetActive();
            ShieldNode pNext;
            GameObject pParent;
            GameObject pObj;
            ShieldCategory pShield;
            while (pCurr != null)
            {
                pNext = (ShieldNode)pCurr.pNext;

                // Get Alien object and its parent. The parent will be AlienGroup or AlienColumn.
                pObj = pCurr.GetAlienObject();

                pParent = (GameObject)pCurr.GetAlienObject().pParent;

                pObj.isDead = false;

                pObj.pPrev = null;
                pObj.pNext = null;

                pParent.Add(pObj);

                if (pObj.type == Component.Container.LEAF)
                {
                    pShield = (ShieldCategory)pObj;

                    SpriteBatch pSB_ShieldSprite = SpriteBatchManager.Find(SpriteBatch.Name.ShieldSprites);
                    Debug.Assert(pSB_ShieldSprite != null);

                    pSB_ShieldSprite.Attach(pObj.pProxySprite);
                    // pMan.pSB_GameSprite.Attach(pObj.pProxySprite);
                }

                SpriteBatch pSB_Collsion = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
                pSB_Collsion.Attach(pObj.pColObject.pBoxSprite);
                // pMan.pSB_CollisionSprite.Attach(pObj.pColObject.pBoxSprite);

                ShieldNodeManager.Remove(pCurr);

                DLink p = pMan.BaseGetActive();

                pCurr = pNext;
            }

            // Checked new Alien Hierarchy is good
            //  pMan.pSB_GameSprite.Dump();
            //  pMan.pSB_CollisionSprite.Dump();
        }


        public static void PrintMe()
        {
            ShieldNodeManager pMan = ShieldNodeManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            ShieldNode pNode = (ShieldNode)pMan.BaseGetActive();
            while (pNode != null)
            {
                pNode.PrintMe();
                pNode = (ShieldNode)pNode.pNext;
            }
        }



        public static ShieldNode Add(GameObject pObj)
        {
            Debug.Assert(pObj != null);

            ShieldNodeManager pMan = ShieldNodeManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Take a node out from Reserve list. 
            // This node is washed in BasePopNode().

            ShieldNode pNode = (ShieldNode)pMan.BasePopNode();
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

        public static void Remove(ShieldNode pNode)
        {
            ShieldNodeManager pMan = ShieldNodeManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        protected override DLink DerivedCreateNode()
        {

            DLink pNode = new ShieldNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            ShieldNode pDataA = (ShieldNode)pLinkA;
            ShieldNode pDataB = (ShieldNode)pLinkB;

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
            ShieldNode pNode = (ShieldNode)pLink;
            pNode.Wash();
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            ShieldNode pData = (ShieldNode)pLink;
            //pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static ShieldNodeManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static ShieldNodeManager pInstance;
        private ShieldNode poNodeCompare;
      //  private SpriteBatch pSB_GameSprite;
      //  private SpriteBatch pSB_CollisionSprite;
    }
}
