using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // This class do nothing. It exists only for UML Diagram.
    public abstract class SBNode_Link : DLink
    { }

    //----------------------------------------------------
    //  SBNode : Sprite Base Node
    //  This class will reference Sprite Base instances.
    //----------------------------------------------------
    public class SBNode : SBNode_Link
    {
        //------------------------------------------------
        // Constructor
        //------------------------------------------------
        public SBNode()
            : base()
        {
            // This class don't own Sprite Base instance. Just reference it so that it can be null.
            // We don't use "new"
            this.pSpriteBase = null;
            this.pBackToSBNodeManager = null;
        }

        //------------------------------------------------
        // Destructor
        //------------------------------------------------
        ~SBNode()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine(~SBNode():{0} ", this.GetHashCode());
#endif
            this.pSpriteBase = null;
        }

        public void Set(SpriteBase pNode, SBNodeManager pSBNodeMan)
        {
            Debug.Assert(pNode != null);
            Debug.Assert(pSBNodeMan != null);

            this.pSpriteBase = pNode;
            this.pSpriteBase.SetBackToSBNode(this);
            this.pBackToSBNodeManager = pSBNodeMan;
        }

        public SBNodeManager GetBackToSBNodeManager()
        {
            Debug.Assert(this.pBackToSBNodeManager != null);
            return this.pBackToSBNodeManager;
        }

        public void Wash()
        {
            // This class don't own Sprite Base instance. Just reference it so that it can be null.
            // We don't use "new"
            //this.pSpriteBase.pBackToSBNode = null;
            this.pSpriteBase = null;
            this.pBackToSBNodeManager = null;
        }

        public void Print()
        {
            Debug.WriteLine("{0}", this.pSpriteBase.GetHashCode());
        }

        public SpriteBase GetSpriteBase()
        {
            Debug.Assert(this.pSpriteBase != null);
            return this.pSpriteBase;
        }
        //------------------------------------------------
        // Data
        //------------------------------------------------
        private SpriteBase pSpriteBase;
        public SBNodeManager pBackToSBNodeManager;
    }
}
