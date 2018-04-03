using System;
using System.Diagnostics;

namespace SpaceInvaders
{   
    //--------------------------------------------------------------------
    // Design Notes:
    //
    //  Only "new" happens in the default constructor Image()
    //
    //  Managers - create a pool of them...
    //  Add - Takes one and reuses it by using Set() 
    //
    //--------------------------------------------------------------------
    abstract public class Image_Link : DLink
    { }

    public class Image: Image_Link
    {
        //----------------------------------------------------------------
        // Enum
        //----------------------------------------------------------------
        public enum Name
        {
            Octopus0,
            Octopus1,
            Crab0,
            Crab1,
            Squid0,
            Squid1,
           
            Alien_Explosion2,

            BombStraight,
            BombZigZag,
            BombCross,
            Bomb_Explosion,

            UFO,
            UFO_Explosion,

            Player,
            Player_Explosion1,
            Player_Explosion2,

            Missile,
            Missile_Explosion,

            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            NullObject,
            Default,    // Hot pink default image
            Uninitialized
        }

        //----------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------
        public Image()
            : base()
        {
            this.poRect = new Azul.Rect();
            Debug.Assert(this.poRect != null);
            this.Clear();
        }

        //----------------------------------------------------------------------
        // Destructor
        //----------------------------------------------------------------------
        ~Image()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Image():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.poRect = null;
        }
        //----------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------
        public void Set(Name name, Texture pTexture, float x, float y, float width, float height)
        {
            this.name = name;

            Debug.Assert(pTexture != null);
            this.pTexture = pTexture;

            this.poRect.Set(x, y, width, height);
        }

        public new void Clear()
        {
            this.pTexture = null;
            this.name = Name.Uninitialized;
            this.poRect.Clear();
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }
        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.poRect != null);
            return this.poRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }
        
        public void SetName(Image.Name newName)
        {
            this.name = newName;
        }

        public Image.Name GetName()
        {
            return this.name;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            if (this.pTexture != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.pTexture.GetName());
            }
            else
            {
                Debug.WriteLine("   Texture: null ");
            }


            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        //-----------------------------
        // Data
        //-----------------------------
        private Name name;
        private Texture pTexture;
        private Azul.Rect poRect;
    }
}
