using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Component : DLink
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            UNKNOWN
        }
        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Print();
        public abstract void Move(float _x, float _y);
        public abstract Component GetFirstChild();
        public abstract void DumpNode();

        //abstract public void Update();

        public Component pParent = null;
       // public Component pReverse = null;
        public Component.Container type = Component.Container.UNKNOWN;

    }
}
