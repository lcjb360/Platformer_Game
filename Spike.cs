using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Spike : Visible_Object
    {
        public int Length;
        
        public Spike(Texture2D texture, Vector2 position, int length)
        {
            Default_Sprite = new Sprite(texture, 0, 92, 9, 10);
            Position = new Vector2(position.X, position.Y);
            Length = length;
            Width = 9;
            Height = 10;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            for (int x = (int)Position.X; x < ((int)Position.X+Length); x+=(int)Width)
            {
                Default_Sprite.Draw(spriteBatch, new Vector2(x , Position.Y), (int)Width, (int)Height);
            }
        }
    }
}
