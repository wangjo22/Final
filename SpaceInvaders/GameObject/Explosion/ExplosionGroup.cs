using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ExplosionGroup : Composite
    {
        public ExplosionGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pBoxSprite.SetColor(0, 1, 0);
        }

        ~ExplosionGroup()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            Debug.Assert(false);
        }



        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }
    }
}
