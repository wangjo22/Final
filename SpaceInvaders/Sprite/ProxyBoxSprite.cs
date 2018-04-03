//using System;
//using System.Diagnostics;

//namespace SpaceInvaders
//{
//    //---------------------------------------------------------------------------------------------------------
//    // Design Notes:
//    //
//    //  Only "new" happens in the default constructor Sprite()
//    //
//    //  Managers - create a pool of them...
//    //  Add - Takes one and reuses it by using Set() 
//    //
//    //---------------------------------------------------------------------------------------------------------
//    abstract public class ProxyBoxSprite_Link : SpriteBase
//    { }
//    public class ProxyBoxSprite : ProxySprite_Link
//    {
//        //--------------------------------------------------------------------
//        // Enum
//        //--------------------------------------------------------------------
//        public enum Name
//        {
//            ProxyBox,
//            Uninitialized
//        }

//        //--------------------------------------------------------------------
//        // Constructors
//        //--------------------------------------------------------------------
//        public ProxyBoxSprite()
//            : base()
//        {
//            this.name = Name.Uninitialized;
//            this.x = 0.0f;
//            this.y = 0.0f;
//            this.pBoxSprite = null;
//        }

//        public ProxyBoxSprite(BoxSprite.Name name)
//        {
//            this.name = Name.ProxyBox;
//            this.x = 0.0f;
//            this.y = 0.0f;
//            this.pBoxSprite = BoxSpriteManager.Find(name);
//            SpriteBatch pBatch = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);
//            //pBatch.Attach(this.pBoxSprite.GetName());
//            Debug.Assert(this.pBoxSprite != null);
//        }

//        //--------------------------------------------------------------------
//        // Destructor
//        //--------------------------------------------------------------------
//        ~ProxyBoxSprite()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~ProxyBoxSprite():{0} ", this.GetHashCode());
//#endif
//            this.name = ProxyBoxSprite.Name.Uninitialized;
//            this.pBoxSprite = null;
//        }

//        //--------------------------------------------------------------------
//        // Methods
//        //--------------------------------------------------------------------
//        public void Set(BoxSprite.Name name)
//        {
//            this.name = Name.ProxyBox;
//            this.x = 0.0f;
//            this.y = 0.0f;
//            this.pBoxSprite = BoxSpriteManager.Find(name);
//            Debug.Assert(this.pBoxSprite != null);
//        }

//        public new void Clear()
//        {
//            this.name = Name.Uninitialized;
//            this.x = 0.0f;
//            this.y = 0.0f;
//            this.pBoxSprite = null;
//        }

//        public void Wash()
//        {
//            base.Clear();
//            this.Clear();
//        }
//        public override void Update()
//        {
//            this.PrivPushToReal();
//            this.pBoxSprite.Update();
//        }

//        private void PrivPushToReal()
//        {
//            Debug.Assert(this.pBoxSprite != null);
//            this.pBoxSprite.x = this.x;
//            this.pBoxSprite.y = this.y;
//        }

//        public void Dump()
//        {

//            // Dump - Print contents to the debug output window
//            //        Using HASH code as its unique identifier 
//            //Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
//            //Debug.WriteLine("             Image: {0} ({1})", this.pImage.GetName(), this.pImage.GetHashCode());
//            //Debug.WriteLine("        AzulSprite: ({0})", this.poSprite.GetHashCode());
//            //Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
//            //Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
//            //Debug.WriteLine("           (angle): {0}", this.angle);


//            //if (this.pNext == null)
//            //{
//            //    Debug.WriteLine("              next: null");
//            //}
//            //else
//            //{
//            //    GameSprite pTmp = (GameSprite)this.pNext;
//            //    Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
//            //}

//            //if (this.pPrev == null)
//            //{
//            //    Debug.WriteLine("              prev: null");
//            //}
//            //else
//            //{
//            //    GameSprite pTmp = (GameSprite)this.pPrev;
//            //    Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
//            //}
//        }

//        public override void Render()
//        {
//            this.PrivPushToReal();
//            this.pBoxSprite.Update();
//            this.pBoxSprite.Render();
//        }

//        public void SetName(ProxyBoxSprite.Name newName)
//        {
//            this.name = newName;
//        }

//        public ProxyBoxSprite.Name GetName()
//        {
//            return this.name;
//        }

//        //--------------------------------------------------------------------
//        // Data
//        //--------------------------------------------------------------------
//        public ProxyBoxSprite.Name name;
//        public float x;
//        public float y;
//        public BoxSprite pBoxSprite;
//    }
//}
