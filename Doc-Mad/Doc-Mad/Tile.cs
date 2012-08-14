using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Doc_Mad
{
    class Tile : Object
    {


        public Tile(Texture2D texture, Vector2 initPosition, Rectangle box, Rectangle rect)
            : base(texture, initPosition, box, rect)
        {
        }


        public void OnCollision()
        {
            ;
        }
    }
}
