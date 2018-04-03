using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallRight : WallCategory
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public WallRight(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY, float width, float height)
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
        ~WallRight()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~WallRight():{0}", this.GetHashCode());
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
            other.VisitWallRight(this);
        }

        public override void VisitAlienGroup(AlienGroup ag)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            Debug.Assert(pColPair != null);
            pColPair.SetObserverSubject(ag, this);
            pColPair.NotifyListeners();
        }

        public override void VisitPlayerGroup(PlayerGroup pPG)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(pPG);
            CollisionPair.Collide(this, pGameObj);
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
