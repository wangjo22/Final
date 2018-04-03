//using System;
//using System.Diagnostics;

//namespace SpaceInvaders
//{
//    //---------------------------------------------------------------------------------------------------------
//    // Design Notes:
//    //
//    //  Singleton class - use only public static methods for customers
//    //
//    //  * One single compare node is owned by this singleton - used for find, prevent extra news
//    //  * Create one - NULL Object - Sprite Default
//    //  * Dependency - ImageManager needs to be initialized before SpriteMan
//    //
//    //---------------------------------------------------------------------------------------------------------
//    abstract public class ProxyBoxSpriteManager_Link : Manager
//    {
//        public new ProxyBoxSprite_Link pActive = null;
//        public ProxyBoxSprite_Link pReserve = null;
//    }

//    public class ProxyBoxSpriteManager : ProxySpriteManager_Link
//    {
//        //---------------------------------------------------------------------------------
//        // Constructors
//        //---------------------------------------------------------------------------------
//        private ProxyBoxSpriteManager(int numReserve = 3, int reserveGrow = 1)
//            : base()
//        {
//            // At this point ImageMan is created, now initialize the reserve
//            this.BaseInitialize(numReserve, reserveGrow);

//            // initialize derived data here
//            this.poNodeCompare = new ProxyBoxSprite();
//        }

//        //--------------------------------------------------------------------
//        // Destructor
//        //--------------------------------------------------------------------
//        ~ProxyBoxSpriteManager()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~ProxyBoxSpriteManager():{0} ", this.GetHashCode());
//#endif
//            this.poNodeCompare = null;
//            ProxyBoxSpriteManager.pInstance = null;
//        }

//        //---------------------------------------------------------------------------------
//        // Static Methods
//        //---------------------------------------------------------------------------------
//        public static void Create(int reserveNum = 3, int reserveGrow = 1)
//        {
//            Debug.Assert(reserveNum > 0);
//            Debug.Assert(reserveGrow > 0);

//            // Initialize the singleton here
//            Debug.Assert(pInstance == null);

//            // Do the initialization
//            if (pInstance == null)
//            {
//                pInstance = new ProxyBoxSpriteManager(reserveNum, reserveGrow);
//            }
//        }

//        public static void Destroy()
//        {
//            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
//            Debug.Assert(pMan != null);

//#if (TRACK_DESTRUCTOR_MANAGER)
//            Debug.WriteLine("---> ProxySpriteManager.Destroy()");
//#endif
//            pInstance.BaseDestroy();

//#if (TRACK_DESTRUCTOR_MANAGER)
//            Debug.WriteLine("   {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
//            Debug.WriteLine("   {0} ({1})", pMan, pMan.GetHashCode());
//#endif
//            pMan.poNodeCompare = null;
//            ProxyBoxSpriteManager.pInstance = null;
//        }

//        public static ProxyBoxSprite Add(BoxSprite.Name name)
//        {
//            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
//            Debug.Assert(pMan != null);
//            ProxyBoxSprite pNode = (ProxyBoxSprite)pMan.BaseAdd();
//            Debug.Assert(pNode != null);

//            pNode.Set(name);
//            return pNode;
//        }

//        public static ProxyBoxSprite Find(ProxyBoxSprite.Name name)
//        {
//            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
//            Debug.Assert(pMan != null);
//            ProxyBoxSprite pData = (ProxyBoxSprite)pMan.BaseFind(pInstance.poNodeCompare);
//            return pData;
//        }
//        public static void Remove(BoxSprite pNode)
//        {
//            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
//            Debug.Assert(pMan != null);
//            Debug.Assert(pNode != null);
//            pMan.BaseRemove(pNode);
//        }
//        public static void Dump()
//        {
//            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
//            Debug.Assert(pMan != null);
//            pMan.BaseDump();
//        }

//        //----------------------------------------------------------------------
//        // Override Abstract methods
//        //----------------------------------------------------------------------
//        protected override bool DerivedCompare(DLink pLinkA, DLink pLinkB)
//        {
//            Debug.Assert(pLinkA != null);
//            Debug.Assert(pLinkB != null);

//            ProxyBoxSprite pDataA = (ProxyBoxSprite)pLinkA;
//            ProxyBoxSprite pDataB = (ProxyBoxSprite)pLinkB;

//            Boolean status = false;

//            if (pDataA.GetName() == pDataB.GetName())
//            {
//                status = true;
//            }

//            return status;
//        }

//        protected override DLink DerivedCreateNode()
//        {
//            DLink pNode = new ProxyBoxSprite();
//            Debug.Assert(pNode != null);
//            return pNode;
//        }

//        protected override void DerivedDumpNode(DLink pLink)
//        {
//            Debug.Assert(pLink != null);
//            ProxyBoxSprite pData = (ProxyBoxSprite)pLink;
//            pData.Dump();
//        }

//        protected override void DerivedWash(DLink pLink)
//        {
//            Debug.Assert(pLink != null);
//            ProxyBoxSprite pNode = (ProxyBoxSprite)pLink;
//            pNode.Wash();
//        }


//        //----------------------------------------------------------------------
//        // Private methods
//        //----------------------------------------------------------------------
//        private static ProxyBoxSpriteManager PrivGetInstance()
//        {
//            // Safety - this forces users to call Create() first before using class
//            Debug.Assert(pInstance != null);
//            return pInstance;
//        }


//        //---------------------------------------------------------------------------------
//        // Data
//        //---------------------------------------------------------------------------------
//        private static ProxyBoxSpriteManager pInstance = null;
//        private ProxyBoxSprite poNodeCompare;
//    }
//}
