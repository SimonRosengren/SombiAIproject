using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    abstract class Weapon
    {
        public float fireRate;
        public int damage;
        public float projectileSpeed;
        public int weaponRange;
        public float areaOfEffect;
        public float projectileSpread;
        public int numberOfProjectilesPerFire;
        

        public Weapon()
        {
            
        }
        public Weapon(int level)
        {

        }

        protected abstract void Update(GameTime gameTime);


        public abstract void FireWeapon(Vector2 position, float angle); //player calls method to fire bullets
          
    }
}
