using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Player : PlayerCategory
    {
        public Player(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pProxySprite.pSprite.SwapColor(0.7f, 0.0f, 0.3f);
        }

        ~Player()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitPlayer(this);
        }

        override public void Update()
        {
            base.Update();
        }

        public void MoveRight()
        {
            this.pMoveState.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.pMoveState.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.pShootState.ShootMissile(this);
        }

        public void SetMoveState(PlayerManager.MoveState pState)
        {
            this.pMoveState = PlayerManager.GetMoveState(pState);
        }

        public void SetShootingState(PlayerManager.ShootState pState)
        {
            this.pShootState = PlayerManager.GetShootState(pState);
        }


        public override void VisitBumperLeft(BumperLeft wl)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(wl, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBumperRight(BumperRight wr)
        {
            CollisionPair pColPair = CollisionPairManager.GetCurrentCollisionPair();
            pColPair.SetObserverSubject(wr, this);
            pColPair.NotifyListeners();
        }

        private PlayerMoveState pMoveState;
        private PlayerShootState pShootState;
    }
}
