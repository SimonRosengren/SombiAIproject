using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Animation
    {
        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; } 
        }

        public int frameWidth; 

        public int frameHeight 
        {
            get { return texture.Height; } 
        }

        float frameTime; 
        public float FrameTime 
        {
            get { return frameTime; }
        }

        public int frameCount;

        bool isLooping; 
        public bool IsLooping
        {
            get { return isLooping; }
        }

        public Animation(Texture2D newTexture, int newFrameWidth, float newFrameTime, bool newIsLooping)
        {
           
            texture = newTexture;
            frameWidth = newFrameWidth;
            frameTime = newFrameTime;
            isLooping = newIsLooping;
            frameCount = texture.Width / frameWidth;

        }
    }
}
