using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Platformer_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Rectangle window;

        private Texture2D SpriteSheet;
        private Player player;
        private Platform platform;
        public List<Platform> platforms = new List<Platform>();
        private Wall wall;
        public List<Wall> walls = new List<Wall>();
        public List<Particle> particles = new List<Particle>();
        private Spike spike;
        public List<Spike> spikes = new List<Spike>();
        private Lava lava;
        public List<Lava> lavas = new List<Lava>();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            window = GraphicsDevice.Viewport.Bounds;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet = Content.Load<Texture2D>("player_images");

            player = new Player(SpriteSheet, new Vector2(0, window.Height + 90));
            
            platform = new Platform(SpriteSheet, new Vector2(0, window.Height - 30), 1000, 30);
            platforms.Add(platform);
            platform = new Platform(SpriteSheet, new Vector2(1000, window.Height - 30 +12), 1 * 60, 30);
            platforms.Add(platform);
            platform = new Platform(SpriteSheet, new Vector2(1060, window.Height - 30), window.Width - 1060, 30);
            platforms.Add(platform);

            wall = new Wall(SpriteSheet, new Vector2(770, window.Height - 130), 30, 100);
            walls.Add(wall);
            foreach (Wall wall in walls)
            {
                platforms.Add(new Platform(SpriteSheet, wall.Position, wall.Width, 7));
            }
            
            spike = new Spike(SpriteSheet, new Vector2(850, window.Height - 40), 5*9);
            spikes.Add(spike);

            lava = new Lava(SpriteSheet, new Vector2(1000, window.Height - 29), 1 * 60);
            lavas.Add(lava);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            player.Update(gameTime, platforms, particles, walls, spikes, lavas, window.Height);

            foreach (Particle particle in particles)
            {
                particle.Update(gameTime, platforms, particles, walls, lavas, window.Height);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Tomato);
            _spriteBatch.Begin();

            player.Draw(_spriteBatch, gameTime);

            foreach (Wall wall in walls)
            {
                wall.Draw(_spriteBatch, gameTime);
            }
            
            foreach (Platform platform in platforms)
            {
                platform.Draw(_spriteBatch, gameTime);
            }

            foreach (Spike spike in spikes)
            {
                spike.Draw(_spriteBatch, gameTime);
            }

            foreach (Lava lava in lavas)
            {
                lava.Draw(_spriteBatch, gameTime);
            }

            foreach (Particle particle in particles)
            {
                particle.Draw(_spriteBatch, gameTime);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
