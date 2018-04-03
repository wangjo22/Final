using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BombGroup : Composite
    {
        public BombGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pBoxSprite.SetColor(0.0f, 1.0f, 0.0f);
        }

        ~BombGroup()
        {

        }

        public override void Move(float _x, float _y)
        {
            this.x += _x;
            this.y += _y;
        }

        public override void Accept(CollisionVisitor other)
        {
            if(this.poHead != null)
            {
                other.VisitBombGroup(this);
            }
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }

        public override void VisitPlayerGroup(PlayerGroup pPG)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(pGameObj, pPG);
        }
    }
}
