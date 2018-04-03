using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienGridMoveToRight : AlienGridMoveState
    {
        public AlienGridMoveToRight()
        {
            this.pIter = new ReverseIterator();
        }

        public override void Handle(AlienGroup pGrid)
        {
            throw new NotImplementedException();
        }

        public override void Execute(AlienGroup pGrid)
        {
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
                pComponent.Move(Constant.ALIEN_SIDE_STEP, 0);
                pComponent = this.pIter.Next();
            }
            pGrid.isInWall = false;

        }
        private ReverseIterator pIter;
    }
}
