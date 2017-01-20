using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class WeaponManager
    {
        public BulletManager bulletManager;
        public Weapon playerOneWeapon;
        public Weapon playerTwoWeapon;
        private float timeSinceLastPlayerOneBullet;
        private float timeSinceLastPlayerTwoBullet;   

        public WeaponManager()
        {
            this.bulletManager = new BulletManager();
            playerOneWeapon = new Explosives();
            playerTwoWeapon = new Explosives();
            timeSinceLastPlayerOneBullet = 100f;
            timeSinceLastPlayerTwoBullet = 100f;
        }


        public void Update(GameTime gameTime)
        {
            bulletManager.Update(gameTime);
            timeSinceLastPlayerOneBullet += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastPlayerTwoBullet += (float)gameTime.ElapsedGameTime.TotalSeconds;
            /*  SwitchWeapon(1);
              SwitchWeapon(2);*/
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            bulletManager.Draw(spriteBatch);
        }

        public void CreateBullets(int PlayerID, Vector2 position, float angle, int explosivesLevel)
        {
            if (PlayerID == 1)
            {
                if (timeSinceLastPlayerOneBullet > playerOneWeapon.fireRate)
                {
                    Bullet b = new Bullet(position, playerOneWeapon.projectileSpeed, angle, playerOneWeapon.damage, playerOneWeapon.weaponRange, PlayerID);
                    Rocket r = new Rocket(position, playerOneWeapon.projectileSpeed, angle, playerOneWeapon.damage, playerOneWeapon.weaponRange, PlayerID, explosivesLevel);

                    if (playerOneWeapon is Rifle)
                    {
                        SoundLibrary.rifleFire.Play();
                        bulletManager.AddBullets(b);
                    }
                    else if (playerOneWeapon is Explosives)
                    {
                        bulletManager.AddBullets(r);
                        SoundLibrary.explosiveFire.Play();
                    }
                    else if (playerOneWeapon is Shotgun)
                    {
                        int angleIndex = 40 / playerOneWeapon.numberOfProjectilesPerFire; //Ändra 30 till spridning för vapen
                        SoundLibrary.shotGunFire.Play();


                        bulletManager.AddBullets(b);
                        for (int i = 0; i < playerOneWeapon.numberOfProjectilesPerFire - 1; i++)
                        {
                            if (i % 2 == 0)
                            {
                                b = new Bullet(position, playerOneWeapon.projectileSpeed, angle - (float)((Math.PI / 180) * angleIndex), playerOneWeapon.damage, playerOneWeapon.weaponRange, PlayerID);

                            }
                            else
                            {
                                b = new Bullet(position, playerOneWeapon.projectileSpeed, angle + (float)((Math.PI / 180) * angleIndex), playerOneWeapon.damage, playerOneWeapon.weaponRange, PlayerID);
                                angleIndex += angleIndex;
                            }
                            bulletManager.AddBullets(b);

                        }

                    }
                    timeSinceLastPlayerOneBullet = 0f;
                }
            }
            if (PlayerID == 2)
            {
                if (timeSinceLastPlayerTwoBullet > playerTwoWeapon.fireRate)
                {
                    Bullet b = new Bullet(position, playerTwoWeapon.projectileSpeed, angle, playerTwoWeapon.damage, playerTwoWeapon.weaponRange, PlayerID);
                    Rocket r = new Rocket(position, playerTwoWeapon.projectileSpeed, angle, playerTwoWeapon.damage, playerTwoWeapon.weaponRange, PlayerID);
                    //for (int i = 0; i < playerTwoWeapon.numberOfProjectilesPerFire; i++)
                    //{
                        if (playerTwoWeapon is Rifle)
                        {
                            SoundLibrary.rifleFire.Play();
                            bulletManager.AddBullets(b);
                        }
                        else if (playerTwoWeapon is Explosives)
                        {
                            bulletManager.AddBullets(r);
                            SoundLibrary.explosiveFire.Play();
                        }
                        else if (playerTwoWeapon is Shotgun)
                        {
                            int angleIndex = 40 / playerTwoWeapon.numberOfProjectilesPerFire; //Ändra 30 till spridning för vapen
                            SoundLibrary.shotGunFire.Play();


                            bulletManager.AddBullets(b);
                            for (int k = 0; k < playerTwoWeapon.numberOfProjectilesPerFire - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    b = new Bullet(position, playerTwoWeapon.projectileSpeed, angle - (float)((Math.PI / 180) * angleIndex), playerTwoWeapon.damage, playerTwoWeapon.weaponRange, PlayerID);
                                }
                                else
                                {
                                    b = new Bullet(position, playerTwoWeapon.projectileSpeed, angle + (float)((Math.PI / 180) * angleIndex), playerTwoWeapon.damage, playerTwoWeapon.weaponRange, PlayerID);
                                    angleIndex += angleIndex;
                                }
                                bulletManager.AddBullets(b);
                            }
                        }
                        timeSinceLastPlayerTwoBullet = 0f;
                        // SoundLibrary.rifleFire.Play();
                    //}

                }
            }

        }
        public void SwitchWeaponRight(int playerID, int shotGunLevel, int explosiveLevel, int rifleLevel)
        {

            switch (playerID)
            {
                case 1:
                    if (playerOneWeapon is Rifle)
                    {
                        playerOneWeapon = new Shotgun(shotGunLevel);
                    }
                    else if (playerOneWeapon is Shotgun)
                    {
                        playerOneWeapon = new Explosives(/*explosiveLevel*/);
                    }
                    else if (playerOneWeapon is Explosives)
                    {
                        playerOneWeapon = new Rifle(rifleLevel);
                    }
                    break;
                case 2:
                    if (playerTwoWeapon is Rifle)
                    {
                        playerTwoWeapon = new Shotgun(shotGunLevel);
                    }
                    else if (playerTwoWeapon is Shotgun)
                    {
                        playerTwoWeapon = new Explosives();
                    }
                    else if (playerTwoWeapon is Explosives)
                    {
                        playerTwoWeapon = new Rifle(rifleLevel);
                    }
                    break;
                default:
                    break;
            }
        }
        public void SwitchWeaponLeft(int playerID, int shotGunLevel, int explosiveLevel, int rifleLevel)
        {
            switch (playerID)
            {
                case 1:

                    if (playerOneWeapon is Rifle)
                    {
                        playerOneWeapon = new Explosives(/*explosiveLevel*/);
                    }
                    else if (playerOneWeapon is Shotgun)
                    {
                        playerOneWeapon = new Rifle(rifleLevel);
                    }
                    else if (playerOneWeapon is Explosives)
                    {
                        playerOneWeapon = new Shotgun(shotGunLevel);
                    }
                    break;
                case 2:
                    if (playerTwoWeapon is Rifle)
                    {
                        playerTwoWeapon = new Explosives();
                    }
                    else if (playerTwoWeapon is Shotgun)
                    {
                        playerTwoWeapon = new Rifle(rifleLevel);
                    }
                    else if (playerTwoWeapon is Explosives)
                    {
                        playerTwoWeapon = new Shotgun(shotGunLevel);
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
