using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpawnBomb : Command
    {
        public SpawnBomb(Composite pGridHead)
        {
            // initialized the sprite animation is attached to
            Debug.Assert(pGridHead != null);
            this.pGridHead = pGridHead;
            this.pRandom = new Random();
        }

        public override void Execute(float deltaTime)
        {
            int bombStyle = pRandom.Next(0, 3);
            int numCol = 0;

            AlienColumn pColumn = (AlienColumn)this.pGridHead.poHead;
            while (pColumn != null)
            {
                numCol++;
                pColumn = (AlienColumn)pColumn.pNext;
            }
            int col = pRandom.Next(0, numCol);
            pColumn = (AlienColumn)this.pGridHead.poHead;
            while (col > 0)
            {
                col--;
                pColumn = (AlienColumn)pColumn.pNext;
            }

            if(pColumn != null)
            {
                GameObject pAlien = (GameObject)pColumn.poLast;
                Bomb pBomb;
                switch (bombStyle)
                {
                    case 0:
                        pBomb = BombManager.GetZigZagFall(pAlien.x, pAlien.y);
                        break;
                    case 1:
                        pBomb = BombManager.GetStraightFall(pAlien.x, pAlien.y);
                        break;
                    case 2:
                        pBomb = BombManager.GetCrossFall(pAlien.x, pAlien.y);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }
     
            float nextDeltaTime = pRandom.Next(5, 20);
            nextDeltaTime /= 10;
            TimerManager.Add(TimeEvent.Name.SpawnBomb, this, nextDeltaTime);
        }


        public void Dump()
        {
            Debug.WriteLine("MovementSprite.Dump()... No Idea what to show now...");
        }
        // Data: ---------------
        private Composite pGridHead;
        private Random pRandom;
    }
}
