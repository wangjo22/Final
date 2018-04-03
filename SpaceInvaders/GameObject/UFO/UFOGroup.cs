using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOGroup : Composite
    {
        public UFOGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pBoxSprite.SetColor(0.0f, 1.0f, 0.0f);
        }

        ~UFOGroup()
        {

        }

        public override void Move(float _x, float _y)
        {
            this.x += _x;
            this.y += _y;
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitUFOGroup(this);
        }

        public override void VisitWallGroup(WallGroup wg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(wg);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitWallRight(WallRight wr)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(this, wr);
            pColPair.NotifyListeners();
        }

        public override void VisitWallLeft(WallLeft wl)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(this, wl);
            pColPair.NotifyListeners();
        }

        public override void VisitMissileGroup(MissileGroup mg)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(this, mg);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }
    }
}
