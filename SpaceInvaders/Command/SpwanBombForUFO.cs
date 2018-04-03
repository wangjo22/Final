using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpawnBombForUFO : Command
    {
        public SpawnBombForUFO(Composite pGridHead)
        {
            // initialized the sprite animation is attached to
            Debug.Assert(pGridHead != null);
            this.pGridHead = pGridHead;
            this.pRandom = new Random();
        }

        public override void Execute(float deltaTime)
        {
            int bombStyle = pRandom.Next(0, 3);

            GameObject pUFO = (GameObject)this.pGridHead.poHead;

            Bomb pBomb;
            if (pUFO.isDead == false)
            {
                switch (bombStyle)
                {
                    case 0:
                        pBomb = BombManager.GetZigZagFall(pUFO.x, pUFO.y);
                        break;
                    case 1:
                        pBomb = BombManager.GetStraightFall(pUFO.x, pUFO.y);
                        break;
                    case 2:
                        pBomb = BombManager.GetCrossFall(pUFO.x, pUFO.y);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            float nextDeltaTime = pRandom.Next(2, 4);
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
