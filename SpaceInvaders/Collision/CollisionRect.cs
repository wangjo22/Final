using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionRect : Azul.Rect
    {
        public CollisionRect(Azul.Rect pRect)
            : base(pRect)
        { }

        public CollisionRect(float x, float y, float width = Constant.ALIEN_WIDTH, float height = Constant.ALIEN_HEIGHT)
            : base(x, y, width, height)
        { }

        public CollisionRect(CollisionRect pRect)
            : base(pRect)
        { }

        public CollisionRect()
            : base()
        { }

        public void SetRect(CollisionRect pRect)
        {
            base.Set(pRect);
        }

        public void SetRect(float x, float y, float width = Constant.ALIEN_WIDTH, float height = Constant.ALIEN_HEIGHT)
        {
            base.Set(x, y, width, height);
        }

        static public bool Intersect(CollisionRect ColRectA, CollisionRect ColRectB)
        {
            float A_minX = ColRectA.x - ColRectA.width / 2;
            float A_minY = ColRectA.y - ColRectA.height / 2;
            float A_maxX = ColRectA.x + ColRectA.width / 2;
            float A_maxY = ColRectA.y + ColRectA.height / 2;

            float B_minX = ColRectB.x - ColRectB.width / 2;
            float B_minY = ColRectB.y - ColRectB.height / 2;
            float B_maxX = ColRectB.x + ColRectB.width / 2;
            float B_maxY = ColRectB.y + ColRectB.height / 2;

            bool isIntersected;
            if(A_minX > B_maxX || A_maxX < B_minX || A_minY > B_maxY || A_maxY < B_minY)
            {
                isIntersected = false;
            }
            else
            {
                isIntersected = true;
            }
            return isIntersected;
        }



        public void Union(CollisionRect ColRect)
        {
            float minX;
            float minY;
            float maxX;
            float maxY;


            if ((this.x - this.width / 2) < (ColRect.x - ColRect.width / 2))
            {
                minX = (this.x - this.width / 2);
            }
            else
            {
                minX = (ColRect.x - ColRect.width / 2);
            }

            if ((this.x + this.width / 2) > (ColRect.x + ColRect.width / 2))
            {
                maxX = (this.x + this.width / 2);
            }
            else
            {
                maxX = (ColRect.x + ColRect.width / 2);
            }

            if ((this.y + this.height / 2) > (ColRect.y + ColRect.height / 2))
            {
                maxY = (this.y + this.height / 2);
            }
            else
            {
                maxY = (ColRect.y + ColRect.height / 2);
            }

            if ((this.y - this.height / 2) < (ColRect.y - ColRect.height / 2))
            {
                minY = (this.y - this.height / 2);
            }
            else
            {
                minY = (ColRect.y - ColRect.height / 2);
            }

            this.width = (maxX - minX);
            this.height = (maxY - minY);
            this.x = minX + this.width / 2;
            this.y = minY + this.height / 2;
        }
    }

    
}
