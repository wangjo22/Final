using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundObserver : CollisionObservers
    {
        public SoundObserver()
        {
            
        }

        public override void Notify()
        {
            SoundManager.Play(Sound.Name.AlienMove4);
        }

    }
}
