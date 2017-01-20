using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    struct AnimationPlayer
    {
        Animation animation;

        public Animation Animation
        {
            get { return animation; }
        }

        int frameIndex;
        public int FrameIndex
        {
            get { return frameIndex; }
            set { frameIndex = value; }
        }

        private float timer;

        public Vector2 Origin
        {
            get { return new Vector2(animation.frameWidth / 2, animation.frameHeight / 2); }
        }

        public float rotation;
        public void PlayAnimation(Animation newAnimation)
        {
            if (animation == newAnimation)
                return;

            animation = newAnimation;
            frameIndex = 0;
            timer = 0;

        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer >= animation.FrameTime)
            {
                timer -= animation.FrameTime;

                if (animation.IsLooping) // Loopar den så sätt frameIndex
                    frameIndex = (frameIndex + 1) % animation.frameCount;
                else frameIndex = Math.Min((frameIndex + 1), (animation.frameCount - 1));
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation)
        {         
            // rectangle är source rectangle som tar från din spritesheet.
            if (animation != null)
            {
                Rectangle rectangle = new Rectangle(frameIndex * animation.frameWidth, 0, animation.frameWidth, animation.frameHeight);
                
                spriteBatch.Draw(animation.Texture, position, rectangle, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
