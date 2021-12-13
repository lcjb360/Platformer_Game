using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Sprite
    {
        public Matrix matrix = new Matrix(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
        public Rectangle window = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public Texture2D Texture { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Sprite(Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        //change x and y etc * ratio
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Vector2 bounds = new Vector2(matrix.Translation.X, matrix.Translation.Y);
            
            spriteBatch.Draw(Texture, position, new Rectangle(X, Y, Width, Height), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Texture, position, new Rectangle(X, Y, Width, Height), color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int width, int height)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(X, Y, Width, Height), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int width, int height, Color color)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(X, Y, Width, Height), color);
        }
    }
}
