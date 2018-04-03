using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Only "new" happens in the default constructor Sprite()
    //
    //  Managers - create a pool of them...
    //  Add - Takes one and reuses it by using Set() 
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class ProxySprite_Link : SpriteBase
    { }
    public class ProxySprite : ProxySprite_Link
    {
        //--------------------------------------------------------------------
        // Enum
        //--------------------------------------------------------------------
        public enum Name
        {
            Proxy,
            Uninitialized
        }

        //--------------------------------------------------------------------
        // Constructors
        //--------------------------------------------------------------------
        public ProxySprite()
            : base()
        {
            this.name = Name.Uninitialized;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = null;
        }

        public ProxySprite(GameSprite.Name name)
        {
            this.name = Name.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = GameSpriteManager.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        //--------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------
        ~ProxySprite()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~ProxySprite():{0} ", this.GetHashCode());
#endif
            this.name = ProxySprite.Name.Uninitialized;
            this.pSprite = null;
        }

        //--------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------
        public void Set(GameSprite.Name name)
        {
            this.name = Name.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = GameSpriteManager.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        public new void Clear()
        {
            this.name = Name.Uninitialized;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pSprite = null;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }
        public override void Update()
        {
            this.PrivPushToReal();
            this.pSprite.Update();
        }

        private void PrivPushToReal()
        {
            Debug.Assert(this.pSprite != null);
            this.pSprite.x = this.x;
            this.pSprite.y = this.y;
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            //Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            //Debug.WriteLine("             Image: {0} ({1})", this.pImage.GetName(), this.pImage.GetHashCode());
            //Debug.WriteLine("        AzulSprite: ({0})", this.poSprite.GetHashCode());
            //Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            //Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            //Debug.WriteLine("           (angle): {0}", this.angle);


            //if (this.pNext == null)
            //{
            //    Debug.WriteLine("              next: null");
            //}
            //else
            //{
            //    GameSprite pTmp = (GameSprite)this.pNext;
            //    Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}

            //if (this.pPrev == null)
            //{
            //    Debug.WriteLine("              prev: null");
            //}
            //else
            //{
            //    GameSprite pTmp = (GameSprite)this.pPrev;
            //    Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}
        }

        public override void Render()
        {
            this.PrivPushToReal();
            this.pSprite.Update();
            this.pSprite.Render();
        }

        public void SetName(ProxySprite.Name newName)
        {
            this.name = newName;
        }

        public ProxySprite.Name GetName()
        {
            return this.name;
        }

        //--------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------
        public ProxySprite.Name name;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public GameSprite pSprite;
        //public BoxSprite pBoxSprite;
    }
}
