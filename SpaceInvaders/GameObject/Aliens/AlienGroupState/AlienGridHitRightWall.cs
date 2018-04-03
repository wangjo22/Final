using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienGridHitRightWall : AlienGridMoveState
    {
        
        public AlienGridHitRightWall()
        {
            this.pIter = new ReverseIterator();
        }

        public override void Handle(AlienGroup pGrid)
        {
            Debug.Assert(pGrid != null);
            pGrid.ChangeState(MoveState.GridMoveToLeft);
        }

        public override void Execute(AlienGroup pGrid)
        {
            Debug.Assert(pGrid != null);

            this.pIter.TakeHierachy(pGrid);
            Component pComponent = this.pIter.First();
            if (pComponent == null)
            {
                return;
                // TODO
                // Player killed all aliens. Should start second stage.
            }
            // TODO
            // UPDATE ITERATOR -> IT SHOULDN"T CHECK PCOMPONENT != THIS
            while (this.pIter.IsDone() || pComponent != pGrid)
            {
                pComponent.Move(0, -Constant.ALIEN_VERTICAL_STEP);
                pComponent = this.pIter.Next();
            }
            this.Handle(pGrid);
        }
        private ReverseIterator pIter;
    }
}
