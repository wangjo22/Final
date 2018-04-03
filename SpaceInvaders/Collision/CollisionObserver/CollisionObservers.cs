using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionObservers : DLink
    {
        public abstract void Notify();
        public virtual bool IsValidCollision()
        {
            return false;
        }
        public CollisionSubjects pSubject;

        public virtual void Execute()
        {

        }
    }


}
