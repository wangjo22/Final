using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //-------------------------------------------------------------------------
    // Design Notes:
    //
    //  Only "new" happens in the default constructor SpriteBox()
    //      Owns - poAzulSpriteBox, poLineColor
    //
    //  Managers - create a pool of them...
    //  Add - Takes one and reuses it by using Set() 
    //
    //-------------------------------------------------------------------------
    abstract public class BoxSprite_Link : SpriteBase
    { }
    public class BoxSprite: BoxSprite_Link
    {
        //--------------------------------------------------------------------
        // Enum
        //--------------------------------------------------------------------
        public enum Name
        {
            CollisionBox,
            Uninitialized
        }

        //--------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------
        public BoxSprite()
            : base()
        {
            this.name = BoxSprite.Name.Uninitialized;

            Debug.Assert(BoxSprite.psTmpRect != null);
            Debug.Assert(BoxSprite.psTmpColor != null);
            BoxSprite.psTmpRect.Set(0.0f, 0.0f, 1.0f, 1.0f);
            BoxSprite.psTmpColor.Set(1.0f, 1.0f, 1.0f);

            // Here is the actual new
            this.poBoxSprite = new Azul.SpriteBox(BoxSprite.psTmpRect, BoxSprite.psTmpColor);
            this.poColor = new Azul.Color(1.0f, 1.0f, 1.0f, 1.0f);
            Debug.Assert(this.poBoxSprite != null);
            Debug.Assert(this.poColor != null);


            this.x = this.poBoxSprite.x;
            this.y = this.poBoxSprite.y;
            this.sx = this.poBoxSprite.sx;
            this.sy = this.poBoxSprite.sy;
            this.angle = this.poBoxSprite.angle;
        }

        //--------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------
        ~BoxSprite()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~BoxSprite():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.poColor = null;
            this.poBoxSprite = null;
        }
        //--------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------
        public void Set(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pColor)
        {
            Debug.Assert(this.poBoxSprite != null);
            Debug.Assert(this.poColor != null);
            Debug.Assert(BoxSprite.psTmpRect != null);
            BoxSprite.psTmpRect.Set(x, y, width, height);

            this.name = name;

            if (pColor == null)
            {
                this.poColor.Set(1.0f, 1.0f, 1.0f);
            }
            else
            {
                this.poColor.Set(pColor);
            }

            this.poBoxSprite.Swap(BoxSprite.psTmpRect, this.poColor);
            Debug.Assert(poBoxSprite != null);

            this.x = poBoxSprite.x;
            this.y = poBoxSprite.y;
            this.sx = poBoxSprite.sx;
            this.sy = poBoxSprite.sy;
            this.angle = poBoxSprite.angle;
        }

        public void Set(BoxSprite.Name name, float x, float y, float width, float height)
        {
            Debug.Assert(this.poBoxSprite != null);
            Debug.Assert(this.poColor != null);
            Debug.Assert(BoxSprite.psTmpRect != null);
            BoxSprite.psTmpRect.Set(x, y, width, height);

            this.name = name;

            this.poBoxSprite.Swap(BoxSprite.psTmpRect, this.poColor);
            Debug.Assert(poBoxSprite != null);

            this.x = poBoxSprite.x;
            this.y = poBoxSprite.y;
            this.sx = poBoxSprite.sx;
            this.sy = poBoxSprite.sy;
            this.angle = poBoxSprite.angle;
        }
        public void Set(BoxSprite.Name boxName, Azul.Rect pRect)
        {
            Debug.Assert(pRect != null);
            this.name = boxName;
            this.poBoxSprite.SwapScreenRect(pRect);
            this.x = poBoxSprite.x;
            this.y = poBoxSprite.y;
            this.sx = poBoxSprite.sx;
            this.sy = poBoxSprite.sy;
            this.angle = poBoxSprite.angle;
        }

        public new void Clear()
        {
            // NOTE:
            // Do not null the poAzulSpriteBox it is created once in Default then reused
            // Do not null the poLineColor it is created once in Default then reused
            this.name = BoxSprite.Name.Uninitialized;
            this.poColor.Set(1.0f, 1.0f, 1.0f);
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }
        
        public void SwapColor(Azul.Color pColor)
        {
            Debug.Assert(pColor != null);
            Debug.Assert(this.poColor != null);
            this.poBoxSprite.SwapColor(pColor);
        }

        
        public void Wash()
        {
            base.Clear();
            this.Clear();
        }
        public override void Update()
        {
            this.poBoxSprite.x = this.x;
            this.poBoxSprite.y = this.y;
            this.poBoxSprite.sx = this.sx;
            this.poBoxSprite.sy = this.sy;
            this.poBoxSprite.angle = this.angle;
            this.poBoxSprite.Update();
        }

        public void SetName(BoxSprite.Name newName)
        {
            this.name = newName;
        }

        public BoxSprite.Name GetName()
        {
            return this.name;
        }
        public void Dump()
        {
            //// Dump - Print contents to the debug output window
            ////        Using HASH code as its unique identifier 
            //Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            //Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            //if (this.poTexture != null)
            //{
            //    Debug.WriteLine("   Texture: {0} ", this.pTexture.name);
            //}
            //else
            //{
            //    Debug.WriteLine("   Texture: null ");
            //}


            //if (this.pNext == null)
            //{
            //    Debug.WriteLine("      next: null");
            //}
            //else
            //{
            //    Image pTmp = (Image)this.pNext;
            //    Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}

            //if (this.pPrev == null)
            //{
            //    Debug.WriteLine("      prev: null");
            //}
            //else
            //{
            //    Image pTmp = (Image)this.pPrev;
            //    Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}
        }

        public override void Render()
        {
            this.poBoxSprite.Render();
        }

        public void SetColor(float _red, float _green, float _blue, float _alpha = 1.0f)
        {
            this.poColor.Set(_red, _green, _blue, _alpha);
            this.poBoxSprite.SwapColor(this.poColor);
        }

        public void SetRect(float x, float y, float width, float height)
        {
            this.Set(this.name, x, y, width, height);
        }


        //--------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------
        private Name name;
        private Azul.SpriteBox poBoxSprite;
        private Azul.Color poColor;

        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        //---------------------------------------------------------------------------------------------------------
        // Static Data - prevent unecessary "new" in the above methods
        //---------------------------------------------------------------------------------------------------------
        static private Azul.Rect psTmpRect = new Azul.Rect();
        static private Azul.Color psTmpColor = new Azul.Color(1, 1, 1);
    }
}
