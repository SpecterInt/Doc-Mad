using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Doc_Mad
{
    class Player : NPC
    {
        private Keys jump = Keys.Up;
        private Keys left = Keys.Left;
        private Keys right = Keys.Right;
        private float iFrame = 3;
        private bool rDirect = true;

        public Player(Texture2D texture, Vector2 initPosition, Rectangle box, Rectangle rect, Vector2 initMovement)
            : base(texture, initPosition, box, rect, initMovement)
        {

        }

        public void Update(List<Tile> objects, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            elapsed = MathHelper.Min(elapsed, 30);
            float ratio = elapsed / 16;
            KeyboardState state = Keyboard.GetState();

            // Pohyb
            if (state.IsKeyDown(left) && state.IsKeyDown(right))
            {
                movement.X = 0;
            }
            else
            {
                if (state.IsKeyDown(left) && (movement.Y == 0))
                {
                    movement.X = MathHelper.Max(movement.X - ratio, -5);
                    if (rDirect) rDirect = false;
                }
                if (state.IsKeyDown(right) && (movement.Y == 0))
                {
                    movement.X = MathHelper.Min(movement.X + ratio, 5);
                    if (!rDirect) rDirect = true;
                }

                if (state.IsKeyDown(left) && (movement.Y != 0))
                {
                    movement.X = MathHelper.Max(movement.X - ratio / 2, -5);
                    if (rDirect) rDirect = false;
                }
                if (state.IsKeyDown(right) && (movement.Y != 0))
                {
                    movement.X = MathHelper.Min(movement.X + ratio / 2, 5);
                    if (!rDirect) rDirect = true;
                }
            }

            if (state.IsKeyDown(jump) && (movement.Y == 0))
                movement.Y = -11;

            if (state.IsKeyUp(left) && state.IsKeyUp(right) && (movement.X != 0) && (movement.Y == 0))
                movement.X = 0;


            movement.Y += ratio * 0.4f;
            position.Y += movement.Y * ratio;


            // Detekce kolizi: vertikalne
            for (int i = 0; i < objects.Count; i++)
            {
                Rectangle cache = new Rectangle((int)Math.Ceiling(position.X + hitBox.X), (int)Math.Ceiling(position.Y + hitBox.Y), hitBox.Width, hitBox.Height);

                if (cache.Intersects(objects[i].GetHitBox()))
                {
                    float posX = position.X;
                    float posY = position.Y;

                    if (movement.Y > 0)
                    {
                        float diff = (posY + hitBox.Y + hitBox.Height - objects[i].GetPosition().Y);
                        if ((diff > 0) && (diff < 20))
                        {
                            position.Y -= diff;
                            movement.Y = 0;
                        }
                    }

                    if (movement.Y < 0)
                    {
                        float diff = (objects[i].GetPosition().Y + objects[i].GetHitBox().Height - posY - hitBox.Y);
                        if ((diff > 0) && (diff < 20))
                        {
                            position.Y += diff;
                            movement.Y = 0.01f;
                        }
                    }


                    // Unused
                    objects[i].OnCollision();
                }
            }


            position.X += movement.X * ratio;

            // Detekce kolizi: horizontalne
            for (int i = 0; i < objects.Count; i++)
            {
                Rectangle cache = new Rectangle((int)Math.Ceiling(position.X + hitBox.X), (int)Math.Ceiling(position.Y + hitBox.Y), hitBox.Width, hitBox.Height);

                if (cache.Intersects(objects[i].GetHitBox()))
                {
                    float posX = position.X;
                    float posY = position.Y;

                    if (movement.X > 0)
                    {
                        float diff = (posX + hitBox.X + hitBox.Width - objects[i].GetPosition().X);
                        if ((diff > 0) && (diff < 20))
                        {
                            position.X -= diff;
                            movement.X = 0;
                        }
                    }

                    if (movement.X < 0)
                    {
                        float diff = (objects[i].GetPosition().X + objects[i].GetHitBox().Width - posX - hitBox.X);
                        if ((diff > 0) && (diff < 20))
                        {
                            position.X += diff;
                            movement.X = 0;
                        }
                    }

                    // Unused
                    objects[i].OnCollision();
                }
            }


            // Nastaveni snimku na vykresleni
            if ((movement.X == 0) && (movement.Y == 0))
                iFrame = (iFrame + ratio / 8) % 4;

            if ((movement.X != 0) && (movement.Y == 0))
            {
                iFrame = (iFrame + ratio / 8) % 4 + 20;
            }

            if (movement.Y != 0)
                iFrame = 62;


        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle frame = new Rectangle((int)(iFrame)%10*80, (int)(iFrame)/10*80, 80, 80);
            Vector2 pos = position;

            if (!rDirect) pos.X -= 6; // Zarovnani pri otoceni

            spriteBatch.Draw(sprite, pos, frame, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), (rDirect ? SpriteEffects.None : SpriteEffects.FlipHorizontally), 1);
        }

    }
}
