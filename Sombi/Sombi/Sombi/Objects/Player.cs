using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Sombi
{
    enum PlayerID
    {
        One,
        Two,
    }

    class Player : GameObject
    {
        //Vector2 direction;
        public Vector2 velocity;
        public float angle;
        public float playerSpeed;
        float maxspeed;
        float timeToRevive;
        float reviveTime;
        public bool reviveing;
        public int cash;
        GamePadState gamePadState;
        GamePadState oldgamePadState;
        GamePadState circularGamePadState;
        //Weapon playerWeapon;
        PlayerID playerID;
        public int ID;
        Rectangle hitBox;
        public int health;
        public bool revive = false;
        public bool dead = false;
        public bool eaten = false;
        public bool gotPackage = false;

        public int shotgunLevel;
        public int rifleLevel;
        public int explosivesLevel;
        // Player1
        Animation player1RifleIdle;
        Animation player1ShotgunIdle;
        Animation player1RocketIdle;
        Animation player1AnimationRifle;
        Animation player1AnimationShotgun;
        Animation player1AnimationRocket;
        Animation player1RifleShootingAnimation;
        Animation player1ShotgunShootingAnimation;
        Animation player1RocketShootingAnimation;
        // Player2
        Animation player2RifleIdle;
        Animation player2ShotgunIdle;
        Animation player2RocketIdle;
        Animation player2AnimationRifle;
        Animation player2AnimationShotgun;
        Animation player2AnimationRocket;
        Animation player2RifleShootingAnimation;
        Animation player2ShotgunShootingAnimation;
        Animation player2RocketShootingAnimation;

        AnimationPlayer animationplayer;


        public Player(/*Weapon weapon, */Vector2 position, int ID)
            : base(position)
        {
            //playerWeapon = weapon;
            this.ID = ID;
            hitBox = new Rectangle((int)position.X, (int)position.Y, TextureLibrary.sourceRectTex.Width, TextureLibrary.sourceRectTex.Height);
            SetPlayerID(ID);
            LoadContent();
        }
        public void Player1Animation()
        {
            player1RifleIdle = new Animation(TextureLibrary.player1RifleIdle, 37, 0.1f, true);
            player1RifleShootingAnimation = new Animation(TextureLibrary.player1RifleSheet, 63, 0.1f, true);
            player1AnimationRifle = new Animation(TextureLibrary.player1RifleAnimationSheet, 63, 0.25f, true);

            player1ShotgunIdle = new Animation(TextureLibrary.player1ShotgunIdle, 37, 0.1f, true);
            player1ShotgunShootingAnimation = new Animation(TextureLibrary.player1ShotgunSheet, 62, 0.1f, true);
            player1AnimationShotgun = new Animation(TextureLibrary.player1ShotgunAnimationSheet, 63, 0.25f, true);

            player1RocketIdle = new Animation(TextureLibrary.player1RocketIdle, 43, 0.1f, true);
            player1RocketShootingAnimation = new Animation(TextureLibrary.player1RocketTex, 63, 0.1f, true);
            player1AnimationRocket = new Animation(TextureLibrary.player1RocketAnimationSheet, 63, 0.25f, true);
        }

        public void Player2Animation()
        {
            player2RifleIdle = new Animation(TextureLibrary.player2RifleIdle, 37, 0.1f, true);
            player2RifleShootingAnimation = new Animation(TextureLibrary.player2RifleSheet, 63, 0.1f, true);
            player2AnimationRifle = new Animation(TextureLibrary.player2RifleAnimationSheet, 63, 0.25f, true);

            player2ShotgunIdle = new Animation(TextureLibrary.player2ShotgunIdle, 37, 0.1f, true);
            player2ShotgunShootingAnimation = new Animation(TextureLibrary.player2ShotgunSheet, 62, 0.1f, true);
            player2AnimationShotgun = new Animation(TextureLibrary.player2ShotgunAnimationSheet, 63, 0.25f, true);

            player2RocketIdle = new Animation(TextureLibrary.player2RocketIdle, 43, 0.1f, true);
            player2RocketShootingAnimation = new Animation(TextureLibrary.player2RocketTex, 63, 0.1f, true);
            player2AnimationRocket = new Animation(TextureLibrary.player2RocketAnimationSheet, 63, 0.25f, true);
        }

        public void LoadContent()
        {
            velocity = new Vector2(0, 0);
            Player1Animation();
            Player2Animation();
            velocity = Vector2.Zero;
            maxspeed = 200.0f;
            playerSpeed = 200.0f;
            health = 1000;
            timeToRevive = 3.0f;
            reviveTime = 0.0f;
            cash = 100;
            rifleLevel = 1;
            shotgunLevel = 1;
            explosivesLevel = 1;
            reviveing = false;
        }

        public Rectangle HitBox
        {
            get { return hitBox; }
            set { }
        }

        public GamePadState GamePadState
        {
            get { return gamePadState; }
            set { }
        }

        public GamePadState OldGamePadState
        {
            get { return oldgamePadState; }
            set { }
        }

        public float ReviveTime
        {
            get { return reviveTime; }
            set { }
        }

        public override void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                dead = true;
                cash = 0;
            }
            if (health <= -3000)
            {
                eaten = true;
                health = -3000;
            }

            if (!dead)
            {
                animationplayer.Update(gameTime);
                UpdateGamepad();
                if (gamePadState.IsConnected)
                {
                    UpdatePosition(gameTime);
                    UpdateRotation();
                    FireWeapon();
                }
                else
                {
                    KeyBoardMovement(gameTime);                 
                }
                UpdateHitbox();
            }
            Revive(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (reviveTime > 0 && reviveTime < 3 && reviveing)
            {
                spriteBatch.DrawString(TextureLibrary.pauseText, (3 - (int)reviveTime).ToString(), pos - new Vector2(6, 45), Color.Green);
            }

            if (!dead)
            {
                if (playerID == PlayerID.One)
                    animationplayer.Draw(spriteBatch, pos, angle);

                if (playerID == PlayerID.Two)
                    animationplayer.Draw(spriteBatch, pos, angle);

            }
        }
        public void DrawDead(SpriteBatch spriteBatch)
        {
            if (dead)
            {
                if (playerID == PlayerID.One)
                    spriteBatch.Draw(TextureLibrary.player1DeadTex, pos, null, Color.White, angle, new Vector2(TextureLibrary.player1IncapacitatedTex.Width / 2, TextureLibrary.player1IncapacitatedTex.Height / 2), 1f, SpriteEffects.None, 0f);
                if (playerID == PlayerID.Two)
                    if (GlobalValues.numberOfPlayers == 2)
                    {
                        spriteBatch.Draw(TextureLibrary.player2DeadTex, pos, null, Color.White, angle, new Vector2(TextureLibrary.player2IncapacitatedTex.Width / 2, TextureLibrary.player2IncapacitatedTex.Height / 2), 1f, SpriteEffects.None, 0f);
                    }
            }
        }
        private void SetPlayerID(int ID)
        {
            if (ID == 1)
            {
                playerID = PlayerID.One;
            }
            else
            {
                playerID = PlayerID.Two;
            }
        }

        public void UpdateAnimation(Weapon weapon)
        {
            RifleAnimation(weapon);
            ShotgunAnimation(weapon);
            RocketAnimation(weapon);
        }


        private void UpdateHitbox()
        {
            hitBox.X = (int)pos.X - ((TextureLibrary.player1RifleTex.Width) / 2) + 10;
            hitBox.Y = (int)pos.Y - ((TextureLibrary.player1RifleTex.Height) / 2);
        }

        public void UpdateGamepad()
        {
            if (playerID == PlayerID.One)
            {
                oldgamePadState = gamePadState;
                gamePadState = GamePad.GetState(PlayerIndex.One);
                circularGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
            }
            else
            {
                oldgamePadState = gamePadState;
                gamePadState = GamePad.GetState(PlayerIndex.Two);
                circularGamePadState = GamePad.GetState(PlayerIndex.Two, GamePadDeadZone.Circular);
            }
        }

        private void UpdatePosition(GameTime gameTime) //Update Velocity and Position based on controller input
        {

            velocity.X = gamePadState.ThumbSticks.Left.X/*  *maxspeed;*/;
            velocity.Y = -gamePadState.ThumbSticks.Left.Y/* * maxspeed;*/;

            if (Grid.grid[(int)(((this.pos.X + ((50 * velocity.X) / 2))) + velocity.X * maxspeed * (float)gameTime.ElapsedGameTime.TotalSeconds) / 50, (int)(((this.pos.Y + ((50 * velocity.Y) / 2))) + velocity.Y * maxspeed * (float)gameTime.ElapsedGameTime.TotalSeconds) / 50].passable)   //Förhindrar flytt om vägg framför
            {
                this.pos += velocity * maxspeed * (float)gameTime.ElapsedGameTime.TotalSeconds;     //Flyttar gubben relativt till delta time
            }

        }

        private void UpdateRotation() //Rotate sprite based on controller input
        {
            if (circularGamePadState.ThumbSticks.Right.X != 0 && circularGamePadState.ThumbSticks.Right.Y != 0)
            {
                angle = (float)Math.Atan2(circularGamePadState.ThumbSticks.Right.X, circularGamePadState.ThumbSticks.Right.Y) - (float)Math.PI / 2;
            }
        }

        public bool FireWeapon() //Fire weapon when button is pressed
        {
            if (gamePadState.Triggers.Right > 0.5f || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void KeyBoardMovement(GameTime gameTime)
        {
            velocity = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity.Y = -1;
                angle = MathHelper.ToRadians(270);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -1;
                angle = MathHelper.ToRadians(180);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity.Y = 1;
                angle = MathHelper.ToRadians(90);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 1;
                angle = MathHelper.ToRadians(0);
            }

            if (Grid.grid[(int)((pos.X + ((50 * velocity.X) / 2)) + velocity.X * maxspeed * (float)gameTime.ElapsedGameTime.TotalSeconds) / 50, (int)((pos.Y + ((50 * velocity.Y) / 2)) + velocity.Y * maxspeed * (float)gameTime.ElapsedGameTime.TotalSeconds) / 50].passable)
            {
                pos += velocity * maxspeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }


        }

        public void handleBulletHit(int damage)
        {
            if (!eaten)
            {
                health -= damage;
            }
        }

        private void Revive(GameTime gameTime)
        {
            if (gamePadState.IsButtonDown(Buttons.A) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                reviveTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                reviveTime = 0;
                revive = false;
            }

            if (reviveTime > timeToRevive)
            {
                revive = true;
            }
        }
        public void RifleAnimation(Weapon weapon)
        {
            if (weapon is Rifle)
            {
                if (playerID == PlayerID.One)
                {
                    if (!FireWeapon() && velocity.X == 0 && velocity.Y == 0)
                    {
                        animationplayer.PlayAnimation(player1RifleIdle);
                    }
                    if (!FireWeapon())
                    {
                        animationplayer.PlayAnimation(player1AnimationRifle);
                    }
                    else if (FireWeapon())
                    {
                        animationplayer.PlayAnimation(player1RifleShootingAnimation);
                    }
                    else
                    {
                        animationplayer.PlayAnimation(player1RifleIdle);
                    }
                }
                if (playerID == PlayerID.Two)
                {
                    if (!FireWeapon() && velocity.X == 0 && velocity.Y == 0)
                    {
                        animationplayer.PlayAnimation(player2RifleIdle);
                    }
                    if (!FireWeapon())
                    {
                        animationplayer.PlayAnimation(player2AnimationRifle);
                    }
                    else if (FireWeapon())
                    {
                        animationplayer.PlayAnimation(player2RifleShootingAnimation);
                    }
                    else
                    {
                        animationplayer.PlayAnimation(player2RifleIdle);
                    }
                }
            }
        }
        public void ShotgunAnimation(Weapon weapon)
        {
            if (weapon is Shotgun)
            {
                if (playerID == PlayerID.One)
                {
                    if (!FireWeapon() && velocity.X == 0 && velocity.Y == 0)
                    {
                        animationplayer.PlayAnimation(player1ShotgunIdle);
                    }
                    else if (!FireWeapon())
                    {
                        animationplayer.PlayAnimation(player1AnimationShotgun);
                    }
                    else if (FireWeapon())
                    {
                        animationplayer.PlayAnimation(player1ShotgunShootingAnimation);
                    }
                    else
                    {
                        animationplayer.PlayAnimation(player1ShotgunIdle);
                    }
                }
                if (playerID == PlayerID.Two)
                {
                    if (!FireWeapon() && velocity.X == 0 && velocity.Y == 0)
                    {
                        animationplayer.PlayAnimation(player2ShotgunIdle);
                    }
                    else if (!FireWeapon())
                    {
                        animationplayer.PlayAnimation(player2AnimationShotgun);
                    }
                    else if (FireWeapon())
                    {
                        animationplayer.PlayAnimation(player2ShotgunShootingAnimation);
                    }
                    else
                    {
                        animationplayer.PlayAnimation(player2ShotgunIdle);
                    }
                }
            }
        }
        public void RocketAnimation(Weapon weapon)
        {
            if (weapon is Explosives)
            {
                if (playerID == PlayerID.One)
                {
                    if (!FireWeapon() && velocity.X == 0 && velocity.Y == 0)
                    {
                        animationplayer.PlayAnimation(player1RocketIdle);
                    }
                    else if (!FireWeapon())
                    {
                        animationplayer.PlayAnimation(player1AnimationRocket);
                    }
                    else if (FireWeapon())
                    {
                        animationplayer.PlayAnimation(player1RocketShootingAnimation);
                    }
                    else
                    {
                        animationplayer.PlayAnimation(player1RocketIdle);
                    }
                }
                if (playerID == PlayerID.Two)
                {
                    if (!FireWeapon() && velocity.X == 0 && velocity.Y == 0)
                    {
                        animationplayer.PlayAnimation(player2RocketIdle);
                    }
                    else if (!FireWeapon())
                    {
                        animationplayer.PlayAnimation(player2AnimationRocket);
                    }
                    else if (FireWeapon())
                    {
                        animationplayer.PlayAnimation(player2RocketShootingAnimation);
                    }
                    else
                    {
                        animationplayer.PlayAnimation(player2RocketIdle);
                    }
                }
            }
        }

    }
}
