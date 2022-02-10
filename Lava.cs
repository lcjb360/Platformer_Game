using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Lava : Visible_Object
    {
        public int Length;
        public new const int Width = 60;

        public Lava(Texture2D texture, Vector2 position, int length)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Sprite = new Sprite(texture, 0, 76, 60, 12);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Length = (int)((float)length * w_ratio);
            Height = 12;
        }
        public Lava(Texture2D texture, Vector2 position, int length, int height)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Sprite = new Sprite(texture, 0, 76, 60, 12);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Length = (int)((float)length * w_ratio);
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int x = (int)Position.X; x < ((int)Position.X + Length); x += (int)Width)
            {
                Default_Sprite.Draw(spriteBatch, new Vector2(x, Position.Y), (int)Width, (int)Height);
            }
        }
    }
}
