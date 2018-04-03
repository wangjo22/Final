using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienNode_Link : DLink
    {
    }
    public class AlienNode : AlienNode_Link
    {
        public AlienNode()
        {
            this.pObj = null;
            this.index = -1;
        }

        public void Set(GameObject pObj)
        {
            Debug.Assert(pObj != null);

            this.pObj = pObj;
            this.index = pObj.index;
        }

        public new void Clear()
        {
            this.pObj = null;
            this.index = -1;
        }

        public void PrintMe()
        {
            Debug.WriteLine("{0} ---- {1}", this.index, this.GetHashCode());
        }

        public GameObject GetAlienObject()
        {
            Debug.Assert(this.pObj != null);
            return this.pObj;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }

        override protected float DerivedCompareValue()
        {
            return this.index;
        }

        private GameObject pObj;
        public int index;
    }
}
