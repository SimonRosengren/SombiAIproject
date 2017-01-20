using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    abstract class MovingObject : GameObject
    {
        protected float speed;
        public MovingObject(Vector2 pos, float speed) : base(pos)
        {
            this.speed = speed;
        }
    }
}
