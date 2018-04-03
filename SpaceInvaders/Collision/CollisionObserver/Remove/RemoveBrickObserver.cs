using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBrickObserver : CollisionObservers
    {
        public RemoveBrickObserver()
        {
            this.pBrick = null;
        }

        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            this.pBrick = b.pBrick;
        }

        ~RemoveBrickObserver()
        {
            this.pBrick = null;
        }

        public override void Notify()
        {
            if( IsValidCollision() && this.pBrick.isDead == false)
            {
                this.pBrick.isDead = true;
                DelayRemoveManager.Attach(new RemoveBrickObserver(this));
            }
        }
        
        public override bool IsValidCollision()
        {
            bool isValid = false;
            if(this.pSubject.pObjA is ShieldBrick && this.pSubject.pObjB is Missile)
            {
                Missile pMissile = (Missile)this.pSubject.pObjB;
                if(!pMissile.isDead)
                {
                    this.pBrick = (ShieldBrick)this.pSubject.pObjA;
                    isValid = true;
                }
            }
            else if (this.pSubject.pObjA is ShieldBrick && this.pSubject.pObjB is Bomb)
            {
                this.pBrick = (ShieldBrick)this.pSubject.pObjA;
                isValid = true;
            }
            else if (this.pSubject.pObjA is ShieldBrick && this.pSubject.pObjB is AlienColumn)
            {
                this.pBrick = (ShieldBrick)this.pSubject.pObjA;
                isValid = true;
            }
            return isValid;
        }

        public override void Execute()
        {
            ShieldNodeManager.Add(this.pBrick);
            this.pBrick.GotHitMissile();
        }

        private ShieldBrick pBrick;
    }
}
