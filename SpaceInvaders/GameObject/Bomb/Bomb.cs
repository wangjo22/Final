using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Bomb : BombCategory
    {
        public Bomb(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
        
            switch (gameSpriteName)
            {
                case GameSprite.Name.BombZigZag:
                    this.pFallStrategy = new FallZigZag();
                    break;
                case GameSprite.Name.BombCross:
                    this.pFallStrategy = new FallCross();
                    break;
                case GameSprite.Name.BombStraight:
                    this.pFallStrategy = new FallStraight();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            this.isDead = true;
            this.pFallStrategy.Reset(this.y);
        }


        ~Bomb()
        {

        }
        
        public void SetStrategy(FallStrategy pStrategy)
        {
            Debug.Assert(pStrategy != null);
            this.pFallStrategy = pStrategy;
        }

        public FallStrategy GetCurrStrategy()
        {
            Debug.Assert(this.pFallStrategy != null);
            return this.pFallStrategy;
        }

        
        public override void Update()
        {
            base.Update();
            this.y -= this.dropSpeed;
            this.pFallStrategy.BombFall(this);
        }

            //if(PlayerManager.GetPlayerDeadOrAlive())
            //{
            //    this.y -= this.dropSpeed;
            //}

    public void ResetPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.pFallStrategy.Reset(this.y);
        }
        public override void Accept(CollisionVisitor other)
        {
            other.VisitBomb(this);
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

            // The bomb and shield will be dealt with by RemoveObserver...
        }

        private FallStrategy pFallStrategy;
    }


}
