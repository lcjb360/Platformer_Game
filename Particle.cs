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
        //public Sprite none;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id)
        {
            Default_Particle = new Sprite(texture, 60, 0, 5, 5);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Velocity = velocity;
            Height = Default_Particle.Height;
            Width = Default_Particle.Width;
            id = particle_id;
        }

        private void HittingHazard(List<Lava> lavas)
        {
            Rectangle particle_edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Lava lava in lavas)
            {
                Rectangle lava_edge = new Rectangle((int)lava.Position.X, (int)lava.Position.Y - 1, (int)lava.Length, (int)lava.Height);
                if (particle_edge.Intersects(lava_edge))
                {
                    Width = 0;
                    Height = 0;
                    //Default_Particle = none;
                }
            }
        }

        private void HittingWall(List<Wall> walls)
        {
            Rectangle particle_edge2 = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width, (int)Height);
            Rectangle particle_edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Wall wall in walls)
            {
                Rectangle wall_edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)wall.Width, (int)wall.Height);
                if (particle_edge.Intersects(wall_edge) || particle_edge2.Intersects(wall_edge) ||wall_edge.Contains(Position) || wall_edge.Contains(Position+Velocity))
                {
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
                    if (Position.X + Width - 1 > platform.Position.X && Position.X + 1 < platform.Position.X + platform.Width)
                    {
                        Y_of_platform = platform.Position.Y;
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(GameTime gameTime, List<Platform> platforms, List<Particle> particles, List<Wall> walls, List<Lava> lavas, float screen_height)
        {
            if (!OnPlatform(platforms) && Velocity.Y < 7)
            {
                Velocity.Y += 1;
            }
            if (OnPlatform(platforms))
            {
                Velocity.Y = 0;
                //if (Velocity.X > 0)
                //{
                //    Velocity.X -= 1;
                //}
                //if (Velocity.X < 0)
                //{
                //    Velocity.X += 1;
                //}
                Velocity.X = 0;
                Position.Y = Y_of_platform - Height;
            }

            bool colliding_H = false;
            bool colliding_V = false;
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
                            colliding_V = true;
                            colliding_with = particle;
                        }
                    }
                    if (particle_edge.Intersects(other_edge))
                    {
                        colliding_H = true;
                        colliding_with = particle;
                    }
                }
            }
            if (colliding_H)
            {
                colliding_with.Velocity = Velocity/2;
                Velocity.X /= 2;
            }
            if (colliding_V)
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
            HittingHazard(lavas);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Particle.Draw(spriteBatch, Position, (int)Width, (int)Height);
        }
    }
}
