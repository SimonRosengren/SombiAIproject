using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Explosives : Weapon
    {
        public Explosives() : base()
        {
            projectileSpeed = 15.0f;
            weaponRange = 610;
            damage = 21;
            fireRate = 1.0f;
            areaOfEffect = 1f;
            numberOfProjectilesPerFire = 1; 
            projectileSpread = 1;
        }

        
        protected override void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        
        public override void FireWeapon(Vector2 position, float angle)
        {
            throw new NotImplementedException();
        }
    }
}
