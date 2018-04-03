using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ReverseIterator : Iterator
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public ReverseIterator()
        {
            this.pCurr = null;
            this.pRoot = null;
            this.bIsDone = false;
        }
        public ReverseIterator(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            this.pCurr = pStart;
            this.pRoot = pStart;
            this.bIsDone = false;
        }

        //--------------------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------------------
        public void TakeHierachy(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            this.pCurr = pStart;
            this.pRoot = pStart;
            this.bIsDone = false;
        }

        override public Component First()
        {
            Debug.Assert(this.pRoot != null);
            Debug.Assert(this.pCurr != null);

            Component pComponent = null;
            while (this.pCurr.type == Component.Container.COMPOSITE)
            {
                Composite pComposite = (Composite)this.pCurr;
                pComponent = (Component)pComposite.poLast;
                if(pComponent == null)
                {
                    this.bIsDone = true;
                    break;
                }
                pComponent.pParent = this.pCurr;
                this.pCurr = pComponent;
            }
            return pComponent;
        }

        override public Component Next()
        {
            Component pComponent = null;
            
            if (this.pCurr.type == Component.Container.LEAF)
            {
                if(this.pCurr.pPrev != null)
                {
                    Leaf pComposite = (Leaf)this.pCurr;
                    pComponent = (Component)pComposite.pPrev;
                    pComponent.pParent = this.pCurr.pParent;
                    this.pCurr = pComponent;
                }
                else
                {      
                    pComponent = this.pCurr.pParent;
                    this.pCurr = pComponent;
                }
            } 
            else if (this.pCurr.type == Component.Container.COMPOSITE)
            {
                if(this.pCurr.pPrev != null)
                {
                    Composite pComposite = (Composite)this.pCurr.pPrev;
                    pComponent = (Component)pComposite.poLast;
                    pComponent.pParent = pComposite;
                    this.pCurr = pComponent;
                }
                else
                {
                    pComponent = this.pCurr.pParent;
                    this.pCurr = pComponent;
                }
            }
           
            if (this.pCurr == null)
            {
                this.bIsDone = true;
            }
            return pComponent;
        }


        override public bool IsDone()
        {
            return this.bIsDone;
        }

        

        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        private Component pCurr;
        private Component pRoot;
        private bool bIsDone;
    }


}
