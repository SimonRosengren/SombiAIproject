using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class LevelMenuManager
    {
        LevelMenu levelMenu;
        Vector2 player1Active;
        Vector2 player2Active;
        KeyboardState currentKeyboard;
        KeyboardState oldKeyboard;
        int upgradeCost;

        public LevelMenuManager()
        {
            levelMenu = new LevelMenu();
            player1Active = new Vector2(0, 1);
            player2Active = new Vector2(3, 1);
            upgradeCost = 100;
        }

        public void Update(ref int shotgunLevelP1, ref int rifleLevelP1, ref int explosiveLevelP1, ref int shotgunLevelP2, ref int rifleLevelP2, ref int explosiveLevelP2, List<Player> players)
        {
            oldKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();
            MovePlayer1Active(players);          
            LevelUpPlayer1(ref shotgunLevelP1, ref rifleLevelP1, ref explosiveLevelP1, players);
            if (GlobalValues.numberOfPlayers == 2)
            {
                MovePlayer2Active(players);
                LevelUpPlayer2(ref shotgunLevelP2, ref rifleLevelP2, ref explosiveLevelP2, players);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, List<Player> players)
        {
            for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
            {
                for (int k = 0; k < levelMenu.numberOfRows; k++)
                {
                    spriteBatch.Draw(TextureLibrary.rifleLevel, levelMenu.hitbox[0, k], Color.White);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Level 1         ", new Vector2(levelMenu.hitbox[i, 1].X, levelMenu.hitbox[0, 0].Y + levelMenu.hitbox[i, k].Height), Color.Black);
                    spriteBatch.Draw(TextureLibrary.shotgunLevel, levelMenu.hitbox[1, k], Color.White);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Level 2     $100", new Vector2(levelMenu.hitbox[i, 1].X, levelMenu.hitbox[0, 1].Y + levelMenu.hitbox[i, k].Height), Color.Black);
                    spriteBatch.Draw(TextureLibrary.rocketLevel, levelMenu.hitbox[2, k], Color.White);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Level 3     $200", new Vector2(levelMenu.hitbox[i, 1].X, levelMenu.hitbox[0, 2].Y + levelMenu.hitbox[i, k].Height), Color.Black);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Level 4     $300", new Vector2(levelMenu.hitbox[i, 1].X, levelMenu.hitbox[0, 3].Y + levelMenu.hitbox[i, k].Height), Color.Black);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Level 5     $400", new Vector2(levelMenu.hitbox[i, 1].X, levelMenu.hitbox[0, 4].Y + levelMenu.hitbox[i, k].Height), Color.Black);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Level 6     $500", new Vector2(levelMenu.hitbox[i, 1].X, levelMenu.hitbox[0, 5].Y + levelMenu.hitbox[i, k].Height), Color.Black);
                    if (GlobalValues.numberOfPlayers == 2)
                    {
                        spriteBatch.Draw(TextureLibrary.rifleLevel, levelMenu.hitbox[3, k], Color.White);
                        spriteBatch.Draw(TextureLibrary.shotgunLevel, levelMenu.hitbox[4, k], Color.White);
                        spriteBatch.Draw(TextureLibrary.rocketLevel, levelMenu.hitbox[5, k], Color.White);
                    }
                }
            }
            shadowLockedUpgrades(spriteBatch, players);
            UnlockedUpgrades(spriteBatch, players);
            spriteBatch.Draw(TextureLibrary.sourceRectTex, levelMenu.hitbox[(int)player1Active.X, (int)player1Active.Y], Color.Black * 0.2f);
            if (GlobalValues.numberOfPlayers == 2)
            {
                spriteBatch.Draw(TextureLibrary.sourceRectTex, levelMenu.hitbox[(int)player2Active.X, (int)player2Active.Y], Color.Black * 0.2f);
            }
            else
            {
                spriteBatch.Draw(TextureLibrary.whiteBox, new Rectangle(950, 0, 950, 1000), Color.White);
            }
        }

        private void MovePlayer1Active(List<Player> players)
        {
            if (currentKeyboard.IsKeyDown(Keys.W) && !oldKeyboard.IsKeyDown(Keys.W) && (int)player1Active.Y > 0)
            {
                player1Active.Y -= 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.S) && !oldKeyboard.IsKeyDown(Keys.S) && (int)player1Active.Y < 5)
            {
                player1Active.Y += 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.A) && !oldKeyboard.IsKeyDown(Keys.A) && (int)player1Active.X > 0)
            {
                player1Active.X -= 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.D) && !oldKeyboard.IsKeyDown(Keys.D) && (int)player1Active.X < 2)
            {
                player1Active.X += 1;
            }
            if (players[0].GamePadState.IsButtonDown(Buttons.DPadUp) && players[0].OldGamePadState.IsButtonUp(Buttons.DPadUp) && (int)player1Active.Y > 0)
            {
                player1Active.Y -= 1;
            }
            if (players[0].GamePadState.IsButtonDown(Buttons.DPadLeft) && players[0].OldGamePadState.IsButtonUp(Buttons.DPadLeft) && (int)player1Active.X > 0)
            {
                player1Active.X -= 1;
            }
            if (players[0].GamePadState.IsButtonDown(Buttons.DPadRight) && players[0].OldGamePadState.IsButtonUp(Buttons.DPadRight) && (int)player1Active.X < 2)
            {
                player1Active.X += 1;
            }
            if (players[0].GamePadState.IsButtonDown(Buttons.DPadDown) && players[0].OldGamePadState.IsButtonUp(Buttons.DPadDown) && (int)player1Active.Y < 5)
            {
                player1Active.Y += 1;
            }
        }

        private void MovePlayer2Active(List<Player> players)
        {
            if (currentKeyboard.IsKeyDown(Keys.Up) && !oldKeyboard.IsKeyDown(Keys.Up) && (int)player2Active.Y > 0)
            {
                player2Active.Y -= 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.Down) && !oldKeyboard.IsKeyDown(Keys.Down) && (int)player2Active.Y < 5)
            {
                player2Active.Y += 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.Left) && !oldKeyboard.IsKeyDown(Keys.Left) && (int)player2Active.X > 3)
            {
                player2Active.X -= 1;
            }
            if (currentKeyboard.IsKeyDown(Keys.Right) && !oldKeyboard.IsKeyDown(Keys.Right) && (int)player2Active.X < 5)
            {
                player2Active.X += 1;
            }
            if (players[1].GamePadState.IsButtonDown(Buttons.DPadUp) && players[1].OldGamePadState.IsButtonUp(Buttons.DPadUp) && (int)player2Active.Y > 0)
            {
                player2Active.Y -= 1;
            }
            if (players[1].GamePadState.IsButtonDown(Buttons.DPadLeft) && players[1].OldGamePadState.IsButtonUp(Buttons.DPadLeft) && (int)player2Active.X > 3)
            {
                player2Active.X -= 1;
            }
            if (players[1].GamePadState.IsButtonDown(Buttons.DPadRight) && players[1].OldGamePadState.IsButtonUp(Buttons.DPadRight) && (int)player2Active.X < 5)
            {
                player2Active.X += 1;
            }
            if (players[1].GamePadState.IsButtonDown(Buttons.DPadDown) && players[1].OldGamePadState.IsButtonUp(Buttons.DPadDown) && (int)player2Active.Y < 5)
            {
                player2Active.Y += 1;
            }
        }

        private void LevelUpPlayer1(ref int shotgunLevel, ref int rifleLevel, ref int explosiveLevel, List<Player> players)
        {
            if (currentKeyboard.IsKeyDown(Keys.Enter) && !oldKeyboard.IsKeyDown(Keys.Enter) || players[0].GamePadState.IsButtonDown(Buttons.A))
            {
                switch ((int)player1Active.X)
                {
                    case 1:
                        if (!(player1Active.Y + 1 == 1))
                        {
                            if (players[0].cash >= (player1Active.Y + 1) * upgradeCost - upgradeCost && shotgunLevel == player1Active.Y)
                            {
                                shotgunLevel = (int)player1Active.Y + 1;
                                players[0].cash -= (int)(player1Active.Y + 1) * upgradeCost - upgradeCost;
                            }
                        }
                        break;
                    case 0:
                        if (!(player1Active.Y + 1 == 1))
                        {
                            if (players[0].cash >= (player1Active.Y + 1) * upgradeCost - upgradeCost && rifleLevel == player1Active.Y)
                            {
                                rifleLevel = (int)player1Active.Y + 1;
                                players[0].cash -= (int)(player1Active.Y + 1) * upgradeCost - upgradeCost;
                            }
                        }
                        break;
                    case 2:
                        if (!(player1Active.Y + 1 == 1))
                        {
                            if (players[0].cash >= (player1Active.Y + 1) * upgradeCost - upgradeCost && explosiveLevel == player1Active.Y)
                            {
                                explosiveLevel = (int)player1Active.Y + 1;
                                players[0].cash -= (int)(player1Active.Y + 1) * upgradeCost - upgradeCost;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void LevelUpPlayer2(ref int shotgunLevel, ref int rifleLevel, ref int explosiveLevel, List<Player> players)
        {
            if (currentKeyboard.IsKeyDown(Keys.Enter) && !oldKeyboard.IsKeyDown(Keys.Enter) || players[1].GamePadState.IsButtonDown(Buttons.A))
            {
                switch ((int)player2Active.X)
                {
                    case 4:
                        if (!(player2Active.Y + 1 == 1))
                        {
                            if (players[1].cash >= (player2Active.Y + 1) * upgradeCost - upgradeCost && shotgunLevel == player2Active.Y)
                            {
                                shotgunLevel = (int)player2Active.Y + 1;
                                players[1].cash -= (int)(player2Active.Y + 1) * upgradeCost - upgradeCost;
                            }
                        }
                        break;
                    case 3:
                        if (!(player2Active.Y + 1 == 1))
                        {
                            if (players[1].cash >= (player2Active.Y + 1) * upgradeCost - upgradeCost && rifleLevel == player2Active.Y)
                            {
                                rifleLevel = (int)player2Active.Y + 1;
                                players[1].cash -= (int)(player2Active.Y + 1) * upgradeCost - upgradeCost;
                            }
                        }
                        break;
                    case 5:
                        if (!(player2Active.Y + 1 == 1))
                        {
                            if (players[1].cash >= (player2Active.Y + 1) * upgradeCost - upgradeCost && explosiveLevel == player2Active.Y)
                            {
                                explosiveLevel = (int)player2Active.Y + 1;
                                players[1].cash -= (int)(player2Active.Y + 1) * upgradeCost - upgradeCost;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void shadowLockedUpgrades(SpriteBatch spriteBatch, List<Player> players)
        {
            for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
            {
                for (int k = players[0].rifleLevel + 1; k < levelMenu.numberOfUpgrades; k++)
                {
                    spriteBatch.Draw(TextureLibrary.lockedUpgrade, levelMenu.hitbox[0, k], Color.White);
                }
            }
            for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
            {
                for (int k = players[0].shotgunLevel + 1; k < levelMenu.numberOfUpgrades; k++)
                {
                    spriteBatch.Draw(TextureLibrary.lockedUpgrade, levelMenu.hitbox[1, k], Color.White);
                }
            }
            for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
            {
                for (int k = players[0].explosivesLevel + 1; k < levelMenu.numberOfUpgrades; k++)
                {
                    spriteBatch.Draw(TextureLibrary.lockedUpgrade, levelMenu.hitbox[2, k], Color.White);
                }
            }
            if (GlobalValues.numberOfPlayers == 2)
            {
                for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
                {
                    for (int k = players[1].rifleLevel + 1; k < levelMenu.numberOfUpgrades; k++)
                    {
                        spriteBatch.Draw(TextureLibrary.lockedUpgrade, levelMenu.hitbox[3, k], Color.White);
                    }
                }
                for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
                {
                    for (int k = players[1].shotgunLevel + 1; k < levelMenu.numberOfUpgrades; k++)
                    {
                        spriteBatch.Draw(TextureLibrary.lockedUpgrade, levelMenu.hitbox[4, k], Color.White);
                    }
                }
                for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
                {
                    for (int k = players[1].explosivesLevel + 1; k < levelMenu.numberOfUpgrades; k++)
                    {
                        spriteBatch.Draw(TextureLibrary.lockedUpgrade, levelMenu.hitbox[5, k], Color.White);
                    }
                }
            }
        }

        private void UnlockedUpgrades(SpriteBatch spriteBatch, List<Player>players)
        {
            for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
            {
                for (int k = players[0].rifleLevel - 1; k >= 0; k--)
                {
                    spriteBatch.Draw(TextureLibrary.rifleLevel, levelMenu.hitbox[0, k], Color.Black * 0.2f);
                }
            }
            for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
            {
                for (int k = players[0].shotgunLevel - 1; k >= 0; k--)
                {
                    spriteBatch.Draw(TextureLibrary.shotgunLevel, levelMenu.hitbox[1, k], Color.Black * 0.2f);
                }
            }
            for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
            {
                for (int k = players[0].explosivesLevel - 1; k >= 0; k--)
                {
                    spriteBatch.Draw(TextureLibrary.rocketLevel, levelMenu.hitbox[2, k], Color.Black * 0.2f);
                }
            }
            if (GlobalValues.numberOfPlayers == 2)
            {
                for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
                {
                    for (int k = players[1].rifleLevel - 1; k >= 0; k--)
                    {
                        spriteBatch.Draw(TextureLibrary.rifleLevel, levelMenu.hitbox[3, k], Color.Black * 0.2f);
                    }
                }
                for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
                {
                    for (int k = players[1].shotgunLevel - 1; k >= 0; k--)
                    {
                        spriteBatch.Draw(TextureLibrary.shotgunLevel, levelMenu.hitbox[4, k], Color.Black * 0.2f);
                    }
                }
                for (int i = 0; i < levelMenu.numberOfUpgrades; i++)
                {
                    for (int k = players[1].explosivesLevel - 1; k >= 0; k--)
                    {
                        spriteBatch.Draw(TextureLibrary.rocketLevel, levelMenu.hitbox[5, k], Color.Black * 0.2f);
                    }
                }
            }
        }
    }
}
