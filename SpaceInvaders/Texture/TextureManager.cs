using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TextureManager_Link : Manager
    {
        public Texture_Link pActive = null;
        public Texture_Link pReserve = null;
    }
    public class TextureManager: TextureManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private TextureManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize();

            // initialize derived data here
            this.poNodeCompare = new Texture();
        }

        //---------------------------------------------------------------------------------
        // Destructor
        //---------------------------------------------------------------------------------
        ~TextureManager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~TextureManager(): {0}", this.GetHashCode());
#endif
            this.poNodeCompare = null;
            pInstance = null;
        }

        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void Create(int reserveNum = 1, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // Initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TextureManager(reserveNum, reserveGrow);

                // Default texture
                TextureManager.Add(Texture.Name.Default, "HotPink.tga");

                // Null Object texture
                TextureManager.Add(Texture.Name.NullObject, "HotPink.tga");
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

        public static Texture Add(Texture.Name name, string pTextureName)
        {
            Debug.Assert(pTextureName != null);

            TextureManager pMan = TextureManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Texture pNode = (Texture)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the Data
            pNode.Set(name, pTextureName);
            return pNode;
        }

        public static Texture Find(Texture.Name name)
        {
            TextureManager pMan = TextureManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            Texture pData = (Texture)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(Image pNode)
        {
            TextureManager pMan = TextureManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            TextureManager pMan = TextureManager.PrivGetInstance();
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

            Texture pDataA = (Texture)pLinkA;
            Texture pDataB = (Texture)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new Texture();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pData = (Texture)pLink;
            pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pNode = (Texture)pLink;
            pNode.Wash();
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TextureManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static TextureManager pInstance = null;
        private Texture poNodeCompare;

    }
}
