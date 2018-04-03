using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PlayerDeadState : PlayerShootState
    {
        public override void Handle(Player pPlayer)
        {
            Debug.Assert(pPlayer != null);
            pPlayer.SetShootingState(PlayerManager.ShootState.MissileReady);
        }

        public override void ShootMissile(Player pPlayer)
        {
            if (!PlayerManager.GetPlayerDeadOrAlive())
            {
                GameObject explosion = ExplosionManager.GetPlayerExplosion();
                pPlayer.x = explosion.x;
                pPlayer.y = explosion.y;
                PlayerManager.SetPlayerAlive();

                GameObject p = PlayerManager.GetMissile();
                p.x = -10;

                explosion.x = -100;
                explosion.y = -100;
                this.Handle(pPlayer);
            }
        }

    }
}
