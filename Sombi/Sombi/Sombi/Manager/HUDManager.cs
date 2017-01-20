using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Sombi
{
    class HUDManager
    {
        KeyboardState currentKeyboard;
        KeyboardState oldKeyboard;
        Rectangle[,] sourceRect;
        List<Player> players;
        Vector2 hudPos;

        int numberOfPackages = 3;
        int weaponRotationIndex = 0;
        int weaponRotationIndex2 = 0;

        public HUDManager(List<Player> players)
        {
            this.players = players;
            hudPos = new Vector2(0, 0);
            sourceRect = new Rectangle[numberOfPackages, 1];

        }

        public void Update(GameTime gameTime, Vector2 cameraPos)
        {
            hudPos.X = cameraPos.X;
            hudPos.Y = cameraPos.Y;
            WeaponRotation();

        }
        public void WeaponRotation()
        {
            oldKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();


            if (currentKeyboard.IsKeyDown(Keys.E) && !oldKeyboard.IsKeyDown(Keys.E))
            {
                weaponRotationIndex++;           //För player 1
                if (weaponRotationIndex > 2)
                {
                    weaponRotationIndex = 0;
                }
            }
            if (currentKeyboard.IsKeyDown(Keys.Q) && !oldKeyboard.IsKeyDown(Keys.Q))
            {
                weaponRotationIndex--;
                if (weaponRotationIndex < 0)
                {
                    weaponRotationIndex = 2;
                }
            }
            if (players[0].GamePadState.IsButtonDown(Buttons.RightShoulder) && !players[0].OldGamePadState.IsButtonDown(Buttons.RightShoulder))
            {
                weaponRotationIndex++;
                if (weaponRotationIndex > 2)
                {
                    weaponRotationIndex = 0;
                }
            }
            if (players[0].GamePadState.IsButtonDown(Buttons.LeftShoulder) && !players[0].OldGamePadState.IsButtonDown(Buttons.LeftShoulder))
            {
                weaponRotationIndex--;
                if (weaponRotationIndex < 0)
                {
                    weaponRotationIndex = 2;
                }
            }
            if (GlobalValues.numberOfPlayers == 2)
            {


                if (players[1].GamePadState.IsButtonDown(Buttons.RightShoulder) && !players[1].OldGamePadState.IsButtonDown(Buttons.RightShoulder))
                {
                    weaponRotationIndex2++;
                    if (weaponRotationIndex2 > 2)
                    {
                        weaponRotationIndex2 = 0;
                    }
                }
                if (players[1].GamePadState.IsButtonDown(Buttons.LeftShoulder) && !players[1].OldGamePadState.IsButtonDown(Buttons.LeftShoulder))
                {
                    weaponRotationIndex2--;
                    if (weaponRotationIndex2 < 0)
                    {
                        weaponRotationIndex2 = 2;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, int numberOfPlayers)
        {
            spriteBatch.Draw(TextureLibrary.player1ScoreHud, new Vector2(hudPos.X + 0, hudPos.Y + 0), Color.White);
            spriteBatch.Draw(TextureLibrary.weaponWheel[weaponRotationIndex], new Vector2(hudPos.X + 0, hudPos.Y + GlobalValues.screenBounds.Y - TextureLibrary.weaponHud.Height), Color.White * 0.8f);
            spriteBatch.DrawString(TextureLibrary.HudText, "Health: " + players[0].health, new Vector2(hudPos.X + 15, hudPos.Y + 10), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, "Cash: " + players[0].cash, new Vector2(hudPos.X + 15, hudPos.Y + 25), Color.Black);

            for (int i = 0; i < numberOfPackages; i++)
            {
                //spriteBatch.Draw(TextureLibrary.smallPackage, sourceRect[i,0]+hudPos.X, Color.White);
                spriteBatch.Draw(TextureLibrary.smallPackage, new Vector2(sourceRect[i, 0].X + sourceRect[i, 0].Width + hudPos.X + 9, sourceRect[i, 0].Y + hudPos.Y + 79), (Color.White * (0.3f + (PackageManager.numberOfCarriedPackages))));
                spriteBatch.Draw(TextureLibrary.smallPackage, new Vector2(sourceRect[i, 0].X + sourceRect[i, 0].Width + hudPos.X + 38, sourceRect[i, 0].Y + hudPos.Y + 79), (Color.White * (0.3f + (PackageManager.numberOfCarriedPackages / 2))));
                spriteBatch.Draw(TextureLibrary.smallPackage, new Vector2(sourceRect[i, 0].X + sourceRect[i, 0].Width + hudPos.X + 67, sourceRect[i, 0].Y + hudPos.Y + 79), (Color.White * (0.3f + (PackageManager.numberOfCarriedPackages / 3))));
                if (numberOfPlayers == 2)
                {
                    spriteBatch.Draw(TextureLibrary.player2ScoreHud, new Vector2(hudPos.X + GlobalValues.screenBounds.X - TextureLibrary.player2ScoreHud.Width, hudPos.Y + 0), Color.White);
                    spriteBatch.Draw(TextureLibrary.weaponWheel[weaponRotationIndex2], new Vector2(hudPos.X + GlobalValues.screenBounds.X - TextureLibrary.weaponHud.Width, hudPos.Y + GlobalValues.screenBounds.Y - TextureLibrary.weaponHud.Height), Color.White * 0.8f);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Health: " + players[1].health, new Vector2(hudPos.X + GlobalValues.screenBounds.X - 165, hudPos.Y + 10), Color.Black);
                    spriteBatch.DrawString(TextureLibrary.HudText, "Cash: " + players[1].cash, new Vector2(hudPos.X + GlobalValues.screenBounds.X - 165, hudPos.Y + 25), Color.Black);

                    //spriteBatch.Draw(TextureLibrary.smallPackage, sourceRect[i,0]+hudPos.X, Color.White);
                    spriteBatch.Draw(TextureLibrary.smallPackage, new Vector2(sourceRect[i, 0].X + sourceRect[i, 0].Width + hudPos.X + (GlobalValues.screenBounds.X - TextureLibrary.player2ScoreHud.Width) + 87, sourceRect[i, 0].Y + hudPos.Y + 79), (Color.White * (0.6f + PackageManager.numberOfCarriedPackages)));
                    spriteBatch.Draw(TextureLibrary.smallPackage, new Vector2(sourceRect[i, 0].X + sourceRect[i, 0].Width + hudPos.X + (GlobalValues.screenBounds.X - TextureLibrary.player2ScoreHud.Width) + 116, sourceRect[i, 0].Y + hudPos.Y + 79), (Color.White * (0.6f + PackageManager.numberOfCarriedPackages / 2)));
                    spriteBatch.Draw(TextureLibrary.smallPackage, new Vector2(sourceRect[i, 0].X + sourceRect[i, 0].Width + hudPos.X + (GlobalValues.screenBounds.X - TextureLibrary.player2ScoreHud.Width) + 145, sourceRect[i, 0].Y + hudPos.Y + 79), (Color.White * (0.6f + PackageManager.numberOfCarriedPackages / 3)));

                }
            }
            spriteBatch.DrawString(TextureLibrary.billBoardText, "" + PackageManager.deliveredPackages, new Vector2(1365, 1454), GlobalValues.billBoardColor);
            spriteBatch.DrawString(TextureLibrary.billBoardText, "" + HighscoreManager.kills, new Vector2(1320, 1478), GlobalValues.billBoardColor);
        }
    }
}