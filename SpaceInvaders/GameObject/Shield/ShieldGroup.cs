using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldGroup : Composite
    {
        public ShieldGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, int _Index, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pBoxSprite.SetColor(0.0f, 1.0f, 0.0f);
            this.index = _Index;
        }

        ~ShieldGroup()
        {

        }

        public override void Move(float _x, float _y)
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup mg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(mg, pGameObj);
        }

        public override void VisitAlienGroup(AlienGroup ag)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(ag);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitAlienColumn(AlienColumn ac)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(this, ac);
        }

        public override void VisitBombGroup(BombGroup bg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(bg);
            CollisionPair.Collide(this, pGameObj);
        }

        public override void VisitBomb(Bomb b)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(b, pGameObj);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }
    }
}
