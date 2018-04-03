using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class DLink
    {
        //---------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------
        protected DLink()
        {
            this.Clear();
        }

        //---------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------
        public void Clear()
        {
            this.pNext = null;
            this.pPrev = null;
        }

        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void AddToFront(ref DLink pHead, DLink pNode)
        {
            Debug.Assert(pNode != null);

            // If the linked list is empty
            if(pHead == null)
            {
                pHead = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            // If the linked list is not empty, Add to the front
            else
            {
                pNode.pPrev = null;
                pNode.pNext = pHead;
                pHead.pPrev = pNode;
                pHead = pNode;
            }
            Debug.Assert(pHead != null);
        }

        public static void AddToLast(ref DLink pHead, ref DLink pLast, DLink pNode)
        {
            Debug.Assert(pNode != null);
            // If pNode is the first node
            //if (pHead == null && pLast == null)
            //{
            //    pHead = pNode;
            //    pLast = pNode;
            //    pNode.pNext = null;
            //    pNode.pPrev = null;
            //}
            //else
            //{
            //    pNode.pPrev = pLast;
            //    pNode.pNext = null;
            //    pLast.pNext = pNode;
            //    pLast = pNode;

            //}
            //Debug.Assert(pHead != null);
            //Debug.Assert(pLast != null);

            if (pHead == pLast && pHead == null)
            {
                pHead = pNode;
                pLast = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;

            }
            // If there exist a node at least
            else
            {
                Debug.Assert(pHead != null);
                Debug.Assert(pLast != null);
                pLast.pNext = pNode;
                pNode.pPrev = pLast;
                pNode.pNext = null;
                pLast = pNode;
            }
            // Ensure that pHead and pLast are not null
            Debug.Assert(pHead != null);
            Debug.Assert(pLast != null);
        }


        public static DLink PullFromFront(ref DLink pHead)
        {
            Debug.Assert(pHead != null);

            // Return node
            DLink pNode = pHead;

            // Update head
            pHead = pHead.pNext;
            if(pHead != null)
            {
                pHead.pPrev = null;
            }

            pNode.Clear();

            return pNode;
        }

        public static void RemoveNode(ref DLink pHead, DLink pNode)
        {
            Debug.Assert(pNode != null);

            // Middle or last node
            if(pNode.pPrev != null)
            {
                pNode.pPrev.pNext = pNode.pNext;    
            } 
            // First node
            else
            {
                pHead = pNode.pNext;
            }

            if(pNode.pNext != null)
            {
                pNode.pNext.pPrev = pNode.pPrev;
            }
        }
        public static void RemoveNodeFromHeadAndLast(ref DLink pHead, ref DLink pLast, DLink pNode)
        {
            //Debug.Assert(pHead != null);
            //Debug.Assert(pLast != null);
            //Debug.Assert(pNode != null);

            //if (pNode.pPrev == null && pNode.pNext == null)
            //{
            //    pHead = null;
            //    pLast = null;
            //}
            //else if (pNode.pPrev == null)
            //{
            //    pHead = pNode.pNext;
            //    pNode.pNext.pPrev = null;
            //}
            //else if (pNode.pNext == null)
            //{
            //    pLast = pNode.pPrev;
            //    pNode.pPrev.pNext = null;
            //}
            //else
            //{
            //    pNode.pPrev.pNext = pNode.pNext;
            //    pNode.pNext.pPrev = pNode.pPrev;
            //}
            //pNode.pNext = null;
            //pNode.pPrev = null;
            Debug.Assert(pNode != null);

            // Quick HACK... might be a bug... need to diagram

            // 4 different conditions... 
            if (pNode.pPrev != null)
            {	// middle or last node
                pNode.pPrev.pNext = pNode.pNext;

                if (pNode == pLast)
                {
                    pLast = pNode.pPrev;
                }
            }
            else
            {  // first
                pHead = pNode.pNext;

                if (pNode == pLast)
                {
                    // Only one node
                    pLast = pNode.pNext;
                }
                else
                {
                    // Only first not the last
                    // do nothing more
                }
            }

            if (pNode.pNext != null)
            {	// middle node
                pNode.pNext.pPrev = pNode.pPrev;
            }
        }
        public static DLink SortedAdd(ref DLink pActive, DLink pNode)
        {
            // If the active list is empty
            if (pActive == null)
            {
                pActive = pNode;
                pNode.pPrev = null;
                pNode.pNext = null;
            }
            // If the active list is not empty
            else
            {
                // If the value of pNode is smaller than that of first node in active list
                // insert the node to the front in active list.
                if (pActive.DerivedCompareValue() > pNode.DerivedCompareValue())
                {
                    pNode.pNext = pActive;
                    pNode.pPrev = null;
                    pActive.pPrev = pNode;
                    pActive = pNode;
                }
                // If the value of the pNode is bigger than that of first node,
                // it should find the right location in active list.
                else
                {
                    DLink pCurr = pActive;
                    while (pCurr.pNext != null)
                    {
                        if (pCurr.pNext.DerivedCompareValue() > pNode.DerivedCompareValue())
                        {
                            break;
                        }
                        pCurr = pCurr.pNext;
                    }
                    pNode.pPrev = pCurr;
                    pNode.pNext = pCurr.pNext;
                    if (pCurr.pNext != null)
                    {
                        pCurr.pNext.pPrev = pNode;
                    }
                    pCurr.pNext = pNode;
                    //pNode.pNext = pCurr.pNext;
                    //pNode.pPrev = pCurr;
                    //pCurr.pNext = pNode;
                }
            }
            return pNode;
        }

        //------------------------------------------------------------
        // Virtual Method
        //      This function must be used only in TimeEvent class.
        //------------------------------------------------------------
        virtual protected float DerivedCompareValue()
        {
            Debug.Assert(false);
            return 0.0f;
        }

        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        public DLink pNext;
        public DLink pPrev;
    }
}
