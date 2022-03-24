using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Platform : Visible_Object
    {
        public Vector2 Start_Position;
        public Vector2 Destination;
        public Vector2 Start_Destination;
        public float Start_Width;
        public float Start_Height;
        public bool Moving;
        public bool Flashing;
        public bool Weak;
        public bool Touched = false;
        public int Ticks;
        public int Moving_ticks = 0;
        public bool Appear = true;
        public bool Start_Appear;
        public Color Colour = Color.White;

        public Platform(Texture2D texture, Vector2 position, float width, float height, bool moving, Vector2 destination, bool flashing, bool weak, bool appear)
        {
            Default_Sprite = new Sprite(texture, 0, 60, 59, 15);
            Position = new Vector2(position.X, position.Y);
            Start_Position = Position;
            Width = width;
            Start_Width = Width;
            Start_Height = height;
            Height = Start_Height;
            Moving = moving;
            Destination = new Vector2(destination.X, destination.Y);
            Start_Destination = Destination;
            Flashing = flashing;
            Weak = weak;
            Appear = appear;
            Start_Appear = appear;
        }

        public Platform(Texture2D texture, Vector2 position, float width, float height, bool moving, Vector2 destination)
        {
            Default_Sprite = new Sprite(texture, 0, 60, 59, 15);
            Position = new Vector2(position.X, position.Y);
            Start_Position = Position;
            Width = width;
            Height = height;
            Moving = moving;
            Destination = new Vector2(destination.X, destination.Y);
            Start_Destination = Destination;
        }

        public Platform(Texture2D texture, Vector2 position, float width, float height)
        {
            Default_Sprite = new Sprite(texture, 0, 60, 59, 15);
            Position = new Vector2(position.X + 1, position.Y);
            Width = width;
            Height = height;
        }

        public Platform(Texture2D texture, Vector2 position, float width, float height, Color color)
        {
            Default_Sprite = new Sprite(texture, 0, 60, 59, 15);
            Position = new Vector2(position.X + 1, position.Y);
            Width = width;
            Height = height;
            Colour = color;
        }

        public void Update(GameTime gameTime)
        {
            float Margin = 7;
            if (Moving)
            {
                Moving_ticks++;
                if ((Position.X - Margin <= Start_Destination.X && Start_Destination.X <= Position.X + Margin) && (Position.Y - Margin <= Start_Destination.Y && Start_Destination.Y <= Position.Y + Margin) && Destination != Start_Position && Moving_ticks > 2)
                {
                    Destination = Start_Position;
                    Moving_ticks = 0;
                }
                if (Position == Start_Position && Start_Position == Destination && Moving_ticks > 2)
                {
                    Destination = Start_Destination;
                    Moving_ticks = 0;
                }
                Vector2 Travelling = Destination - Position;
                Travelling.Normalize();
                if (Travelling.X == 0)
                {
                    Position += 8 * Travelling;
                }
                else
                {
                    Position += 8 * Travelling;
                }
            }
            if (Flashing)
            {
                Ticks++;
                if (Ticks >= 50)
                {
                    Appear = !Appear;
                    Ticks = 0;
                }
                if (!Appear)
                {
                    Width = 0;
                    Height = 0;
                }
                else
                {
                    Height = Start_Height;
                    Width = Start_Width;
                }
            }
            if (Weak)
            {
                if (Appear)
                {
                    Height = Start_Height;
                    Width = Start_Width;
                }
                if (Touched)
                {
                    Ticks++;
                    if (Ticks >= 50)
                    {
                        Appear = false;
                        Ticks = 0;
                    }
                    if (!Appear)
                    {
                        Width = 0;
                        Height = 0;
                    }
                    else
                    {
                        Height = Start_Height;
                        Width = Start_Width;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Appear)
            {
                Default_Sprite.Draw(spriteBatch, Position, (int)Width, (int)Height, Colour);
            }
        }
    }
}
