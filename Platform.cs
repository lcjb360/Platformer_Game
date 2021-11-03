using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Platform
    {
        public Rectangle window = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        
        public Sprite Default_Platform;
        public Vector2 Position;
        public Vector2 Start_Position;
        public Vector2 Destination;
        public Vector2 Start_Destination;
        public float Width;
        public float Start_Width;
        public float Height;
        public float Start_Height;
        public bool Moving;
        public bool Flashing;
        public bool Weak;
        public bool Touched = false;
        public int ticks;
        public bool Appear = true;
        public bool Start_Appear;

        public Platform(Texture2D texture, Vector2 position, float width, float height, bool moving, Vector2 destination, bool flashing, bool weak, bool appear)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Platform = new Sprite(texture, 0, 60, 59, 15);
            Position = new Vector2(position.X*w_ratio, position.Y*h_ratio);
            Start_Position = Position;
            Width = (float)width * w_ratio;
            Start_Width = Width;
            Start_Height = (float)height * h_ratio;
            Height = Start_Height;
            Moving = moving;
            Destination = new Vector2(destination.X*w_ratio, destination.Y*h_ratio);
            Start_Destination = Destination;
            Flashing = flashing;
            Weak = weak;
            Appear = appear;
            Start_Appear = appear;
        }

        public Platform(Texture2D texture, Vector2 position, float width, float height, bool moving, Vector2 destination)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Platform = new Sprite(texture, 0, 60, 59, 15);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Start_Position = Position;
            Width = (float)width * w_ratio;
            Height = (float)height * h_ratio;
            Moving = moving;
            Destination = new Vector2(destination.X * w_ratio, destination.Y * h_ratio);
            Start_Destination = Destination;
        }

        public Platform(Texture2D texture, Vector2 position, float width, float height)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Platform = new Sprite(texture, 0, 60, 59, 15);
            Position = new Vector2(position.X * w_ratio + 1, position.Y * h_ratio);
            Width = width * w_ratio + 1;
            Height = height * h_ratio + 1;
        }

        public void Update(GameTime gameTime)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float margin = 7 * w_ratio;
            if (Moving)
            {
                if ((Position.X - margin <= Start_Destination.X && Start_Destination.X <= Position.X + margin) && (Position.Y - margin <= Start_Destination.Y && Start_Destination.Y <= Position.Y + margin) && Destination != Start_Position)
                {
                    Destination = Start_Position;
                }
                if (Position == Start_Position && Start_Position == Destination)
                {
                    Destination = Start_Destination;
                }
                Vector2 travelling = Destination - Position;
                travelling.Normalize();
                Position += 8 * travelling;
            }
            if (Flashing)
            {
                ticks++;
                if (ticks >= 50)
                {
                    Appear = !Appear;
                    ticks = 0;
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
                    ticks++;
                    if (ticks >= 50)
                    {
                        Appear = false;
                        ticks = 0;
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
                Default_Platform.Draw(spriteBatch, Position, (int)Width, (int)Height);
            }
        }
    }
}
