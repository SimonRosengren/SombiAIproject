using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Shotgun : Weapon
    {
        public Shotgun() : base()
        {
            projectileSpeed = 15f;
            weaponRange = 100;
            damage = 10;
            fireRate = 1;
            numberOfProjectilesPerFire = 7;
            projectileSpread = 40; //grader på konen
        }
        public Shotgun(int level) : base()
        {
            SetVariables(level);
        }
        protected override void Update(GameTime gameTime)
        {

        }
        public void SetVariables(int level)
        {
            switch (level)
            {
                case 1:
                    projectileSpeed = 15f;
                    weaponRange = 100;
                    damage = 10;
                    fireRate = 1;
                    numberOfProjectilesPerFire = 3;
                    projectileSpread = 40;
                    break;
                case 2:
                    projectileSpeed = 15f;
                    weaponRange = 125;
                    damage = 12;
                    fireRate = 1;
                    numberOfProjectilesPerFire = 3;
                    projectileSpread = 40;
                    break;
                case 3:
                    projectileSpeed = 15f;
                    weaponRange = 150;
                    damage = 15;
                    fireRate = 1;
                    numberOfProjectilesPerFire = 5;
                    projectileSpread = 40;
                    break;
                case 4:
                    projectileSpeed = 15f;
                    weaponRange = 175;
                    damage = 18;
                    fireRate = 0.75f;
                    numberOfProjectilesPerFire = 5;
                    projectileSpread = 40;
                    break;
                case 5:
                    projectileSpeed = 15f;
                    weaponRange = 200;
                    damage = 22;
                    fireRate = 1;
                    numberOfProjectilesPerFire = 5;
                    projectileSpread = 40;
                    break;
                case 6:
                    projectileSpeed = 15f;
                    weaponRange = 225;
                    damage = 25;
                    fireRate = 0.5f;
                    numberOfProjectilesPerFire = 7;
                    projectileSpread = 40;
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
        private void SpreadBullets()
        {

        }
        public override void FireWeapon(Vector2 position, float angle)
        {
            throw new NotImplementedException();
        }
    }
}
