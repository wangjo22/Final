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
    //  * Create one - NULL Object - SpriteBox Default
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class BoxSpriteManager_Link : Manager
    {
        public BoxSprite_Link pActive = null;
        public BoxSprite_Link pReserve = null;
    }
    public class BoxSpriteManager : BoxSpriteManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private BoxSpriteManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new BoxSprite();
        }

        //---------------------------------------------------------------------------------
        // Destructor
        //---------------------------------------------------------------------------------
        ~BoxSpriteManager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~BoxSpriteMan():{0}", this.GetHashCode());
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
                pInstance = new BoxSpriteManager(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {
#if (TRACK_DESTRUCTOR_MANAGER)
            Debug.WriteLine("---> TextureManager.Destroy()");
#endif
            pInstance.BaseDestroy();

#if (TRACK_DESTRUCTOR_MANAGER)
            Debug.WriteLine("   {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("   {0} ({1})", pInstance, pInstance.GetHashCode());
#endif
            pInstance.poNodeCompare = null;
            pInstance = null;
        }

        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            BoxSprite pNode = (BoxSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the Data
            pNode.Set(name, x, y, width, height, pColor);
            return pNode;
        }

        public static BoxSprite Add(BoxSprite.Name name, Azul.Rect pRect)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            BoxSprite pNode = (BoxSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the Data
            pNode.Set(name, pRect);
            return pNode;
        }

        public static BoxSprite Find(BoxSprite.Name name)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            BoxSprite pData = (BoxSprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(BoxSprite pNode)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
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

            BoxSprite pDataA = (BoxSprite)pLinkA;
            BoxSprite pDataB = (BoxSprite)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new BoxSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            BoxSprite pData = (BoxSprite)pLink;
            pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            BoxSprite pNode = (BoxSprite)pLink;
            pNode.Wash();
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static BoxSpriteManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static BoxSpriteManager pInstance = null;
        private BoxSprite poNodeCompare;
    }
}
