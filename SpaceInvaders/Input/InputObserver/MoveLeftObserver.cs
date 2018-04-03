﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveLeftObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Move Left");
            Player pPlayer = PlayerManager.GetPlayer();
            pPlayer.MoveLeft();
        }
    }
}
