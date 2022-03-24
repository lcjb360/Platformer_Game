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
        public Rectangle Window;
        public int Level_number;
        public string Game_state;
        private MouseState mouseState = new MouseState();
        const int screen_height = 768;
        const int screen_width = 1366;

        private Texture2D SpriteSheet;
        private Texture2D NumberSheet;
        public Sprite Box_Sprite;
        public Box Box;
        public Sprite One;
        public Sprite Two;
        public Sprite Three;
        public Sprite Four;
        public Sprite Five;
        public List<Box> Boxes = new List<Box>();

        public List<Level> Levels = new List<Level>();
        public Level Tutorial;
        public Level Two_one;
        public Level Three_one;
        public Level Four_one;
        public Level Five_one;
        public Level One_two;
        public Level Two_two;
        public Level Three_two;
        public Level Four_two;
        public Level Five_two;
        public Level One_three;
        public Level Two_three;
        public Level Three_three;
        public Level Four_three;
        public Level Five_three;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            //_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 60;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            Window = GraphicsDevice.Viewport.Bounds;
            Game_state = "Main_Menu";
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet = Content.Load<Texture2D>("player_images");
            NumberSheet = Content.Load<Texture2D>("Numbers");

            Box_Sprite = new Sprite(NumberSheet, 5, 5, 100, 100);
            One = new Sprite(NumberSheet, 14, 226, 42, 73);
            Two = new Sprite(NumberSheet, 73, 226, 64, 73);
            Three = new Sprite(NumberSheet, 144, 226, 63, 73);
            Four = new Sprite(NumberSheet, 215, 226, 62, 73);
            Five = new Sprite(NumberSheet, 285, 226, 63, 73);


            Tutorial = new Level(true, false, Level_number, Window, new Vector2(screen_width - (50), screen_height - 50),
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

            Two_one = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
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

            Three_one = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, 120),
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

            Four_one = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
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

            Five_one = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
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

            One_two = new Level(true, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
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

            Two_two = new Level(false, false, Level_number, Window, new Vector2(1320, screen_height - 530),
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

            Three_two = new Level(false, false, Level_number, Window, new Vector2(1290, screen_height - 650),
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

            Four_two = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
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

            Five_two = new Level(true, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 330),
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
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(0, screen_height - 250), screen_width, 250) },
                SpriteSheet);

            One_three = new Level(true, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), screen_width, 30),
                                       new Platform(SpriteSheet, new Vector2(110, screen_height - 250), 200, 30)},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(500, 30), 30, screen_height -60, Color.Yellow),
                                       new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)
                                       },
                new List<Spike>() { },
                new List<Lava>() { },
                new List<Key>() { new Key(SpriteSheet, new Vector2(200, screen_height - 300)) },
                SpriteSheet);

            Two_three = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
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

            Three_three = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(screen_width - 60, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(60, screen_height - 30), 150, 30, true, new Vector2(screen_width - (60 + 150), screen_height - 30)),
                                       new Platform(SpriteSheet, new Vector2(screen_width - (60 + 150), screen_height - 90), 150, 30, true, new Vector2(60, screen_height - 90))},
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30),
                                       new Wall(SpriteSheet, new Vector2(screen_width/2, 0), 50, screen_height, true)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(NumberSheet, new Vector2(0, screen_height), screen_width) },
                SpriteSheet);

            Four_three = new Level(false, false, Level_number, Window, new Vector2(screen_width - 50, 100),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(screen_width - 60, 130), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(0, screen_height), screen_width, 30)
                                     },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30)},
                new List<Spike>() { },
                new List<Lava>() { new Lava(SpriteSheet, new Vector2(0, screen_height - 10), screen_width),
                                   new Lava(SpriteSheet, new Vector2(0, screen_height - 300), screen_width - 160, 100),
                                   new Lava(SpriteSheet, new Vector2(160, screen_height - 600), screen_width, 100)},
                SpriteSheet);

            Five_three = new Level(true, false, Level_number, Window, new Vector2(screen_width - 50, screen_height - 50),
                new Player(SpriteSheet, new Vector2(30, screen_height - 90), 200),
                new List<Platform>() { new Platform(SpriteSheet, new Vector2(0, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(60, screen_height - 30), 400, 30, false, new Vector2(0,0), false, true, true),
                                       new Platform(SpriteSheet, new Vector2(screen_width - 60, screen_height - 30), 60, 30),
                                       new Platform(SpriteSheet, new Vector2(420, screen_height - 31), 150, 30, true, new Vector2(820, screen_height - 31), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(820, screen_height - 90), 150, 30, true, new Vector2(420, screen_height - 90), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(420, screen_height - 90), 150, 30, true, new Vector2(820, screen_height - 90), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(820, screen_height - 31), 150, 30, true, new Vector2(420, screen_height - 31), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(970, screen_height - 31), 100, 30, true, new Vector2(970, screen_height - 431), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(850, screen_height - 640), 100, 30, true, new Vector2(850, screen_height - 240), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(420, screen_height - 580), 150, 30, true, new Vector2(820, screen_height - 640), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(820, screen_height - 640), 150, 30, true, new Vector2(420, screen_height - 580), true, false, false),
                                       new Platform(SpriteSheet, new Vector2(420, screen_height - 640), 150, 30, true, new Vector2(820, screen_height - 580), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(820, screen_height - 580), 150, 30, true, new Vector2(420, screen_height - 640), true, false, true),
                                       new Platform(SpriteSheet, new Vector2(0, screen_height - 230), 60, 30, false, new Vector2(0,0), false, true, true),
                                       },
                new List<Wall>() {     new Wall(SpriteSheet, new Vector2(0, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(screen_width-30, 0), 30, screen_height),
                                       new Wall(SpriteSheet, new Vector2(0, 0), screen_width, 30),
                                       new Wall(SpriteSheet, new Vector2(screen_width/2, 0), 50, screen_height, true),
                                       new Wall(SpriteSheet, new Vector2(60, screen_height - 500), 30, 500 - 200),
                                       new Wall(SpriteSheet, new Vector2(screen_width - 100, screen_height - 100), 30, 100, Color.Yellow),
                                       new Wall(SpriteSheet, new Vector2(screen_width - 100, screen_height - 100), 100, 30, Color.Yellow),},
                new List<Spike>() { new Spike(SpriteSheet, new Vector2(60, screen_height - 530), 300), },
                new List<Lava>() { new Lava(NumberSheet, new Vector2(0, screen_height), screen_width),
                                   new Lava(SpriteSheet, new Vector2(0, screen_height - 230), 60, 30),
                                   new Lava(SpriteSheet, new Vector2(60, screen_height - 500), 30, 300)},
                new List<Key>() { new Key(SpriteSheet, new Vector2(35, screen_height - 260)) },
                SpriteSheet);

            for (int i = 160; i < (screen_width - 30); i += 100)
            {
                Four_three.Platforms.Add(new Platform(SpriteSheet, new Vector2(i, screen_height - 30), 1, 30));
            }
            for (int i = 140; i < (screen_width - 30); i += 100)
            {
                Four_three.Platforms.Add(new Platform(SpriteSheet, new Vector2(i, screen_height - 330), 1, 30));
            }
            for (int i = 190; i < (screen_width - 30); i += 100)
            {
                Four_three.Platforms.Add(new Platform(SpriteSheet, new Vector2(i, screen_height - 630), 1, 30));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.End))
            {
                Exit();
            }
            switch (Game_state)
            {
                case "1,1":
                    if (Tutorial.Update(gameTime))
                    {
                        Tutorial.Completed = true;
                        Two_one.Unlocked = true;
                        Game_state = "2,1";
                    }
                    break;
                case "2,1":
                    if (Two_one.Unlocked)
                    {
                        if (Two_one.Update(gameTime))
                        {
                            Two_one.Completed = true;
                            Three_one.Unlocked = true;
                            Game_state = "3,1";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "3,1":
                    if (Three_one.Unlocked)
                    {
                        if (Three_one.Update(gameTime))
                        {
                            Three_one.Completed = true;
                            Four_one.Unlocked = true;
                            Game_state = "4,1";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "4,1":
                    if (Four_one.Unlocked)
                    {
                        if (Four_one.Update(gameTime))
                        {
                            Four_one.Completed = true;
                            Five_one.Unlocked = true;
                            Game_state = "5,1";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "5,1":
                    if (Five_one.Unlocked)
                    {
                        if (Five_one.Update(gameTime))
                        {
                            Five_one.Completed = true;
                            One_two.Unlocked = true;
                            Game_state = "Main_Menu";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "1,2":
                    if (One_two.Unlocked)
                    {
                        if (One_two.Update(gameTime))
                        {
                            One_two.Completed = true;
                            Two_two.Unlocked = true;
                            Game_state = "2,2";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "2,2":
                    if (Two_two.Unlocked)
                    {
                        if (Two_two.Update(gameTime))
                        {
                            Two_two.Completed = true;
                            Three_two.Unlocked = true;
                            Game_state = "3,2";
                        }
                    }
                    break;
                case "3,2":
                    if (Three_two.Unlocked)
                    {
                        if (Three_two.Update(gameTime))
                        {
                            Three_two.Completed = true;
                            Four_two.Unlocked = true;
                            Game_state = "4,2";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "4,2":
                    if (Four_two.Unlocked)
                    {
                        if (Four_two.Update(gameTime))
                        {
                            Four_two.Completed = true;
                            Five_two.Unlocked = true;
                            Game_state = "5,2";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "5,2":
                    if (Five_two.Unlocked)
                    {
                        if (Five_two.Update(gameTime))
                        {
                            Five_two.Completed = true;
                            Game_state = "Main_Menu";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "1,3":
                    if (One_three.Unlocked)
                    {
                        if (One_three.Update(gameTime))
                        {
                            One_three.Completed = true;
                            Two_three.Unlocked = true;
                            Game_state = "2,3";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "2,3":
                    if (Two_three.Unlocked)
                    {
                        if (Two_three.Update(gameTime))
                        {
                            Two_three.Completed = true;
                            Three_three.Unlocked = true;
                            Game_state = "3,3";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "3,3":
                    if (Three_three.Unlocked)
                    {
                        if (Three_three.Update(gameTime))
                        {
                            Three_three.Completed = true;
                            Four_three.Unlocked = true;
                            Game_state = "4,3";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "4,3":
                    if (Four_three.Unlocked)
                    {
                        if (Four_three.Update(gameTime))
                        {
                            Four_three.Completed = true;
                            Five_three.Unlocked = true;
                            Game_state = "5,3";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case "5,3":
                    if (Five_three.Unlocked)
                    {
                        if (Five_three.Update(gameTime))
                        {
                            Five_three.Completed = true;
                            Game_state = "Main_Menu";
                        }
                    }
                    else
                    {
                        Game_state = "Main_Menu";
                    }
                    break;
                case ("Main_Menu"):
                    Game_state = Menu_Update(gameTime);
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float w_ratio = (float)Window.Width / (float)1366;
            float h_ratio = (float)Window.Height / (float)screen_height;
            Matrix matrix = new Matrix(new Vector4(w_ratio, 0, 0, 0), new Vector4(0, h_ratio, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
    matrix);

            switch (Game_state)
            {
                case "1,1":
                    GraphicsDevice.Clear(Color.Tomato);
                    Tutorial.Draw(_spriteBatch, gameTime, SpriteSheet);
                    break;
                case "2,1":
                    if (Two_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        Two_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "3,1":
                    if (Three_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        Three_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "4,1":
                    if (Four_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        Four_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "5,1":
                    if (Five_one.Unlocked)
                    {
                        GraphicsDevice.Clear(Color.Tomato);
                        Five_one.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "1,2":
                    if (One_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        One_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "2,2":
                    if (Two_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        Two_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "3,2":
                    if (Three_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        Three_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "4,2":
                    if (Four_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        Four_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "5,2":
                    if (Five_two.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(100, 200, 75));
                        Five_two.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "1,3":
                    if (One_three.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(150, 200, 255));
                        One_three.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "2,3":
                    if (Two_three.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(150, 200, 255));
                        Two_three.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "3,3":
                    if (Three_three.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(150, 200, 255));
                        Three_three.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "4,3":
                    if (Four_three.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(150, 200, 255));
                        Four_three.Draw(_spriteBatch, gameTime, SpriteSheet);
                    }
                    break;
                case "5,3":
                    if (Five_three.Unlocked)
                    {
                        GraphicsDevice.Clear(new Color(150, 200, 255));
                        Five_three.Draw(_spriteBatch, gameTime, SpriteSheet);
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
                Box = new Box(new Vector2(screen_width * 1 / 11, screen_height * ((2 * i) - 1) / 7), color, $"1,{i}", Box_Sprite);
                Boxes.Add(Box);
                Box.Draw(spriteBatch);
                One.Draw(spriteBatch, new Vector2((screen_width * 1 / 11) + (Box_Sprite.Width - One.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - One.Height) / 2)));

                Box = new Box(new Vector2(screen_width * 3 / 11, screen_height * ((2 * i) - 1) / 7), color, $"2,{i}", Box_Sprite);
                Boxes.Add(Box);
                Box.Draw(spriteBatch);
                Two.Draw(spriteBatch, new Vector2((screen_width * 3 / 11) + (Box_Sprite.Width - Two.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - Two.Height) / 2)));

                Box = new Box(new Vector2(screen_width * 5 / 11, screen_height * ((2 * i) - 1) / 7), color, $"3,{i}", Box_Sprite);
                Boxes.Add(Box);
                Box.Draw(spriteBatch);
                Three.Draw(spriteBatch, new Vector2((screen_width * 5 / 11) + (Box_Sprite.Width - Three.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - Three.Height) / 2)));

                Box = new Box(new Vector2(screen_width * 7 / 11, screen_height * ((2 * i) - 1) / 7), color, $"4,{i}", Box_Sprite);
                Boxes.Add(Box);
                Box.Draw(spriteBatch);
                Four.Draw(spriteBatch, new Vector2((screen_width * 7 / 11) + (Box_Sprite.Width - Four.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - Four.Height) / 2)));

                Box = new Box(new Vector2(screen_width * 9 / 11, screen_height * ((2 * i) - 1) / 7), color, $"5,{i}", Box_Sprite);
                Boxes.Add(Box);
                Box.Draw(spriteBatch);
                Five.Draw(spriteBatch, new Vector2((screen_width * 9 / 11) + (Box_Sprite.Width - Five.Width) / 2, (screen_height * ((2 * i) - 1)) / 7 + ((Box_Sprite.Height - Four.Height) / 2)));
            }
        }

        public string Menu_Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 mousePosVect = new Vector2(mouseState.X, mouseState.Y);
                foreach (Box box in Boxes)
                {
                    if (box.Bounds.Contains(mousePosVect))
                    {
                        return box.State;
                    }
                }
            }
            return "Main_Menu";
        }
    }
}
