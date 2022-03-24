using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Particle : Physics_Object
    {
        public int Id;
        public Color Colour = Color.Black;
        public bool Burning = false;
        public bool Liquid = false;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id)
        {
            Default_Sprite = new Sprite(texture, 60, 0, 5, 5);
            Position = position;
            Velocity = new Vector2(velocity.X, velocity.Y);
            Height = 9;
            Width = 9;
            Id = particle_id;
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id, Color color)
        {
            Default_Sprite = new Sprite(texture, 60, 0, 5, 5);
            Position = position;
            Velocity = new Vector2(velocity.X, velocity.Y);
            Height = 9;
            Width = 9;
            Id = particle_id;
            Colour = color;
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int particle_id, Color color, bool burning, bool liquid)
        {
            Default_Sprite = new Sprite(texture, 60, 0, 5, 5);
            Position = position;
            Velocity = new Vector2(velocity.X, velocity.Y);
            Height = 9;
            Width = 9;
            Id = particle_id;
            Colour = color;
            Burning = burning;
            Liquid = liquid;
        }

        private void HittingHazard(List<Lava> lavas)
        {
            Rectangle Particle_Edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Lava lava in lavas)
            {
                Rectangle Lava_Edge = new Rectangle((int)lava.Position.X, (int)lava.Position.Y, (int)lava.Length, (int)lava.Height);
                if (Particle_Edge.Intersects(Lava_Edge))
                {
                    Width = 0;
                    Height = 0;
                }
            }
        }

        private void HittingWall(List<Wall> walls)
        {
            Rectangle Particle_Edge2 = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width + 1, (int)Height + 1);
            Rectangle Particle_Edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Wall wall in walls)
            {
                if (wall.Destructible && Burning)
                {
                    for (int i = 0; i < wall.Parts.Count; i++)
                    {
                        Rectangle part = wall.Parts[i];
                        if (Particle_Edge.Intersects(part) || Particle_Edge2.Intersects(part))
                        {
                            wall.Parts.RemoveAt(i);
                            Width = 0;
                            Height = 0;
                            Position = new Vector2(-100, 500);
                            return;
                        }
                    }
                }
                Rectangle Next_Particle_Edge = new Rectangle((int)(Position.X) + (int)Velocity.X, (int)(Position.Y) + (int)Velocity.Y, (int)Width, (int)Height);
                Rectangle Next_Particle_Edge_Top = new Rectangle((int)(Position.X) + (int)Velocity.X + 10, (int)(Position.Y) + (int)Velocity.Y, (int)Width - 20, (int)Height / 2);
                Rectangle Next_Particle_Edge_Bottom = new Rectangle((int)(Position.X) + (int)Velocity.X + 10, (int)(Position.Y) + (int)Velocity.Y + ((int)Height / 2), (int)Width - 20, ((int)Height / 2));
                Rectangle Next_Particle_Edge_Right = new Rectangle((int)(Position.X) + (int)Velocity.X + ((int)Width / 2), (int)(Position.Y) + 5 + (int)Velocity.Y, (int)Width / 2, (int)Height - 10);
                Rectangle Next_Particle_Edge_Left = new Rectangle((int)(Position.X) + (int)Velocity.X, (int)(Position.Y) + 5 + (int)Velocity.Y, (int)Width / 2, (int)Height - 10);

                if (wall.Destructible)
                {
                    for (int i = 0; i < wall.Parts.Count; i++)
                    {
                        Rectangle part = wall.Parts[i];
                        if (Next_Particle_Edge.Intersects(part))
                        {
                            if (Next_Particle_Edge_Bottom.Intersects(part))
                            {
                                Velocity.Y = 0;
                                Position.Y = part.Y - Height;
                            }
                            if (Next_Particle_Edge_Right.Intersects(part))
                            {
                                Velocity.X = 0;
                                Position.X = part.X - Width;
                            }
                            if (Next_Particle_Edge_Left.Intersects(part))
                            {
                                Velocity.X = 0;
                                Position.X = part.X + part.Width;
                            }
                            if (Next_Particle_Edge_Top.Intersects(part))
                            {
                                Velocity.Y = 0;
                                Position.Y = part.Y + part.Height;
                            }

                        }
                    }
                }
                else
                {
                    Rectangle Wall_Edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)wall.Width, (int)wall.Height);
                    if (Particle_Edge.Intersects(Wall_Edge) || Particle_Edge2.Intersects(Wall_Edge))
                    {
                        if (Burning)
                        {
                            Position = new Vector2(-100, -100);
                        }
                        if (Liquid)
                        {
                            Velocity.X = (float)(-1) * Velocity.X;
                        }
                        else
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
        }

        public Platform Touched_Platform = null;
        public float Y_Of_Platform;
        public Vector2 Platform_Velocity;
        public bool Platform_Moving;
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
                            Touched_Platform = platform;
                        }
                        if (platform.Moving)
                        {
                            Platform_Velocity = (platform.Destination - platform.Position);
                            if (Platform_Velocity.X > 0)
                            {
                                Platform_Velocity.X = (float)8;
                            }
                            if (Platform_Velocity.X < 0)
                            {
                                Platform_Velocity.X = (float)-8;
                            }

                            if (Platform_Velocity.X != 0)
                            {
                                Platform_Moving = true;
                            }
                            else
                            {
                                Platform_Moving = false;
                            }
                        }
                        else
                        {
                            Platform_Moving = false;
                        }
                        Y_Of_Platform = platform.Position.Y;
                        if (Burning)
                        {
                            Position = new Vector2(-100, -100);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(GameTime gameTime, List<Platform> platforms, List<Particle> particles, List<Wall> walls, List<Lava> lavas)
        {
            if (!OnPlatform(platforms) && Velocity.Y < 5)
            {
                Velocity.Y += 1;
            }
            if (OnPlatform(platforms))
            {
                if (Touched_Platform != null)
                {
                    Touched_Platform.Touched = true;
                }
                if (Platform_Moving)
                {
                    Velocity.X = (float)1.2 * Platform_Velocity.X;
                }
                else
                {
                    if (!Liquid)
                    {
                        Velocity.X = 0;
                    }
                    else
                    {
                        Random Rand = new Random();
                        if (Velocity.X > 0 && Velocity.X != 0)
                        {
                            Velocity.X += (float)5 * (float)Rand.NextDouble();
                        }
                        if (Velocity.X < 0 && Velocity.X != 0)
                        {
                            Velocity.X -= (float)5 * (float)Rand.NextDouble();
                        }
                    }
                }
                Velocity.Y = 0;
                Position.Y = Y_Of_Platform - Height;
            }

            bool Colliding_H = false;
            bool Colliding_V = false;
            Particle Colliding_With = null;
            Rectangle Particle_Edge = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width, (int)Height);
            foreach (Particle particle in particles)
            {
                if (Id != particle.Id)
                {
                    Rectangle Other_Edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                    if (Position.Y + Height > particle.Position.Y && Position.Y + Height < particle.Position.Y + particle.Height)
                    {
                        if (Position.X + Width > particle.Position.X && Position.X < particle.Position.X + particle.Width)
                        {
                            Colliding_V = true;
                            if (Burning && !particle.Burning)
                            {
                                particle.Position = new Vector2(-100, -100);
                                Position = new Vector2(-100, -100);
                            }
                            Colliding_With = particle;
                        }
                    }
                    if (Particle_Edge.Intersects(Other_Edge))
                    {
                        Colliding_H = true;
                        if (Burning && !particle.Burning)
                        {
                            particle.Position = new Vector2(-100, -100);
                            Position = new Vector2(-100, -100);
                        }
                        Colliding_With = particle;
                    }
                }
            }
            if (Colliding_H && !Liquid)
            {
                if (!Colliding_With.Liquid)
                {
                    Colliding_With.Velocity.X = 0;
                }
                Velocity.X = 0;
            }
            if (Colliding_H && !Colliding_With.Liquid)
            {
                Velocity.X = -Velocity.X;
            }
            if (Colliding_V)
            {
                if (Liquid)
                {
                    Random Rand = new Random();
                    if (Velocity.X > 0 && Velocity.X != 0)
                    {
                        Velocity.X += (float)5 * (float)Rand.NextDouble();
                    }
                    if (Velocity.X < 0 && Velocity.X != 0)
                    {
                        Velocity.X -= (float)5 * (float)Rand.NextDouble();
                    }
                }
                Velocity.Y = 0;
                if (!Liquid)
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
                Colliding_With.Velocity.Y = 0;
                Position.Y = Colliding_With.Position.Y - Height;
            }
            HittingWall(walls);
            Position += Velocity;
            HittingHazard(lavas);
            if (Liquid)
            {
                Velocity = (float)0.7 * Velocity;
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Sprite.Draw(spriteBatch, Position, (int)Width, (int)Height, Colour);
        }
    }
}
