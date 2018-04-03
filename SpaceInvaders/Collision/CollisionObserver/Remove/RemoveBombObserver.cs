using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBombObserver : CollisionObservers
    {
        public RemoveBombObserver()
        {
            this.pBomb = null;
        }

        public RemoveBombObserver(RemoveBombObserver b)
        {
            Debug.Assert(b != null);
            this.pBomb = b.pBomb;
        }

        ~RemoveBombObserver()
        {
            this.pBomb = null;
        }

        public override void Notify()
        {
            if( IsValidCollision() && !this.pBomb.isDead)
            {
                pBomb.isDead = true;

                ExplosionManager.GetBombExplosion(this.pBomb);

                DelayRemoveManager.Attach(new RemoveBombObserver(this));
            }
        }
        
        public override bool IsValidCollision()
        {
            bool isValid = false;
            if(this.pSubject.pObjA is ShieldBrick && this.pSubject.pObjB is Bomb)
            {
                this.pBomb = (Bomb)this.pSubject.pObjB;
                isValid = true;
            } else if(this.pSubject.pObjA is Bomb && this.pSubject.pObjB is WallBottom)
            {
                this.pBomb = (Bomb)this.pSubject.pObjA;
                isValid = true;
            }
            else if (this.pSubject.pObjA is Bomb && this.pSubject.pObjB is MissileGroup)
            {
                this.pBomb = (Bomb)this.pSubject.pObjA;
                isValid = true;
            }
            else if (this.pSubject.pObjA is Bomb && this.pSubject.pObjB is PlayerGroup)
            {
                this.pBomb = (Bomb)this.pSubject.pObjA;
                isValid = true;
            }
            return isValid;
        }

        public override void Execute()
        {
            BombManager.DeactiveBomb(this.pBomb);
        }

        private Bomb pBomb;
    }
}
