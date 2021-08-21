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
        public int Screen_Width;
        public int Screen_Height;
        public Rectangle window;

        private Texture2D SpriteSheet;
        private Player player;
        private Platform platform;
        public List<Platform> platforms = new List<Platform>();
        private Wall wall;
        public List<Wall> walls = new List<Wall>();
        public List<Particle> particles = new List<Particle>();
        

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
            player = new Player(SpriteSheet, new Vector2(0, 0));
            platform = new Platform(SpriteSheet, new Vector2(0, 800), 1000, 20);
            platforms.Add(platform);
            wall = new Wall(SpriteSheet, new Vector2(775, 700), 25, 100);
            walls.Add(wall);
            foreach (Wall wall in walls)
            {
                platforms.Add(new Platform(SpriteSheet, wall.Position, wall.Width, 7));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            player.Update(gameTime, platforms, particles, walls, window.Height);

            foreach (Particle particle in particles)
            {
                particle.Update(gameTime, platforms, particles, walls, window.Height);
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

            foreach (Particle particle in particles)
            {
                particle.Draw(_spriteBatch, gameTime);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
