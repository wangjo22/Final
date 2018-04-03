using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : MissileCategory
    {
        public Missile(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.isFlying = false;
        }

        public override void Update()
        {
            base.Update();

            if(PlayerManager.GetPlayerDeadOrAlive())
            {
                this.y += 10.0f;
            }
        }

        ~Missile()
        {

        }


        public void Hit()
        {
           // this.isHit = true;
            this.x = -10;
            isDead = true;
            this.SetFlyingStatus(false);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissile(this);
        }


        public bool GetFlyingStatus()
        {
            return this.isFlying;
        }

        public bool SetFlyingStatus(bool status)
        {
            return this.isFlying = status;
        }

        public override void VisitShieldBrick(ShieldBrick sb)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(sb, this);
            pColPair.NotifyListeners();

            // Shield will be deactivated by removeObserver
        }

        public void DeactiveMissile()
        {
            this.x = -100;
        }


        //public bool isHit;
        private bool isFlying;
    }

    
}
