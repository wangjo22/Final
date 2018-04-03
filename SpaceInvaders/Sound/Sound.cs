using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  No "new" happens constructor Texture() 
    //      Initializes texture to "HotPink" - static texture.
    //
    //  Managers - create a pool of them...
    //  Add - Takes one and reuses it by using Set() 
    //        Actually creates a new texture and replaces the HotPink
    //  Design - side effect
    //      If you recycle multiple textures it's going to garabage collect old texture
    //      Will not do this the first recycle - since texture is pointing to the static HotPink
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class Sound_Link : DLink
    {
    }
    public class Sound: Sound_Link
    {
        //----------------------------------------------------------------------
        // Enum
        //----------------------------------------------------------------------
        public enum Name
        {
            AlienMove1,
            AlienMove2, // HotPink Texture
            AlienMove3,
            AlienMove4,
            PlayerShoot,
            PlayerExplode,
            AlienExplode,
            UFO_High,
            UFO_Low,
            Uninitialized
        }

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------
        public Sound()
            :base()
        {
            this.sound = null;
            this.name = Sound.Name.Uninitialized;
        }

        //----------------------------------------------------------------------
        // Destructor
        //----------------------------------------------------------------------
        ~Sound()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Texture():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.sound = null;
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Set(Name name, String soundFileName, IrrKlang.ISoundEngine soundEngine)
        {
            Debug.Assert(soundFileName != null);
            Debug.Assert(soundEngine != null);

            this.name = name;
            this.sound = soundEngine.AddSoundSourceFromFile(soundFileName);
            Debug.Assert(this.sound != null);
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

        public void SetName(Sound.Name newName)
        {
            this.name = newName;
        }

        public Sound.Name GetName()
        {
            return this.name;
        }

        public IrrKlang.ISoundSource GetSoundSource()
        {
            return this.sound;
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 

        }
        


        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private Name name;
        private IrrKlang.ISoundSource sound;
    }
}
