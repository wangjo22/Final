using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PlayerHitRightWall : PlayerMoveState
    {
        public override void Handle(Player pPlayer)
        {
            Debug.Assert(pPlayer != null);
            pPlayer.SetMoveState(PlayerManager.MoveState.PlayerMoveNormal);
        }

        public override void MoveRight(Player pPlayer)
        {

        }

        public override void MoveLeft(Player pPlayer)
        {
            Debug.Assert(pPlayer != null);
            pPlayer.x -= Constant.PLAYER_SPEED - 1;
            this.Handle(pPlayer);
        }
    }
}
