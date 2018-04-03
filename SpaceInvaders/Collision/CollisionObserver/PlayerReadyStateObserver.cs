using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerReadyStateObserver : CollisionObservers
    {
        public PlayerReadyStateObserver()
        {
            
        }

        public override void Notify()
        {
            if ( this.IsValidCollision() )
            {
                Player pPlayer = PlayerManager.GetPlayer();
                Debug.Assert(pPlayer != null);

                pPlayer.SetShootingState(PlayerManager.ShootState.MissileReady);
            }
        }

        public override bool IsValidCollision()
        {
            bool isValid = false;
            if (this.pSubject.pObjA is Missile && this.pSubject.pObjB is WallTop)
            {
                isValid = true;
            }
            else if (this.pSubject.pObjA is AlienCategory || this.pSubject.pObjB is AlienCategory)
            {
                isValid = true;
            }
            else if (this.pSubject.pObjA is ShieldBrick && this.pSubject.pObjB is Missile)
            {
                isValid = true;
            }
            return isValid;
        }
        
    }
}
