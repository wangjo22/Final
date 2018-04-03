using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBrick : ShieldCategory
    {
        public ShieldBrick(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, int _Index, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.index = _Index;
        }

        public override void Update()
        {
            base.Update();
        }

        ~ShieldBrick()
        {

        }




        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldBrick(this);
        }

        public override void VisitBombGroup(BombGroup bg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(bg);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(m);
            CollisionPair.Collide(this, pGameObj);
        }


    }
}
