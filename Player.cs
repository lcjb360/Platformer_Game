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
        public Sprite Stationary_Right_Sprite;
        public Sprite Stationary_Left_Sprite;
        public Sprite Moving_Right_Sprite;
        public Sprite Moving_Left_Sprite;
        public Sprite Current_Sprite;
        public Vector2 Position;
        public int width;
        public int height;
        public Vector2 Velocity;

        public Player(Texture2D texture, Vector2 position)
        {
            Stationary_Right_Sprite = new Sprite(texture, 0, 0, 30, 60);
            Stationary_Left_Sprite = new Sprite(texture, 0, 0, 30, 60);
            Moving_Right_Sprite = new Sprite(texture, 30, 0, 30, 60);
            Moving_Left_Sprite = new Sprite(texture, 30, 0, 30, 60);
            Current_Sprite = Stationary_Right_Sprite;
            width = Current_Sprite.Width;
            height = Current_Sprite.Height;
            Position = position;
        }

        private bool OnPlatform()
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Current_Sprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
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
            Position += Velocity;


            //Sprite Control
            if (Velocity.X == 0)
            {
                if (Current_Sprite == Moving_Right_Sprite)
                {
                    Current_Sprite = Stationary_Right_Sprite;
                }
                if (Current_Sprite == Moving_Left_Sprite)
                {
                    Current_Sprite = Stationary_Left_Sprite;
                }
            }
            if (Velocity.X > 0)
            {
                Current_Sprite = Moving_Right_Sprite;
            }
            if (Velocity.X < 0)
            {
                Current_Sprite = Moving_Left_Sprite;
            }
        }
    }
}
