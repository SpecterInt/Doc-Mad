#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Doc_Mad
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int cd = 1000;

        Texture2D background;
        Vector2 bgPos = new Vector2(0, 0);

        Player player;

        List<Tile> tiles = new List<Tile>();


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Nacteni hrace
            player = new Player(Content.Load<Texture2D>("davis2_0.png"), new Vector2(100, 130), new Rectangle(21, 14, 32, 63), new Rectangle(0, 0, 80, 80), new Vector2(0, 0));


            // Nacteni pozadi
            background = Content.Load<Texture2D>("clouds.png");


            // Nahodime si nejake ty dilky
            for (int i = 0; i < 11; i++)
            {
                tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(32 * (i + 1), 350), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            }

            for (int i = 0; i < 3; i++)
            {
                tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(32 * (i + 12), 400), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            }

            for (int i = 0; i < 5; i++)
            {
                tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(32 * (i + 15), 350), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            }

            for (int i = 0; i < 5; i++)
            {
                tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(32 * (i + 8), 190), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            }

            for (int i = 0; i < 15; i++)
            {
                tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(0, 32 * i), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            }

            for (int i = 0; i < 15; i++)
            {
                tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(610, 32 * i), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            }

            for (int i = 0; i < 2; i++)
            {
                tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(32 * (i +1), 250), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            }

            //tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(120, 350), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
            //tiles.Add(new Tile(Content.Load<Texture2D>("tile.png"), new Vector2(157, 350), new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32)));
           
            



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            
            // Prepocet fyziky zacne az vterinu po spusteni, aby se nepadalo do textur
            cd -= gameTime.ElapsedGameTime.Milliseconds;

            if (cd <= 0)
            {
                player.Update(tiles, gameTime);
            }


            // Posun pozadi
            bgPos.X = (float)(bgPos.X - (gameTime.ElapsedGameTime.TotalMilliseconds / 128)) % 512;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(background, new Vector2(bgPos.X + (512 * i), bgPos.Y), new Rectangle(0, 0, 512, 512), Color.White);
            }

            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
