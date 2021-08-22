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
        public int level_number;
        public string game_state;

        private Texture2D SpriteSheet;

        public Level Tutorial;


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
            game_state = "Tutorial";

            level_number = 0;

            Tutorial = new Level(true, false, level_number, window,
                new Player(SpriteSheet, new Vector2(0, window.Height - 90)), 
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, window.Height - 30), 1000, 30),
                                       new Platform(SpriteSheet, new Vector2(1000, window.Height - 30 + 12), 1 * 60, 30),
                                       new Platform(SpriteSheet, new Vector2(1060, window.Height - 30), window.Width - 1060, 30)}, 
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(770, window.Height - 130), 30, 100) }, 
                new List<Spike>() {    new Spike(SpriteSheet, new Vector2(850, window.Height - 40), 5 * 9) }, 
                new List<Lava>() {     new Lava(SpriteSheet, new Vector2(1000, window.Height - 29), 1 * 60) },
                SpriteSheet);

            level_number++;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            switch (game_state)
            {
                case ("Tutorial"):
                    Tutorial.Update(gameTime);
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Tomato);
            _spriteBatch.Begin();

            switch (game_state)
            {
                case ("Tutorial"):
                    Tutorial.Draw(_spriteBatch ,gameTime);
                    break;
                default:
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
