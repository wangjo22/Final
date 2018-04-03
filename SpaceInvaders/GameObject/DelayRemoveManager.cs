using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class DelayRemoveManager
    {
        private DelayRemoveManager()
        {
            this.head = null;
        }

        ~DelayRemoveManager()
        {

        }
        
        static public void Attach(CollisionObservers pObs)
        {
            Debug.Assert(pObs != null);

            DelayRemoveManager pMan = DelayRemoveManager.PrivGetInstance();

            if (pMan.head == null)
            {
                pMan.head = pObs;
                pObs.pNext = null;
                pObs.pPrev = null;
            }
            else
            {
                pObs.pNext = pMan.head;
                pObs.pPrev = null;
                pMan.head.pPrev = pObs;
                pMan.head = pObs;
            }
        }
        private void PrivDetach(CollisionObservers node, ref CollisionObservers head)
        {
            Debug.Assert(node != null);

            if (node.pPrev != null)
            {
                node.pPrev.pNext = node.pNext;
            }
            else
            {
                head = (CollisionObservers)node.pNext;
            }

            if (node.pNext != null)
            {
                node.pNext.pPrev = node.pPrev;
            }
        }
        static public void Process()
        {
            DelayRemoveManager pMan = DelayRemoveManager.PrivGetInstance();

            CollisionObservers pNode = pMan.head;

            while (pNode != null)
            {
                pNode.Execute();
                pNode = (CollisionObservers)pNode.pNext;
            }

            pNode = pMan.head;
            CollisionObservers pTmp = null;

            while (pNode != null)
            {
                pTmp = pNode;
                pNode = (CollisionObservers)pNode.pNext;
                pMan.PrivDetach(pTmp, ref pMan.head);
            }
        }

        static public void RemoveAll()
        {
            DelayRemoveManager pMan = DelayRemoveManager.PrivGetInstance();
            CollisionObservers pNode = pMan.head;
            CollisionObservers pTmp = null;

            while (pNode != null)
            {
                pTmp = pNode;
                pNode = (CollisionObservers)pNode.pNext;
                pMan.PrivDetach(pTmp, ref pMan.head);
            }
        }
        private static DelayRemoveManager PrivGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new DelayRemoveManager();
            }
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // Data: ------------------------
        private CollisionObservers head;
        private static DelayRemoveManager pInstance = null;
    }
}
