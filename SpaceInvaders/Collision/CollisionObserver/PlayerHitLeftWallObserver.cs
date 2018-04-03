using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerHitLeftWallObserver : CollisionObservers
    {
        public PlayerHitLeftWallObserver()
        {
            
        }

        public override void Notify()
        {
            if ( this.IsValidCollision() )
            {
                Player pPlayer = PlayerManager.GetPlayer();
                Debug.Assert(pPlayer != null);

                pPlayer.SetMoveState(PlayerManager.MoveState.PlayerHitLeftWall);
            }
        }

        public override bool IsValidCollision()
        {
            bool isValid = false;
            if (this.pSubject.pObjA is BumperLeft && this.pSubject.pObjB is Player)
            {
                isValid = true;
            }
            
            return isValid;
        }
    }
}
