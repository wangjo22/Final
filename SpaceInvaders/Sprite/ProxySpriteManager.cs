using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Singleton class - use only public static methods for customers
    //
    //  * One single compare node is owned by this singleton - used for find, prevent extra news
    //  * Create one - NULL Object - Sprite Default
    //  * Dependency - ImageManager needs to be initialized before SpriteMan
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class ProxySpriteManager_Link : Manager
    {
        public ProxySprite_Link pActive = null;
        public ProxySprite_Link pReserve = null;
    }

    public class ProxySpriteManager : ProxySpriteManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private ProxySpriteManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new ProxySprite();
        }

        //--------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------
        ~ProxySpriteManager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~ProxySpriteManager():{0} ", this.GetHashCode());
#endif
            this.poNodeCompare = null;
            pInstance = null;
        }

        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // Initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new ProxySpriteManager(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {
#if (TRACK_DESTRUCTOR_MANAGER)
            Debug.WriteLine("---> ProxySpriteManager.Destroy()");
#endif
            pInstance.BaseDestroy();

#if (TRACK_DESTRUCTOR_MANAGER)
            Debug.WriteLine("   {0} ({1})", pInstance.poNodeCompare, pInstance.poNodeCompare.GetHashCode());
            Debug.WriteLine("   {0} ({1})", pInstance, pInstance.GetHashCode());
#endif
            pInstance.poNodeCompare = null;
            pInstance = null;
        }

        public static ProxySprite Add(GameSprite.Name name)
        {
            ProxySprite pNode = (ProxySprite)pInstance.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);
            return pNode;
        }

        public static ProxySprite Find(ProxySprite.Name name)
        {
            ProxySpriteManager pMan = ProxySpriteManager.PrivGetInstance();
            pMan.poNodeCompare.SetName(name);
            ProxySprite pData = (ProxySprite)pMan.BaseFind(pInstance.poNodeCompare);
            return pData;
        }
        public static void Remove(GameSprite pNode)
        {
            ProxySpriteManager pMan = ProxySpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            ProxySpriteManager pMan = ProxySpriteManager.PrivGetInstance();
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

            ProxySprite pDataA = (ProxySprite)pLinkA;
            ProxySprite pDataB = (ProxySprite)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new ProxySprite();
            Debug.Assert(pNode != null);
            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            ProxySprite pData = (ProxySprite)pLink;
            pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            ProxySprite pNode = (ProxySprite)pLink;
            pNode.Wash();
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static ProxySpriteManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);
            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static ProxySpriteManager pInstance = null;
        private ProxySprite poNodeCompare;
    }
}
