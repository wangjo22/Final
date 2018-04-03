using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerDieObserver : CollisionObservers
    {
        public PlayerDieObserver()
        {
        }

        public PlayerDieObserver(PlayerDieObserver m)
        {
            Debug.Assert(m != null);
        }

        ~PlayerDieObserver()
        {
        }

        public override void Notify()
        {
            if ( this.IsValidCollision() )
            {
                ExplosionManager.GetPlayerExplosion(this.pPlayer);
                PlayerManager.PlayerDead();
            }
        }
        
        public override bool IsValidCollision()
        {
            bool isValid = false;
            if (this.pSubject.pObjA is Bomb && this.pSubject.pObjB is PlayerGroup)
            {
                PlayerGroup pGroup = (PlayerGroup)this.pSubject.pObjB;
                this.pPlayer = (Player)pGroup.poHead;
                isValid = true;
            } 
            return isValid;
        }

        private Player pPlayer;
    }
}
