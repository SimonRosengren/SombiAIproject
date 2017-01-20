using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    public class SoundLibrary
    {
        public static Song menuSong;
        public static SoundEffect shotGunFire;
        public static SoundEffect rifleFire;
        public static SoundEffect explosiveFire;
        public static SoundEffect explosion;
        public static SoundEffectInstance explosionInstance;

        public static void LoadContent(ContentManager Content)
        {
            menuSong = Content.Load<Song>("Neuro_Rhythm");
            MediaPlayer.Play(menuSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.4f;

            shotGunFire = Content.Load<SoundEffect>("Shotgun1");
            SoundEffect.MasterVolume = 0.5f;

            rifleFire = Content.Load<SoundEffect>(@"LMG1");
            SoundEffect.MasterVolume = 0.5f;

            explosiveFire = Content.Load<SoundEffect>("FireRocket");
            SoundEffect.MasterVolume = 0.5f;

            explosion = Content.Load<SoundEffect>("Explosion");
            SoundEffect.MasterVolume = 0.5f;
        }
        public static SoundEffectInstance Explosion
        {
            get
            {
                if (explosionInstance == null)
                {
                    explosionInstance = explosion.CreateInstance();
                }
                return explosionInstance;
            }
        }
        public static void PlaySound(SoundEffectInstance sound)
        {

            if (sound.State == SoundState.Stopped)
            {
                sound.Play();
            }

        }
        public static void StopSound(SoundEffectInstance sound)
        {
            if (sound.State == SoundState.Playing)
            {
                sound.Stop();
            }
        }
    }
}
