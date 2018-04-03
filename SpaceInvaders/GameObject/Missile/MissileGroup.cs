using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileGroup : Composite
    {
        public MissileGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pBoxSprite.SetColor(0.0f, 1.0f, 0.0f);
        }

        ~MissileGroup()
        {

        }

        public override void Move(float _x, float _y)
        {
            this.x += _x;
            this.y += _y;
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissileGroup(this);
        }

        public override void VisitBombGroup(BombGroup bg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(bg);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitBomb(Bomb b)
        {
            Debug.Assert(b != null);
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(b, this);
            pColPair.NotifyListeners();
        }


        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }
    }
}
