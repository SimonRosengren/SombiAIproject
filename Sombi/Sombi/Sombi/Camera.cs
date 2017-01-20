using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sombi
{
    public class Camera
    {
        public Vector2 position;
        //Matrix viewMatrix;

        public Camera(int viewTileSize)
        {
            ViewTileSize = viewTileSize;            
        }

        public Matrix ViewMatrix { get; set; }
        public int ViewTileSize { get; set; }

        public void Update(Vector2 playerOnePos, Vector2 playerTwoPos)
        {
            MultiPlayerCamera(playerOnePos, playerTwoPos);

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }

        public void Update(Vector2 playerOnePos)
        {
            SinglePlayerCamera(playerOnePos);

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }

        private void SinglePlayerCamera(Vector2 playerOnePos)
        {
            position.X = playerOnePos.X - (GlobalValues.cameraBounds.X / 2);
            position.Y = playerOnePos.Y - (GlobalValues.cameraBounds.Y / 2);

            if (position.X < 0)
            {
                position.X = 0;
            }
            else if (position.X > GlobalValues.TILE_SIZE * GlobalValues.gridSize.X - GlobalValues.cameraBounds.X)
            {
                position.X = GlobalValues.TILE_SIZE * GlobalValues.gridSize.X - GlobalValues.cameraBounds.X;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y > GlobalValues.TILE_SIZE * GlobalValues.gridSize.Y - GlobalValues.cameraBounds.Y)
            {
                position.Y = GlobalValues.TILE_SIZE * GlobalValues.gridSize.Y - GlobalValues.cameraBounds.Y;
            }
        }

        private void MultiPlayerCamera(Vector2 playerOnePos, Vector2 playerTwoPos)
        {
            position.X = (playerOnePos.X + playerTwoPos.X) / 2 - (GlobalValues.cameraBounds.X / 2);
            position.Y = (playerOnePos.Y + playerTwoPos.Y) / 2 - (GlobalValues.cameraBounds.Y / 2);

            if (position.X < 0)
            {
                position.X = 0;
            }
            else if (position.X > GlobalValues.TILE_SIZE * GlobalValues.gridSize.X - GlobalValues.cameraBounds.X)
            {
                position.X = GlobalValues.TILE_SIZE * GlobalValues.gridSize.X - GlobalValues.cameraBounds.X;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y > GlobalValues.TILE_SIZE * GlobalValues.gridSize.Y - GlobalValues.cameraBounds.Y)
            {
                position.Y = GlobalValues.TILE_SIZE * GlobalValues.gridSize.Y - GlobalValues.cameraBounds.Y;
            }
        }
    }
}

