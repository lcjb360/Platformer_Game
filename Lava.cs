using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Lava
    {
        public Rectangle window = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        
        public Sprite Default_Lava;
        public Vector2 Position;
        public int Length;
        public int Width = 60;
        public int Height = 12;

        public Lava(Texture2D texture, Vector2 position, int length)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Lava = new Sprite(texture, 0, 76, Width, Height);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Length = (int)((float)length * w_ratio);
            Width = (int)((float)Width * w_ratio);
            Height = (int)((float)Height * h_ratio);
        }
        public Lava(Texture2D texture, Vector2 position, int length, int height)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Lava = new Sprite(texture, 0, 76, Width, Height);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Length = (int)((float)length * w_ratio);
            Width = (int)((float)Width * w_ratio) + 1;
            Height = (int)((float)height * h_ratio);
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
