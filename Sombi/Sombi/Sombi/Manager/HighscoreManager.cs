using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sombi
{
    class HighscoreManager
    {
        public static int score;
        public static int kills;
        List<int> highScores;

        public List<int> HighScores
        {
            get { return highScores; }
            set { }
        }

        public HighscoreManager()
        {
            LoadContent();
        }

        private void LoadContent()
        {
            score = 0;
            highScores = new List<int>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[0].ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[1].ToString(), new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[2].ToString(), new Vector2(100, 300), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[3].ToString(), new Vector2(100, 400), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[4].ToString(), new Vector2(100, 500), Color.Black);
        }

        public void WriteScore()
        {
            string textScore = score.ToString();
            StreamWriter file = new StreamWriter("Highscore.txt",true);
            file.WriteLine(textScore);
            file.Close();
            SortList();
        }

        public void ReadScore()
        {
            StreamReader file = new StreamReader("Highscore.txt");
            while (!file.EndOfStream)
            {
                string test = file.ReadLine();
                int testInt = Int32.Parse(test);
                highScores.Add(testInt);
            }
            SortList();
            file.Close();
        }

        private void SortList()
        {
            highScores.Sort((a, b) => b.CompareTo(a));
        }
    }
}
