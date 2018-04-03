using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerGroup : Composite
    {
        public PlayerGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pBoxSprite.SetColor(0, 1, 0);
        }

        ~PlayerGroup()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitPlayerGroup(this);
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
