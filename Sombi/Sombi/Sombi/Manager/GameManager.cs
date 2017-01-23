using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sombi
{
    enum GameState
    {
        MainMenu,
        Settings,
        Highscore,
        Playing,
        Paused,
        LevelUp,
    }
    class GameManager
    {
        const int TILE_SIZE = 50;
        PlayerManager playerManager;
        ContentManager contentManager;
        EnemyManager enemyManager;
        HUDManager hudManager;
        FPSManager fpsManager;
        PackageManager packageManager;
        HighscoreManager highscoreManager;
        MenuManager menuManager;
        LevelMenuManager levelMenuManager;
        FloatingTextures floatingTextures;
        GameState currentGameState = GameState.MainMenu;
        KeyboardState currentKeyboard;
        KeyboardState oldKeyboard;
        Game1 game;
        Camera camera;
        float fadeInPercentage = 1;
        float fadeOutPercentage = 0;

        PackageMarker marker;

        public GameManager(ContentManager contentManager, Game1 game)
        {
            this.contentManager = contentManager;
            TextureLibrary.LoadContent(contentManager);
            SoundLibrary.LoadContent(contentManager);
            Grid.CreateGridFactory();
            camera = new Camera((int)GlobalValues.TILE_SIZE);
            enemyManager = new EnemyManager();
            playerManager = new PlayerManager(camera, enemyManager.intZombies);
            hudManager = new HUDManager(playerManager.players);
            fpsManager = new FPSManager();
            floatingTextures = new FloatingTextures();
            highscoreManager = new HighscoreManager();
            highscoreManager.ReadScore();
            menuManager = new MenuManager(playerManager.players);
            levelMenuManager = new LevelMenuManager();
            packageManager = new PackageManager(enemyManager);



            this.game = game;

            

        }

        public Matrix ViewMatrix
        {
            get { return camera.ViewMatrix; }
        }

        public void Update(GameTime gameTime)
        {
            oldKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    {
                        MenuUpdate(gameTime);
                        if (currentKeyboard.IsKeyDown(Keys.A)) ///enbart för test, tas bort sen
                        {
                            currentGameState = GameState.Highscore;
                        }

                    }
                    break;

                case GameState.Highscore:
                    {
                        if (currentKeyboard.IsKeyDown(Keys.A) && !oldKeyboard.IsKeyDown(Keys.A)) // enbart för test, tas bort sen lolololo
                        {
                            currentGameState = GameState.Highscore;
                        }
                        break;
                    }

                case GameState.Playing:
                    {
                        PlayingUpdate(gameTime);
                        Upgrade();
                        break;
                    }

                case GameState.Paused:
                    {
                        floatingTextures.Update();
                        if (currentKeyboard.IsKeyDown(Keys.Escape) && !oldKeyboard.IsKeyDown(Keys.Escape))
                        {
                            currentGameState = GameState.Playing;
                        }
                        break;
                    }

                case GameState.LevelUp:
                    {
                        camera.position = new Vector2(0, 0);
                        camera.ViewMatrix = Matrix.CreateTranslation(new Vector3(-camera.position, 0));
                        levelMenuManager.Update(ref playerManager.player1.shotgunLevel, ref playerManager.player1.rifleLevel, ref playerManager.player1.explosivesLevel, ref playerManager.player2.shotgunLevel, ref playerManager.player2.rifleLevel, ref playerManager.player2.explosivesLevel, playerManager.players);
                        if (currentKeyboard.IsKeyDown(Keys.Escape) && !oldKeyboard.IsKeyDown(Keys.Escape))
                        {
                            playerManager.players[0].pos = GlobalValues.PLAYER_ONE_START_POS;
                            if (GlobalValues.numberOfPlayers == 2)
                            {
                                playerManager.players[1].pos = GlobalValues.PLAYER_TWO_START_POS;
                            }                           
                            currentGameState = GameState.Playing;
                        }   
                        foreach (Player p in playerManager.players)
                        {
                            p.UpdateGamepad();
                            if (p.GamePadState.IsButtonDown(Buttons.B))
                            {
                                playerManager.players[0].pos = GlobalValues.PLAYER_ONE_START_POS;
                                if (GlobalValues.numberOfPlayers == 2)
                                {
                                    playerManager.players[1].pos = GlobalValues.PLAYER_TWO_START_POS;
                                }
                               
                                currentGameState = GameState.Playing;
                            }
                        }
                       

                        break;
                    }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    {
                        MenuDraw(spriteBatch);
                        break;
                    }
                case GameState.Highscore:
                    {
                        highscoreManager.Draw(spriteBatch);
                        break;
                    }
                case GameState.Playing:
                    {
                        PlayingDraw(spriteBatch);
                        break;
                    }
                case GameState.Paused:
                    {
                        PauseDraw(spriteBatch);

                        break;
                    }

                case GameState.LevelUp:
                    {
                        levelMenuManager.Draw(spriteBatch, playerManager.players);
                        break;
                    }

            }
            if (playerManager.GameOver())
            {
                Color fadeOutColor = Color.Red;
                Color fadeInColor = Color.Black;
                spriteBatch.Draw(TextureLibrary.fadeScreenTex, camera.position, fadeOutColor * fadeOutPercentage);
                spriteBatch.Draw(TextureLibrary.fadeScreenTex, camera.position, fadeInColor * fadeInPercentage);
                spriteBatch.DrawString(TextureLibrary.billBoardText, "YOU DIED", new Vector2(camera.position.X + 905, camera.position.Y + 450), Color.Black);
            }
        }

        private void StartGame()
        {
            if (menuManager.start)
            {
                //enemyManager.AddZombiesToRandomLocation(24 * GlobalValues.difficultyLevel * GlobalValues.numberOfPlayers);
                enemyManager.AddNewWave(0.5f, 36 * GlobalValues.difficultyLevel * GlobalValues.numberOfPlayers);
                packageManager.AddPackage();


                //Pathfinder
                PathFinder.CreateMap();
                //PathFinder.CalculateClosestPath();
                //TEST
                enemyManager.intZombies.Add(new IntelligentZombie(new Vector2(600, 200)));


                marker = new PackageMarker(playerManager.player1.pos, packageManager.package.pos);

                if (GlobalValues.numberOfPlayers == 1)
                {
                    playerManager.CreateOnePlayer();
                    playerManager.players[0].pos = GlobalValues.PLAYER_ONE_START_POS;
                }
                else if (GlobalValues.numberOfPlayers == 2)
                {
                    playerManager.CreatePlayers();
                    playerManager.players[0].pos = GlobalValues.PLAYER_ONE_START_POS;
                    playerManager.players[1].pos = GlobalValues.PLAYER_TWO_START_POS;
                }

                currentGameState = GameState.Playing;
            }
        }

        private void Settings()
        {
            if (menuManager.settings)
            {
                currentGameState = GameState.Settings;
            }
        }

        private void Highscore()
        {
            if (menuManager.highscore)
            {
                highscoreManager.ReadScore();
                currentGameState = GameState.Highscore;
            }
        }

        private void ExitGame()
        {
            if (menuManager.exit)
            {
                game.Exit();
            }
        }

        private void MenuDraw(SpriteBatch spriteBatch)
        {
            menuManager.Draw(spriteBatch);
            playerManager.Draw(spriteBatch);
        }

        private void MenuUpdate(GameTime gameTime)
        {
            camera.Update(playerManager.players[0].pos, playerManager.players[1].pos);
            menuManager.Update(gameTime);
            playerManager.Update(gameTime);
            floatingTextures.Update();
            StartGame();
            ExitGame();
            Settings();
            Highscore();

        }

        private void PlayingDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureLibrary.mapTex, Vector2.Zero, Color.White);
            packageManager.Draw(spriteBatch); 
            enemyManager.DrawBlood(spriteBatch);
            fpsManager.Draw(spriteBatch);
            playerManager.Draw(spriteBatch);
            enemyManager.DrawZombie(spriteBatch);
            enemyManager.DrawZombieCount(spriteBatch);
            floatingTextures.Draw(spriteBatch);
            hudManager.Draw(spriteBatch, GlobalValues.numberOfPlayers);
            Color fadeInColor = Color.Black;
            spriteBatch.Draw(TextureLibrary.fadeScreenTex, new Vector2(camera.position.X,camera.position.Y), fadeInColor * fadeInPercentage);
            marker.Draw(spriteBatch);
        }

        private void PlayingUpdate(GameTime gameTime)
        {

            if (GlobalValues.numberOfPlayers == 2)
            {
                if (playerManager.player1.eaten)
                {
                    camera.Update(playerManager.players[1].pos, playerManager.players[1].pos);
                }
                else if (playerManager.player2.eaten)
                {
                    camera.Update(playerManager.players[0].pos, playerManager.players[0].pos);
                }
                else
                {
                    camera.Update(playerManager.players[0].pos, playerManager.players[1].pos);
                }
            }
            else
            {
                camera.Update(playerManager.players[0].pos);
            }
            if (currentKeyboard.IsKeyDown(Keys.P) && !oldKeyboard.IsKeyDown(Keys.P))
            {
                currentGameState = GameState.Paused;
            }
            enemyManager.Update(gameTime, playerManager.weaponManager.bulletManager.bullets);
            playerManager.Update(gameTime);
            packageManager.Update(gameTime, playerManager.players);
            floatingTextures.Update();
            hudManager.Update(gameTime, camera.position);
            fpsManager.Update(gameTime);
            enemyManager.CheckPlayerZombieCollisions(playerManager.players);
            playerManager.CheckPlayerBulletCollisions();
            fadeInPercentage -= 0.008f;
            marker.Update(playerManager.player1.pos, packageManager.package.pos);
            if (playerManager.GameOver())
            {
                fadeOutPercentage += 0.010f;
                fadeInPercentage += 0.035f;

                if (fadeOutPercentage >= 3)
                {
                    Grid.menu = true;
                    Grid.CreateGridFactory();
                    menuManager.start = false;
                    currentGameState = GameState.MainMenu;
                    GlobalValues.difficultyLevel = 1;
                    highscoreManager.WriteScore();
                    playerManager.CreatePlayers();
                    enemyManager.zombies.Clear();
                }
            }
        }

        private void PauseDraw(SpriteBatch spriteBatch)
        {
            PlayingDraw(spriteBatch);
            spriteBatch.DrawString(TextureLibrary.billBoardText, "PAUSED - PRESS ESC TO UNPAUSE", camera.position + new Vector2(800,500), Color.Red);
            spriteBatch.DrawString(TextureLibrary.billBoardText, "PAUSED - PRESS BACKSPACE TO EXIT GAME", camera.position + new Vector2(800,540), Color.Red);
        }

        private void Upgrade()
        {
            if (GlobalValues.numberOfPlayers == 2)
            {
                if ((playerManager.players[0].HitBox.Intersects(packageManager.dropZone) || playerManager.players[1].HitBox.Intersects(packageManager.dropZone)) && currentKeyboard.IsKeyDown(Keys.B) && !oldKeyboard.IsKeyDown(Keys.B))
                {
                    currentGameState = GameState.LevelUp;
                }
                foreach (Player p in playerManager.players)
                {
                    if (p.HitBox.Intersects(packageManager.dropZone) && p.GamePadState.IsButtonDown(Buttons.X))
                    {
                        currentGameState = GameState.LevelUp;
                    }
                }
            }
            else
            {
                if (playerManager.players[0].HitBox.Intersects(packageManager.dropZone) && currentKeyboard.IsKeyDown(Keys.B) && !oldKeyboard.IsKeyDown(Keys.B))
                {
                    currentGameState = GameState.LevelUp;
                }
                foreach (Player p in playerManager.players)
                {
                    if (p.HitBox.Intersects(packageManager.dropZone) && p.GamePadState.IsButtonDown(Buttons.X))
                    {
                        currentGameState = GameState.LevelUp;
                    }
                }
            }
        }
    }
}