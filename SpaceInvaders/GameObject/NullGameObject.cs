using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class NullGameObject : Leaf
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public NullGameObject()
            : base(GameObject.Name.Uninitialized, GameSprite.Name.NullObject)
        { }

        //--------------------------------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------------------------------
        ~NullGameObject()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~NullGameObject():{0}", this.GetHashCode());
#endif
        }

        //--------------------------------------------------------------------------------------------
        // Override method
        //--------------------------------------------------------------------------------------------
        public override void Update()
        {
            // It is Null Object. It does not have any behavior.
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitNullGameObject(this);
        }
    }
}
