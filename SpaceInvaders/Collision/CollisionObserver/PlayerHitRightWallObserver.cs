using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerHitRightWallObserver : CollisionObservers
    {
        public PlayerHitRightWallObserver()
        {
            
        }

        public override void Notify()
        {
            if ( this.IsValidCollision() )
            {
                Player pPlayer = PlayerManager.GetPlayer();
                Debug.Assert(pPlayer != null);

                pPlayer.SetMoveState(PlayerManager.MoveState.PlayerHitRightWall);
            }
        }

        public override bool IsValidCollision()
        {
            bool isValid = false;
            if (this.pSubject.pObjA is BumperRight && this.pSubject.pObjB is Player)
            {
                isValid = true;
            }
            
            return isValid;
        }
    }
}
