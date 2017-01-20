using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class BloodStain
    {
        int bloodNr;
        Vector2 pos;

        public BloodStain(Vector2 pos)
        {
            this.bloodNr = GlobalValues.rnd.Next(0, 10);
            this.pos = pos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureLibrary.bloodStain[bloodNr], pos, null, Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            

        }
    }
}
