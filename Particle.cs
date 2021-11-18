using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Particle
    {
        public Rectangle window = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public Sprite Default_Particle;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public float Height;
        public float Width;
        public int id;
        public Color Colour = Color.Black;
        public bool Burning = false;
        public bool Liquid = false;
        //public Sprite none;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Particle = new Sprite(texture, 60, 0, 5, 5);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Velocity = new Vector2(velocity.X * w_ratio, velocity.Y * h_ratio);
            Height = (float)9 * h_ratio;
            Width = (float)9 * w_ratio;
            id = particle_id;
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id, Color color)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Particle = new Sprite(texture, 60, 0, 5, 5);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Velocity = new Vector2(velocity.X * w_ratio, velocity.Y * h_ratio);
            Height = (float)9 * h_ratio;
            Width = (float)9 * w_ratio;
            id = particle_id;
            Colour = color;
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id, Color color, bool burning, bool liquid)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Particle = new Sprite(texture, 60, 0, 5, 5);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Velocity = new Vector2(velocity.X * w_ratio, velocity.Y * h_ratio);
            Height = (float)9 * h_ratio;
            Width = (float)9 * w_ratio;
            id = particle_id;
            Colour = color;
            Burning = burning;
            Liquid = liquid;
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
            Rectangle particle_edge2 = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width + 1, (int)Height + 1);
            Rectangle particle_edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Wall wall in walls)
            {
                if (wall.Destructible)
                {
                    for (int i = 0; i < wall.parts.Count; i++)
                    {
                        Rectangle part = wall.parts[i];
                        if (particle_edge.Intersects(part) || particle_edge2.Intersects(part))
                        {
                            wall.parts.RemoveAt(i);
                            Width = 0;
                            Height = 0;
                            Position = new Vector2(-100, 500);
                            return;
                        }
                    }
                }
                else
                { 
                    Rectangle wall_edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)wall.Width, (int)wall.Height);
                    if (particle_edge.Intersects(wall_edge) || particle_edge2.Intersects(wall_edge))
                    {
                        //if (new Rectangle((int)(Position.X + Velocity.X), (int)((Position.Y + Velocity.Y) + (Height / 2)), (int)Width, (int)(Height / 2)).Intersects(wall_edge))
                        //{
                        //    Velocity.Y = 0;
                        //    Position.Y = wall.Position.Y - Height;
                        //}
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
                                if ((Position.X + Width) / 2 > (wall.Position.X + wall.Width) / 2)
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
        }

        public Platform touched_platform = null;
        public float Y_of_platform;
        public Vector2 platform_Velocity;
        public bool platform_Moving;
        public bool OnPlatform(List<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                if (Position.Y + Height > platform.Position.Y && Position.Y + Height < platform.Position.Y + platform.Height)
                {
                    if (Position.X + Width - 1 > platform.Position.X && Position.X + 1 < platform.Position.X + platform.Width)
                    {
                        if (platform.Weak)
                        {
                            touched_platform = platform;
                        }
                        if (platform.Moving)
                        {
                            platform_Velocity = (platform.Destination - platform.Position);
                            if (platform_Velocity.X > 0)
                            {
                                platform_Velocity.X = (float)8;
                            }
                            if (platform_Velocity.X < 0)
                            {
                                platform_Velocity.X = (float)-8;
                            }

                            if (platform_Velocity.X != 0)
                            {
                                platform_Moving = true;
                            }
                            else
                            {
                                platform_Moving = false;
                            }
                        }
                        else
                        {
                            platform_Moving = false;
                        }
                        Y_of_platform = platform.Position.Y;
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(GameTime gameTime, List<Platform> platforms, List<Particle> particles, List<Wall> walls, List<Lava> lavas, float screen_height)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            if (!OnPlatform(platforms) && Velocity.Y < 5)
            {
                Velocity.Y += (float)1 * h_ratio;
            }
            if (OnPlatform(platforms))
            {   
                if (touched_platform != null)
                {
                    touched_platform.Touched = true;
                }
                if (platform_Moving)
                {
                    Velocity.X = ((float)1.2 * platform_Velocity.X) * w_ratio;
                }
                else
                {
                    Velocity.X = 0;
                }
                Velocity.Y = 0;
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
                colliding_with.Velocity.X = 0;
                Velocity.X = 0;
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
            Default_Particle.Draw(spriteBatch, Position, (int)Width, (int)Height, Colour);
        }
    }
}
