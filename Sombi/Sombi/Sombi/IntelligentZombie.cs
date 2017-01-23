using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sombi
{
    class IntelligentZombie
    {

        enum behaviourState { idle, chasingPlayer, investigatingSound}
        behaviourState currentBehaviourState = behaviourState.idle;

        List<Vector2> path = new List<Vector2>();

        Vector2 pos;
        float distanceMoved = 0;

        Animation walkAnimation;
        AnimationPlayer animationPlayer;

        public float hp { get; private set; }
        public Vector2 nextTile = new Vector2(0, 0);
        public Vector2 currentTile;

        public IntelligentZombie(Vector2 pos)
        {
            this.pos = pos;
            this.hp = 10;
            currentTile = new Vector2(((int)pos.X + 25) / 50, ((int)pos.Y + 25) / 50);
            walkAnimation = new Animation(TextureLibrary.fatZombieTex, 50, 0.2f, true);
        }
        public void Update(GameTime time)
        {
            switch (currentBehaviourState)
            {
                case behaviourState.idle:
                    break;
                case behaviourState.chasingPlayer:
                    break;
                case behaviourState.investigatingSound:
                    TryMove(time);
                    break;
                default:
                    break;
            }

            animationPlayer.PlayAnimation(walkAnimation);
            animationPlayer.Update(time);
        }
        /// <summary>
        /// Makes the move if allowed
        /// </summary>
        /// <param name="time"></param>
        public void Move(GameTime time)
        {
            this.pos += Vector2.Normalize(nextTile * 50 - pos) * (float)time.ElapsedGameTime.TotalSeconds * 100; //Moves towards next tile
            distanceMoved = Vector2.Distance(currentTile * 50, pos); //Distance moved from tile
            if (distanceMoved >= GlobalValues.TILE_SIZE)    //Checks if we arrived at next tile
            {
                currentTile = nextTile; 
                for (int i = 0; i < path.Count; i++)
                {
                    if (i == path.Count - 1)
                    {
                        //Monster arrived at goal
                        return;
                    }
                    if (path[i].X == currentTile.X && path[i].Y == currentTile.Y)    //Hittar nurvarande tile i listan
                    {
                        nextTile = path[i + 1];  //Sätter nexTile till tilen efter currentTile
                        return;
                    }
                }
                distanceMoved = 0;
            }
        }
        /// <summary>
        /// Checks if we are allowed to make the move. Looking for walls
        /// </summary>
        public void TryMove(GameTime time)
        {
            if (nextTile.X < GlobalValues.gridSize.X && nextTile.Y < GlobalValues.gridSize.Y)
            {
                if (Grid.grid[(int)nextTile.X, (int)nextTile.Y].passable)
                {
                    Move(time);
                }
            }

            else
                return;
        }
        public void Draw(SpriteBatch sb)
        {
            animationPlayer.Draw(sb, pos, (float)Math.Atan2(Vector2.Normalize(nextTile * 50 - pos).X * -1, Vector2.Normalize(nextTile * 50 - pos).Y));
        }
        public Vector2 getCenterPos()
        {
            return new Vector2((int)pos.X + 25, (int)pos.Y + 25);
        }
        public void TakeDamage(float damage)
        {
            this.hp -= damage;
        }
        /// <summary>
        /// Sends the zombie to investigate a spec tile
        /// </summary>
        /// <param name="soundTile">Tile to investigate</param>
        public void InvestigateSound(Vector2 soundTile)
        {
                PathFinder.CalculateClosestPath(ref path, pos, soundTile);
                nextTile = path[0]; //Next tile is the first tile in the path
                currentBehaviourState = behaviourState.investigatingSound;
                Console.WriteLine(path[0]);   
        }
    }
}
