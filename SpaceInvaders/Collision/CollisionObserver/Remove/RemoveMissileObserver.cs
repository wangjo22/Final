using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveMissileObserver : CollisionObservers
    {
        public RemoveMissileObserver()
        {
            this.pMissile = null;
        }

        public RemoveMissileObserver(RemoveMissileObserver m)
        {
            this.pMissile = m.pMissile;
        }

        ~RemoveMissileObserver()
        {
            this.pMissile = null;
        }

        public override void Notify()
        {
            if ( this.IsValidCollision() )
            {
                if (this.pMissile.isDead == false)
                {
                    Player pPlayer = PlayerManager.GetPlayer();
                    Debug.Assert(pPlayer != null);
                    pPlayer.SetShootingState(PlayerManager.ShootState.MissileReady);

                    this.pMissile.isDead = true;
                    this.pMissile.SetFlyingStatus(false);

                    DelayRemoveManager.Attach(new RemoveMissileObserver(this));
                }
            }
        }
        
        public override bool IsValidCollision()
        {
            bool isValid = false;
            if (this.pSubject.pObjA is Missile && this.pSubject.pObjB is WallTop)
            {
                this.pMissile = (Missile)this.pSubject.pObjA;
                isValid = true;
            }
            else if (this.pSubject.pObjA is ShieldBrick && this.pSubject.pObjB is Missile)
            {
                this.pMissile = (Missile)this.pSubject.pObjB;
                isValid = true;
            }
            else if (this.pSubject.pObjA is Missile && this.pSubject.pObjB is AlienCategory)
            {
                this.pMissile = (Missile)this.pSubject.pObjA;
                isValid = true;
            }
            else if (this.pSubject.pObjA is Bomb && this.pSubject.pObjB is MissileGroup)
            {
                MissileGroup mg = (MissileGroup)this.pSubject.pObjB;
                this.pMissile = (Missile)mg.poHead;
                isValid = true;
            }
            else if (this.pSubject.pObjA is UFOGroup && this.pSubject.pObjB is MissileGroup)
            {
                MissileGroup mg = (MissileGroup)this.pSubject.pObjB;
                this.pMissile = (Missile)mg.poHead;
                isValid = true;
            }
            
            return isValid;
        }

        public override void Execute()
        {
            PlayerManager.DeactiveMissile();
            //this.pMissile.DeactiveMissile();
        }

        private Missile pMissile;
      //  private GameObject pWallTop;
    }
}
