using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionSubjects
    {
        public CollisionSubjects()
        {
            this.pHead = null;
            this.pObjA = null;
            this.pObjB = null;
        }

        ~CollisionSubjects()
        {
            this.pHead = null;
            this.pObjA = null;
            this.pObjB = null;
        }

        public void AttachObserver(CollisionObservers observer)
        {
            Debug.Assert(observer != null);

            observer.pSubject = this;

            // Attach Observer to the front in Collision Pair
            if(this.pHead == null)
            {
                observer.pNext = null;
                observer.pPrev = null;
                this.pHead = observer;
            }
            else
            {
                observer.pNext = this.pHead;
                observer.pPrev = null;
                this.pHead.pPrev = observer;
                this.pHead = observer;
            }
        }

        public void Notify()
        {
            CollisionObservers pNode = pHead;
            while(pNode != null)
            {
                pNode.Notify();
                pNode = (CollisionObservers)pNode.pNext;
            }
        }

        public void Detach()
        {
        }

        private CollisionObservers pHead;
        public GameObject pObjA;
        public GameObject pObjB;
    }


}
