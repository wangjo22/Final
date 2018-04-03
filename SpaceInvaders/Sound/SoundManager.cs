using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundManager
    {
        public enum Name
        {
            AlienMove1,
            AlienMove2, 
            AlienMove3,
            AlienMove4,
            PlayerShoot,
            PlayerExplode,
            AlienExplode,
            UFO_High,
            UFO_Low,
            Uninitialized
        }
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private SoundManager()
        {
            this.poSoundEngine = new IrrKlang.ISoundEngine();
            this.poSoundEngine.SoundVolume = 0.04f;
            this.poAlienMove1 = this.poSoundEngine.AddSoundSourceFromFile("fastinvader1.wav");
            this.poAlienMove2 = this.poSoundEngine.AddSoundSourceFromFile("fastinvader2.wav");
            this.poAlienMove3 = this.poSoundEngine.AddSoundSourceFromFile("fastinvader3.wav");
            this.poAlienMove4 = this.poSoundEngine.AddSoundSourceFromFile("fastinvader4.wav");
            this.poPlayerShoot = this.poSoundEngine.AddSoundSourceFromFile("shoot.wav");
            this.poPlayerExplode = this.poSoundEngine.AddSoundSourceFromFile("explosion.wav");
            this.poAlienExplode = this.poSoundEngine.AddSoundSourceFromFile("invaderkilled.wav");
            this.poUFO_High = this.poSoundEngine.AddSoundSourceFromFile("ufo_highpitch.wav");
            this.poUFO_Low = this.poSoundEngine.AddSoundSourceFromFile("ufo_lowpitch.wav");


            this.poAlienMove1.DefaultVolume = 0.7f;
            this.poAlienMove2.DefaultVolume = 0.7f;
            this.poAlienMove3.DefaultVolume = 0.7f;
            this.poAlienMove4.DefaultVolume = 0.7f;
            this.poPlayerShoot.DefaultVolume = 0.7f;
            this.poPlayerExplode.DefaultVolume = 0.7f;
            this.poAlienExplode.DefaultVolume = 0.7f;
            this.poUFO_High.DefaultVolume = 0.7f;
            this.poUFO_Low.DefaultVolume = 0.7f;
        }

        //---------------------------------------------------------------------------------
        // Destructor
        //---------------------------------------------------------------------------------
        ~SoundManager()
        {

        }

        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void Create()
        {
            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new SoundManager();
            }
        }


        public static void Destroy()
        {

        }


        public static void Play(Sound.Name nameToPlay)
        {
            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.PrivPlaySound(nameToPlay);
        }

        private void PrivPlaySound(Sound.Name nameToPlay)
        {
            switch(nameToPlay)
            {
                case Sound.Name.AlienMove1:
                    this.poSoundEngine.Play2D(this.poAlienMove1, false, false, false);
                    break;
                case Sound.Name.AlienMove2:
                    this.poSoundEngine.Play2D(this.poAlienMove2, false, false, false);
                    break;
                case Sound.Name.AlienMove3:
                    this.poSoundEngine.Play2D(this.poAlienMove3, false, false, false);
                    break;
                case Sound.Name.AlienMove4:
                    this.poSoundEngine.Play2D(this.poAlienMove4, false, false, false);
                    break;
                case Sound.Name.PlayerShoot:
                    this.poSoundEngine.Play2D(this.poPlayerShoot, false, false, false);
                    break;
                case Sound.Name.PlayerExplode:
                    this.poSoundEngine.Play2D(this.poPlayerExplode, false, false, false);
                    break;
                case Sound.Name.AlienExplode:
                    this.poSoundEngine.Play2D(this.poAlienExplode, false, false, false);
                    break;
                case Sound.Name.UFO_High:
                    this.poSoundEngine.Play2D(this.poUFO_High, false, false, false);
                    break;
                case Sound.Name.UFO_Low:
                    this.poSoundEngine.Play2D(this.poUFO_Low, false, false, false);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
        


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static SoundManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static SoundManager pInstance = null;
        private IrrKlang.ISoundEngine poSoundEngine;
        private IrrKlang.ISoundSource poAlienMove1;
        private IrrKlang.ISoundSource poAlienMove2;
        private IrrKlang.ISoundSource poAlienMove3;
        private IrrKlang.ISoundSource poAlienMove4;
        private IrrKlang.ISoundSource poPlayerShoot;
        private IrrKlang.ISoundSource poPlayerExplode;
        private IrrKlang.ISoundSource poAlienExplode;
        private IrrKlang.ISoundSource poUFO_High;
        private IrrKlang.ISoundSource poUFO_Low;

    }
}
