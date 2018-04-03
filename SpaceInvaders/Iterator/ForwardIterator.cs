using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ForwardIterator : Iterator
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public ForwardIterator(Component pStart)
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



        override public Component First()
        {
            Debug.Assert(this.pRoot != null);
            Debug.Assert(this.pCurr != null);

            this.pCurr = this.pRoot;
            return this.pCurr;
            //Debug.Assert(this.pRoot != null);
            //Component pNode = this.pRoot;

            //while(pNode.type == Component.Container.COMPOSITE)
            //{
            //    pNode = GetChild(pNode);
            //}
            //Debug.Assert(pNode != null);
            //this.pCurr = pNode;
            ////Debug.WriteLine("---> {0} ", this.pCurr.GetHashCode());
            //return this.pCurr;
        }
        override public Component Next()
        {
            Component pComponent = null;
            if(pCurr.type == Component.Container.COMPOSITE)
            {
                Composite pComposite = (Composite)pCurr;
                pComponent = (Component)pComposite.poHead;
                pComponent.pParent = pCurr;
                pCurr = pComponent;
            }
            else if(pCurr.type == Component.Container.LEAF)
            {
                Leaf pCurrLeaf = (Leaf)pCurr;
                if(pCurrLeaf.pNext != null)
                {
                    pComponent = (Leaf)pCurrLeaf.pNext;
                    pComponent.pParent = pCurr.pParent;
                    pCurr = pComponent;
                }
                else
                {
                    pComponent = pCurr.pParent;
                    if(pComponent.pNext != null)
                    {
                        pComponent = (Component)pComponent.pNext;
                        pCurr = pComponent;
                    }
                    else
                    {
                        pComponent = pComponent.pParent;
                        pCurr = pComponent;
                    }
                }
            }
            if (this.pCurr == null || this.pCurr == this.pRoot)
            {
                this.bIsDone = true;
            }
            return pComponent;
        }

        //public Component CurrentItem()
        //{
        //    return null;
        //}
        override public bool IsDone()
        {
            return this.bIsDone;
        }


        //--------------------------------------------------------------------------------------------
        // Static methods
        //--------------------------------------------------------------------------------------------
        //static public Component GetParent(Component pNode)
        //{
        //    Debug.Assert(pNode != null);
        //    Component pParent = pNode.pParent;
        //    return pParent;
        //}
        static public Component GetChild(Component pNode)
        {
            Debug.Assert(pNode != null);
            Composite pComposite = (Composite)pNode;
            return (Component)pComposite.poHead;
        }
        static public Component GetSibling(Component pNode)
        {
            Debug.Assert(pNode != null);
            return (Component)pNode.pNext;
        }

        //private Component PrivNextStep(Component pNode, Component pParent, Component pSibling)
        //{
        //    if(pSibling != null)
        //    {
        //        pNode = pSibling;
        //        while(pNode.type == Component.Container.COMPOSITE)
        //        {
        //            pNode = GetChild(pNode);
        //        }
        //    }
        //    else
        //    {
        //        while(pParent != null)
        //        {
        //            pNode = pParent;
        //            if(pNode.pNext != null)
        //            {
        //                pNode = (Component)pNode.pNext;
        //                while(pNode.type == Component.Container.COMPOSITE)
        //                {
        //                    pNode = GetChild(pNode);
        //                }
        //                break;
        //            }
        //            else
        //            {
        //                pParent = GetParent(pNode);
        //            }
        //        }
        //    }

        //    if( (pNode.type != Component.Container.LEAF) && (pParent == null) )
        //    {
        //        pNode = null;
        //        this.bIsDone = true;
        //    }
        //    return pNode;
        //}

        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        private Component pCurr;
        private Component pRoot;
        private bool bIsDone;
    }


}
