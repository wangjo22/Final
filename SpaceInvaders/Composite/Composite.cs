using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Composite : GameObject
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public Composite(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
            : base(gameObjectName, gameSpriteName)
        {
            this.poHead = null;
            this.poLast = null;
            this.type = Component.Container.COMPOSITE;
        }

        //--------------------------------------------------------------------------------------------
        // Method
        //--------------------------------------------------------------------------------------------
        override public void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.AddToLast(ref this.poHead, ref this.poLast, pComponent);
            pComponent.pParent = this;
        }

        override public void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.RemoveNode(ref this.poHead, pComponent);
        }

        public void RemoveFromHeadAndLast(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.RemoveNodeFromHeadAndLast(ref this.poHead, ref this.poLast, pComponent);
        }

        public override void Print()
        {
            //DLink pNode = this.poHead;

            //Debug.WriteLine("---------------------------------");
            //while (pNode != null)
            //{
            //    Component pComponent = (Component)pNode;
            //    pComponent.Print();

            //    pNode = pNode.pNext;
            //}
            string parent = "";
            if (this.pParent == null)
            {
                parent = "null";
            }
            else
            {
                parent = this.pParent.GetHashCode().ToString();
            }
            Debug.WriteLine("GameObject Name: {0} ({1}) {2}   <------- Composite", this.GetName(), this.GetHashCode(), parent);
        }

        public override void Move(float _x, float _y)
        {
            Debug.Assert(false);
        }

        public void MoveAllAliens()
        {
            this.Move(Constant.ALIEN_SIDE_STEP, 0.0f);
        }

        public override Component GetFirstChild()
        {
            Debug.Assert(this.poHead != null);
            DLink pNode = this.poHead;
            return (Component)pNode;
        }

        //public override void DumpNode()
        //{
        //    Debug.WriteLine(" GameObject Name: ({0}) <------- Composite", this.GetHashCode());
        //}
        public override void DumpNode()
        {
            Debug.WriteLine("GameObject Name: {0} ({1}) <------- Composite", this.GetName(), this.GetHashCode());
        }


        public void RemoveComposite()
        {
            if (this.poHead != null && this.poLast != null)
            {
                return;
            }

            if (this is AlienColumn)
            {
                AlienManager.Add(this);
            }

            if (this is ShieldColumn)
            {
                ShieldNodeManager.Add(this);
            }

            Composite pParent = (Composite)this.pParent;
            if (this.pPrev == null && this.pNext == null)
            {
                pParent.poLast = null;
                pParent.poHead = null;
            }
            else if (this.pPrev == null)
            {
                pParent.poHead = this.pNext;
                this.pNext.pPrev = null;
            }
            else if (this.pNext == null)
            {
                pParent.poLast = this.pPrev;
                this.pPrev.pNext = null;
            }
            else
            {
                this.pPrev.pNext = this.pNext;
                this.pNext.pPrev = this.pPrev;
            }
            this.RemoveFromSpriteBatch();
            this.pPrev = null;
            this.pNext = null;
            this.Remove(this);

            if (pParent.poHead == null && pParent.poLast == null && pParent.pNext == null && pParent.pPrev == null)
            {
                pParent.Remove(this);
                if (pParent is AlienGroup)
                {
                    GameScene.Handle();
                }
                //AlienManager.PrintMe();
                //AlienManager.ResetAllAlienGrid();
                //pParent.RemoveFromSpriteBatch();
            }
        }

        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        public DLink poHead;
        public DLink poLast;

    }
}
