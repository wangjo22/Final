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
    abstract public class GameSpriteManager_Link : Manager
    {
        public GameSprite_Link pActive = null;
        public GameSprite_Link pReserve = null;
    }
    public class GameSpriteManager: GameSpriteManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private GameSpriteManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new GameSprite();
        }

        //--------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------
        ~GameSpriteManager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~GameSpriteManager():{0} ", this.GetHashCode());
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
                pInstance = new GameSpriteManager(reserveNum, reserveGrow);
                GameSpriteManager.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
            }
        }
        public static void Destroy()
        {
#if (TRACK_DESTRUCTOR_MANAGER)
            Debug.WriteLine("---> GameSpriteManager.Destroy()");
#endif
            pInstance.BaseDestroy();

#if (TRACK_DESTRUCTOR_MANAGER)
            Debug.WriteLine("   {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("   {0} ({1})", pInstance, pInstance.GetHashCode());
#endif
            pInstance.poNodeCompare = null;
            pInstance = null;
        }

        public static GameSprite Add(GameSprite.Name spriteName, Image.Name imageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            GameSpriteManager pMan = GameSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameSprite pNode = (GameSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the Data
            Image pImage = ImageManager.Find(imageName);
            Debug.Assert(pImage != null);

            pNode.Set(spriteName, pImage, x, y, width, height, pColor);
            return pNode;
        }

        public static GameSprite Find(GameSprite.Name name)
        {
            GameSpriteManager pMan = GameSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            GameSprite pData = (GameSprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(GameSprite pNode)
        {
            GameSpriteManager pMan = GameSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            GameSpriteManager pMan = GameSpriteManager.PrivGetInstance();
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

            GameSprite pDataA = (GameSprite)pLinkA;
            GameSprite pDataB = (GameSprite)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new GameSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pData = (GameSprite)pLink;
            pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pNode = (GameSprite)pLink;
            pNode.Wash();
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameSpriteManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);
            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static GameSpriteManager pInstance = null;
        private GameSprite poNodeCompare;
    }
}
