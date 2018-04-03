using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienColumn : Composite
    {
        public AlienColumn(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, int _Index, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.size = 0.0f;
            this.pColObject.pBoxSprite.SetColor(1.0f, 0.0f, 0.0f, 0.0f);
            this.index = _Index;
        }

        public override void Move(float _x, float _y)
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitAlienColumn(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitPlayerGroup(PlayerGroup pPG)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(pPG, pGameObj);
        }

        public override void VisitShieldGroup(ShieldGroup sg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(sg);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitShieldColumn(ShieldColumn sc)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(sc);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitShieldBrick(ShieldBrick sb)
        {
            Debug.Assert(sb != null);
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(sb, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }

        public float size;
    }

    
}
