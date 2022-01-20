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
        //(1366 * 768)
        public Rectangle window;
        public int level_number;
        public string game_state;
        private MouseState mouseState = new MouseState();
        const int screen_height = 768;
        const int screen_width = 1366;

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
        public Level four_two;
        public Level five_two;
        public Level one_three;
        public Level two_three;


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

            //default = new Level(false, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
            //    new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
            //    new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), screen_width, 30)
            //                         },
            //    new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
            //                           new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
            //                           new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
            //    new List<Spike>() { },
            //    new List<Lava>() { },
            //    SpriteSheet);

            float w_ratio = 1;
            float h_ratio = 1;


            Tutorial = new Level(true, false, level_number, window, new Vector2(screen_width - (50), screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 1000, 30),
                                       new Platform(SpriteSheet, new Vector2(1060, screen_height - 30), screen_width - 1060, 30)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(770, screen_height - 130), 30, 100),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { new Spike(SpriteSheet, new Vector2(850, screen_height - 40), 5 * 9) },
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(1000, screen_height - 23), 1 * 60, 25), },
                SpriteSheet);

            two_one = new Level(false, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), screen_width, 30)
                                     },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30),
                                       new Wall(SpriteSheet, new Vector2(100, screen_height - 300), screen_width - 200, screen_height - 300)},
                new List<Spike>() { new Spike(SpriteSheet, new Vector2(100, screen_height - 310), screen_width - 200 - ((screen_width - 200) % 9)) },
                new List<Lava>() { },
                SpriteSheet);

            three_one = new Level(false, false, level_number, window, new Vector2(screen_width - 50, 120),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(1200, 150), 400, 30),
                                       new Platform(SpriteSheet, new Vector2(300, screen_height - 50), 150, 30, true, new Vector2(300, screen_height - 350)),
                                       new Platform(SpriteSheet, new Vector2(600, screen_height - 450), 150, 30, true, new Vector2(600, screen_height - 150)),
                                       new Platform(SpriteSheet, new Vector2(900, screen_height - 350), 150, 30, true, new Vector2(900, screen_height - 650))},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(60, screen_height - 25), 25 * 60, 25) },
                SpriteSheet);

            four_one = new Level(false, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(screen_width - 60, screen_height - 30), 60, 30) },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height)
                                       },
                new List<Spike>() {    new Spike(SpriteSheet, new Vector2(200, screen_height - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(500, screen_height - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(800, screen_height - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(1100, screen_height - 500), 15 * 9),
                                       new Spike(SpriteSheet, new Vector2(335, screen_height - 200), 18 * 9),
                                       new Spike(SpriteSheet, new Vector2(635, screen_height - 200), 18 * 9),
                                       new Spike(SpriteSheet, new Vector2(935, screen_height - 200), 18 * 9)
                                       },
                new List<Lava>() { },
                SpriteSheet);

            five_one = new Level(false, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(screen_width - 60, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(200, screen_height - 30), 100, 30, false, new Vector2(0, 0), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(400, screen_height - 30), 100, 30, false, new Vector2(0, 0), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(600, screen_height - 30), 100, 30, false, new Vector2(0, 0), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(800, screen_height - 30), 100, 30, false, new Vector2(0, 0), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(1000, screen_height - 30), 100, 30, false, new Vector2(0, 0), true, false, true)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(NumberSheet, new Vector2(0, screen_height), screen_width) },
                SpriteSheet);

            one_two = new Level(true, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(70, screen_height - 50), 100, 30, true, new Vector2(370, screen_height - 50)),
                                       new Platform(SpriteSheet, new Vector2(770, screen_height - 80), 100, 30, true, new Vector2(470, screen_height - 80)),
                                       new Platform(SpriteSheet, new Vector2(870, screen_height - 50), 100, 30, true, new Vector2(1170, screen_height - 50)),
                                       new Platform(SpriteSheet, new Vector2(screen_width - 60, screen_height - 30), 60, 30)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(0, screen_height), screen_width) },
                SpriteSheet);

            two_two = new Level(false, false, level_number, window, new Vector2(1320, screen_height - 530),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(65, screen_height - 85), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(170, screen_height - 125), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(275, screen_height - 165), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(380, screen_height - 205), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(485, screen_height - 245), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(590, screen_height - 285), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(695, screen_height - 325), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(800, screen_height - 365), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(905, screen_height - 405), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1010, screen_height - 445), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1115, screen_height - 485), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1220, screen_height - 525), 100, 30, false, new Vector2(0,0), false, true, true)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(0, screen_height), screen_width) },
                SpriteSheet);

            three_two = new Level(false, false, level_number, window, new Vector2(1290, screen_height - 650),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(165, screen_height - 125), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(330, screen_height - 205), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(495, screen_height - 285), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(660, screen_height - 365), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(825, screen_height - 445), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(990, screen_height - 525), 100, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(1155, screen_height - 605), 100, 30, false, new Vector2(0,0), false, true, true),},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(0, screen_height), screen_width) },
                SpriteSheet);

            four_two = new Level(false, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), screen_width, 30)
                                     },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(300, 1), 30, screen_height, true),
                                       new Wall(SpriteSheet, new Vector2(600, 1), 30, screen_height, true),
                                       new Wall(SpriteSheet, new Vector2(900, 1), 30, screen_height, true),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() { },
                SpriteSheet);

            five_two = new Level(false, false, level_number, window, new Vector2(screen_width - 50, screen_height - 330),
                new Player(SpriteSheet, new Vector2(30, screen_height - 290 - 60), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 290), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(screen_width - 60, screen_height - 290), 60, 30),
                                       new Platform(SpriteSheet, new Vector2( 110, screen_height - 100), 100, 30, true, new Vector2( 110, screen_height - 500), true, false, true),
                                       new Platform(SpriteSheet, new Vector2( 310, screen_height - 500), 100, 30, true, new Vector2( 310, screen_height - 100), true, false, false),
                                       new Platform(SpriteSheet, new Vector2( 510, screen_height - 100), 100, 30, true, new Vector2( 510, screen_height - 500), true, false, true),
                                       new Platform(SpriteSheet, new Vector2( 710, screen_height - 500), 100, 30, true, new Vector2( 710, screen_height - 100), true, false, false),
                                       new Platform(SpriteSheet, new Vector2( 910, screen_height - 100), 100, 30, true, new Vector2( 910, screen_height - 500), true, false, true),
                                       new Platform(SpriteSheet, new Vector2( 1110, screen_height - 500), 100, 30, true, new Vector2( 1110, screen_height - 100), true, false, false),

                                     },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() {     new Lava(SpriteSheet, new Vector2(0, screen_height - 250), screen_width, 250) },
                SpriteSheet);

            one_three = new Level(true, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), screen_width, 30)
                                     },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(500, 30), 30, screen_height -60, Color.Yellow),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)
                                       },
                new List<Spike>() { },
                new List<Lava>() { },
                new List<Key>() { new Key(SpriteSheet, new Vector2(200, screen_height - 50)) },
                SpriteSheet);

            two_three = new Level(true, false, level_number, window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), screen_width, 30),
                                       new Platform(SpriteSheet, new Vector2(1128, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1153, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1178, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1203, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1228, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1253, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1278, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1303, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1328, 350), 5, 30),
                                       new Platform(SpriteSheet, new Vector2(1353, 350), 5, 30),},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(1100, 350), 30, screen_height - 380, Color.Yellow),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)
                                       },
                new List<Spike>() { },
                new List<Lava>() { },
                new List<Key>() { new Key(SpriteSheet, new Vector2(1200, screen_height - 50)) },
                SpriteSheet);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.End))
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
                            four_two.Unlocked = true;
                            game_state = "4,2";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "4,2":
                    if (four_two.Unlocked)
                    {
                        if (four_two.Update(gameTime))
                        {
                            four_two.Completed = true;
                            five_two.Unlocked = true;
                            game_state = "5,2";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "5,2":
                    if (five_two.Unlocked)
                    {
                        if (five_two.Update(gameTime))
                        {
                            five_two.Completed = true;
                            game_state = "Main_Menu";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "1,3":
                    if (one_three.Unlocked)
                    {
                        if (one_three.Update(gameTime))
                        {
                            one_three.Completed = true;
                            two_three.Unlocked = true;
                            game_state = "2,3";
                        }
                    }
                    else
                    {
                        game_state = "Main_Menu";
                    }
                    break;
                case "2,3":
                    if (two_three.Unlocked)
                    {
                        if (two_three.Update(gameTime))
                        {
                            two_three.Completed = true;
                            //three_three.Unlocked = true;
                            game_state = "3,3";
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
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)screen_height;
            Matrix matrix = new Matrix(new Vector4(w_ratio, 0, 0, 0), new Vector4(0, h_ratio, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
    matrix);

            switch (game_state)
            {
                case "1,1":
                    GraphicsDevice.Clear(Color.Tomato);
                    Tutorial.Draw(_spriteBatch, gameTime, SpriteSheet);
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
                case "4,2":
                    if (four_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        four_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "5,2":
                    if (five_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        five_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "1,3":
                    if (one_three.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(150, 200, 255));
                        one_three.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "2,3":
                    if (two_three.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(150, 200, 255));
                        two_three.Draw(_spriteBatch, gameTime, SpriteSheet);
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
                box = new Box(new Vector2(screen_width * 1 / 11, screen_height * ((2 * i) - 1) / 7), color, $"1,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                one.Draw(spriteBatch, new Vector2((screen_width * 1 / 11) + (Box_Sprite.Width - one.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - one.Height) / 2)));

                box = new Box(new Vector2(screen_width * 3 / 11, screen_height * ((2 * i) - 1) / 7), color, $"2,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                two.Draw(spriteBatch, new Vector2((screen_width * 3 / 11) + (Box_Sprite.Width - two.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - two.Height) / 2)));

                box = new Box(new Vector2(screen_width * 5 / 11, screen_height * ((2 * i) - 1) / 7), color, $"3,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                three.Draw(spriteBatch, new Vector2((screen_width * 5 / 11) + (Box_Sprite.Width - three.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - three.Height) / 2)));

                box = new Box(new Vector2(screen_width * 7 / 11, screen_height * ((2 * i) - 1) / 7), color, $"4,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                four.Draw(spriteBatch, new Vector2((screen_width * 7 / 11) + (Box_Sprite.Width - four.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - four.Height) / 2)));

                box = new Box(new Vector2(screen_width * 9 / 11, screen_height * ((2 * i) - 1) / 7), color, $"5,{i}", Box_Sprite);
                boxes.Add(box);
                box.Draw(spriteBatch);
                five.Draw(spriteBatch, new Vector2((screen_width * 9 / 11) + (Box_Sprite.Width - five.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - four.Height) / 2)));
            }
        }

        public string Menu_Update(GameTime gameTime)
        {
            float w_ratio = (float)window.Width / (float)screen_width;
            float h_ratio = (float)window.Height / (float)screen_height;
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
