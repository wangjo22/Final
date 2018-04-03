using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PlayerReadyToShoot : PlayerShootState
    {
        public override void Handle(Player pPlayer)
        {
            Debug.Assert(pPlayer != null);
            pPlayer.SetShootingState(PlayerManager.ShootState.MissileFlying);
        }

        public override void ShootMissile(Player pPlayer)
        {
            if (PlayerManager.GetPlayerDeadOrAlive())
            {
                Missile pMissile = PlayerManager.GetMissile();
                pMissile.x = pPlayer.x;
                pMissile.y = pPlayer.y + 20.0f;
                pMissile.SetFlyingStatus(true);
                pMissile.isDead = false;
                SoundManager.Play(Sound.Name.PlayerShoot);
                this.Handle(pPlayer);
            }
        }

    }
}
