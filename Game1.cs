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
        //(1366 * 767)
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
        public Level three_one;
        public Level four_one;
        public Level five_one;
        public Level one_two;
        public Level two_two;
        public Level three_two;


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
            //    new Player(SpriteSheet, new Vector2(30, window.Height - 90), 200),
            //    new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, window.Height - 30), window.Width, 30)
            //                         },
            //    new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, window.Height),
            //                           new Wall(SpriteSheet, new Vector2(window.Width-30, 0), 30, window.Height),
            //                           new Wall(SpriteSheet, new Vector2(0, 0), window.Width, 30)},
            //    new List<Spike>() { },
            //    new List<Lava>() { },
            //    SpriteSheet);

            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;

            Tutorial = new Level(true, false, level_number, window, new Vector2(1366 - (50), 768 - 50),
                new Player(SpriteSheet, new Vector2(30, 768 - 90), 200), 
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 1000, 30),
                                       new Platform(SpriteSheet, new Vector2(1000, 768 - 30 + 12), 1 * 60, 30),
                                       new Platform(SpriteSheet, new Vector2(1060, 768 - 30), 1366 - 1060, 30)}, 
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(770, 768 - 130), 30, 100),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(1366-30, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 1366, 30)}, 
                new List<Spike>() {    new Spike(SpriteSheet, new Vector2(850, 768 - 40), 5 * 9) }, 
                new List<Lava>() {     new Lava(SpriteSheet, new Vector2(1000, 768 - 29), 1 * 60) },
                SpriteSheet);

            two_one = new Level(false, false, level_number, window, new Vector2(1366 - 50, 768 -50),
                new Player(SpriteSheet, new Vector2(30, 768 - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 1366, 30)
                                     },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(1366-30, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 1366, 30),
                                       new Wall(SpriteSheet, new Vector2(100, 768 - 300), 1366 - 200, 768 - 300)},
                new List<Spike>() {    new Spike(SpriteSheet, new Vector2(100, 768 - 310), 1366 - 200 - ((1366 -200) % 9)) },
                new List<Lava>() {  },
                SpriteSheet);

            three_one = new Level(false, false, level_number, window, new Vector2(1366 - 50, 120),
                new Player(SpriteSheet, new Vector2(30, 768 - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(1200, 150), 400, 30),
                                       new Platform(SpriteSheet, new Vector2(300, 768 - 50), 100, 30, true, new Vector2(300, 768 - 350)),
                                       new Platform(SpriteSheet, new Vector2(600, 768 - 450), 100, 30, true, new Vector2(600, 768 - 150)),
                                       new Platform(SpriteSheet, new Vector2(900, 768 - 350), 100, 30, true, new Vector2(900, 768 - 650))},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(1366-30, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 1366, 30)},
                new List<Spike>() { },
                new List<Lava>() {     new Lava(SpriteSheet, new Vector2(60, 768 - 25), 25*60, 25)},
                SpriteSheet);

            four_one = new Level(false, false, level_number, window, new Vector2(1366 - 50, 768 - 50),
                new Player(SpriteSheet, new Vector2(30, 768 - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(1366 - 60, 768 - 30), 60, 30) },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(1366-30, 0), 30, 768)
                                       },
                new List<Spike>() {    new Spike(SpriteSheet, new Vector2(200, 768 - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(500, 768 - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(800, 768 - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(1100, 768 - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(335, 768 - 200), 18 * 9),
                                       new Spike(SpriteSheet, new Vector2(635, 768 - 200), 18 * 9),
                                       new Spike(SpriteSheet, new Vector2(935, 768 - 200), 18 * 9)
                                       },
                new List<Lava>() { },
                SpriteSheet);

            five_one = new Level(false, false, level_number, window, new Vector2(1366 - 50, 768 - 50),
                new Player(SpriteSheet, new Vector2(30, 768 - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(1366 - 60, 768 - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(200, 768 - 30), 100, 30, false, new Vector2(0, 0), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(400, 768 - 30), 100, 30, false, new Vector2(0, 0), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(600, 768 - 30), 100, 30, false, new Vector2(0, 0), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(800, 768 - 30), 100, 30, false, new Vector2(0, 0), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(1000, 768 - 30), 100, 30, false, new Vector2(0, 0), true, false, true)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(1366-30, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 1366, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(NumberSheet, new Vector2(0, 768), 1366) },
                SpriteSheet);

            one_two = new Level(true, false, level_number, window, new Vector2(1366 - 50, 768 - 50),
                new Player(SpriteSheet, new Vector2(30, 768 - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(70, 768 - 50), 100, 30, true, new Vector2(370, 768 - 50)),
                                       new Platform(SpriteSheet, new Vector2(770, 768 - 80), 100, 30, true, new Vector2(470, 768 - 80)),
                                       new Platform(SpriteSheet, new Vector2(870, 768 - 50), 100, 30, true, new Vector2(1170, 768 - 50)),
                                       new Platform(SpriteSheet, new Vector2(1366 - 60, 768 - 30), 60, 30)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(1366-30, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 1366, 30)},
                new List<Spike>() { },
                new List<Lava>(){      new Lava(SpriteSheet, new Vector2(0, 768), 1366) },
                SpriteSheet);

            two_two = new Level(true, false, level_number, window, new Vector2(1320, 768 - 530),
                new Player(SpriteSheet, new Vector2(30, window.Height - 90), 200), 
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(65, 768 - 85), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(170, 768 - 125), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(275, 768 - 165), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(380, 768 - 205), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(485, 768 - 245), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(590, 768 - 285), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(695, 768 - 325), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(800, 768 - 365), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(905, 768 - 405), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1010, 768 - 445), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1115, 768 - 485), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1220, 768 - 525), 100, 30, false, new Vector2(0,0), false, true, true)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(window.Width-30, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(0, 0), window.Width, 30)},
                new List<Spike>() { },
                new List<Lava>() {     new Lava(SpriteSheet, new Vector2(0, 768), 1366) },
                SpriteSheet);

            three_two = new Level(true, false, level_number, window, new Vector2(1320, 768 - 650),
                new Player(SpriteSheet, new Vector2(30, window.Height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, 768 - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(165, 768 - 125), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(330, 768 - 205), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(495, 768 - 285), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(660, 768 - 365), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(825, 768 - 445), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(990, 768 - 525), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1155, 768 - 605), 100, 30, false, new Vector2(0,0), false, true, true),},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(window.Width-30, 0), 30, 768),
                                       new Wall(SpriteSheet, new Vector2(0, 0), window.Width, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(0, 768), 1366) },
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
                        game_state = "2,1";
                    }
                    break;
                case "2,1":
                    if (two_one.Unlocked)
                    {
                        if (two_one.Update(gameTime))
                        {
                            two_one.Completed = true;
                            three_one.Unlocked = true;
                            game_state = "3,1";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "3,1":
                    if (three_one.Unlocked)
                    {
                        if (three_one.Update(gameTime))
                        {
                            three_one.Completed = true;
                            four_one.Unlocked = true;
                            game_state = "4,1";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "4,1":
                    if (four_one.Unlocked)
                    {
                        if (four_one.Update(gameTime))
                        {
                            four_one.Completed = true;
                            five_one.Unlocked = true;
                            game_state = "5,1";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "5,1":
                    if (five_one.Unlocked)
                    {
                        if (five_one.Update(gameTime))
                        {
                            five_one.Completed = true;
                            one_two.Unlocked = true;
                            game_state = "Main_Menu";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "1,2":
                    if (one_two.Unlocked)
                    {
                        if (one_two.Update(gameTime))
                        {
                            one_two.Completed = true;
                            two_two.Unlocked = true;
                            game_state = "2,2";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "2,2":
                    if (two_two.Unlocked)
                    {
                        if (two_two.Update(gameTime))
                        {
                            two_two.Completed = true;
                            three_two.Unlocked = true;
                            game_state = "3,2";
                        }
                    }
                    break;
                case "3,2":
                    if (three_two.Unlocked)
                    {
                        if (three_two.Update(gameTime))
                        {
                            three_two.Completed = true;
                            //four_two.Unlocked = true;
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
                    Tutorial.Draw(_spriteBatch ,gameTime, SpriteSheet);
                    break;
                case "2,1":
                    if (two_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        two_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "3,1":
                    if (three_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        three_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "4,1":
                    if (four_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        four_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "5,1":
                    if (five_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        five_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "1,2":
                    if (one_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        one_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "2,2":
                    if (two_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        two_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "3,2":
                    if (three_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        three_two.Draw(_spriteBatch, gameTime, SpriteSheet);
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
            for (int i = 1; i < 4; i += 1)
            {
                if (i == 2)
                {
                    color = Color.Green;
                }
                if (i == 3)
                {
                    color = Color.Blue;
                }
                box = new Box(new Vector2(window.Width * 1 / 11, window.Height * ((2 * i) - 1) / 7), color, $"1,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                one.Draw(spriteBatch, new Vector2((window.Width * 1 / 11) + (Box_Sprite.Width - one.Width) / 2, (window.Height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - one.Height) / 2)));

                box = new Box(new Vector2(window.Width * 3 / 11, window.Height * ((2 * i) - 1) / 7), color, $"2,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                two.Draw(spriteBatch, new Vector2((window.Width * 3 / 11) + (Box_Sprite.Width - two.Width) / 2, (window.Height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - two.Height) / 2)));

                box = new Box(new Vector2(window.Width * 5 / 11, window.Height * ((2 * i) - 1) / 7), color, $"3,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                three.Draw(spriteBatch, new Vector2((window.Width * 5 / 11) + (Box_Sprite.Width - three.Width) / 2, (window.Height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - three.Height) / 2)));

                box = new Box(new Vector2(window.Width * 7 / 11, window.Height * ((2 * i) - 1) / 7), color, $"4,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                four.Draw(spriteBatch, new Vector2((window.Width * 7 / 11) + (Box_Sprite.Width - four.Width) / 2, (window.Height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - four.Height) / 2)));

                box = new Box(new Vector2(window.Width * 9 / 11, window.Height * ((2 * i) - 1) / 7), color, $"5,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                five.Draw(spriteBatch, new Vector2((window.Width * 9 / 11) + (Box_Sprite.Width - five.Width) / 2, (window.Height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - four.Height) / 2)));
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
