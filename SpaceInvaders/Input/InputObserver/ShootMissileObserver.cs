using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShootMissileObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Shoot Observer");
            Player pPlayer = PlayerManager.GetPlayer();
            pPlayer.ShootMissile();


        }
    }
}
