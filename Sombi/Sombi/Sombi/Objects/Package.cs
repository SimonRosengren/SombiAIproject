using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Package : GameObject
    {
        Vector2 packagePos;
        public Rectangle hitBox;
        public bool taken;
        public Package(Vector2 packagePos)
            : base(packagePos)
        {
            this.packagePos = packagePos;
            hitBox = new Rectangle((int)packagePos.X, (int)packagePos.Y, GlobalValues.TILE_SIZE, GlobalValues.TILE_SIZE);
            taken = false;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureLibrary.moneyPackTex, packagePos, Color.White);
        }
    }
}
