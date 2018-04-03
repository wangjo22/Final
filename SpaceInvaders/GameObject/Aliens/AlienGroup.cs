using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGroup : Composite
    {
        public AlienGroup(GameObject.Name gameObjectname, GameSprite.Name gameSpriteName, int _Index, float posX, float posY)
            : base(gameObjectname, gameSpriteName)
        {
            this.x = posX;
            this.y = posY;
            this.pIter = new ReverseIterator();
            this.pColObject.pBoxSprite.SetColor(0.0f, 1.0f, 0.0f, 1.0f);
            this.isInWall = false;
            this.index = _Index;
            this.pCurrState = null;
            this.pMoveToRight = new AlienGridMoveToRight();
            this.pHitRightWall = new AlienGridHitRightWall();
            this.pMoveToLeft = new AlienGridMoveToLeft();
            this.pHitLeftWall = new AlienGridHitLeftWall();
            this.pCurrState = this.pMoveToRight;
        }               
        public void ResetState()
        {
            this.ChangeState(AlienGridMoveState.MoveState.GridMoveToRight);
            this.isInWall = false;
        }
        public override void Accept(CollisionVisitor other)
        {
            other.VisitAlienGroup(this);
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

        public override void VisitWallGroup(WallGroup wg)
        {
            GameObject pGameObj = Iterator.GetChildGameObject(this);
            CollisionPair.Collide(wg, pGameObj);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox();
            base.Update();
        }

        public override void Move(float _x, float _y)
        {
            this.pCurrState.Execute(this);
        }

        public void ChangeState(AlienGridMoveState.MoveState newState)
        {
            switch(newState)
            {
                case AlienGridMoveState.MoveState.GridMoveToRight:
                    this.pCurrState = this.pMoveToRight;
                    break;
                case AlienGridMoveState.MoveState.GridHitRightWall:
                    this.pCurrState = this.pHitRightWall;
                    break;
                case AlienGridMoveState.MoveState.GridMoveToLeft:
                    this.pCurrState = this.pMoveToLeft;
                    break;
                case AlienGridMoveState.MoveState.GridHitLeftWall:
                    this.pCurrState = this.pHitLeftWall;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private ReverseIterator pIter;
        public bool isInWall;
        private AlienGridMoveState pCurrState;
        private AlienGridMoveToRight pMoveToRight;
        private AlienGridHitRightWall pHitRightWall;
        private AlienGridMoveToLeft pMoveToLeft;
        private AlienGridHitLeftWall pHitLeftWall;
    }
}
