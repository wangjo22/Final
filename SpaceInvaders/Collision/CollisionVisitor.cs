using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class CollisionVisitor : Component
    {
        public virtual void VisitAlienGroup(AlienGroup ag)
        {
            Debug.Assert(false);
        }

        public virtual void VisitAlienColumn(AlienColumn ac)
        {
            Debug.Assert(false);
        }

        public virtual void VisitAlienCrab(AlienCrab ac)
        {
            Debug.Assert(false);
        }

        public virtual void VisitAlienSquid(AlienSquid asq)
        {
            Debug.Assert(false);
        }

        public virtual void VisitAlienOctopus(AlienOctopus ao)
        {
            Debug.Assert(false);
        }
        public virtual void VisitNullGameObject(NullGameObject n)
        {
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup m)
        {
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile m)
        {
            Debug.Assert(false);
        }

        public virtual void VisitPlayer(Player p)
        {
            Debug.Assert(false);
        }

        public virtual void VisitPlayerGroup(PlayerGroup pPG)
        {
            Debug.Assert(false);
        }

        public virtual void VisitWallLeft(WallLeft wl)
        {
            Debug.Assert(false);
        }

        public virtual void VisitWallRight(WallRight wr)
        {
            Debug.Assert(false);
        }

        public virtual void VisitWallGroup(WallGroup wg)
        {
            Debug.Assert(false);
        }

        public virtual void VisitWallTop(WallTop wt)
        {
            Debug.Assert(false);
        }

        public virtual void VisitWallBottom(WallBottom wb)
        {
            Debug.Assert(false);
        }

        public virtual void VisitBomb(Bomb b)
        {
             Debug.Assert(false);
        }

        public virtual void VisitBombGroup(BombGroup bg)
        {
            Debug.Assert(false);
        }

        public virtual void VisitShieldGroup(ShieldGroup sg)
        {
            Debug.Assert(false);
        }
        public virtual void VisitShieldColumn(ShieldColumn sc)
        {
            Debug.Assert(false);
        }
        public virtual void VisitShieldBrick(ShieldBrick sb)
        {
            Debug.Assert(false);
        }

        public virtual void VisitUFO(UFO u)
        {
            Debug.Assert(false);
        }

        public virtual void VisitUFOGroup(UFOGroup ug)
        {
            Debug.Assert(false);
        }

        public virtual void VisitBumperLeft(BumperLeft bl)
        {
            Debug.Assert(false);
        }

        public virtual void VisitBumperRight(BumperRight br)
        {
            Debug.Assert(false);
        }


        abstract public void Accept(CollisionVisitor other);
    }
}
