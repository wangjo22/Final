using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SLink
    {
        //---------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------
        protected SLink()
        {
            this.Clear();
        }

        //---------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------
        public void Clear()
        {
            this.pNext = null;
        }

        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void AddToFront(ref SLink pHead, SLink pNode)
        {
            Debug.Assert(pNode != null);

            // If the linked list is empty
            if(pHead == null)
            {
                pHead = pNode;
                pNode.pNext = null;
            }
            // If the linked list is not empty
            else
            {
                pNode.pNext = pHead;
                pHead = pNode;
            }
            Debug.Assert(pHead != null);
        }

        public static SLink PullFromFront(ref SLink pHead)
        {
            Debug.Assert(pHead != null);

            // Return node
            SLink pNode = pHead;

            // Update head
            pHead = pHead.pNext;
            pNode.Clear();

            return pNode;
        }

        //public static void RemoveNode(ref SLink pHead, SLink pNode)
        //{
        //    Debug.Assert(pNode != null);

        //    SLink nextNode = pNode.pNext;



        //    //void deleteNode(Node* node)
        //    //{
        //    //    Node* temp = node->next;
        //    //    node->data = node->next->data;
        //    //    node->next = temp->next;
        //    //    free(temp);
        //    //}
        //}


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        public SLink pNext;
    }
}
