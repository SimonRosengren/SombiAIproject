using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sombi
{
    class FPSManager
    {
        int totalFrames;
        float elapsedTime;
        int fps; 
        public FPSManager()
        {
            totalFrames = 0;
            elapsedTime = 0;
            fps = 0;
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= 1000.0f)
            {
                fps = totalFrames;
                totalFrames = 0;
                elapsedTime = 0;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            totalFrames++;
            spriteBatch.DrawString(TextureLibrary.HudText, "FPS: " + fps, new Vector2(450, 0), Color.Black);
        }
    }
}
