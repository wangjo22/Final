using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Leaf : GameObject
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public Leaf(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
            : base(gameObjectName, gameSpriteName)
        {
            this.type = Component.Container.LEAF;
        }

        //--------------------------------------------------------------------------------------------
        // Method
        //--------------------------------------------------------------------------------------------
        override public void Add(Component c)
        {
            Debug.Assert(false);
        }

        override public void Remove(Component c)
        {
            Debug.Assert(false);
        }

        override public void Print()
        {
            Debug.WriteLine("\tGameObject : {0} ({1}, {2}) {3} {4}", this.GetName(), this.GetX(), this.GetY(), this.pParent, this.pParent.GetHashCode());
        }

        override public void Move(float _x, float _y)
        {
            Debug.Assert(false);
        }

        override public void DumpNode()
        {
            Debug.WriteLine("GameObject Name:\t{0} ({1})", this.GetName(), this.GetHashCode());
        }

        override public Component GetFirstChild()
        {
            Debug.Assert(false);
            return null;
        }

        public void GotHitMissile()
        {
            Debug.Assert(this.pParent.type == Component.Container.COMPOSITE);
            Composite pParent = (Composite)this.pParent;
            this.RemoveFromHierachy();
            this.RemoveFromSpriteBatch();

            pParent.RemoveComposite();
            
        }

        public void RemoveFromHierachy()
        {
            Composite pParent = (Composite)this.pParent;
            pParent.RemoveFromHeadAndLast(this);
            //if(pParent.poHead == null && pParent.poLast == null)
            //{
            //    pParent.RemoveComposite();
            //}
        }
    }
}
