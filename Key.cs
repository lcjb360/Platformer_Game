using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Key
    {
        public Sprite Default_Key;
        public Vector2 Position;
        public Vector2 Start_Position;
        public Vector2 Velocity;
        public float Height;
        public float Width;
        public Color Colour = Color.Yellow;

        public Key(Texture2D texture, Vector2 position)
        {
            Default_Key = new Sprite(texture, 0, 61, 59, 14);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Start_Position = position;
            Height = (float)20;
            Width = (float)20;
        }

        public Key(Texture2D texture, Vector2 position, Color color)
        {
            Default_Key = new Sprite(texture, 0, 61, 59, 14);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Start_Position = position;
            Height = (float)20;
            Width = (float)20;
            Colour = color;
        }

        private void HittingHazard(List<Lava> lavas)
        {
            Rectangle key_edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Lava lava in lavas)
            {
                Rectangle lava_edge = new Rectangle((int)lava.Position.X, (int)lava.Position.Y, (int)lava.Length, (int)lava.Height);
                if (key_edge.Intersects(lava_edge))
                {
                    Width = 0;
                    Height = 0;
                    //Default_Particle = none;
                }
            }
        }

        private void HittingWall(List<Wall> walls)
        {
            Rectangle key_edge2 = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width + 1, (int)Height + 1);
            Rectangle key_edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Wall wall in walls)
            {

                Rectangle key_next_edge = new Rectangle((int)(Position.X) + (int)Velocity.X, (int)(Position.Y) + (int)Velocity.Y, (int)Width, (int)Height);
                Rectangle key_next_top_edge = new Rectangle((int)(Position.X) + (int)Velocity.X + 10, (int)(Position.Y) + (int)Velocity.Y, (int)Width - 20, (int)Height / 2);
                Rectangle key_next_bottom_edge = new Rectangle((int)(Position.X) + (int)Velocity.X + 10, (int)(Position.Y) + (int)Velocity.Y + ((int)Height / 2), (int)Width - 20, ((int)Height / 2));
                Rectangle key_next_right_edge = new Rectangle((int)(Position.X) + (int)Velocity.X + ((int)Width / 2), (int)(Position.Y) + 5 + (int)Velocity.Y, (int)Width / 2, (int)Height - 10);
                Rectangle key_next_left_edge = new Rectangle((int)(Position.X) + (int)Velocity.X, (int)(Position.Y) + 5 + (int)Velocity.Y, (int)Width / 2, (int)Height - 10);

                if (wall.Destructible)
                {
                    for (int i = 0; i < wall.parts.Count; i++)
                    {
                        Rectangle part = wall.parts[i];
                        if (key_next_edge.Intersects(part))
                        {
                            if (key_next_bottom_edge.Intersects(part))
                            {
                                Velocity.Y = 0;
                                Position.Y = part.Y - Height;
                                //return true;
                            }
                            if (key_next_right_edge.Intersects(part))
                            {
                                Velocity.X = 0;
                                Position.X = part.X - Width;
                                //return true;
                            }
                            if (key_next_left_edge.Intersects(part))
                            {
                                Velocity.X = 0;
                                Position.X = part.X + part.Width;
                                //return true;
                            }
                            if (key_next_top_edge.Intersects(part))
                            {
                                Velocity.Y = 0;
                                Position.Y = part.Y + part.Height;
                                //return true;
                            }

                        }
                    }
                }
                else
                {
                    Rectangle wall_edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)wall.Width, (int)wall.Height);
                    if (key_edge.Intersects(wall_edge) || key_edge2.Intersects(wall_edge))
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

        public void Update(GameTime gameTime, Player player,List<Platform> platforms, List<Particle> particles, List<Wall> walls, List<Lava> lavas, float screen_height)
        {
            Rectangle player_edge = new Rectangle((int)(player.Position.X), (int)(player.Position.Y), (int)player.Width, (int)player.Height);
            Rectangle edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            if (edge.Intersects(player_edge))
            {
                Width = 0;
                Height = 0;
                player.Inventory.Add(Colour);
            }
            float w_ratio = 1;
            float h_ratio = 1;
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
                Rectangle other_edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                if (Position.Y + Height > particle.Position.Y && Position.Y + Height < particle.Position.Y + particle.Height)
                {
                    if (Position.X + Width > particle.Position.X && Position.X < particle.Position.X + particle.Width)
                    {
                        colliding_V = true;
                        if (particle.Burning)
                        {
                            particle.Position = new Vector2(-100, -100);
                        }
                        colliding_with = particle;
                    }
                }
                if (particle_edge.Intersects(other_edge))
                {
                    colliding_H = true;
                    if (particle.Burning)
                    {
                        particle.Position = new Vector2(-100, -100);
                    }
                    colliding_with = particle;
                }

            }
            if (colliding_H)
            {
                if (!colliding_with.Liquid)
                {
                    colliding_with.Velocity.X = 0;
                }
                Velocity.X = 0;
                if (colliding_with.Liquid)
                {
                    Velocity.X = colliding_with.Velocity.X;
                }
            }
            if (colliding_H && !colliding_with.Liquid)
            {
                Velocity.X = -Velocity.X;
            }
            if (colliding_V)
            {
                Velocity.Y = 0;
                colliding_with.Velocity.Y = 0;
                Position.Y = colliding_with.Position.Y - Height;
            }
            HittingWall(walls);
            Position += Velocity;
            HittingHazard(lavas);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Key.Draw(spriteBatch, Position, (int)Width, (int)Height, Colour);
        }
    }
}
