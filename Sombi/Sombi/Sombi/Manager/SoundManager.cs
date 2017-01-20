using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace Sombi
{
    public class SoundManager
    {
        //SoundLibrary soundLibrary = new SoundLibrary();

        public static SoundEffectInstance shotGunFireInstance;
        public static SoundEffectInstance rifleFireInstance;
        public static SoundEffectInstance explosiveFireInstance;
        
        //public static Song song { get; private set; }

        //public static void LoadContent(ContentManager content)
        //{
        //    song = content.Load<Song>("BackgroundMusic");
        //    destroyed = content.Load<SoundEffect>("rock_impact_3");
        //}

        //public static void Playsound(int soundNbr, ContentManager content)
        //{
        //    LoadContent(content);
        //    if (soundNbr == 1)
        //    {
        //        MediaPlayer.Play(song);
        //        MediaPlayer.IsRepeating = true;
        //        MediaPlayer.Volume = 0.3f;
        //    }
        //    if (soundNbr == 2)
        //    {
        //        destroyed.Play();
        //    }
        //}

        public static void PlaySound(SoundEffectInstance sound)
        {
            if (sound != null)
            {
                if (sound.State == SoundState.Stopped)
                {
                    sound.Play();
                }
            }
        }
        public static void StopSound(SoundEffectInstance sound)
        {
            if (sound.State == SoundState.Playing)
            {
                sound.Stop();
            }
        }
        
        //public SoundEffectInstance ShotGunFire
        //{
        //    get
        //    {
        //        if (SoundLibrary.shotGunFireInstance == null)
        //        {
        //            SoundLibrary.shotGunFireInstance = SoundLibrary.shotGunFire.CreateInstance();
        //        }
        //        return SoundLibrary.shotGunFireInstance;
        //    }
        //}
        public static SoundEffectInstance RifleFire
        {
            get
            {
                if (rifleFireInstance == null)
                {
                    rifleFireInstance = SoundLibrary.rifleFire.CreateInstance();
                }
                return rifleFireInstance;
            }
        }
        //public SoundEffectInstance ExplosiveFire
        //{
        //    get
        //    {
        //        if (SoundLibrary.explosiveFireInstance == null)
        //        {
        //            SoundLibrary.explosiveFireInstance = SoundLibrary.explosiveFire.CreateInstance();
        //        }
        //        return SoundLibrary.explosiveFireInstance;
        //    }
        //}        
    }
}
