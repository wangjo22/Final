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
    abstract public class GameSprite_Link : SpriteBase
    { }
    public class GameSprite : GameSprite_Link
    {
        //--------------------------------------------------------------------
        // Enum
        //--------------------------------------------------------------------
        public enum Name
        {
            Octopus,
            Crab,
            Squid,
            Alien_Explosion,

            UFO,
            UFO_Explosion,

            Missile,
            Missile_Explosion,

            Player,
            Player_Explosion,

            BombStraight,
            BombZigZag,
            BombCross,
            Bomb_Explosion,

            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            

            NullObject,
            Uninitialized
        }

        //--------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------

        public GameSprite()
            : base()
        {
            this.name = GameSprite.Name.Uninitialized;

            // Use the default - it will be replaced in the Set
            this.pImage = ImageManager.Find(Image.Name.Default);
            Debug.Assert(this.pImage != null);

            this.poRect = new Azul.Rect();
            Debug.Assert(this.poRect != null);
            this.poRect.Clear();

            Debug.Assert(GameSprite.psTmpColor != null);
            GameSprite.psTmpColor.Set(1, 1, 1);

            // here is the actual new
            this.poSprite = new Azul.Sprite(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poRect, psTmpColor);
            Debug.Assert(this.poSprite != null);
            this.poColor = new Azul.Color(1.0f, 1.0f, 1.0f);
            Debug.Assert(this.poColor != null);

            this.x = poSprite.x;
            this.y = poSprite.y;
            this.sx = poSprite.sx;
            this.sy = poSprite.sy;
            this.angle = poSprite.angle;
        }

        //--------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------
        ~GameSprite()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~GameSprite():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.pImage = null;
            this.poColor = null;
            this.poSprite = null;
        }

        //--------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------
        public void Set(GameSprite.Name name, Image pImage, float x, float y, float width, float height, Azul.Color pColor)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(this.poRect != null);
            Debug.Assert(this.poSprite != null);

            this.poRect.Set(x, y, width, height);
            this.pImage = pImage;
            this.name = name;

            if (pColor == null)
            {
                Debug.Assert(GameSprite.psTmpColor != null);
                GameSprite.psTmpColor.Set(1, 1, 1);

                this.poColor.Set(psTmpColor);
            }
            else
            {
                this.poColor.Set(pColor);
            }

            this.poSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poRect, this.poColor);

            this.x = poSprite.x;
            this.y = poSprite.y;
            this.sx = poSprite.sx;
            this.sy = poSprite.sy;
            this.angle = poSprite.angle;
        }

        public new void Clear()
        {
            // NOTE:
            // Do not NULL the poAzulSprite it is created once in Default then reused
            // Do not NULL the poColor it is created once in Default then reused

            this.pImage = null;
            this.poColor.Set(1.0f, 1.0f, 1.0f);
            this.name = GameSprite.Name.Uninitialized;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }
        public override void Update()
        {
            this.poSprite.x = this.x;
            this.poSprite.y = this.y;
            this.poSprite.sx = this.sx;
            this.poSprite.sy = this.sy;
            this.poSprite.angle = this.angle;
            this.poSprite.Update();
        }
        public void SwapColor(Azul.Color _pColor)
        {
            Debug.Assert(_pColor != null);
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poSprite != null);
            this.poColor.Set(_pColor);
            this.poSprite.SwapColor(_pColor);
        }

        public void SwapColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poSprite != null);
            this.poColor.Set(red, green, blue, alpha);
            this.poSprite.SwapColor(this.poColor);
        }

        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(this.poSprite != null);
            Debug.Assert(pNewImage != null);
            this.pImage = pNewImage;

            this.poSprite.SwapTexture(this.pImage.GetAzulTexture());
            this.poSprite.SwapTextureRect(this.pImage.GetAzulRect());
        }

        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.poRect != null);
            return this.poRect;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Image: {0} ({1})", this.pImage.GetName(), this.pImage.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poSprite.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);


            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        public override void Render()
        {
            this.poSprite.Render();
        }

        public void SetColor(float _red, float _green, float _blue)
        {
            this.poSprite.SwapColor(new Azul.Color(_red, _green, _blue));
        }

        public void SetName(GameSprite.Name newName)
        {
            this.name = newName;
        }

        public GameSprite.Name GetName()
        {
            return this.name;
        }

        //--------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------
        private Name name;
        private Image pImage;
        private Azul.Color poColor;
        private Azul.Sprite poSprite;
        private Azul.Rect poRect;

        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        //---------------------------------------------------------------------------------------------------------
        // Static Data - prevent unecessary "new" in the above methods
        //---------------------------------------------------------------------------------------------------------
        //static private Azul.Rect psTmpRect = new Azul.Rect();
        static private Azul.Color psTmpColor = new Azul.Color(1, 1, 1);
    }
}
