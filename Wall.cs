﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Wall
    {
        public Sprite Default_Wall;
        public Vector2 Position;
        public int Width;
        public int Height;

        public Wall(Texture2D texture, Vector2 position, int width, int height)
        {
            Default_Wall = new Sprite(texture, 0, 60, 59, 15);
            Position = position;
            Width = width;
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Wall.Draw(spriteBatch, Position, Width, Height);
        }
    }
}