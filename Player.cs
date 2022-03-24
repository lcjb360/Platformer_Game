using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Player : Physics_Object
    {
        private MouseState Mouse_State = new MouseState();
        public Sprite Current_Sprite;
        public Sprite Container_Bar;
        public string State;
        public string Particle_State = "plain";
        public Texture2D Texture;
        public string Living_State;
        public Vector2 Start_Position;
        public int Capacity;
        public int Particle_Id;
        public int Ticker = 0;
        bool Waiting_To_Switch = false;
        bool Waiting_To_Reset = false;
        public List<Color> Inventory = new List<Color>();

        public Rectangle Player_Edge;
        public Rectangle Player_Edge_Top;
        public Rectangle Player_Edge_Bottom;
        public Rectangle Player_Edge_Right;
        public Rectangle Player_Edge_Left;

        public void define_edges()
        {
            Player_Edge = new Rectangle((int)(Position.X) + (int)Velocity.X, (int)(Position.Y) + (int)Velocity.Y, (int)Width, (int)Height);
            Player_Edge_Top = new Rectangle((int)(Position.X) + (int)Velocity.X + 10, (int)(Position.Y) + (int)Velocity.Y, (int)Width - 20, (int)Height / 2);
            Player_Edge_Bottom = new Rectangle((int)(Position.X) + (int)Velocity.X + 10, (int)(Position.Y) + (int)Velocity.Y + ((int)Height / 2), (int)Width - 20, ((int)Height / 2));
            Player_Edge_Right = new Rectangle((int)(Position.X) + (int)Velocity.X + ((int)Width / 2), (int)(Position.Y) + 5 + (int)Velocity.Y, (int)Width / 2, (int)Height - 10);
            Player_Edge_Left = new Rectangle((int)(Position.X) + (int)Velocity.X, (int)(Position.Y) + 5 + (int)Velocity.Y, (int)Width / 2, (int)Height - 10);
        }

        public Player(Texture2D texture, Vector2 start_position, int capacity)
        {
            Container_Bar = new Sprite(texture, 61, 0, 5, 5);
            Current_Sprite = new Sprite(texture, 0, 0, 30, 60);
            State = "alive";
            Width = Current_Sprite.Width;
            Height = Current_Sprite.Height;
            Position = start_position;
            Start_Position = Position;
            Texture = texture;
            Capacity = 400;
        }

        private bool OnWall(List<Wall> walls)
        {
            Rectangle Player_Edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height + 1);
            Rectangle Player_Bottom_Edge = new Rectangle((int)(Position.X) + 2, (int)(Position.Y) + ((int)Height / 2), (int)Width - 4, (int)(Height / 2) + 1);
            foreach (Wall wall in walls)
            {
                if (wall.Destructible)
                {
                    for (int i = 0; i < wall.Parts.Count; i++)
                    {
                        Rectangle part = wall.Parts[i];
                        if (Player_Edge.Intersects(part))
                        {
                            if (Player_Bottom_Edge.Intersects(part))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


        private bool HittingWall(List<Wall> walls)
        {
            define_edges();
            foreach (Wall wall in walls)
            {
                if (!Inventory.Contains(wall.Colour))
                {
                    if (wall.Destructible)
                    {
                        for (int i = 0; i < wall.Parts.Count; i++)
                        {
                            Rectangle part = wall.Parts[i];
                            if (Player_Edge.Intersects(part))
                            {
                                if (Player_Edge_Bottom.Intersects(part))
                                {
                                    Velocity.Y = 0;
                                    Position.Y = part.Y - Height;
                                }
                                if (Player_Edge_Right.Intersects(part))
                                {
                                    Velocity.X = 0;
                                    Position.X = part.X - Width;
                                }
                                if (Player_Edge_Left.Intersects(part))
                                {
                                    Velocity.X = 0;
                                    Position.X = part.X + part.Width;
                                }
                                if (Player_Edge_Top.Intersects(part))
                                {
                                    Velocity.Y = 0;
                                    Position.Y = part.Y + part.Height;
                                }

                            }
                        }
                    }
                    else
                    {
                        Rectangle wall_edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)wall.Width, (int)wall.Height);
                        if (Player_Edge.Intersects(wall_edge))
                        {
                            if (Player_Edge_Bottom.Intersects(wall_edge) && Velocity.Y >= 0)
                            {
                                Velocity.Y = 0;
                                Position.Y = wall.Position.Y - Height;
                                define_edges();
                            }
                            if (Player_Edge_Top.Intersects(wall_edge) && Velocity.Y < 0)
                            {
                                Velocity.Y = 0;
                                Position.Y = wall.Position.Y + wall.Height;
                                define_edges();
                            }
                            if (Player_Edge_Right.Intersects(wall_edge))
                            {
                                Velocity.X = 0;
                                Position.X = wall.Position.X - Width;
                                define_edges();
                            }
                            if (Player_Edge_Left.Intersects(wall_edge))
                            {
                                Velocity.X = 0;
                                Position.X = wall.Position.X + wall.Width;
                                define_edges();
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void HittingHazard(List<Spike> spikes, List<Lava> lavas)
        {
            Rectangle Player_Edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            foreach (Spike spike in spikes)
            {
                Rectangle spike_edge = new Rectangle((int)spike.Position.X, (int)spike.Position.Y, (int)spike.Length, (int)spike.Height);
                if (Player_Edge.Intersects(spike_edge))
                {
                    Living_State = "dead";
                    Particle_Id = 0;
                    Position = Start_Position;
                    Velocity = new Vector2(0, 0);
                }
            }
            foreach (Lava lava in lavas)
            {
                Rectangle lava_edge = new Rectangle((int)lava.Position.X, (int)lava.Position.Y + 1, (int)lava.Length, (int)lava.Height);
                if (Player_Edge.Intersects(lava_edge))
                {
                    Living_State = "dead";
                    Particle_Id = 0;
                    Position = Start_Position;
                    Velocity = new Vector2(0, 0);
                }
            }
        }

        public float Y_Of_Platform;
        public Vector2 Platform_Velocity;
        public bool Platform_Moving;
        public Platform Touched_Platform = null;
        public float Y_Of_Particle;
        private bool OnPlatform(List<Platform> platforms, List<Particle> particles, List<Spike> spikes, List<Lava> lavas)
        {
            HittingHazard(spikes, lavas);
            foreach (Platform platform in platforms)
            {
                if (Position.Y + Height >= platform.Position.Y && Position.Y + Height <= platform.Position.Y + platform.Height && platform.Width != 0)
                {
                    if (Position.X + Width - 1 >= platform.Position.X && Position.X + 1 <= platform.Position.X + platform.Width)
                    {
                        Touched_Platform = platform;
                        Position.Y = platform.Position.Y - Height;
                        if (platform.Moving)
                        {
                            Platform_Velocity = (platform.Destination - platform.Position);
                            if (Platform_Velocity.X > 0 && Platform_Velocity.Y == 0)
                            {
                                Platform_Velocity.X = (float)8;
                            }
                            if (Platform_Velocity.Y == 0 && Platform_Velocity.X < 0)
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
                            if (Platform_Velocity.Y != 0)
                            {
                                Platform_Moving = false;
                            }
                        }
                        else
                        {
                            Platform_Moving = false;
                        }
                        return true;
                    }
                }
            }
            foreach (Particle particle in particles)
            {
                if (Position.Y + Height >= particle.Position.Y && Position.Y + Height <= particle.Position.Y + particle.Height)
                {
                    if (Position.X + Width >= particle.Position.X && Position.X <= particle.Position.X + particle.Width)
                    {
                        if (particle.Velocity.Y <= 1)
                        {
                            Position.Y = particle.Position.Y - Height;
                            Velocity.Y = 0;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Current_Sprite.Draw(spriteBatch, Position, (int)(Width), (int)(Height));
            Container_Bar.Draw(spriteBatch, new Vector2(Position.X, Position.Y - 15), (int)(Width - (Width * (float)Particle_Id / (float)Capacity)), 5);

        }

        public void Update(GameTime gameTime, List<Platform> platforms, List<Particle> particles, List<Wall> walls, List<Spike> spikes, List<Lava> lavas, int screen_height)
        {
            Rectangle Player_Edge = new Rectangle((int)(Position.X), (int)(Position.Y), (int)Width, (int)Height);
            Living_State = "alive";
            if (Ticker > 0)
            { Ticker--; }
            Mouse_State = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Waiting_To_Switch = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Q) && Waiting_To_Switch)
            {
                Waiting_To_Switch = false;
                if (Particle_State == "fire")
                {
                    Particle_State = "liquid";
                }
                else if (Particle_State == "liquid")
                {
                    Particle_State = "plain";
                }
                else if (Particle_State == "plain")
                {
                    Particle_State = "fire";
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                Waiting_To_Reset = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.K) && Waiting_To_Reset)
            {
                Waiting_To_Reset = false;
                Living_State = "dead";
                Particle_Id = 0;
                Position = Start_Position;
                Velocity = new Vector2(0, 0);
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) && Velocity.X > -10)
            {
                if (OnPlatform(platforms, particles, spikes, lavas))
                {
                    Velocity.X -= 1;
                }
                else
                {
                    Velocity.X -= (float)0.5;
                }
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)) && Velocity.X < 10)
            {
                if (OnPlatform(platforms, particles, spikes, lavas))
                {
                    Velocity.X += 1;
                }
                else
                {
                    Velocity.X += (float)0.5;
                }
            }
            if ((Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.D)) &&
                (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.A)) && Velocity.X != 0)
            {
                if (OnPlatform(platforms, particles, spikes, lavas))
                {
                    if (Platform_Moving && Platform_Velocity.Y == 0)
                    {
                        Velocity.X = Platform_Velocity.X;
                        Platform_Velocity.X = 0;
                        Platform_Moving = false;
                    }
                    else
                    {
                        Platform_Velocity.X = 0;
                        Velocity.X /= (float)1.3;
                    }
                }
                else
                {
                    Velocity.X /= (float)1.2;
                }
            }
            bool Jumping = false;
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.W)) && (OnPlatform(platforms, particles, spikes, lavas) || OnWall(walls)))
            {
                Velocity.Y = (float)-10;
                Jumping = true;
            }

            Position += Velocity;

            if (!OnPlatform(platforms, particles, spikes, lavas) && Velocity.Y < 8)
            {
                Velocity.Y += 1;
            }

            bool Colliding1 = HittingWall(walls);
            OnPlatform(platforms, particles, spikes, lavas);
            if (Touched_Platform != null)
            {


                Touched_Platform.Touched = true;
            }
            if (Position.Y > screen_height)
            {
                Position.Y = 0;
            }
            HittingHazard(spikes, lavas);



            Vector2 mousePosVect = new Vector2(Mouse_State.X, Mouse_State.Y);
            mousePosVect -= new Vector2(Position.X + (Width / 2), Position.Y + (Height / 2));
            //Sprite Control
            if (mousePosVect.X > Position.X + (Width / 2))
            {
                State = "right";
            }
            if (mousePosVect.X < Position.X + (Width / 2))
            {
                State = "left";
            }

            //particle creation
            if (((Mouse_State.LeftButton == ButtonState.Pressed) && Ticker == 0 && Particle_Id < Capacity))
            {
                if ((mousePosVect / 10).Length() > 10)
                {
                    mousePosVect.Normalize();
                    mousePosVect *= 10;
                }
                else
                {
                    mousePosVect /= 10;
                }

                bool colliding = false;
                if (State == "left")
                {
                    Particle particle = new Particle(Texture, new Vector2(Position.X, Position.Y + 30), new Vector2((mousePosVect.X), mousePosVect.Y), Particle_Id, Color.Black);
                    if (Particle_State == "fire")
                    {
                        particle.Burning = true;
                        particle.Colour = Color.Red;
                    }
                    if (Particle_State == "liquid")
                    {
                        particle.Liquid = true;
                        particle.Colour = Color.MediumPurple;
                    }

                    Rectangle particle_edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                    foreach (Particle particlex in particles)
                    {
                        if (particle.Id != particlex.Id)
                        {
                            Rectangle other_edge = new Rectangle((int)(particlex.Position.X + particlex.Velocity.X), (int)(particlex.Position.Y + particlex.Velocity.Y), (int)particlex.Width, (int)particlex.Height);
                            if (particle_edge.Intersects(other_edge))
                            {
                                colliding = true;
                            }
                        }
                    }
                    if (!colliding)
                    {
                        particles.Add(particle);
                        Particle_Id++;
                        Ticker = 2;
                    }

                }
                if (State == "right")
                {
                    Particle particle = new Particle(Texture, new Vector2(Position.X, Position.Y + 30), new Vector2((mousePosVect.X), mousePosVect.Y), Particle_Id, Color.Black);
                    if (Particle_State == "fire")
                    {
                        particle.Burning = true;
                        particle.Colour = Color.Red;
                    }
                    if (Particle_State == "liquid")
                    {
                        particle.Liquid = true;
                        particle.Colour = Color.MediumPurple;
                    }
                    Rectangle particle_edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                    foreach (Particle particlex in particles)
                    {
                        if (particle.Id != particlex.Id)
                        {
                            Rectangle other_edge = new Rectangle((int)(particlex.Position.X + particlex.Velocity.X), (int)(particlex.Position.Y + particlex.Velocity.Y), (int)particlex.Width, (int)particlex.Height);
                            if (particle_edge.Intersects(other_edge))
                            {
                                colliding = true;
                            }
                        }
                    }
                    if (!colliding)
                    {
                        particles.Add(particle);
                        Particle_Id++;
                        Ticker = 2;
                    }
                }
            }
        }
    }
}
