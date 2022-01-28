using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Wall : Visible_Object
    {
        public bool Destructible = false;
        public List<Rectangle> parts = new List<Rectangle>();
        public Color Colour = Color.White;

        public Wall(Texture2D texture, Vector2 position, float width, float height)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Width = width * w_ratio;
            Height = height * h_ratio;
        }

        public Wall(Texture2D texture, Vector2 position, float width, float height, Color colour)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Width = width * w_ratio;
            Height = height * h_ratio;
            Colour = colour;
        }

        public Wall(Texture2D texture, Vector2 position, float width, float height, bool destructable)
        {
            float w_ratio = 1;
            float h_ratio = 1;
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Width = width * w_ratio;
            Height = height * h_ratio;
            Destructible = destructable;
            if (destructable)
            {
                for (int x = (int)Position.X; x < (int)(Position.X + Width); x += (int)(((float)9) * w_ratio))
                {
                    for (int y = (int)Position.Y; y < (int)(Position.Y + Height); y += (int)(((float)9) * h_ratio))
                    {
                        parts.Add(new Rectangle((int)x, (int)y, (int)((float)9 * w_ratio), (int)((float)9 * h_ratio)));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            if (Destructible)
            {
                foreach (Rectangle rectangle in parts)
                {
                    Default_Sprite.Draw(spriteBatch, new Vector2(rectangle.X, rectangle.Y), rectangle.Width, rectangle.Width, Color.Red);
                }
            }
            else
            {
                Default_Sprite.Draw(spriteBatch, Position, (int)Width, (int)Height, Colour);
            }
        }
    }
}
