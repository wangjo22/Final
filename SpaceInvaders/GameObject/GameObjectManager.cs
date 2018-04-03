using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObjectManager_Link : Manager
    {
        public GameObjectNode_Link pActive = null;
        public GameObjectNode_Link pReserve = null;
    }
    public class GameObjectManager : GameObjectManager_Link
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        private GameObjectManager(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeCompare = new GameObjectNode();
            this.poNullGameObject = new NullGameObject();
            this.poNodeCompare.pGameObject = this.poNullGameObject;
            //this.poReverseIterator = new ReverseIterator();
        }

        //--------------------------------------------------------------------------------------------
        // Static Methods
        //--------------------------------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(pInstance == null);

            if(pInstance == null)
            {
                pInstance = new GameObjectManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            Debug.Assert(pInstance != null);
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->GameSpriteMan.Destroy()");
#endif
            pInstance.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pInstance.poNodeCompare, pInstance.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", pInstance, pInstance.GetHashCode());
#endif

            pInstance.poNodeCompare = null;
            pInstance = null;

        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            Debug.Assert(pInstance != null);

            GameObjectNode pNode = (GameObjectNode)pInstance.BaseAdd();
            Debug.Assert(pNode != null);
            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObject Find(GameObject.Name name)
        {
            Debug.Assert(pInstance != null);

            pInstance.poNodeCompare.pGameObject.name = name;
            GameObjectNode pNode = (GameObjectNode)pInstance.BaseFind(pInstance.poNodeCompare);
            Debug.Assert(pNode != null);
            return pNode.pGameObject;
        }

        public static void Remove(GameObjectNode pNode)
        {
            Debug.Assert(pInstance != null);

            Debug.Assert(pNode != null);
            pInstance.BaseRemove(pNode);
        }



        public static void Update()
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseGetActive();
            //= new ReverseIterator(pNode.pGameObject);

            while (pNode != null)
            {
                // Update the node
                Debug.Assert(pNode.pGameObject != null);

                //ReverseIterator pRIter = new ReverseIterator(pNode.pGameObject);
                GameObjectManager.poReverseIterator.TakeHierachy(pNode.pGameObject);
                GameObject pComponent = (GameObject)GameObjectManager.poReverseIterator.First();
                while (!poReverseIterator.IsDone())
                {
                    //pComponent.Print();
                    pComponent.Update();
                    pComponent = (GameObject)GameObjectManager.poReverseIterator.Next();
                }
                //pNode.pGameObject.Update();

                pNode = (GameObjectNode)pNode.pNext;
            }

        }

        public static void Dump()
        {
            Debug.Assert(pInstance != null);
            pInstance.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new GameObjectNode();
            Debug.Assert(pNode != null);

            return pNode;
        }
        protected override Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameObjectNode pDataA = (GameObjectNode)pLinkA;
            GameObjectNode pDataB = (GameObjectNode)pLinkB;

            Boolean status = false;

            if (pDataA.pGameObject.GetName() == pDataB.pGameObject.GetName())
            {
                status = true;
            }

            return status;
        }
        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pNode = (GameObjectNode)pLink;
            pNode.Wash();
        }
        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pData = (GameObjectNode)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameObjectManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        private static GameObjectManager pInstance = null;
        private GameObjectNode poNodeCompare;
        private NullGameObject poNullGameObject;
        private static ReverseIterator poReverseIterator = new ReverseIterator();
    }
}
