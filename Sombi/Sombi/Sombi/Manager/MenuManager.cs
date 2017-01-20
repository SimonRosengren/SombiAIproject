using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class MenuManager
    {
        Menu menu;
        List<Player> players;
        public bool start = false;
        public bool settings = false;
        public bool highscore = false;
        public bool exit = false;
        float fadePercentage = 1;
        public int numberOfPlayers;

        public MenuManager(List<Player> players)
        {
            menu = new Menu();
            this.players = players;
        }

        public void Update(GameTime gameTime)
        {
            if (fadePercentage >= 0)
            {
                fadePercentage -= 0.005f;
            }
            else
            {
                fadePercentage = 0;
            }
            CheckStart(gameTime);
            CheckExit(gameTime);
            CheckSettings(gameTime);
            CheckHighscore(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureLibrary.startButton, menu.startRect, Color.White);
            spriteBatch.Draw(TextureLibrary.settingButton, menu.settingRect, Color.White);
            spriteBatch.Draw(TextureLibrary.highscoreButton, menu.highscoreRect, Color.White);
            spriteBatch.Draw(TextureLibrary.exitButton, menu.exitRect, Color.White);
            spriteBatch.Draw(TextureLibrary.logoTex, menu.logoRect, Color.White);
            Color fadeColor = Color.Black;
            spriteBatch.Draw(TextureLibrary.fadeScreenTex, Vector2.Zero, fadeColor * fadePercentage);
        }

        private void CheckStart(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                numberOfPlayers = 2;
                start = true;
                Grid.menu = false;
                Grid.CreateGridFactory();
            }

            for (int i = 0; i < players.Count; i++)
            {
                if (players[0].GamePadState.IsButtonDown(Buttons.A) && players[0].HitBox.Intersects(menu.startRect) && !players[1].HitBox.Intersects(menu.startRect))
                {
                    numberOfPlayers = 1;
                    fadePercentage += 0.03f;

                    if (fadePercentage > 1)
                    {
                        start = true;
                        Grid.menu = false;
                        Grid.CreateGridFactory();
                        fadePercentage = 1;
                    }
                }

                else if (players[0].GamePadState.IsButtonDown(Buttons.A) && players[0].HitBox.Intersects(menu.startRect) && players[1].HitBox.Intersects(menu.startRect))
                {
                    numberOfPlayers = 2;
                    fadePercentage += 0.03f;

                    if (fadePercentage > 1)
                    {
                        start = true;
                        Grid.menu = false;
                        Grid.CreateGridFactory();
                        fadePercentage = 1;
                    }

                }
                GlobalValues.numberOfPlayers = numberOfPlayers;
            }
        }

        private void CheckExit(GameTime gameTime)
        {
            foreach (Player player in players)
            {
                if (player.GamePadState.IsButtonDown(Buttons.A) && player.HitBox.Intersects(menu.exitRect))
                {
                    fadePercentage += 0.03f;

                    if (fadePercentage > 1)
                    {
                        exit = true;
                    }

                }
            }
        }

        private void CheckSettings(GameTime gameTime)
        {
            foreach (Player player in players)
            {
                if (player.GamePadState.IsButtonDown(Buttons.A) && player.HitBox.Intersects(menu.settingRect))
                {
                    fadePercentage += 0.03f;

                    if (fadePercentage > 1)
                    {
                        settings = true;
                    }

                }
            }
        }

        private void CheckHighscore(GameTime gameTime)
        {
            foreach (Player player in players)
            {
                if (player.GamePadState.IsButtonDown(Buttons.A) && player.HitBox.Intersects(menu.highscoreRect))
                {
                    fadePercentage += 0.03f;

                    if (fadePercentage > 1)
                    {
                        highscore = true;
                    }

                }
            }
        }

    }
}
