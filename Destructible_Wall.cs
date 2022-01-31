using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Platformer_Game
{
    public class Destructible_Wall : Wall
    {
        

        public Destructible_Wall(Texture2D texture, Vector2 position, float width, float height) : base(texture, position, width, height)
        {
            float w_ratio = (float)window.Width / (float)1366;
            float h_ratio = (float)window.Height / (float)768;
            Default_Wall = new Sprite(texture, 0, 61, 59, 14);
            Position = new Vector2(position.X * w_ratio, position.Y * h_ratio);
            Width = width * w_ratio;
            Height = height * h_ratio;
            for (int x = (int)Position.X; x < (int)(Position.X + Width); x += (int)(((float)9) * w_ratio))
            {
                for (int y = (int)Position.Y; y < (int)(Position.Y + Height); y += (int)(((float)9) * h_ratio))
                {
                    parts.Add(new Rectangle((int)x, (int)y, (int)((float)9 * w_ratio), (int)((float)9 * h_ratio)));
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Rectangle rectangle in parts)
            {
                Default_Wall.Draw(spriteBatch, new Vector2(rectangle.X, rectangle.Y), rectangle.Width, rectangle.Width, Color.Red);
            }
            
        }
    }
}
