using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallGroup : Composite
    {

        public WallGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObject.pBoxSprite.SetColor(0.0f, 1.0f, 0.0f, 1.0f);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallGroup(this);
        }
        public override void VisitAlienGroup(AlienGroup ag)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(ag, pGameObj);
        }

        public override void VisitMissile(Missile m)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(m, pGameObj);
        }


        public override void VisitBombGroup(BombGroup bg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(bg, pGameObj);
        }

        public override void VisitPlayerGroup(PlayerGroup pg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(pg, pGameObj);
        }


        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }

        public override void Move(float _x, float _y)
        {

        }


    }
}
