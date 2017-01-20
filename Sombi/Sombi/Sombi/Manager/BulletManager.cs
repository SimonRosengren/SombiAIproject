using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class BulletManager
    {
        public List<Projectile> bullets;
        public BulletManager()
        {
            bullets = new List<Projectile>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Projectile b in bullets)
            {
                b.Update(gameTime);
            }
            

            RemoveBullets();
            RemoveExplosions();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile b in bullets)
            {
                b.Draw(spriteBatch);
            }
            
        }

        public void AddBullets(Projectile proj)
        {
            bullets.Add(proj);
        }

        private void RemoveBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (!GlobalValues.windowBounds.Contains(bullets[i].Pos) || !Grid.grid[(int)bullets[i].Pos.X / 50, (int)bullets[i].Pos.Y / 50].passable || bullets[i].distanceTraveled > bullets[i].range && !bullets[i].exploding)
                {
                    bullets[i].Explode();
                    if (bullets[i].timeToLiveAfterImpact == 0)
                    {
                        bullets.Remove(bullets[i]);
                    }                    
                }

            }
        }
        private void RemoveExplosions()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].timeToLiveAfterImpact < 0)
                {
                    bullets.RemoveAt(i);
                }
            }
        }
        
    }
}
