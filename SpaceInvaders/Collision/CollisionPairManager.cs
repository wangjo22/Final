using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionPairManager_Link : Manager
    {
        public CollisionPair_Link pActive;
        public CollisionPair_Link pReserve;
    }

    public class CollisionPairManager : CollisionPairManager_Link
    {
        //---------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------
        private CollisionPairManager(int reserveNum = 3, int reserveGrow = 2)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new CollisionPair();
            this.pCurrentCollisionPair = null;
        }

        //---------------------------------------------------------------------------
        // Destructor
        //---------------------------------------------------------------------------
        ~CollisionPairManager()
        {
            this.poNodeCompare = null;
            this.pCurrentCollisionPair = null;
        }


        //---------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 2)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);
            if (pInstance == null)
            {
                pInstance = new CollisionPairManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {

        }

        public static CollisionPair Add(CollisionPair.Name colPairName, GameObject treeA, GameObject treeB)
        {
            CollisionPairManager pMan = CollisionPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            CollisionPair pPair = (CollisionPair)pMan.BaseAdd();
            Debug.Assert(pPair != null);

            pPair.SetCollisionPair(colPairName, treeA, treeB);
            return pPair;
        }

        public static void Process()
        {
            CollisionPairManager pMan = CollisionPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            CollisionPair pPair = (CollisionPair)pMan.BaseGetActive();
            Debug.Assert(pPair != null);

            while (pPair != null)
            {
                pMan.pCurrentCollisionPair = pPair;
                pPair.Process();
                pPair = (CollisionPair)pPair.pNext;
            }
        }

        public static void Remove(CollisionPair pNode)
        {
            Debug.Assert(pNode != null);

            CollisionPairManager pMan = CollisionPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseRemove(pNode);
        }

        public static void Dump()
        {
            CollisionPairManager pMan = CollisionPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new CollisionPair();
            Debug.Assert(pNode != null);
            return pNode;
        }

        protected override bool DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            CollisionPair pA = (CollisionPair)pLinkA;
            CollisionPair pB = (CollisionPair)pLinkB;


            bool areSame = false;
            if (pA.GetName() == pB.GetName())
            {
                areSame = true;
            }
            return areSame;
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            CollisionPair pP = (CollisionPair)pLink;
            pP.Wash();
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            CollisionPair pP = (CollisionPair)pLink;
            pP.Dump();
        }

        public static CollisionPair GetCurrentCollisionPair()
        {
            CollisionPairManager pMan = CollisionPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pMan.pCurrentCollisionPair != null);

            return pMan.pCurrentCollisionPair;
        }

        //---------------------------------------------------------------------------
        // Private Methods
        //---------------------------------------------------------------------------
        private static CollisionPairManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        //---------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------
        private static CollisionPairManager pInstance = null;
        private CollisionPair poNodeCompare = null;
        private CollisionPair pCurrentCollisionPair = null;

    }

}
