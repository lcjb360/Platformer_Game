using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Platform
    {
        public Sprite Default_Platform;
        public Vector2 Position;
        public int Width;
        public int Height;

        public Platform(Texture2D texture, Vector2 position, int width, int height)
        {
            Default_Platform = new Sprite(texture, 0, 60, 59, 15);
            Position = position;
            Width = width;
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
           Default_Platform.Draw(spriteBatch, Position, Width, Height);
        }
    }
}
