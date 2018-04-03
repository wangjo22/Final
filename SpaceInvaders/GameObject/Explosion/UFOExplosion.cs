using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOExplosion : ExplosionCategory
    {
        public UFOExplosion(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pProxySprite.pSprite.SetColor(1.0f, 0.0f, 0.0f);
        }

        ~UFOExplosion()
        {

        }



        override public void Update()
        {
            base.Update();
        }


        public override void Accept(CollisionVisitor other)
        {
            Debug.Assert(false);
        }
    }
}
