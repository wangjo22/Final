using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionPair_Link : DLink
    { }

    public class CollisionPair : CollisionPair_Link
    {
        public enum Name
        {
            Alien_VS_Missile,
            Alien_VS_PlayerShip,
            Alien_VS_Wall,
            Alien_VS_Shield,

            Missile_VS_Wall,
            Missile_VS_Shield,
            Missile_VS_UFO,

            Bomb_VS_Wall,
            Bomb_VS_Missile,
            Bomb_VS_Shield,

            PlayerShip_VS_Wall,
            PlyaerShip_VS_Bomb,

            Wall_VS_UFO,

            NullObject,
            Uninitialized
        }

        public CollisionPair()
            : base()
        {
            this.name = CollisionPair.Name.Uninitialized;
            this.treeA = null;
            this.treeB = null;
            this.poColSubject = new CollisionSubjects();

            Debug.Assert(this.poColSubject != null);
        }

        public CollisionPair(CollisionPair.Name colPairName, GameObject pTreeA, GameObject pTreeB)
        {
            Debug.Assert(pTreeA != null);
            Debug.Assert(pTreeB != null);

            this.name = colPairName;
            this.treeA = pTreeA;
            this.treeB = pTreeB;
            this.poColSubject = new CollisionSubjects();

            Debug.Assert(this.poColSubject != null);
        }

        ~CollisionPair()
        {
            this.name = CollisionPair.Name.Uninitialized;
            this.treeA = null;
            this.treeB = null;
            this.poColSubject = null;
        }

        public void SetCollisionPair(CollisionPair.Name colPairName, GameObject pTreeA, GameObject pTreeB)
        {
            Debug.Assert(pTreeA != null);
            Debug.Assert(pTreeB != null);
            //Debug.Assert(this.poColSubject != null);

            this.name = colPairName;
            this.treeA = pTreeA;
            this.treeB = pTreeB;
        }

        public void Wash()
        {
            this.name = CollisionPair.Name.Uninitialized;
            this.treeA = null;
            this.treeB = null;
        }

        public CollisionPair.Name GetName()
        {
            return this.name;
        }

        public void Process()
        {
            Collide(this.treeA, this.treeB);
        }

        static public void Collide(GameObject pTreeA, GameObject pTreeB)
        {
            GameObject pNodeA = pTreeA;
            GameObject pNodeB = pTreeB;

            while (pNodeA != null)
            {
                pNodeB = pTreeB;
                while (pNodeB != null)
                {
                    CollisionRect rectA = pNodeA.pColObject.pColRect;
                    CollisionRect rectB = pNodeB.pColObject.pColRect;
                    //Debug.WriteLine("Collision Pair : {0} <----------> {1}", pNodeA.name, pNodeB.name);
                    if (CollisionRect.Intersect(rectA, rectB))
                    {
                        pNodeA.Accept(pNodeB);
                        break;
                    }
                    pNodeB = Iterator.GetSiblingGameObject(pNodeB);
                }
                pNodeA = Iterator.GetSiblingGameObject(pNodeA);
            }
        }

        public void AttachObserver(CollisionObservers observer)
        {
            Debug.Assert(observer != null);
            Debug.Assert(this.poColSubject != null);
            this.poColSubject.AttachObserver(observer);
        }

        public void NotifyListeners()
        {
            Debug.Assert(this.poColSubject != null);
            this.poColSubject.Notify();
        }

        public void SetObserverSubject(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);

            this.poColSubject.pObjA = pObjA;
            this.poColSubject.pObjB = pObjB;
        }


        public void SetName(CollisionPair.Name pairName)
        {
            this.name = pairName;
        }

        public void Dump()
        {
            Debug.WriteLine("\tCollision Pair : {0}  ({1})", this.GetName(), this.GetHashCode());
        }

        //--------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------
        public CollisionPair.Name name;
        public GameObject treeA;
        public GameObject treeB;
        public CollisionSubjects poColSubject;
    }
}
