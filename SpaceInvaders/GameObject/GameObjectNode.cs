using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObjectNode_Link : DLink
    { }
    public class GameObjectNode : GameObjectNode_Link
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public GameObjectNode()
            : base()
        {
            this.pGameObject = null;
        }

        //--------------------------------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------------------------------
        ~GameObjectNode()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~GameObjectNode():{0}", this.GetHashCode());
#endif
            this.pGameObject = null;
        }

        //--------------------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------------------
        public void Set(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObject = pGameObject;
        }

        public void Wash()
        {
            this.pGameObject = null;
        }

        public Enum GetName()
        {
            return this.pGameObject.GetName();
        }

        public void Dump()
        {
            Debug.Assert(this.pGameObject != null);
            Debug.WriteLine("\t\t     GameObject: {0}", this.GetHashCode());

            this.pGameObject.Dump();
        }
        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        public GameObject pGameObject;
    }
}
