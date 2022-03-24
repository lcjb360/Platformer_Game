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
        public List<Rectangle> Parts = new List<Rectangle>();
        public Color Colour = Color.White;

        public Wall(Texture2D texture, Vector2 position, float width, float height)
        {
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X, position.Y);
            Width = width;
            Height = height;
        }

        public Wall(Texture2D texture, Vector2 position, float width, float height, Color colour)
        {
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X, position.Y);
            Width = width;
            Height = height;
            Colour = colour;
        }

        public Wall(Texture2D texture, Vector2 position, float width, float height, bool destructable)
        {
            Default_Sprite = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X, position.Y);
            Width = width;
            Height = height;
            Destructible = destructable;
            if (destructable)
            {
                for (int x = (int)Position.X; x < (int)(Position.X + Width); x += 9)
                {
                    for (int y = (int)Position.Y; y < (int)(Position.Y + Height); y += 9)
                    {
                        Parts.Add(new Rectangle((int)x, (int)y, 9, 9));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            if (Destructible)
            {
                foreach (Rectangle rectangle in Parts)
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
