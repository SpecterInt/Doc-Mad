using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Doc_Mad
{
    class NPC
    {
        protected Vector2 position;
        protected Vector2 movement;
        protected Rectangle hitBox;
        protected Rectangle rectangle; // velikost framu

        protected Texture2D sprite;


        public NPC(Texture2D texture, Vector2 initPosition, Rectangle box, Rectangle rect, Vector2 initMovement)
        {
            sprite = texture;
            position = initPosition;
            movement = initMovement;
            rectangle = rect;
            hitBox = box;
        }

    }
}
