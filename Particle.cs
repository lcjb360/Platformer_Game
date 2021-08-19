using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Particle
    {
        public Sprite Default_Particle;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public float Height;
        public float Width;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            Default_Particle = new Sprite(texture, 60, 0, 5, 5);
            Position = position;
            Velocity = velocity;
            Height = Default_Particle.Height;
            Width = Default_Particle.Width;
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

        public void Update(GameTime gameTime, List<Platform> platforms, List<Particle> particles, float screen_height)
        {

            if (!OnPlatform(platforms))
            {
                Velocity.Y += 1;
            }
            if (OnPlatform(platforms))
            {
                Velocity.Y = 0;
                if (Velocity.X > 0)
                {
                    Velocity.X -= 1;
                }
                if (Velocity.X < 0)
                {
                    Velocity.X += 1;
                }
                Position.Y = Y_of_platform - Height;
            }
            Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Particle.Draw(spriteBatch, Position);
        }
    }
}
