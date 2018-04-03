using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FallCross : FallStrategy
    {
        public FallCross()
        {
            this.prevY = 0.0f;
        }

        ~FallCross()
        {

        }

        public override void BombFall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);

            float distance = this.prevY - pBomb.GetY();
            if (distance > 50)
            {
                pBomb.SetScaleY(-1.0f);
                this.prevY = pBomb.GetY();
            }
        }
        
        public override void Reset(float posY)
        {
            this.prevY = posY;
        }

        private float prevY;
    }
}
