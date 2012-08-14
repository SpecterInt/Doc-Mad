using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Doc_Mad
{
    class Object
    {
        protected Vector2 position;
        protected Rectangle hitBox;
        protected Rectangle rectangle; // velikost framu

        protected Texture2D sprite;


        public Object(Texture2D texture, Vector2 initPosition, Rectangle box, Rectangle rect)
        {
            sprite = texture;
            position = initPosition;
            hitBox = box;
            rectangle = rect;
        }

        public Rectangle GetHitBox()
        {
            return new Rectangle((int)Math.Round(position.X + hitBox.X), (int)Math.Round(position.Y + hitBox.Y), hitBox.Width, hitBox.Height);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, rectangle, Color.White);
        }

    }
}
