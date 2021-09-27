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
        public Vector2 Start_Position;
        public Vector2 Destination;
        public Vector2 Start_Destination;
        public int Width;
        public int Start_Width;
        public int Height;
        public int Start_Height;
        public bool Moving;
        public bool Flashing;
        public bool Weak;
        public int ticks;
        public bool Appear = true;
        public bool Start_Appear;

        public Platform(Texture2D texture, Vector2 position, int width, int height, bool moving, Vector2 destination, bool flashing, bool weak, bool appear)
        {
            Default_Platform = new Sprite(texture, 0, 60, 59, 15);
            Position = position;
            Start_Position = position;
            Width = width;
            Start_Width = width;
            Start_Height = height;
            Height = height;
            Moving = moving;
            Destination = destination;
            Start_Destination = destination;
            Flashing = flashing;
            Weak = weak;
            Appear = appear;
            Start_Appear = appear;
        }

        public Platform(Texture2D texture, Vector2 position, int width, int height, bool moving, Vector2 destination)
        {
            Default_Platform = new Sprite(texture, 0, 60, 59, 15);
            Position = position;
            Start_Position = position;
            Width = width;
            Height = height;
            Moving = moving;
            Destination = destination;
            Start_Destination = destination;
        }

        public Platform(Texture2D texture, Vector2 position, int width, int height)
        {
            Default_Platform = new Sprite(texture, 0, 60, 59, 15);
            Position = position;
            Width = width;
            Height = height;
        }

        public void Update(GameTime gameTime)
        {
            
            if (Moving)
            {
                if ((Position.X - 5 <= Start_Destination.X && Start_Destination.X <= Position.X + 5) && (Position.Y - 5 <= Start_Destination.Y && Start_Destination.Y <= Position.Y + 5) && Destination != Start_Position)
                {
                    Destination = Start_Position;
                }
                if (Position == Start_Position && Start_Position == Destination)
                {
                    Destination = Start_Destination;
                }
                Vector2 travelling = Destination - Position;
                travelling.Normalize();
                Position += 5 * travelling;
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
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Appear)
            {
                Default_Platform.Draw(spriteBatch, Position, Width, Height);
            }
        }
    }
}
