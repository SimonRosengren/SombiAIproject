using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Rifle : Weapon
    {
        public Rifle() : base()
        {
            projectileSpeed = 15.0f;
            weaponRange = 290;
            damage = 19;
            fireRate = 0.55f;
            numberOfProjectilesPerFire = 1;
            projectileSpread = 40;         
        }
        public Rifle(int level) : base()
        {
            SetVariables(level);
        }
        public void SetVariables(int level)
        {
            switch (level)
            {
                case 1:
                    projectileSpeed = 15.0f;
                    weaponRange = 290;
                    damage = 19;
                    fireRate = 0.55f;
                    numberOfProjectilesPerFire = 1;
                    projectileSpread = 1000;
                    break;
                case 2:
                    projectileSpeed = 15.0f;
                    weaponRange = 310;
                    damage = 24;
                    fireRate = 0.5f;
                    numberOfProjectilesPerFire = 1;
                    projectileSpread = 40;
                    break;
                case 3:
                    projectileSpeed = 15.0f;
                    weaponRange = 330;
                    damage = 29;
                    fireRate = 0.45f;
                    numberOfProjectilesPerFire = 1;
                    projectileSpread = 40;
                    break;
                case 4:
                    projectileSpeed = 15.0f;
                    weaponRange = 350;
                    damage = 34;
                    fireRate = 0.4f;
                    numberOfProjectilesPerFire = 1;
                    projectileSpread = 40;
                    break;
                case 5:
                    projectileSpeed = 15.0f;
                    weaponRange = 370;
                    damage = 39;
                    fireRate = 0.35f;
                    numberOfProjectilesPerFire = 1;
                    projectileSpread = 40;
                    break;
                case 6:
                    projectileSpeed = 15.0f;
                    weaponRange = 390;
                    damage = 44;
                    fireRate = 0.30f;
                    numberOfProjectilesPerFire = 1;
                    projectileSpread = 40;
                    break;
                default:
                    break;
            }
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
