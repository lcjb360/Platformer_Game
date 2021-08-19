using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    class Player
    {
        public Sprite Stationary_Right_Sprite;
        public Sprite Stationary_Left_Sprite;
        public Sprite Moving_Right_Sprite;
        public Sprite Moving_Left_Sprite;
        public Sprite Current_Sprite;
        public Vector2 Position;
        public int Width;
        public int Height;
        public Vector2 Velocity;

        public Player(Texture2D texture, Vector2 position)
        {
            Stationary_Right_Sprite = new Sprite(texture, 0, 0, 30, 60);
            Stationary_Left_Sprite = new Sprite(texture, 0, 0, 30, 60);
            Moving_Right_Sprite = new Sprite(texture, 30, 0, 30, 60);
            Moving_Left_Sprite = new Sprite(texture, 30, 0, 30, 60);
            Current_Sprite = Stationary_Right_Sprite;
            Width = Current_Sprite.Width;
            Height = Current_Sprite.Height;
            Position = position;
        }

        public float Y_of_platform;
        private bool OnPlatform(List<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                if (Position.Y + Height >= platform.Position.Y && Position.Y + Height <= platform.Position.Y + platform.Height)
                {
                    if (Position.X + Width >= platform.Position.X && Position.X <= platform.Position.X + platform.Width)
                    {
                        Y_of_platform = platform.Position.Y;
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Current_Sprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime, List<Platform> platforms, int screen_height)
        {
            //Movement Control
            if ((Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) && Velocity.X > -10)
            {
                Velocity.X -= 1;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)) && Velocity.X < 10)
            {
                Velocity.X += 1;
            }
            if ((Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.D)) && 
                (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.A)) && Velocity.X != 0)
            {
                if (Velocity.X > 0)
                {
                    Velocity.X -= 1;
                }
                if (Velocity.X < 0)
                {
                    Velocity.X += 1;
                }
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.W)) && OnPlatform(platforms))
            {
                Velocity.Y = -10;
            }
            Position += Velocity;
            if (!OnPlatform(platforms) && Velocity.Y < 5)
            {
                Velocity.Y += 1;
            }
            if (OnPlatform(platforms))
            {
                Velocity.Y = 0;
                Position.Y = Y_of_platform - Height;
            }
            if (Position.Y > screen_height)
            {
                Position.Y = 0;
            }

            //Sprite Control
            if (Velocity.X == 0)
            {
                if (Current_Sprite == Moving_Right_Sprite)
                {
                    Current_Sprite = Stationary_Right_Sprite;
                }
                if (Current_Sprite == Moving_Left_Sprite)
                {
                    Current_Sprite = Stationary_Left_Sprite;
                }
            }
            if (Velocity.X > 0)
            {
                Current_Sprite = Moving_Right_Sprite;
            }
            if (Velocity.X < 0)
            {
                Current_Sprite = Moving_Left_Sprite;
            }
        }
    }
}
