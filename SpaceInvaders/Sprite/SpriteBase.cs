using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {
        //------------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------------
        public SpriteBase()
            :base()
        {
            this.pBackToSBNode = null;
        }


        //------------------------------------------------------------------
        // Destructor
        //------------------------------------------------------------------
        ~SpriteBase()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("   ~SpriteBase():{0} ", this.GetHashCode());
#endif
        }


        //------------------------------------------------------------------
        // Method
        //------------------------------------------------------------------
        public SBNode GetBackToSBNode()
        {
            Debug.Assert(this.pBackToSBNode != null);
            return this.pBackToSBNode;
        }

        public void SetBackToSBNode(SBNode pSBNode)
        {
            Debug.Assert(pSBNode != null);
            this.pBackToSBNode = pSBNode;
        }

        //------------------------------------------------------------------
        // Abstract Methods - Should be overriden in child classes
        //------------------------------------------------------------------
        abstract public void Update();
        abstract public void Render();

        //------------------------------------------------------------------
        // Data
        //------------------------------------------------------------------
        public SBNode pBackToSBNode;
    }
}
