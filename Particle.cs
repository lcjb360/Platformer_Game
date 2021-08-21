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
        public int id;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id)
        {
            Default_Particle = new Sprite(texture, 60, 0, 5, 5);
            Position = position;
            Velocity = velocity;
            Height = Default_Particle.Height;
            Width = Default_Particle.Width;
            id = particle_id;
        }


        private void HittingWall(List<Wall> walls)
        {
            Rectangle particle_edge = new Rectangle((int)(Position.X + Velocity.X - 2), (int)(Position.Y + Velocity.Y - 2), (int)Width + 2, (int)Height + 2);
            foreach (Wall wall in walls)
            {
                Rectangle wall_edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)Width, (int)Height);
                if (particle_edge.Intersects(wall_edge))
                {
                    Velocity.Y = 100;
                    if (Velocity.X > 0)
                    {
                        Position.X = wall.Position.X - Width;
                        Velocity.X = 0;
                    }
                    if (Velocity.X < 0)
                    {
                        Position.X = wall.Position.X + wall.Width;
                        Velocity.X = 0;
                    }
                    if (Velocity.X == 0)
                    {
                        if ((Position.X + Width)/2 > (wall.Position.X + wall.Width) / 2)
                        {
                            Position.X = wall.Position.X + wall.Width;
                        }
                        else
                        {
                            Position.X = wall.Position.X - Width;
                        }
                    }
                }
            }
        }

        public float Y_of_platform;
        private bool OnPlatform(List<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                if (Position.Y + Height > platform.Position.Y && Position.Y + Height < platform.Position.Y + platform.Height)
                {
                    if (Position.X + Width > platform.Position.X && Position.X < platform.Position.X + platform.Width)
                    {
                        Y_of_platform = platform.Position.Y;
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(GameTime gameTime, List<Platform> platforms, List<Particle> particles, List<Wall> walls, float screen_height)
        {
            if (!OnPlatform(platforms) && Velocity.Y < 5)
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

            bool colliding_L = false;
            bool colliding_B = false;
            Particle colliding_with = null;
            Rectangle particle_edge = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width, (int)Height);
            foreach (Particle particle in particles)
            {
                if (id != particle.id)
                {
                    Rectangle other_edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                    if (Position.Y + Height > particle.Position.Y && Position.Y + Height < particle.Position.Y + particle.Height)
                    {
                        if (Position.X + Width > particle.Position.X && Position.X < particle.Position.X + particle.Width)
                        {
                            colliding_B = true;
                            colliding_with = particle;
                        }
                    }
                    if (particle_edge.Intersects(other_edge))
                    {
                        colliding_L = true;
                        colliding_with = particle;
                    }
                }
            }
            if (colliding_L)
            {
                colliding_with.Velocity = Velocity/2;
                Velocity.X /= 2;
            }
            if (colliding_B)
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
                colliding_with.Velocity.Y = 0;
                Position.Y = colliding_with.Position.Y - Height;
            }
            HittingWall(walls);
            Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Particle.Draw(spriteBatch, Position);
        }
    }
}
