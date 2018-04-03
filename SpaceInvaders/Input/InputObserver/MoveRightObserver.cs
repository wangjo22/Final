using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveRightObserver : InputObserver
    {
        public override void Notify()
        {
            // Debug.WriteLine("Move Right");
            Player pPlayer = PlayerManager.GetPlayer();
            pPlayer.MoveRight();
        }
    }
}
