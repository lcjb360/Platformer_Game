using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Spike
    {
        public Sprite Default_Spike;
        public Vector2 Position;
        public int Length;
        public int Width = 9;
        public int Height = 10;

        public Spike(Texture2D texture, Vector2 position, int length)
        {
            Default_Spike = new Sprite(texture, 0, 92, Width, Height);
            Position = position;
            Length = length;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int x = (int)Position.X; x < ((int)Position.X+Length); x+=Width)
            {
                Default_Spike.Draw(spriteBatch, new Vector2(x, Position.Y), Width, Height);
            }
        }
    }
}
