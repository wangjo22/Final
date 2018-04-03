using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Manager
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        protected Manager()
        {
            this.pReserve = null;
            this.pActive = null;
            this.mNumReserve = 0;
            this.mNumActive = 0;
            this.mNumTotal = 0;
            this.mInitialNumReserved = 0;
            this.growthSize = 0;
            this.maximumNumActive = 0;
        }

        //---------------------------------------------------------------------------------
        // Destructor
        //---------------------------------------------------------------------------------
        ~Manager()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("   ~Manager():{0}, this.GetHashCode());
#endif
            this.pActive = null;
            this.pReserve = null;
        }


        protected void BaseInitialize(int initialNumReserved = 5, int sizeGrow = 2)
        {
            // Check Inputs
            Debug.Assert(initialNumReserved >= 0);
            Debug.Assert(sizeGrow > 0);

            // Initialize my data
            this.growthSize = sizeGrow;
            this.mInitialNumReserved = initialNumReserved;

            // Create reserve nodes in reserve list
            this.PrivFillReservedPool(initialNumReserved);
        }

        //---------------------------------------------------------------------------------
        // Base Methods
        //---------------------------------------------------------------------------------
        protected void BaseSetReserve(int reserveNum, int reserveGrow)
        {
            this.growthSize = reserveGrow;
            if(reserveNum > this.mNumReserve)
            {
                this.PrivFillReservedPool(reserveNum - this.mNumReserve);
            }
        }

        protected DLink BaseAdd()
        {
            // If any node doesn't exist in reserve list.
            if(this.pReserve == null)
            {
                // Refill the reserve list by the growthSize
                this.PrivFillReservedPool(this.growthSize);
            }

            // Always take from the reserve list
            DLink pLink = DLink.PullFromFront(ref this.pReserve);
            Debug.Assert(pLink != null);

            // Wash it
            this.DerivedWash(pLink);

            // Update stats
            this.mNumActive++;
            this.mNumReserve--;
            if (this.mNumActive > this.maximumNumActive)
            {
                this.maximumNumActive = this.mNumActive;
            }

            // copy to active
            DLink.AddToFront(ref this.pActive, pLink);
            return pLink;
        }


        protected DLink BaseFind(DLink pNodeTarget)
        {
            // Get the front node in active list
            DLink pLink = this.pActive;

            // Walk through nodes in active list
            while(pLink != null)
            {
                // Compare name in current node and target node. 
                if(DerivedCompare(pLink, pNodeTarget))
                {
                    //Debug.WriteLine("found");
                    break;
                }
                pLink = pLink.pNext;
            }
            return pLink;
        }

        protected void BaseRemove(DLink pNode)
        {
            Debug.Assert(pNode != null);

            // Transfer the node in active list to reserve list.
            DLink.RemoveNode(ref this.pActive, pNode);
            this.DerivedWash(pNode);
            DLink.AddToFront(ref this.pReserve, pNode);

            // Update Stats
            this.mNumActive--;
            this.mNumReserve++;
        }

        protected DLink BasePopNode()
        {
            // If any node doesn't exist in reserve list.
            if (this.pReserve == null)
            {
                // Refill the reserve list by the growthSize
                this.PrivFillReservedPool(this.growthSize);
            }

            // Always take from the reserve list
            DLink pLink = DLink.PullFromFront(ref this.pReserve);
            Debug.Assert(pLink != null);

            // Wash it
            this.DerivedWash(pLink);

            // Update stats
            this.mNumReserve--;

            return pLink;
        }

        protected DLink BaseSortedAdd(DLink pNode)
        {
            Debug.Assert(pNode != null);

            this.mNumActive++;
            if(this.maximumNumActive < this.mNumActive)
            {
                this.maximumNumActive = this.mNumActive;
            }

            DLink.SortedAdd(ref this.pActive, pNode);
            return pNode;
        }


        public DLink BaseGetActive()
        {
            return this.pActive;
        }

        public ref DLink BaseGetRefActive()
        {
            return ref this.pActive;
        }

        protected void BaseDestroy()
        {
            DLink pNode;
            DLink pTmpNode;

            pNode = this.pActive;
            while(pNode != null)
            {
                pTmpNode = pNode;
                pNode = pNode.pNext;

                Debug.Assert(pTmpNode != null);
                this.DerivedDestroyNode(pTmpNode);
                DLink.RemoveNode(ref this.pActive, pTmpNode);
                pTmpNode = null;
                this.mNumActive--;
                this.mNumTotal--;
            }

            pNode = this.pReserve;
            while(pNode != null)
            {
                pTmpNode = pNode;
                pNode = pNode.pNext;
                Debug.Assert(pTmpNode != null);
                this.DerivedDestroyNode(pTmpNode);
                DLink.RemoveNode(ref this.pReserve, pTmpNode);
                pTmpNode = null;

                this.mNumReserve--;
                this.mNumTotal--;
            }
        }


        protected void BaseDump()
        {
            this.BaseDumpStats();
            this.BaseDumpNodes();
        }


        protected void BaseDumpNodes()
        {
            Debug.WriteLine("");
            Debug.WriteLine("------ Active List: ---------------------------\n");

            DLink pNode = this.pActive;
            int i;

            if (pNode == null)
            {
                Debug.WriteLine("  <list empty>");
            }
            else
            {
                i = 0;
                while (pNode != null)
                {
                    Debug.WriteLine("{0}: -------------", i);
                    this.DerivedDumpNode(pNode);
                    i++;
                    pNode = pNode.pNext;
                }
            }
            Debug.WriteLine("");
            Debug.WriteLine("------ Reserve List: ---------------------------\n");

            pNode = this.pReserve;

            if (pNode == null)
            {
                Debug.WriteLine("  <list empty>");
            }
            else
            {
                i = 0;
                while (pNode != null)
                {
                    Debug.WriteLine("{0}: -------------", i);
                    this.DerivedDumpNode(pNode);
                    i++;
                    pNode = pNode.pNext;
                }
            }
        }
        protected void BaseDumpStats()
        {
            Debug.WriteLine("");
            Debug.WriteLine("-------- Stats: -------------");
            Debug.WriteLine("  Total Num Nodes: {0}", this.mNumTotal);
            Debug.WriteLine("       Num Active: {0}", this.mNumActive);
            Debug.WriteLine("     Num Reserved: {0}", this.mNumReserve);
            Debug.WriteLine("       Delta Grow: {0}", this.growthSize);


        }


        //----------------------------------------------------------------------
        // Abstract methods - the "contract" Derived class must implement
        //----------------------------------------------------------------------
        abstract protected DLink DerivedCreateNode();
        abstract protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB);
        abstract protected void DerivedWash(DLink pLink);
        abstract protected void DerivedDumpNode(DLink pLink);
        virtual protected void DerivedDestroyNode(DLink pLink)
        {
            // default: do nothing
#if (TRACK_DESTRUCTOR_MANAGER)
            Debug.WriteLine("   {0} ({1})", pLink, pLink.GetHashCode());
#endif
            pLink = null;
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private void PrivFillReservedPool(int count)
        {
            Debug.Assert(count > 0);

            this.mNumTotal += count;
            this.mNumReserve += count;

            for(int i = 0; i < count; ++i)
            {
                DLink pNode = this.DerivedCreateNode();
                Debug.Assert(pNode != null);

                DLink.AddToFront(ref this.pReserve, pNode);
            }
        }

        //-------------------------------------
        // Data
        //-------------------------------------
        private DLink pReserve;
        private DLink pActive;
        private int mNumReserve;
        private int mNumActive;
        private int growthSize;
        private int mNumTotal;
        private int mInitialNumReserved;
        private int maximumNumActive;
    }

}
