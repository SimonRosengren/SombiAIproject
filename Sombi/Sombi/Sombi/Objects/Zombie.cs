using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Zombie : GameObject
    {
        bool haveTarget;
        public float health;
        Vector2 direction;
        public Vector2 currentTile;
        float velocity;
        public int activationRange;
        Animation walkAnimation;
        AnimationPlayer animationPlayer;
        Rectangle hitBox;
        public Zombie(Vector2 startPos) : base(startPos)
        {
            this.velocity = (float)GlobalValues.rnd.Next(55, 70*(GlobalValues.difficultyLevel + 1 / 2));   //Rör ej för i helvete ;)
            //this.pos = startPos;
            this.direction = new Vector2(0, 1);
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
            this.health = 0;//20 + GlobalValues.difficultyLevel * GlobalValues.numberOfPlayers;
            
            this.activationRange = 250;
            this.health = 25 + GlobalValues.difficultyLevel * GlobalValues.numberOfPlayers;

            this.activationRange = 250 + GlobalValues.difficultyLevel + GlobalValues.numberOfPlayers;
            this.haveTarget = false;
            //CalculateCurrentTile();
        }

        public void LoadContent()
        {
            //walkAnimation = new Animation(TextureLibrary.fastZombieTex, 50, 0.08f, true);
            //walkAnimation = new Animation(TextureLibrary.zombieTex, 50, 0.2f, true);
            walkAnimation = new Animation(TextureLibrary.fatZombieTex, 50, 0.2f, true);
            currentTile = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            CalculateCurrentTile();
            pos += direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitBox.X = (int)pos.X - TextureLibrary.fatZombieTex.Width / 12;
            hitBox.Y = (int)pos.Y - TextureLibrary.fatZombieTex.Height / 3;
            FindWallThroughMatrix();
            
            // Console.WriteLine((int)((pos.X + 25) / 50) + (int)direction.X);

            if (pos.X != 0)
                animationPlayer.PlayAnimation(walkAnimation);
            else if (pos.Y != 0)
                animationPlayer.PlayAnimation(walkAnimation);

            animationPlayer.Update(gameTime);

        }
        public void handleBulletHit(int damage)
        {
            this.health -= damage;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            animationPlayer.Draw(spriteBatch, pos, animationPlayer.rotation = (float)Math.Atan2(-direction.X, direction.Y));
            //spriteBatch.Draw(TextureLibrary.sourceRectTex, new Vector2(hitBox.X, hitBox.Y), Color.Red);

        }
        public void FindWallThroughMatrix()
        {
            CalculateCurrentTile();
            if (direction.X > 0)
            {
                if (!Grid.grid[(int)currentTile.X + 1, (int)currentTile.Y].passable || Grid.grid[(int)currentTile.X + 1, (int)currentTile.Y].hasZombie)
                {
                    if (!haveTarget)
                    {
                        FindNewRandomDirection();
                    }
                    else
                    {
                        direction.X = 0;
                    }
                    
                }
            }
            else if (direction.X < 0)
            {
                if (!Grid.grid[(int)currentTile.X - 1, (int)currentTile.Y].passable || Grid.grid[(int)currentTile.X - 1, (int)currentTile.Y].hasZombie)
                {
                    if (!haveTarget)
                    {
                        FindNewRandomDirection();
                    }
                    else
                    {
                        direction.X = 0;
                    }
                }
            }
            if (direction.Y > 0)
            {
                if (!Grid.grid[(int)currentTile.X, (int)currentTile.Y + 1].passable || Grid.grid[(int)currentTile.X, (int)currentTile.Y + 1].hasZombie)
                {
                    if (!haveTarget)
                    {
                        FindNewRandomDirection();
                    }
                    else
                    {
                        direction.Y = 0;
                    }
                }
            }
            else if (direction.Y < 0)
            {
                if (!Grid.grid[(int)currentTile.X, (int)currentTile.Y - 1].passable || Grid.grid[(int)currentTile.X, (int)currentTile.Y - 1].hasZombie)
                {
                    if (!haveTarget)
                    {
                        FindNewRandomDirection();
                    }
                    else
                    {
                        direction.Y = 0;
                    }
                }
            }
        }
        public void FindNewRandomDirection()
        {
            int random = GlobalValues.rnd.Next(1, 3);

            switch (random)
            {
                case 1:
                    if (Grid.grid[(int)currentTile.X, (int)currentTile.Y - 1].passable && !Grid.grid[(int)currentTile.X, (int)currentTile.Y - 1].hasZombie)
                    {
                        direction.X = 0;
                        direction.Y = -1;
                         
                        
                    }
                    else if (Grid.grid[(int)currentTile.X + 1, (int)currentTile.Y].passable && !Grid.grid[(int)currentTile.X + 1, (int)currentTile.Y].hasZombie)
                    {
                        direction.X = 1;
                        direction.Y = 0;
                       
                    }
                    else if (Grid.grid[(int)currentTile.X, (int)currentTile.Y + 1].passable && !Grid.grid[(int)currentTile.X, (int)currentTile.Y + 1].hasZombie)
                    {
                        direction.X = 0;
                        direction.Y = 1;
                          
                    }
                    else if (Grid.grid[(int)currentTile.X - 1, (int)currentTile.Y].passable && !Grid.grid[(int)currentTile.X - 1, (int)currentTile.Y].hasZombie)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                                                  
                    }
                    break;
                case 2:
                    if (Grid.grid[(int)currentTile.X - 1, (int)currentTile.Y].passable && !Grid.grid[(int)currentTile.X - 1, (int)currentTile.Y].hasZombie)
                    {
                        direction.X = -1;
                        direction.Y = 0;
                                                   
                    }
                    else if (Grid.grid[(int)currentTile.X, (int)currentTile.Y - 1].passable && !Grid.grid[(int)currentTile.X, (int)currentTile.Y - 1].hasZombie)
                    {
                        direction.X = 0;
                        direction.Y = -1;
                      
                    }
                    else if (Grid.grid[(int)currentTile.X, (int)currentTile.Y + 1].passable && !Grid.grid[(int)currentTile.X, (int)currentTile.Y + 1].hasZombie)
                    {
                        direction.X = 0;
                        direction.Y = 1;
                        
                    }
                    else if (Grid.grid[(int)currentTile.X + 1, (int)currentTile.Y].passable && !Grid.grid[(int)currentTile.X + 1, (int)currentTile.Y].hasZombie)
                    {
                        direction.X = 1;
                        direction.Y = 0;
                         
                    }
                    break;
                default:
                    break;
            }
        }
        public void SetChasingDirection(Vector2 playerPos)
        {
            this.haveTarget = true;
            this.direction = Vector2.Normalize(playerPos - this.pos);
             
            FindWallThroughMatrix();
            
        }
        public Rectangle GetHitbox()
        {
            return hitBox;
        }
        public void CalculateCurrentTile()
        {

            Grid.SetCurrentTileHasZombie(false, currentTile);
            currentTile = new Vector2((int)(pos.X) / 50, (int)(pos.Y) / 50);
            Grid.SetCurrentTileHasZombie(true, currentTile);
        }
        public void ResetTarget()
        {
            this.haveTarget = false;
        }
    }

}
