using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class PlayerShootState
    {
        public abstract void Handle(Player pPlayer);

        public abstract void ShootMissile(Player pPlayer);

    }
}
