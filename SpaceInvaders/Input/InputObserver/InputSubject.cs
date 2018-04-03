using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputSubject
    {
        public void Attach(InputObserver observer)
        {
            Debug.Assert(observer != null);

            observer.pSubject = this;

            // add to front
            if (this.pHead == null)
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
            InputObserver pNode = this.pHead;
            while (pNode != null)
            {
                pNode.Notify();
                pNode = (InputObserver)pNode.pNext;
            }
        }

        public void Detach()
        {
        }


        // Data: ------------------------
        private InputObserver pHead;

    }
}
