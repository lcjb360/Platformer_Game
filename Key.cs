using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Platformer_Game
{
    public class Key : Physics_Object
    {
        public Vector2 Start_Position;
        public Color Colour = Color.Yellow;

        public Key(Texture2D texture, Vector2 position)
        {
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Start_Position = position;
            Height = (float)20;
            Width = (float)20;
        }

        public Key(Texture2D texture, Vector2 position, Color color)
        {
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            //none = new Sprite(texture, 1, 93, 1, 1);
            Position = position;
            Start_Position = position;
            Height = (float)20;
            Width = (float)20;
            Colour = color;
        }

        private void HittingHazard(List<Lava> lavas)
        {
            return;
        }

        private void HittingWall(List<Wall> walls)
        {
            Rectangle Key_Edge2 = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width + 1, (int)Height + 1);
            Rectangle Key_Edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Wall wall in walls)
            {
                Rectangle Wall_Edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)wall.Width, (int)wall.Height);
                if (Key_Edge.Intersects(Wall_Edge) || Key_Edge2.Intersects(Wall_Edge))
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

        public Platform Touched_platform = null;
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
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(GameTime gameTime, Player player, List<Platform> platforms, List<Particle> particles,
                           List<Wall> walls, List<Lava> lavas)
        {
            Rectangle Player_Edge = new Rectangle((int)(player.Position.X), (int)(player.Position.Y), (int)player.Width, (int)player.Height);
            Rectangle Edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            if (Edge.Intersects(Player_Edge))
            {
                Width = 0;
                Height = 0;
                player.Inventory.Add(Colour);
            }
            if (!OnPlatform(platforms) && Velocity.Y < 5)
            {
                Velocity.Y += 1;
            }
            if (OnPlatform(platforms))
            {
                if (Touched_platform != null)
                {
                    Touched_platform.Touched = true;
                }
                if (Platform_Moving)
                {
                    Velocity.X = ((float)1.2 * Platform_Velocity.X);
                }
                else
                {
                    Velocity.X = 0;
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
                Rectangle Other_Edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                if (Position.Y + Height > particle.Position.Y && Position.Y + Height < particle.Position.Y + particle.Height)
                {
                    if (Position.X + Width > particle.Position.X && Position.X < particle.Position.X + particle.Width)
                    {
                        Colliding_V = true;
                        if (particle.Burning)
                        {
                            particle.Position = new Vector2(-100, -100);
                        }
                        Colliding_With = particle;
                    }
                }
                if (Particle_Edge.Intersects(Other_Edge))
                {
                    Colliding_H = true;
                    if (particle.Burning)
                    {
                        particle.Position = new Vector2(-100, -100);
                    }
                    Colliding_With = particle;
                }

            }
            if (Colliding_H)
            {
                if (!Colliding_With.Liquid)
                {
                    Colliding_With.Velocity.X = 0;
                }
                Velocity.X = 0;
                if (Colliding_With.Liquid)
                {
                    Velocity.X = Colliding_With.Velocity.X;
                }
            }
            if (Colliding_H && !Colliding_With.Liquid)
            {
                Velocity.X = -Velocity.X;
            }
            if (Colliding_V)
            {
                Velocity.Y = 0;
                Colliding_With.Velocity.Y = 0;
                Position.Y = Colliding_With.Position.Y - Height;
            }
            HittingWall(walls);
            Position += Velocity;
            HittingHazard(lavas);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Default_Sprite.Draw(spriteBatch, Position, (int)Width, (int)Height, Colour);
        }
    }
}
