using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sombi
{
    class PackageMarker
    {
        Vector2 startPos, endPos;
        
        public PackageMarker(Vector2 playerPos, Vector2 packagePos)
        {
            this.startPos = playerPos;
            this.endPos = packagePos;
        }

        public void setNewEndPos(Vector2 newPos)
        {

        }
        public void Update(Vector2 playerPos, Vector2 endPos)
        {
            startPos = playerPos;
            this.endPos = endPos;

        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 pos = startPos;
            for (int i = 0; i < 4; i++)
            {
                pos += Vector2.Normalize(endPos - startPos) * 50;
                sb.Draw(TextureLibrary.guideLineTex, pos, Color.White * 0.7f);               
            }
        }
    }
}
