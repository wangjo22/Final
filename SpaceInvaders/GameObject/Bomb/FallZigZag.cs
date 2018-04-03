using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FallZigZag : FallStrategy
    {
        public FallZigZag()
        {
            this.prevY = 0.0f;
        }
        
        ~FallZigZag()
        {

        }

        public override void BombFall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);

            float distance = this.prevY - pBomb.GetY();
            if(distance > 50)
            {
                pBomb.SetScaleX(-1.0f);
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
