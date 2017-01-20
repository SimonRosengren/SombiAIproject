using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sombi
{
    class PlayerManager
    {
        public Player player1;
        public Player player2;
        public List<Player> players;
        public WeaponManager weaponManager;

        Camera camera;
        KeyboardState currentKeyboard;
        KeyboardState oldKeyboard;

        public PlayerManager(Camera camera)
        {
            weaponManager = new WeaponManager();
            players = new List<Player>();
            this.camera = camera;
            CreatePlayers();
        }

        public bool GameOver()
        {
            if (player1.dead && player2.dead)
            {
                return true;
            }   
            else
            {
                return false;
            }
        }

        public void Update(GameTime gameTime)
        {
            player1.UpdateAnimation(weaponManager.playerOneWeapon);
            player2.UpdateAnimation(weaponManager.playerTwoWeapon);
            if (!player1.eaten)
            {
                player1.Update(gameTime);
            }
            if (!player2.eaten)
            {
                player2.Update(gameTime);
            }
            
            if (player1.FireWeapon() && !player1.dead)
            {
                weaponManager.CreateBullets(1, player1.pos, player1.angle, player1.explosivesLevel);
            }

            if (player2.FireWeapon() && !player2.dead)
            {
                weaponManager.CreateBullets(2, player2.pos, player2.angle, player2.explosivesLevel);
            }
            SwitchWeapon();
            weaponManager.Update(gameTime);
            Revive();
            ScreenCollide();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player1.DrawDead(spriteBatch);
            player2.DrawDead(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);            
            weaponManager.Draw(spriteBatch);
        }

        protected void ScreenCollide()
        {
            foreach (Player p in players)
            {
                if (p.pos.X <= camera.position.X - GlobalValues.cameraBounds.X)
                {
                    p.velocity.X = 0;
                    p.pos.X = camera.position.X - GlobalValues.cameraBounds.X  + 1;
                }
                if (p.pos.X >= camera.position.X + GlobalValues.cameraBounds.X)
                {
                    p.velocity.X = 0;
                    p.pos.X = camera.position.X + GlobalValues.cameraBounds.X - 1;
                }
                if (p.pos.Y <= camera.position.Y - GlobalValues.cameraBounds.Y)
                {
                    p.velocity.Y = 0;
                    p.pos.Y = camera.position.Y - GlobalValues.cameraBounds.Y + 1;
                }
                if (p.pos.Y >= camera.position.Y + GlobalValues.cameraBounds.Y)
                {
                    p.velocity.Y = 0;
                    p.pos.Y = camera.position.Y + GlobalValues.cameraBounds.Y + 1;
                }
            }
        }

        public void CheckPlayerBulletCollisions()
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int k = 0; k < weaponManager.bulletManager.bullets.Count; k++)
                {
                    if (players[i].HitBox.Contains(weaponManager.bulletManager.bullets[k].Pos) && players[i].ID != weaponManager.bulletManager.bullets[k].ID)
                    {
                        players[i].handleBulletHit(weaponManager.bulletManager.bullets[k].damage);
                        weaponManager.bulletManager.bullets[k].Explode();
                        if (weaponManager.bulletManager.bullets[k].timeToLiveAfterImpact == 0)
                        {
                            weaponManager.bulletManager.bullets.RemoveAt(k);
                        }

                    }
                }
            }
        }
        public void CreatePlayers()
        {
            players.Clear();
            player1 = new Player(new Vector2(150, 150), 1);
            player2 = new Player(new Vector2(150, 200), 2);
            players.Add(player1);
            players.Add(player2);
        }

        private void Revive()
        {
            if (GlobalValues.numberOfPlayers == 2)
            {
                if (player1.HitBox.Intersects(player2.HitBox))
                {
                    player1.reviveing = true;
                    player2.reviveing = true;
                    if (player1.revive && player2.dead && !player2.eaten)
                    {
                        player2.health = 500;
                        player2.dead = false;
                    }
                    if (player2.revive && player1.dead && !player1.eaten)
                    {
                        player1.health = 500;
                        player1.dead = false;
                    }
                }
                else
                {
                    player1.reviveing = false;
                    player2.reviveing = false;
                }
            }
        }
        public void CreateOnePlayer()
        {
            CreatePlayers();
            player2.dead = true;
            players.Remove(player2);
        }
        public void SwitchWeapon()
        {
            oldKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();


            if (currentKeyboard.IsKeyDown(Keys.E) && !oldKeyboard.IsKeyDown(Keys.E))
            {
                weaponManager.SwitchWeaponRight(1, player1.shotgunLevel, player1.explosivesLevel, player1.rifleLevel);           //För player 1
                weaponManager.SwitchWeaponRight(2, player2.shotgunLevel, player2.explosivesLevel, player2.rifleLevel);           //För player 2

            }
            if (currentKeyboard.IsKeyDown(Keys.Q) && !oldKeyboard.IsKeyDown(Keys.Q))
            {
                weaponManager.SwitchWeaponLeft(1, player1.shotgunLevel, player1.explosivesLevel, player1.rifleLevel);
                weaponManager.SwitchWeaponLeft(2, player2.shotgunLevel, player2.explosivesLevel, player2.rifleLevel);
            }

            if (player1.GamePadState.IsButtonDown(Buttons.RightShoulder) && !player1.OldGamePadState.IsButtonDown(Buttons.RightShoulder))
            {
                weaponManager.SwitchWeaponRight(1, player1.shotgunLevel, player1.explosivesLevel, player1.rifleLevel);
            }
            if (player1.GamePadState.IsButtonDown(Buttons.LeftShoulder) && !player1.OldGamePadState.IsButtonDown(Buttons.LeftShoulder))
            {
                weaponManager.SwitchWeaponLeft(1, player1.shotgunLevel, player1.explosivesLevel, player1.rifleLevel);
            }
            if (player2.GamePadState.IsButtonDown(Buttons.LeftShoulder) && !player2.OldGamePadState.IsButtonDown(Buttons.LeftShoulder))
            {
                weaponManager.SwitchWeaponLeft(2, player2.shotgunLevel, player2.explosivesLevel, player2.rifleLevel);
            }
            if (player2.GamePadState.IsButtonDown(Buttons.RightShoulder) && !player2.OldGamePadState.IsButtonDown(Buttons.RightShoulder))
            {
                weaponManager.SwitchWeaponRight(2, player2.shotgunLevel, player2.explosivesLevel, player2.rifleLevel);
            }



        }

    }
}
