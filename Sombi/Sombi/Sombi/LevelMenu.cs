using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    class LevelMenu
    {
        public Rectangle[,] hitbox;
        public int numberOfUpgrades;
        public int numberOfRows;

        public LevelMenu()
        {
            numberOfRows = 6;
            numberOfUpgrades = 6;
            hitbox = new Rectangle[numberOfUpgrades, numberOfRows];
            CreatGraphicMatrix();
        }

        private void CreatGraphicMatrix()
        {
            int y = 100;
            int x = 200;
            for (int i = 0; i < numberOfUpgrades; i++)
            {
                for (int k = 0; k < numberOfRows; k++)
                {
                    if (i >= 3)
                    {
                        hitbox[i, k] = new Rectangle(x + 200 * (i + 1), y * (k + 1), 110, 54);
                    }
                    else
                    {
                        hitbox[i, k] = new Rectangle(x * (i + 1), y * (k + 1), 110, 54);
                    }
                    hitbox[i, k].X += 100;
                    hitbox[i, k].Y += 100;
                }
            }
        }
    }
}
