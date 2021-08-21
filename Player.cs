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
        private MouseState mouseState = new MouseState();
        public Sprite Stationary_Right_Sprite;
        public Sprite Stationary_Left_Sprite;
        public Sprite Moving_Right_Sprite;
        public Sprite Moving_Left_Sprite;
        public Sprite Current_Sprite;
        public Sprite Container_Bar;
        public Texture2D Texture;
        public Vector2 Position;
        public float Width;
        public float Height;
        public Vector2 Velocity;
        public int capacity;
        public int particle_id;
        public int ticker = 0;

        public Player(Texture2D texture, Vector2 position)
        {
            Stationary_Right_Sprite = new Sprite(texture, 0, 0, 30, 60);
            Stationary_Left_Sprite = new Sprite(texture, 0, 0, 30, 60);
            Moving_Right_Sprite = new Sprite(texture, 30, 0, 30, 60);
            Moving_Left_Sprite = new Sprite(texture, 30, 0, 30, 60);
            Container_Bar = new Sprite(texture, 61, 0, 5, 5);
            Current_Sprite = Stationary_Right_Sprite;
            Width = Current_Sprite.Width;
            Height = Current_Sprite.Height;
            Position = position;
            Texture = texture;
            capacity = 200;
        }

        private void HittingWall(List<Wall> walls)
        {
            Rectangle particle_edge = new Rectangle((int)(Position.X + Velocity.X), (int)(Position.Y + Velocity.Y), (int)Width, (int)Height);
            foreach (Wall wall in walls)
            {
                Rectangle wall_edge = new Rectangle((int)wall.Position.X, (int)wall.Position.Y, (int)Width, (int)Height);
                if (particle_edge.Intersects(wall_edge))
                {
                    if (Velocity.X > 0)
                    {
                        if (Position.X < wall.Position.X + (wall.Width/2))
                        {
                            Position.X = wall.Position.X - Width;
                            Velocity.X = 0;
                        }
                    }
                    if (Velocity.X < 0)
                    {
                        Position.X = wall.Position.X + wall.Width;
                        Velocity.X = 0;
                    }
                }
            }
        }
        public float Y_of_platform;
        public float Y_of_particle;
        private bool OnPlatform(List<Platform> platforms, List<Particle> particles)
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
            foreach (Particle particle in particles)
            {
                if (Position.Y + Height >= particle.Position.Y && Position.Y + Height <= particle.Position.Y + particle.Height)
                {
                    if (Position.X + Width >= particle.Position.X && Position.X <= particle.Position.X + particle.Width)
                    {
                        Y_of_particle = particle.Position.Y;
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Current_Sprite.Draw(spriteBatch, Position);
            Container_Bar.Draw(spriteBatch, new Vector2(Position.X, Position.Y - 15), (int)(Width - (Width * (float)particle_id / (float)capacity)), 5);
        }

        public void Update(GameTime gameTime, List<Platform> platforms, List<Particle> particles, List<Wall> walls, int screen_height)
        {
            if (ticker > 0)
            { ticker--; }
            mouseState = Mouse.GetState();
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
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.W)) && OnPlatform(platforms, particles))
            {
                Velocity.Y = -10;
            }
            Position += Velocity;
            if (!OnPlatform(platforms, particles) && Velocity.Y < 5)
            {
                Velocity.Y += 1;
            }
            Y_of_particle = 9999;
            Y_of_platform = 9999;
            if (OnPlatform(platforms, particles))
            {
                Velocity.Y = 0;
                if (Y_of_platform != 9999)
                {
                    Position.Y = Y_of_platform - Height;
                }
                if (Y_of_particle != 9999)
                {
                    Position.Y = Y_of_particle - Height;
                }
            }
            if (Position.Y > screen_height)
            {
                Position.Y = 0;
            }
            HittingWall(walls);

            
            Vector2 mousePosVect = new Vector2(mouseState.X, mouseState.Y);
            mousePosVect -= Position;
            //Sprite Control
            if (Velocity.X == 0)
            {
                if (mousePosVect.X > Position.X+(Width/2))
                {
                    Current_Sprite = Stationary_Right_Sprite;
                }
                if (mousePosVect.X < Position.X + (Width / 2))
                {
                    Current_Sprite = Stationary_Left_Sprite;
                }
            }
            if (Velocity.X != 0 && mousePosVect.X > Position.X + (Width / 2))
            {
                Current_Sprite = Moving_Right_Sprite;
            }
            if (Velocity.X != 0 && mousePosVect.X < Position.X + (Width / 2))
            {
                Current_Sprite = Moving_Left_Sprite;
            }

            //particle creation
            if (mouseState.LeftButton == ButtonState.Pressed && ticker == 0 && particle_id < capacity)
            {
                mousePosVect.Normalize();
                mousePosVect *= 5;
                bool colliding = false;
                if (Current_Sprite == Moving_Left_Sprite || Current_Sprite == Stationary_Left_Sprite)
                {
                    Particle particle = new Particle(Texture, new Vector2(Position.X, Position.Y+30), new Vector2((mousePosVect.X), mousePosVect.Y), particle_id);
                    Rectangle particle_edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                    foreach (Particle particlex in particles)
                    {
                        if (particle.id != particlex.id)
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
                        particle_id++;
                        ticker = 2;
                    }
                    
                }
                if (Current_Sprite == Moving_Right_Sprite || Current_Sprite == Stationary_Right_Sprite)
                {
                    Particle particle = new Particle(Texture, new Vector2(Position.X + Width, Position.Y + 30), new Vector2((mousePosVect.X), mousePosVect.Y), particle_id);
                    Rectangle particle_edge = new Rectangle((int)(particle.Position.X + particle.Velocity.X), (int)(particle.Position.Y + particle.Velocity.Y), (int)particle.Width, (int)particle.Height);
                    foreach (Particle particlex in particles)
                    {
                        if (particle.id != particlex.id)
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
                        particle_id++;
                        ticker = 2;
                    }
                }
            }
        }
    }
}
