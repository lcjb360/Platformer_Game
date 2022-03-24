using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Box
    {
        private Vector2 Position;
        private Color Colour;
        public Rectangle Bounds;
        public string State;
        private Sprite Sprite;

        public Box(Vector2 position, Color color, string state, Sprite sprite)
        {
            Position = position;
            Colour = color;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 100, 100);
            State = state;
            Sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position, Colour);
        }
    }
}
