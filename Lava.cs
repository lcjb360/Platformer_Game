using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Lava
    {
        public Sprite Default_Lava;
        public Vector2 Position;
        public int Length;
        public int Width = 60;
        public int Height = 12;

        public Lava(Texture2D texture, Vector2 position, int length)
        {
            Default_Lava = new Sprite(texture, 0, 76, Width, Height);
            Position = position;
            Length = length;
        }
        public Lava(Texture2D texture, Vector2 position, int length, int height)
        {
            Default_Lava = new Sprite(texture, 0, 76, Width, Height);
            Position = position;
            Length = length;
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int x = (int)Position.X; x < ((int)Position.X + Length); x += Width)
            {
                Default_Lava.Draw(spriteBatch, new Vector2(x, Position.Y), Width, Height);
            }
        }
    }
}
