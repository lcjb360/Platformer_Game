﻿using Microsoft.Xna.Framework;
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
        public bool Destructible = false;
        public List<Rectangle> parts = new List<Rectangle>();
        public Color Colour = Color.White;

        public Wall(Texture2D texture, Vector2 position, float width, float height)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Wall = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, (position.Y * h_ratio));
            Width = width * w_ratio;
            Height = height * h_ratio + 1;
        }

        public Wall(Texture2D texture, Vector2 position, float width, float height, Color color)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Wall = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, (position.Y * h_ratio));
            Width = width * w_ratio;
            Height = height * h_ratio + 1;
            Colour = color;
        }

        public Wall(Texture2D texture, Vector2 position, float width, float height, bool destructable)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Wall = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, (position.Y * h_ratio) - ((float)7 * h_ratio));
            Width = width * w_ratio;
            Height = height * h_ratio + 1;
            Height += (float)7 * h_ratio;
            Destructible = destructable;
            if (destructable)
            {
                for (float x = Position.X; x < (Position.X + Width); x += (((float)9) * w_ratio))
                {
                    for (float y = Position.Y; y < (Position.Y + Height); y += (((float)9) * h_ratio))
                    {
                        parts.Add(new Rectangle((int)(x * w_ratio), (int)(y*h_ratio), (int)(((float)9) * w_ratio), (int)(((float)9) * h_ratio)));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            if (Destructible)
            {
                foreach (Rectangle rectangle in parts)
                {
                    Default_Wall.Draw(spriteBatch, new Vector2(rectangle.X, rectangle.Y), rectangle.Width, rectangle.Width, Color.Red);
                }
            }
            else
            {
                Default_Wall.Draw(spriteBatch, Position, (int)Width, (int)Height, Colour);
            }
        }
    }
}
