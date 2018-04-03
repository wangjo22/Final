using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PlayerMissileFlyingState : PlayerShootState
    {
        public override void Handle(Player pPlayer)
        {
            Debug.Assert(pPlayer != null);
            pPlayer.SetShootingState(PlayerManager.ShootState.MissileReady);
        }

        public override void ShootMissile(Player pPlayer)
        {
            Missile pMissile = PlayerManager.GetMissile();
            if (pMissile.x < 0)
            {
                this.Handle(pPlayer);
            }
        }

    }
}
