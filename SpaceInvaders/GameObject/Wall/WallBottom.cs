using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallBottom : WallCategory
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public WallBottom(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY, float width, float height)
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
        ~WallBottom()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~WallBottom():{0}", this.GetHashCode());
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
            other.VisitWallBottom(this);
        }
        public override void VisitBombGroup(BombGroup bg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(bg);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitAlienGroup(AlienGroup ag)
        {
            GameScene.Player1Dead();
        }


        public override void VisitBomb(Bomb b)
        {
            Debug.Assert(b != null);
            //BombManager.DeactiveBomb(b);
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(b, this);
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
