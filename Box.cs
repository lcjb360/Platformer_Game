using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer_Game
{
    public class Box
    {
        private Vector2 position;
        private Color color;
        public Rectangle box;
        public string state;
        private Sprite sprite;

        public Box(Vector2 Position, Color Color, string State, Sprite Sprite)
        {
            position = Position;
            color = Color;
            box = new Rectangle((int)Position.X, (int)Position.Y, 100, 100);
            state = State;
            sprite = Sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, color);
        }
    }
}
