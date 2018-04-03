using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MovementSprite : Command
    {
        public MovementSprite(Composite pGridHead)
        {
            // initialized the sprite animation is attached to
            Debug.Assert(pGridHead != null);
            this.pGridHead = pGridHead;
            this.soundIdx = 0;
        }

        public override void Execute(float deltaTime)
        {
            Debug.Assert(this.pGridHead != null);
            if(PlayerManager.GetPlayerDeadOrAlive())
            {
                switch (soundIdx)
                {
                    case 0:
                        SoundManager.Play(Sound.Name.AlienMove1);
                        break;
                    case 1:
                        SoundManager.Play(Sound.Name.AlienMove2);
                        break;
                    case 2:
                        SoundManager.Play(Sound.Name.AlienMove3);
                        break;
                    case 3:
                        SoundManager.Play(Sound.Name.AlienMove4);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
                soundIdx++;
                if (soundIdx > 3)
                {
                    soundIdx = 0;
                }
                this.pGridHead.MoveAllAliens();
            }
            AlienManager.CheckDeltaTime();
            TimerManager.Add(TimeEvent.Name.MovementSprite, this, TimerManager.GetAlienDeltatTime());
        }


        public void Dump()
        {
            Debug.WriteLine("MovementSprite.Dump()... No Idea what to show now...");
        }
        // Data: ---------------
        private Composite pGridHead;
        private int soundIdx;
    }
}
