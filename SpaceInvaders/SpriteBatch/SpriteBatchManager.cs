using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // This class do nothing. It exists only for UML Diagram.
    public abstract class SpriteBatchManager_Link : Manager
    {
        public SpriteBatch_DLink pActive = null;
        public SpriteBatch_DLink pReserve = null;
    }
    public class SpriteBatchManager : SpriteBatchManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------
        private SpriteBatchManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new SpriteBatch();
        }

        //---------------------------------------------------------------------------------
        // DEstructor
        //---------------------------------------------------------------------------------
        ~SpriteBatchManager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SpriteBatchManager():{0} ", this.GetHashCode());
#endif
            pInstance = null;
            this.poNodeCompare = null;
        }
        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // Initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new SpriteBatchManager(reserveNum, reserveGrow);
            }
        }

        override protected void DerivedDestroyNode(DLink pLink)
        {
            // default: do nothing
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pLink, pLink.GetHashCode());
#endif
            SpriteBatch pNode = (SpriteBatch)pLink;
            Debug.Assert(pNode != null);
            pNode.Destroy();
        }


        public static void Destroy()
        {
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->SpriteBatchMan.Destroy()");
#endif
            pInstance.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", SpriteBatchMan.pInstance, SpriteBatchMan.pInstance.GetHashCode());
#endif

            pInstance.poNodeCompare = null;
            pInstance = null;
        }

        public static SpriteBatch Add(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        { 
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pNode = (SpriteBatch)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the Data
            pNode.Set(name, reserveNum, reserveGrow);
            return pNode;
        }

        public static void Draw()
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.BaseGetActive();

            while(pSpriteBatch != null)
            {
                SBNodeManager pSBNodeMan = pSpriteBatch.GetSBNodeManager();
                Debug.Assert(pSBNodeMan != null);

                pSBNodeMan.Draw();

                pSpriteBatch = (SpriteBatch)pSpriteBatch.pNext;
            }
        }

        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            SpriteBatch pData = (SpriteBatch)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(SpriteBatch pNode)
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Remove(SBNode pSBNode)
        {
            Debug.Assert(pSBNode != null);
            SBNodeManager pSBNodeMan = pSBNode.GetBackToSBNodeManager();

            Debug.Assert(pSBNodeMan != null);
            pSBNodeMan.Remove(pSBNode);
        }

        public static void Dump()
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        protected override bool DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteBatch pDataA = (SpriteBatch)pLinkA;
            SpriteBatch pDataB = (SpriteBatch)pLinkB;

            bool status = false;
            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new SpriteBatch();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pData = (SpriteBatch)pLink;
            pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pNode = (SpriteBatch)pLink;
            pNode.Wash();
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static SpriteBatchManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static SpriteBatchManager pInstance = null;
        private SpriteBatch poNodeCompare;
    }
}

