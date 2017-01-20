using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class Menu
    {

        public Rectangle startRect;
        public Rectangle settingRect;
        public Rectangle highscoreRect;
        public Rectangle exitRect;
        public Rectangle logoRect;

        public Menu()
        {
            startRect = new Rectangle((int)GlobalValues.screenBounds.X / 2 - TextureLibrary.startButton.Width / 2, (int)GlobalValues.screenBounds.Y / 2 - TextureLibrary.startButton.Height / 2 - 200, TextureLibrary.startButton.Width, TextureLibrary.startButton.Height);
            settingRect = new Rectangle((int)GlobalValues.screenBounds.X / 2 - TextureLibrary.settingButton.Width / 2, (int)GlobalValues.screenBounds.Y / 2 - TextureLibrary.settingButton.Height / 2, TextureLibrary.settingButton.Width, TextureLibrary.settingButton.Height);
            highscoreRect = new Rectangle((int)GlobalValues.screenBounds.X / 2 - TextureLibrary.highscoreButton.Width / 2, (int)GlobalValues.screenBounds.Y / 2 - TextureLibrary.highscoreButton.Height / 2 + 200, TextureLibrary.highscoreButton.Width, TextureLibrary.highscoreButton.Height);
            exitRect = new Rectangle((int)GlobalValues.screenBounds.X / 2 - TextureLibrary.exitButton.Width / 2, (int)GlobalValues.screenBounds.Y / 2 - TextureLibrary.exitButton.Height / 2 + 400, TextureLibrary.exitButton.Width, TextureLibrary.exitButton.Height);
            logoRect = new Rectangle((int)GlobalValues.screenBounds.X / 2 - TextureLibrary.logoTex.Width / 2, 40, TextureLibrary.logoTex.Width, TextureLibrary.logoTex.Height);
        }
    }
}
