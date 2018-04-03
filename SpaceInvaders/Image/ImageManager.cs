using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ImageManager_Link : Manager
    {
        public Image_Link pActive = null;
        public Image_Link pReserve = null;
    }
    public class ImageManager : ImageManager_Link
    {
        //---------------------------------------------------------------------------------------------------------
        // Design Notes:
        //
        //  Singleton class - use only public static methods for customers
        //
        //  * One single compare node is owned by this singleton - used for find, prevent extra news
        //  * Create one - NULL Object - Image Default
        //  * Dependency - TextureMan needs to be initialized before ImageMan
        //
        //---------------------------------------------------------------------------------------------------------
        
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private ImageManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new Image();
        }

        //---------------------------------------------------------------------------------
        // Destructor
        //---------------------------------------------------------------------------------
        ~ImageManager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~ImageManager(): {0}", this.GetHashCode());
#endif
            this.poNodeCompare = null;
            pInstance = null;
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
                pInstance = new ImageManager(reserveNum, reserveGrow);

                // Add a Null Image to the Manager.
                ImageManager.Add(Image.Name.NullObject, Texture.Name.NullObject, 0, 0, 10, 10);

                // Add a Default image to the Manager.
                ImageManager.Add(Image.Name.Default, Texture.Name.Default, 0, 0, 10, 10);
            }

            // Default image manager
            //ImageManager.Add(Image.Name.Default, Texture.Name.Default, 0, 0, 128, 128);
        }
        public static void Destroy()
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        public static Image Add(Image.Name imageName, Texture.Name textureName, float x, float y, float width, float height)
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Image pNode = (Image)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the Data
            Texture pTexture = TextureManager.Find(textureName);
            Debug.Assert(pTexture != null);

            pNode.Set(imageName, pTexture, x, y, width, height);
            return pNode;
        }

        public static Image Find(Image.Name name)
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            Image pData = (Image)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(Image pNode)
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
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

            Image pDataA = (Image)pLinkA;
            Image pDataB = (Image)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new Image();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Image pData = (Image)pLink;
            pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Image pNode = (Image)pLink;
            pNode.Wash();
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static ImageManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static ImageManager pInstance = null;
        private Image poNodeCompare;
    }
}
