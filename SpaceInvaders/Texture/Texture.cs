using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Texture_Link : DLink
    {
    }
    public class Texture: Texture_Link
    {
        //----------------------------------------------------------------------
        // Enum
        //----------------------------------------------------------------------
        public enum Name
        {
            Aliens,
            Shield,
            Consolas36pt,
            Default, // HotPink Texture
            NullObject,
            Uninitialized
        }

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------
        public Texture()
            :base()
        {
            Debug.Assert(Texture.psDefaultAzulTexture != null);

            this.poTexture = psDefaultAzulTexture;
            Debug.Assert(this.poTexture != null);

            this.name = Name.Default;
        }

        //----------------------------------------------------------------------
        // Destructor
        //----------------------------------------------------------------------
        ~Texture()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Texture():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.poTexture = null;
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Set(Name name, string pTextureName)
        {
            // Copy the data over
            this.name = name;

            Debug.Assert(pTextureName != null);
            Debug.Assert(this.poTexture != null);

            //  Here is a Texture Swap
            //
            //  Replace the existing texture
            //     Manage Language is doing some work here....
            //     Since we are replacing the "HotPink" texture, its removing its reference
            //     A new allocation is replacing the old "HotPink"
            //     Now the old "HotPink" is marked for garabage collection....but its a static (yeah) no GC.
            //     But if it Set() is called on a User defined texture multiple times... GC is envoked
            //
            //  Not super happy... but this only happens in setup.

            this.poTexture = new Azul.Texture(pTextureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            Debug.Assert(this.poTexture != null);
        }

        public new void Clear()
        {
            // NOTE:
            // Do not clear the poTexture it is created once in Default then replaced in Set
            this.name = Name.Uninitialized;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }
        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.poTexture != null);
            return this.poTexture;
        }

        public void SetName(Texture.Name newName)
        {
            this.name = newName;
        }

        public Texture.Name GetName()
        {
            return this.name;
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            if (this.poTexture != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.poTexture.GetHashCode());
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
                Texture pTmp = (Texture)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Texture pTmp = (Texture)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
        


        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private Name name;
        private Azul.Texture poTexture;

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        static private Azul.Texture psDefaultAzulTexture = new Azul.Texture("HotPink.tga", Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
    }
}
