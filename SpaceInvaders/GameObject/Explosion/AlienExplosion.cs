using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienExplosion : ExplosionCategory
    {
        public AlienExplosion(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pProxySprite.pSprite.SwapColor(1.0f, 1.0f, 1.3f);
        }

        ~AlienExplosion()
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
