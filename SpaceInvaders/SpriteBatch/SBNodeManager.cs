using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // This class do nothing. It exists only for UML Diagram.
    public abstract class SBNodeManager_Link : Manager
    {
        public SBNode_Link pActive = null;
        public SBNode_Link pReserve = null;
    }

    //----------------------------------------------------
    //  SBNodeManager : Sprite Base Node Manager.
    //----------------------------------------------------
    public class SBNodeManager : SBNodeManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------
        public SBNodeManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new SBNode();
            this.collisionBoxToggle = true;
           // this.pBackToSpriteBatch = null;
        }

        //---------------------------------------------------------------------------------
        // Destructor
        //---------------------------------------------------------------------------------
        ~SBNodeManager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SBNodeManager():{0} ", this.GetHashCode());
#endif
            this.name = SpriteBatch.Name.Uninitialized;
            this.poNodeCompare = null;
           // this.pBackToSpriteBatch = null;
        }

        //---------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------     
        public void Destroy()
        {
            // Get the instance
            Debug.WriteLine("         SBNodeManager.Destroy({0})", this.GetHashCode());
            this.BaseDestroy();

#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("             {0} ({1})", this.poNodeCompare, this.poNodeCompare.GetHashCode());
#endif
            this.name = SpriteBatch.Name.Uninitialized;
            this.poNodeCompare = null;
        }
        override protected void DerivedDestroyNode(DLink pLink)
        {
            // default: do nothing
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("             {0} ({1})", pLink, pLink.GetHashCode());
#endif
            pLink = null;
        }

        public void Set(SpriteBatch.Name name, int reserveNum, int reserveGrow)
        {
            this.name = name;
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            this.BaseSetReserve(reserveNum, reserveGrow);
        }
       

        public SBNode Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);
            SBNode pSBNode = (SBNode)this.BaseAdd();
            Debug.Assert(pSBNode != null);

            pSBNode.Set(pNode, this);
            return pSBNode;
        }

        //public void Detach(SpriteBase pNode)
        //{
        //    Debug.Assert(pNode != null);
        //    SBNode pSBNode = this.Find(pNode);
        //    //if(pSBNode == null)
        //    //{
        //    //    Debug.Assert(true);
        //    //}
        //    this.BaseRemove(pSBNode);

        //}

        public void Detach(SpriteBase pSB)
        {
            Debug.Assert(pSB != null);
            SBNode pSBNode = pSB.GetBackToSBNode();
            pSB.pBackToSBNode = null;

            SBNodeManager pMan = pSBNode.GetBackToSBNodeManager();
            pSBNode.pBackToSBNodeManager = null;

            pMan.Remove(pSBNode);
            //this.poSBNodeMan.Detach(pNode);
        }

        public void Print()
        {
            SBNode pNode = (SBNode)this.BaseGetActive();
            while(pNode != null)
            {
                Debug.WriteLine("{0}", pNode.GetSpriteBase().GetType());
                pNode = (SBNode)pNode.pNext;
            }
        }


        //public SpriteBatch GetBackToSpriteBatch()
        //{
        //    Debug.Assert(this.pBackToSpriteBatch != null);
        //    return this.pBackToSpriteBatch;
        //}

        //public void SetBackToSpriteBatch(SpriteBatch pSpriteBatch)
        //{
        //    Debug.Assert(pSpriteBatch != null);
        //    this.pBackToSpriteBatch = pSpriteBatch;
        //}

        public void Draw()
        {
            SBNode pNode = (SBNode)this.BaseGetActive();

            // Update() should be called in each sprites before call this Draw() function.

            if (this.collisionBoxToggle == true)
            {
                while (pNode != null)
                {
                    pNode.GetSpriteBase().Render();
                    pNode = (SBNode)pNode.pNext;
                }
            }

        }

        public void Remove(SBNode pNode)
        {
            Debug.Assert(pNode != null);
            //pNode.Wash();
            this.BaseRemove(pNode);
        }

        public void Remove(SpriteBase pSprite)
        {
            Debug.Assert(pSprite != null);
            this.BaseRemove(pSprite);
        }

        public void Dump()
        {
            SBNode pNode = (SBNode)this.BaseGetActive();

            // Update() should be called in each sprites before call this Draw() function.
            while (pNode != null)
            {
                pNode.Print();
                pNode = (SBNode)pNode.pNext;
            }

        }
        
        public void ToggleDraw()
        {
            this.collisionBoxToggle = !this.collisionBoxToggle;
        }


        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        protected override bool DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SBNode pDataA = (SBNode)pLinkA;
            SBNode pDataB = (SBNode)pLinkB;

            Boolean status = false;

            if (pDataA.GetSpriteBase() == pDataB.GetSpriteBase())
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new SBNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SBNode pData = (SBNode)pLink;
            //pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SBNode pNode = (SBNode)pLink;
            pNode.Wash();
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private SBNode poNodeCompare;
        private SpriteBatch.Name name;
        private bool collisionBoxToggle;
        //private SpriteBatch pBackToSpriteBatch;
    }
}

