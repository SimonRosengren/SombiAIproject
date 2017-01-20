using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class EnemyManager
    {
        private int spawnIndex;
        private int currentWaveSize;
        private float waveTimer;
        private float currentWaveInterval;

        public List<Zombie> zombies = new List<Zombie>();
        public List<BloodStain> bloodPositions = new List<BloodStain>();
        public void Update(GameTime gameTime, List<Projectile> bulletList)
        {

            CheckForBulletCollisions(bulletList);
            ClearZombies();
            UpdateZombies(gameTime);

        }

        private void UpdateZombies(GameTime gameTime)
        {
            foreach (Zombie z in zombies)
            {
                z.Update(gameTime);
            }
            WaveSpawner(gameTime);
        }

        public void AddZombie(Vector2 startPos)
        {
            Zombie z = new Zombie(startPos);
            zombies.Add(z);
            z.LoadContent();
        }

        public void ClearZombies()
        {
            for (int i = zombies.Count - 1; i >= 0; i--)
            {
                if (zombies[i].health < 1)
                {
                    Grid.SetCurrentTileHasZombie(false, zombies[i].currentTile);
                    bloodPositions.Add(new BloodStain(zombies[i].pos));
                    zombies.RemoveAt(i);
                    HighscoreManager.score++;
                    HighscoreManager.kills++;
                }
            }
        }

        public void AddZombiesToRandomLocation(int nrOfZombiesToAdd)//All zombies at once
        {
            for (int i = 0; i < nrOfZombiesToAdd; i++)
            {
                spawnIndex = GlobalValues.rnd.Next(0, Grid.spawnPoints.Count);

                AddZombie(Grid.spawnPoints[spawnIndex]);
            }
        }

        public void DrawZombie(SpriteBatch spriteBatch)
        {
            foreach (Zombie z in zombies)
            {
                z.Draw(spriteBatch);
            }
        }

        public void DrawBlood(SpriteBatch spriteBatch)
        {
            foreach (BloodStain bs in bloodPositions)
            {
                bs.Draw(spriteBatch);
            }
        }

        public void DrawZombieCount(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureLibrary.billBoardText, "" + zombies.Count, new Vector2(1366, 1430), GlobalValues.billBoardColor);
        }

        public void CheckForBulletCollisions(List<Projectile> bulletList)
        {
            for (int i = 0; i < zombies.Count; i++)
            {
                for (int k = 0; k < bulletList.Count; k++)
                {
                    if (zombies[i].GetHitbox().Intersects(bulletList[k].GetHitBox()))
                    {
                        zombies[i].handleBulletHit(bulletList[k].damage);
                        bulletList[k].Explode();
                        if (bulletList[k].timeToLiveAfterImpact == 0)
                        {
                            bulletList.RemoveAt(k);
                        }
                    }
                }
            }
        }
        public void AddNewWave(float intervalTime, int waveSize) //addas ny wave på den andra sätts hela intervallet till det nya
        {
            currentWaveSize += waveSize;
            currentWaveInterval = intervalTime;
        }

        private void WaveSpawner(GameTime gameTime)
        {
            waveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (waveTimer >= currentWaveInterval)
            {
                AddZombiesToRandomLocation(1);
                waveTimer = 0;
                currentWaveSize--;
            }
        }

        public void CheckPlayerZombieCollisions(List<Player> players)
        {
            for (int i = 0; i < zombies.Count; i++)
            {
                for (int j = 0; j < players.Count; j++)
                {
                    if (zombies[i].GetHitbox().Intersects(players[j].HitBox))
                    {
                        players[j].handleBulletHit(10);
                    }
                    if (Vector2.Distance(zombies[i].pos, players[j].pos) < zombies[i].activationRange && !players[j].eaten)
                    {
                        zombies[i].SetChasingDirection(players[j].pos);
                    }
                    else if (Vector2.Distance(zombies[i].pos, players[j].pos) > zombies[i].activationRange)
                    {
                        zombies[i].ResetTarget();

                    }

                }
            }
        }
    }
}
