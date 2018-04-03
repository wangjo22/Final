using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionObject
    {
        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        public CollisionObject(ProxySprite proxySprite)
        {
            Debug.Assert(proxySprite != null);

            GameSprite pSprite = proxySprite.pSprite;
            Debug.Assert(pSprite != null);

            this.pColRect = new CollisionRect(pSprite.GetScreenRect());
            Debug.Assert(this.pColRect != null);

            this.pBoxSprite = BoxSpriteManager.Add(BoxSprite.Name.CollisionBox, this.pColRect.x, this.pColRect.y, this.pColRect.width, this.pColRect.height);
            Debug.Assert(this.pBoxSprite != null);
            this.pBoxSprite.SetColor(1.0f, 1.0f, 1.0f);
        }
        public void UpdatePos(float x, float y)
        {
            this.pColRect.x = x;
            this.pColRect.y = y;
            this.pBoxSprite.x = x;
            this.pBoxSprite.y = y;
            this.pBoxSprite.SetRect(x, y, this.pColRect.width, this.pColRect.height);
            this.pBoxSprite.Update();
        }

        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        public BoxSprite pBoxSprite;
        public CollisionRect pColRect;
    }
}
