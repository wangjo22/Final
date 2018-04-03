using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PlayerHitLeftWall : PlayerMoveState
    {
        public override void Handle(Player pPlayer)
        {
            Debug.Assert(pPlayer != null);
            pPlayer.SetMoveState(PlayerManager.MoveState.PlayerMoveNormal);
        }

        public override void MoveRight(Player pPlayer)
        {
            Debug.Assert(pPlayer != null);
            pPlayer.x += Constant.PLAYER_SPEED + 1;
            this.Handle(pPlayer);
        }

        public override void MoveLeft(Player pPlayer)
        {
            
        }

        //public override void ShootMissile(Player pPlayer)
        //{
        //    Missile pMissile = PlayerManager.GetMissile();
        //    pMissile.x = pPlayer.x;
        //    pMissile.y = pPlayer.y + 20.0f;
        //    pMissile.SetFlyingStatus(true);
        //    pMissile.isDead = false;
        //    SoundManager.Play(Sound.Name.PlayerShoot);
        //    this.Handle(pPlayer);
        //}

    }
}
