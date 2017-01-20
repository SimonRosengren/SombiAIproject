using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sombi
{
    public class GlobalValues
    {


        public const int TILE_SIZE = 50;
        public static Vector2 gridSize = new Vector2(114, 90);
        public static int numberOfPlayers;
        public static int difficultyLevel = 1;
        public static Vector2 PLAYER_ONE_START_POS = new Vector2(1490, 1630);
        public static Vector2 PLAYER_TWO_START_POS = new Vector2(1900, 1630);

        public static Random rnd = new Random();
        public static Rectangle windowBounds = new Rectangle(0, 0, (int)gridSize.X * TILE_SIZE, (int)gridSize.Y * TILE_SIZE);
        public static Vector2 cameraBounds = new Vector2(1900, 1000);
        public static Vector2 screenBounds = new Vector2(1900,1000);
        public static Color billBoardColor = new Color(225, 229, 38, 255);

        
    }
}
