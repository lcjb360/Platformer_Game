using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Spike
    {
        public Rectangle window = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public Sprite Default_Spike;
        public Vector2 Position;
        public int Length;
        public int Width = 9;
        public int Height = 10;

        public Spike(Texture2D texture, Vector2 position, int length)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Spike = new Sprite(texture, 0, 92, Width, Height);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Length = (int)((float)length * w_ratio);
            Width = (int)((float)Width * w_ratio);
            Height = (int)((float)Height * h_ratio);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            for (int x = (int)Position.X; x < ((int)Position.X+Length); x+=Width)
            {
                Default_Spike.Draw(spriteBatch, new Vector2(x , Position.Y), Width, Height);
            }
        }
    }
}
