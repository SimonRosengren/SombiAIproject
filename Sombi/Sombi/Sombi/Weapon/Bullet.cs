using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Bullet : Projectile
    {
        public Bullet(Vector2 pos,float speed,float angle,int damage, int range, int ID) : base(pos, speed, angle, damage, range, ID)   
        {

        }

        public override void Update(GameTime gameTime)
        {
            pos += velocity * speed;
             distanceTraveled = Vector2.Distance(Pos,startPos);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureLibrary.bulletBlue, pos, null, Color.White, angle, new Vector2(TextureLibrary.bulletBlue.Width / 2, TextureLibrary.bulletBlue.Height / 2), 1f, SpriteEffects.None, 0f);
            
        }

        public override void Explode()
        {
            
        }

        public override Rectangle GetHitBox()
        {
            Rectangle hb = new Rectangle((int)pos.X, (int)pos.Y, 4, 4);
            return hb;
        }
    }
}
