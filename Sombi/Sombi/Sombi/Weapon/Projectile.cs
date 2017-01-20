using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    abstract class Projectile : MovingObject
    {
        public Vector2 startPos;
        public float distanceTraveled;
        public float angle;
        public float timeToLiveAfterImpact;
        public int damage;
        public int range;
        public bool exploding;
        public Vector2 velocity;
        public int ID;
        public Vector2 Pos
        {
            get
            {
                return pos;

            }
            set { }
        }
        
        public Projectile(Vector2 pos, float speed, float angle, int damage, int range, int ID) : base (pos, speed)
        {
            this.startPos = pos;
            this.angle = angle;
            this.damage = damage;
            timeToLiveAfterImpact = 0;
            this.ID = ID;
            velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            this.range = range;
            startPos = pos;
        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        public abstract Rectangle GetHitBox();

           
        public abstract void Explode();
    }
}
