using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveUFOObserver : CollisionObservers
    {
        public RemoveUFOObserver()
        {
            this.pRandom = new Random();
        }

        public RemoveUFOObserver(RemoveUFOObserver m)
        {
            Debug.Assert(m != null);
        }

        ~RemoveUFOObserver()
        {
        }

        public override void Notify()
        {
            if ( this.IsValidCollision() )
            {
                if(this.isMissileHit == true)
                {
                    ExplosionManager.GetUFOExplosion(this.pUFO);
                }
                UFOManager.DeactiveUFO();

                float time = pRandom.Next(10, 16);
                SpawnUFO pSpawn = new SpawnUFO();
                TimerManager.Add(TimeEvent.Name.SpawnBomb, pSpawn, time);
            }
        }
        
        public override bool IsValidCollision()
        {
            bool isValid = false;
            this.isMissileHit = false;

            if (this.pSubject.pObjA is UFOGroup && this.pSubject.pObjB is WallRight)
            {
                isValid = true;
            } 
            else if(this.pSubject.pObjA is UFOGroup && this.pSubject.pObjB is WallLeft)
            {
                isValid = true;
            }
            else if (this.pSubject.pObjA is UFOGroup && this.pSubject.pObjB is MissileGroup)
            {
                ScoreManager.AddScoreToPlayer1(200);
                this.isMissileHit = true;
                isValid = true;
            }
            this.pUFO = Iterator.GetChildGameObject(this.pSubject.pObjA);
            return isValid;
        }

        private GameObject pUFO;
        private bool isMissileHit;
        private Random pRandom;
    }
}
