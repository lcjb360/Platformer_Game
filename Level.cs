using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Level
    {
        public bool Unlocked;
        public bool Completed;
        public int id;
        public Rectangle Window;
        private Player Player;
        private List<Platform> Platforms;
        private List<Wall> Walls;
        private List<Spike> Spikes;
        private List<Lava> Lavas;
        private List<Particle> Particles = new List<Particle>();

        public Level(bool unlocked, bool completed, int id, Rectangle window, Player player, List<Platform> platforms, List<Wall> walls, List<Spike> spikes, List<Lava> lavas, Texture2D spriteSheet)
        {
            Unlocked = unlocked;
            Completed = completed;
            this.id = id;
            Window = window;
            Player = player;
            Platforms = platforms;
            Walls = walls;
            Spikes = spikes;
            Lavas = lavas;
            foreach (Wall wall in walls)
            {
                Platforms.Add(new Platform(spriteSheet, wall.Position, wall.Width, 7));
            }
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime, Platforms, Particles, Walls, Spikes, Lavas, Window.Height);

            foreach (Particle particle in Particles)
            {
                particle.Update(gameTime, Platforms, Particles, Walls, Lavas, Window.Height);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            

            foreach (Wall wall in Walls)
            {
                wall.Draw(spriteBatch, gameTime);
            }

            foreach (Platform platform in Platforms)
            {
                platform.Draw(spriteBatch, gameTime);
            }

            Player.Draw(spriteBatch, gameTime);

            foreach (Spike spike in Spikes)
            {
                spike.Draw(spriteBatch, gameTime);
            }

            foreach (Lava lava in Lavas)
            {
                lava.Draw(spriteBatch, gameTime);
            }

            foreach (Particle particle in Particles)
            {
                particle.Draw(spriteBatch, gameTime);
            }

        }
    }
}
