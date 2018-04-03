using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldColumn : Composite
    {
        public ShieldColumn(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, int _Index, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pBoxSprite.SetColor(1.0f, 0.0f, 0.0f);
            this.index = _Index;
        }

        ~ShieldColumn()
        {

        }

        public override void Move(float _x, float _y)
        {
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldColumn(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }

        public override void VisitMissileGroup(MissileGroup mg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(mg, pGameObj);
        }

        public override void VisitMissile(Missile m)
        {
           
        }

        public override void VisitAlienColumn(AlienColumn ac)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(ac, pGameObj);
        }

        public override void VisitBombGroup(BombGroup bg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(bg, pGameObj);
        }
    }
    
}
