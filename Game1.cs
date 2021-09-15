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
        private MouseState mouseState = new MouseState();

        private Texture2D SpriteSheet;
        private Texture2D NumberSheet;
        public Sprite Box_Sprite;
        public Box box;
        public Sprite one;
        public Sprite two;
        public Sprite three;
        public Sprite four;
        public Sprite five;
        public List<Box> boxes = new List<Box>();

        public List<Level> levels = new List<Level>();
        public Level Tutorial;
        public Level two_one;


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
            game_state = "Main_Menu";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet = Content.Load<Texture2D>("player_images");
            NumberSheet = Content.Load<Texture2D>("Numbers");

            Box_Sprite = new Sprite(NumberSheet, 5, 5, 100, 100);
            one = new Sprite(NumberSheet, 14, 226, 42, 73);
            two = new Sprite(NumberSheet, 73, 226, 64, 73);
            three = new Sprite(NumberSheet, 144, 226, 63, 73);
            four = new Sprite(NumberSheet, 215, 226, 62, 73);
            five = new Sprite(NumberSheet, 285, 226, 63, 73);

            //default = new Level(false, false, level_number, window, new Vector2(window.Width - 50, window.Height - 50),
            //    new Player(SpriteSheet, new Vector2(30, window.Height - 90)),
            //    new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, window.Height - 30), window.Width, 30)
            //                         },
            //    new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, window.Height),
            //                           new Wall(SpriteSheet, new Vector2(window.Width-30, 0), 30, window.Height),
            //                           new Wall(SpriteSheet, new Vector2(0, 0), window.Width, 30)},
            //    new List<Spike>() { },
            //    new List<Lava>() { },
            //    SpriteSheet);

            level_number = 0;

            Tutorial = new Level(true, false, level_number, window, new Vector2(window.Width - 50, window.Height - 50),
                new Player(SpriteSheet, new Vector2(30, window.Height - 90)), 
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, window.Height - 30), 1000, 30),
                                       new Platform(SpriteSheet, new Vector2(1000, window.Height - 30 + 12), 1 * 60, 30),
                                       new Platform(SpriteSheet, new Vector2(1060, window.Height - 30), window.Width - 1060, 30)}, 
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(770, window.Height - 130), 30, 100),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 30, window.Height),
                                       new Wall(SpriteSheet, new Vector2(window.Width-30, 0), 30, window.Height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), window.Width, 30)}, 
                new List<Spike>() {    new Spike(SpriteSheet, new Vector2(850, window.Height - 40), 5 * 9) }, 
                new List<Lava>() {     new Lava(SpriteSheet, new Vector2(1000, window.Height - 29), 1 * 60) },
                SpriteSheet);

            levels.Add(Tutorial);
            level_number++;

            two_one = new Level(false, false, level_number, window, new Vector2(window.Width - 50, window.Height -50),
                new Player(SpriteSheet, new Vector2(30, window.Height - 90)),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, window.Height - 30), window.Width, 30)
                                     },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, window.Height),
                                       new Wall(SpriteSheet, new Vector2(window.Width-30, 0), 30, window.Height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), window.Width, 30),
                                       new Wall(SpriteSheet, new Vector2(100, window.Height - 300), window.Width - 200, window.Height - 300)},
                new List<Spike>() {    new Spike(SpriteSheet, new Vector2(100, window.Height - 310), window.Width - 200 - ((window.Width -200) % 9)) },
                new List<Lava>() {  },
                SpriteSheet);

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            switch (game_state)
            {
                case "1,1":
                    if (Tutorial.Update(gameTime))
                    {
                        Tutorial.Completed = true;
                        two_one.Unlocked = true;
                        game_state = "Main_Menu";
                    }
                    break;
                case "2,1":
                    if (two_one.Unlocked)
                    {
                        if (two_one.Update(gameTime))
                        {
                            two_one.Completed = true;

                            game_state = "Main_Menu";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case ("Main_Menu"):
                    game_state = Menu_Update(gameTime);
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            switch (game_state)
            {
                case "1,1":
                    GraphicsDevice.Clear(Color.Tomato);
                    Tutorial.Draw(_spriteBatch ,gameTime);
                    break;
                case "2,1":
                    if (two_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        two_one.Draw(_spriteBatch, gameTime);
                    }
                    break;
                case ("Main_Menu"):
                    GraphicsDevice.Clear(Color.White);
                    Menu_Draw(_spriteBatch, gameTime);
                    break;
                default:
                    GraphicsDevice.Clear(Color.Black);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Menu_Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Color color = Color.Red;
            for (int i = 200; i < 601; i += 200)
            {
                if (i == 400)
                {
                    color = Color.Green;
                }
                if (i == 600)
                {
                    color = Color.Blue;
                }
                box = new Box(new Vector2(200 - (Box_Sprite.Width - one.Width) / 2, i - (Box_Sprite.Height - one.Height) / 2), color, $"1,{i/200}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                one.Draw(spriteBatch, new Vector2(200, i));

                box = new Box(new Vector2(400 - (Box_Sprite.Width - two.Width) / 2, i - (Box_Sprite.Height - two.Height) / 2), color, $"2,{i/200}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                two.Draw(spriteBatch, new Vector2(400, i));

                box = new Box(new Vector2(600 - (Box_Sprite.Width - three.Width) / 2, i - (Box_Sprite.Height - three.Height) / 2), color, $"3,{i/200}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                three.Draw(spriteBatch, new Vector2(600, i));

                box = new Box(new Vector2(800 - (Box_Sprite.Width - four.Width) / 2, i - (Box_Sprite.Height - four.Height) / 2), color, $"4,{i/200}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                four.Draw(spriteBatch, new Vector2(800, i));

                box = new Box(new Vector2(1000 - (Box_Sprite.Width - five.Width) / 2, i - (Box_Sprite.Height - five.Height) / 2), color, $"5,{i/200}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                five.Draw(spriteBatch, new Vector2(1000, i));
            }
        }

        public string Menu_Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 mousePosVect = new Vector2(mouseState.X, mouseState.Y);
                foreach (Box box in boxes)
                {
                    if (box.box.Contains(mousePosVect))
                    {
                        return box.state;
                    }
                }
            }
            return "Main_Menu";
        }
    }
}
