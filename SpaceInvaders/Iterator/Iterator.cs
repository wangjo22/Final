using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Iterator
    {
        abstract public Component Next();
        abstract public bool IsDone();
        abstract public Component First();

        static public GameObject GetSiblingGameObject(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObject pGameObj = (GameObject)ForwardIterator.GetSibling(pNode);
            return pGameObj;
        }

        static public GameObject GetChildGameObject(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObject pGameObj = (GameObject)ForwardIterator.GetChild(pNode);
            return pGameObj;
        }


        //static public GameObject GetParentGameObject(GameObject pNode)
        //{
        //    GameObject pGameObj = null;
        //    Component pComponent = ForwardIterator.GetParent(pNode);
        //    if(pComponent.type == Component.Container.LEAF)
        //    {
        //        pGameObj = (GameObject)pComponent;
        //    }
        //    return pGameObj;
        //}
    }
}
