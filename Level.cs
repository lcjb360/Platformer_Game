using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Level
    {
        public Rectangle window = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public bool Unlocked;
        public bool Completed;
        public Vector2 Finish_Point;
        public int id;
        public Rectangle Window;
        private Player Player;
        public List<Platform> Platforms;
        private List<Wall> Walls;
        private List<Spike> Spikes;
        private List<Lava> Lavas;
        private List<Particle> Particles = new List<Particle>();
        private List<Key> Keys = new List<Key>();
        public Rectangle player_edge;

        public Level(bool unlocked, bool completed, int id, Rectangle window, Vector2 finish_point, Player player, List<Platform> platforms, List<Wall> walls, List<Spike> spikes, List<Lava> lavas, Texture2D spriteSheet)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Unlocked = unlocked;
            Completed = completed;
            this.id = id;
            Window = window;
            Player = player;
            Platforms = platforms;
            Walls = walls;
            Spikes = spikes;
            Lavas = lavas;
            Finish_Point = new Vector2(finish_point.X * w_ratio, finish_point.Y * h_ratio);
            foreach (Wall wall in walls)
            {
                if (!wall.Destructible)
                {
                    Platforms.Add(new Platform(spriteSheet, new Vector2((wall.Position.X / w_ratio) - 1, (wall.Position.Y / h_ratio)), (wall.Width / w_ratio) - (float)0.25, (float)(7) / h_ratio));
                }

            }
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Position.Y += 7;
                walls[i].Height -= 7;
            }
            foreach (Spike spike in spikes)
            {
                Platforms.Add(new Platform(spriteSheet, new Vector2(spike.Position.X / w_ratio, (spike.Position.Y + 10) / h_ratio), (float)spike.Length / w_ratio, 20));
            }
        }

        public Level(bool unlocked, bool completed, int id, Rectangle window, Vector2 finish_point, Player player, List<Platform> platforms, List<Wall> walls, List<Spike> spikes, List<Lava> lavas, List<Key> keys, Texture2D spriteSheet)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Unlocked = unlocked;
            Completed = completed;
            this.id = id;
            Window = window;
            Player = player;
            Platforms = platforms;
            Walls = walls;
            Spikes = spikes;
            Lavas = lavas;
            Keys = keys;
            Finish_Point = new Vector2(finish_point.X * w_ratio, finish_point.Y * h_ratio);
            foreach (Wall wall in walls)
            {
                if (!wall.Destructible)
                {
                    Platforms.Add(new Platform(spriteSheet, new Vector2((wall.Position.X / w_ratio) - 1, (wall.Position.Y / h_ratio)), (wall.Width / w_ratio) - (float)0.25, (float)(7) / h_ratio, wall.Colour));
                }

            }
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Position.Y += 7;
                walls[i].Height -= 7;
            }
            foreach (Spike spike in spikes)
            {
                Platforms.Add(new Platform(spriteSheet, new Vector2(spike.Position.X / w_ratio, (spike.Position.Y + 10) / h_ratio), (float)spike.Length / w_ratio, 20));
            }
        }

        public bool Update(GameTime gameTime)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Player.Update(gameTime, Platforms, Particles, Walls, Spikes, Lavas, Window.Height);
            if (Player.living_state == "dead")
            {
                Particles = new List<Particle>();
                for (int i = 0; i < Platforms.Count; i++)
                {
                    if (Platforms[i].Flashing || Platforms[i].Weak)
                    {

                        Platforms[i].ticks = 0;
                        Platforms[i].Appear = Platforms[i].Start_Appear;
                        Platforms[i].Touched = false;
                    }
                    if (Platforms[i].Moving)
                    {
                        Platforms[i].Position = Platforms[i].Start_Position;
                        Platforms[i].Destination = Platforms[i].Start_Destination;
                    }
                }
                for (int i = 0; i < Walls.Count; i++)
                {
                    if (Walls[i].Destructible)
                    {
                        Walls[i].parts = new List<Rectangle>();
                        for (int x = (int)Walls[i].Position.X; x < (int)(Walls[i].Position.X + Walls[i].Width); x += (int)(((float)9) * w_ratio))
                        {
                            for (int y = (int)Walls[i].Position.Y; y < (int)(Walls[i].Position.Y + Walls[i].Height); y += (int)(((float)9) * h_ratio))
                            {
                                Walls[i].parts.Add(new Rectangle((int)x, (int)y, (int)((float)9 * w_ratio), (int)((float)9 * h_ratio)));
                            }
                        }
                    }
                }
                for (int i = 0; i < Keys.Count; i++)
                {
                    Keys[i].Position = Keys[i].Start_Position;
                    Keys[i].Width = 20;
                    Keys[i].Height = 20;
                    Player.Inventory.Remove(Keys[i].Colour);
                }
            }
            player_edge = new Rectangle((int)Player.Position.X, (int)Player.Position.Y, (int)Player.Width, (int)Player.Height);
            if (player_edge.Contains(Finish_Point))
            {
                Particles = new List<Particle>();
                Player.Position = Player.Start_Position;
                Player.Velocity = new Vector2(0, 0);
                return true;
            }

            foreach (Platform platform in Platforms)
            {
                platform.Update(gameTime);
            }

            foreach (Particle particle in Particles)
            {
                particle.Update(gameTime, Platforms, Particles, Walls, Lavas, Window.Height);
            }

            foreach (Key key in Keys)
            {
                key.Update(gameTime, Player, Platforms, Particles, Walls, Lavas, Window.Height);
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Texture2D spriteSheet)
        {
            foreach (Platform platform in Platforms)
            {
                platform.Draw(spriteBatch, gameTime);
            }
            

            foreach (Spike spike in Spikes)
            {
                spike.Draw(spriteBatch, gameTime);
            }

            foreach (Wall wall in Walls)
            {
                wall.Draw(spriteBatch, gameTime);
            }

            foreach (Lava lava in Lavas)
            {
                lava.Draw(spriteBatch, gameTime);
            }

            Player.Draw(spriteBatch, gameTime);

            foreach (Particle particle in Particles)
            {
                particle.Draw(spriteBatch, gameTime);
            }

            foreach (Key key in Keys)
            {
                key.Draw(spriteBatch, gameTime);
            }

            Particle finish_point = new Particle(spriteSheet, Finish_Point, new Vector2(0, 0), 0, Color.White);
            finish_point.Draw(spriteBatch, gameTime);
            finish_point.Position.X += finish_point.Width;
            finish_point.Colour = Color.Black;
            finish_point.Draw(spriteBatch, gameTime);
            finish_point.Position.Y += finish_point.Width;
            finish_point.Colour = Color.White;
            finish_point.Draw(spriteBatch, gameTime);
            finish_point.Position.X -= finish_point.Width;
            finish_point.Colour = Color.Black;
            finish_point.Draw(spriteBatch, gameTime);
        }
    }
}
