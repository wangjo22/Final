using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallTop : WallCategory
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public WallTop(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY, float width, float height)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pColObject.pColRect.Set(posX, posY, width, height);
            this.pColObject.pBoxSprite.SetColor(1.0f, 1.0f, 1.0f);
        }


        //--------------------------------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------------------------------
        ~WallTop()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~WallTop():{0}", this.GetHashCode());
#endif
        }


        //--------------------------------------------------------------------------------------------
        // Override method
        //--------------------------------------------------------------------------------------------
        public override void Move(float _x, float _y)
        {

        }


        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallTop(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(m, this);
            pColPair.NotifyListeners();
        }



 
        override public void Update()
        {
            base.Update();
        }



        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        //private static float speedX = Constant.ALIEN_SIDE_STEP;
        //private float speedX = 3.0f;
    }
}
