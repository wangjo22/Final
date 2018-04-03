using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFO: UFOCategory
    {
        public UFO(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pProxySprite.pSprite.SetColor(1.0f, 0.0f, 0.0f);
            this.speed = 5.0f;
        }

        public override void Update()
        {
            base.Update();
            if(PlayerManager.GetPlayerDeadOrAlive())
            {
                this.x += this.speed;
            }
        }

        public void UpdateOnlySprite()
        {
            base.Update();
        }

        public void SetPos(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
        }

        ~UFO()
        {

        }

        public void SetMoveToRight()
        {
            this.speed = 2.0f;
        }

        public void SetMoveToLeft()
        {
            this.speed = -2.0f;
        }
        public override void Accept(CollisionVisitor other)
        {
            other.VisitUFO(this);
        }

        private float speed;
    }

    
}
