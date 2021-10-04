using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Wall
    {
        public Rectangle window = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public Sprite Default_Wall;
        public Vector2 Position;
        public float Width;
        public float Height;

        public Wall(Texture2D texture, Vector2 position, float width, float height)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Wall = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Width = width * w_ratio;
            Height = height * h_ratio;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Wall.Draw(spriteBatch, Position, (int)Width, (int)Height);
        }
    }
}
