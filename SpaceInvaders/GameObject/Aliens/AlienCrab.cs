using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienCrab : AlienCategory
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public AlienCrab(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, int _Index, float posX, float posY)
            : base(gameObjectname, gameSpriteName, _Index, posX, posY)
        {
            this.x = posX;
            this.y = posY;
        }


        //--------------------------------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------------------------------
        ~AlienCrab()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~AlienCrab():{0}", this.GetHashCode());
#endif
        }


        //--------------------------------------------------------------------------------------------
        // Override method
        //--------------------------------------------------------------------------------------------
        public override void Move(float _x, float _y)
        {
            this.x += _x;
            this.y += _y;

        }


        public override void Accept(CollisionVisitor other)
        {
            other.VisitAlienCrab(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
           // Debug.WriteLine("\t\tCollide : {0} <-> {1}", m.name, this.name);
            GameObject pGameObj = Iterator.GetChildGameObject(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            ScoreManager.AddScoreToPlayer1(20);
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(m, this);
            pColPair.NotifyListeners();
        }
        public override void VisitPlayerGroup(PlayerGroup pPG)
        {
         //   Debug.WriteLine("\t\tCollide : {0} <-> {1}", pPG.name, this.name);
            GameObject pGameObj = Iterator.GetChildGameObject(pPG);
            CollisionPair.Collide(pGameObj, this);
        }


        public override void VisitPlayer(Player p)
        {
            ExplosionManager.GetPlayerExplosion(p);
            PlayerManager.PlayerDead();
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
