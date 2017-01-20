using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sombi
{
    public class Grid
    {

        public static Tile[,] grid;
        public static bool menu = true;
        public static List<Vector2> spawnPoints;
        public static List<Vector2> packageSpawnPoints;

        public static void CreateGridFactory()
        {
            spawnPoints = new List<Vector2>();
            packageSpawnPoints = new List<Vector2>();
            if (!menu)
            {

                StreamReader sr = new StreamReader(@"Content\Testmap.txt");
                List<string> stringList = new List<string>();

                while (!sr.EndOfStream)
                {
                    stringList.Add(sr.ReadLine());
                }
                grid = new Tile[(int)GlobalValues.gridSize.X, (int)GlobalValues.gridSize.Y];
                for (int i = 0; i < GlobalValues.gridSize.Y; i++)
                {
                    for (int k = 0; k < GlobalValues.gridSize.X; k++)
                    {
                        switch (stringList[i][k])
                        {
                            case '.':
                                grid[k, i] = new Tile(new Vector2(k, i), true);
                                break;
                            case 'W':
                                grid[k, i] = new Tile(new Vector2(k, i), false);
                                break;
                            case 'S':
                                grid[k, i] = new Tile(new Vector2(k, i), true);
                                spawnPoints.Add(new Vector2(k * 50, i * 50));
                                break;
                            case 'P':
                                grid[k, i] = new Tile(new Vector2(k, i), true);
                                packageSpawnPoints.Add(new Vector2(k * 50, i * 50));
                                break;
                        }
                    }
                }
            }
            else
            {
                StreamReader sr = new StreamReader(@"Content\Menumap.txt");
                List<string> stringList = new List<string>();

                while (!sr.EndOfStream)
                {
                    stringList.Add(sr.ReadLine());
                }
                grid = new Tile[38, 20];
                for (int i = 0; i < 20; i++)
                {
                    for (int k = 0; k < 38; k++)
                    {
                        switch (stringList[i][k])
                        {
                            case '0':
                                grid[k, i] = new Tile(new Vector2(k, k), true);
                                break;
                            case '1':
                                grid[k, i] = new Tile(new Vector2(k, i), false);
                                break;
                        }
                    }
                }
            }

        }
        public static void SetCurrentTilePassable(bool b, Vector2 index)
        {
            grid[(int)index.X, (int)index.Y].passable = b;
        }
        public static void SetCurrentTileHasZombie(bool b, Vector2 index)
        {
            grid[(int)index.X, (int)index.Y].hasZombie = b;
        }
    }
}
