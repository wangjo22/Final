using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGridObserver : CollisionObservers
    {
        public AlienGridObserver()
        {
        }

        ~AlienGridObserver()
        {

        }

        public override void Notify()
        {
            // It is very important to know the data type of pObjA and pObjB
            AlienGroup pAlien = (AlienGroup)this.pSubject.pObjA;
            GameObject pWall = (GameObject)this.pSubject.pObjB;

            if (pWall.GetName() == GameObject.Name.WallRight)
            {
                if (!pAlien.isInWall)
                {
                    pAlien.isInWall = true;
                    pAlien.ChangeState(AlienGridMoveState.MoveState.GridHitRightWall);
                }
            }
            else if (pWall.GetName() == GameObject.Name.WallLeft)
            {
                if (!pAlien.isInWall)
                {
                    pAlien.isInWall = true;
                    pAlien.ChangeState(AlienGridMoveState.MoveState.GridHitLeftWall);
                }
            }
        }
    }
}
