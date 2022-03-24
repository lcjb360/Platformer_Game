using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Sprite
    {
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
