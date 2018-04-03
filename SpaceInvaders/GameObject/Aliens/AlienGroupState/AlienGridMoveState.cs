using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienGridMoveState
    {
        public enum MoveState
        {
            GridMoveToRight,
            GridHitRightWall,
            GridMoveToLeft,
            GridHitLeftWall
        }

        // state()
        public abstract void Handle(AlienGroup pGrid);

        // strategy()
        public abstract void Execute(AlienGroup pGrid);

    }
}
