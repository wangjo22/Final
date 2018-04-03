using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveAlienObserver : CollisionObservers
    {
        public RemoveAlienObserver()
        {
        }

        public RemoveAlienObserver(RemoveAlienObserver m)
        {
            Debug.Assert(m != null);
            this.pAlien = m.pAlien;
        }

        ~RemoveAlienObserver()
        {

        }

        public override void Notify()
        {
            if ( this.IsValidCollision() )
            {
                if (this.pAlien.isDead == false)
                {   
                    SoundManager.Play(Sound.Name.AlienExplode);
                    this.pAlien.isDead = true;
                    ExplosionManager.GetAlienExplosion(this.pAlien);
                    DelayRemoveManager.Attach(new RemoveAlienObserver(this));
                }
            }
        }
        
        public override bool IsValidCollision()
        {
            bool isValid = false;
            if (this.pSubject.pObjA is Missile && this.pSubject.pObjB is AlienCategory)
            {
                this.pAlien = (AlienCategory)this.pSubject.pObjB;
                isValid = true;
            }
            return isValid;
        }

        public override void Execute()
        {
            AlienManager.Add(this.pAlien);
            this.pAlien.GotHitMissile();
        }

        public AlienCategory pAlien;
    }
}
