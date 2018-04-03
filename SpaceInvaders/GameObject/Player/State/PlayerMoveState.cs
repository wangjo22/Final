using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class PlayerMoveState
    {
        // state()
        public abstract void Handle(Player pPlayer);

        // strategy()
        public abstract void MoveRight(Player pPlayer);
        public abstract void MoveLeft(Player pPlayer);
        //public abstract void ShootMissile(Player pPlayer);

    }
}
