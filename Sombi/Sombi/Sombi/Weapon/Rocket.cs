using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Rocket : Projectile
    {
        private float explodeDuration = 2;
        private float explodeTimer = 0;
        Animation animation;
        private int AOE;
        AnimationPlayer animationPlayer;
        private Rectangle explodingHb;
        GamePadState circularGamePadState;

        public Rocket(Vector2 pos, float speed, float angle, int damage, int range, int ID)
            : base(pos, speed, angle, damage, range, ID)
        {
            this.explodingHb = new Rectangle((int)this.pos.X, (int)this.pos.Y, 4, 4);
            animation = new Animation(TextureLibrary.rocketExplosion, 125, 0.1f, false);
            this.timeToLiveAfterImpact = 1;
            AOE = 100;
        }
        public Rocket(Vector2 pos, float speed, float angle, int damage, int range, int ID, int level)
            : base(pos, speed, angle, damage, range, ID)
        {
            this.explodingHb = new Rectangle((int)this.pos.X, (int)this.pos.Y, 4, 4);
            animation = new Animation(TextureLibrary.rocketExplosion, 125, 0.1f, false);
            SetVariables(level);
        }


        public override void Update(GameTime gameTime)
        {
            pos += velocity * speed;
            distanceTraveled = Vector2.Distance(Pos, startPos);
            explodingHb.X = (int)pos.X - (explodingHb.Width / 2);
            explodingHb.Y = (int)pos.Y - (explodingHb.Height / 2);
            if (exploding && explodeTimer < explodeDuration)
            {
                timeToLiveAfterImpact -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                explodeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                animationPlayer.PlayAnimation(animation);
                animationPlayer.Update(gameTime);
            }
            UpdateRocketRotation();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureLibrary.rocket, pos, null, Color.White, angle, new Vector2(TextureLibrary.rocket.Width / 2, TextureLibrary.rocket.Height / 2), 1f, SpriteEffects.None, 0f);


            animationPlayer.Draw(spriteBatch, pos, angle);
        }
        public override void Explode()
        {
            this.speed = 0;
            explodingHb.Width = AOE;
            explodingHb.Height = AOE;
            exploding = true;
            //SoundLibrary.explosion.Play();
            SoundLibrary.PlaySound(SoundLibrary.Explosion);

        }
        public override Rectangle GetHitBox()
        {
            return explodingHb;
        }
        private void UpdateRocketRotation() //Rotate sprite based on controller input
        {
            if (circularGamePadState.ThumbSticks.Right.X != 0 && circularGamePadState.ThumbSticks.Right.Y != 0)
            {
                angle = (float)Math.Atan2(circularGamePadState.ThumbSticks.Right.X, circularGamePadState.ThumbSticks.Right.Y) - (float)Math.PI / 2;
            }
        }
        public void SetVariables(int level)
        {
            switch (level)
            {
                case 1:
                    this.timeToLiveAfterImpact = 1;
                    AOE = 50;
                    break;
                case 2:
                    this.timeToLiveAfterImpact = 1;
                    AOE = 75;
                    break;
                case 3:
                    this.timeToLiveAfterImpact = 1;
                    AOE = 100;
                    break;
                case 4:
                    this.timeToLiveAfterImpact = 1;
                    AOE = 125;
                    break;
                case 5:
                    this.timeToLiveAfterImpact = 1;
                    AOE = 150;
                    break;
                case 6:
                    this.timeToLiveAfterImpact = 1;
                    AOE = 175;
                    break;
                default:
                    break;
            }
        }

    }
}
