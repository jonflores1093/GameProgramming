#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace IntroFonts
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declare a new SpriteFont
        SpriteFont Font;

        //Declare a Texture2D and vector2
        Texture2D PacMan;
        Vector2 PacManLoc;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;


            this.IsFixedTimeStep = true;

            TargetElapsedTime = TimeSpan.FromTicks(333333);

            graphics.ApplyChanges();


            graphics.IsFullScreen = false;
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
            //Load SpriteFont
            Font = Content.Load<SpriteFont>("SpriteFont1");

            //Locate Texture for PacMan
            PacMan = Content.Load<Texture2D>("pacmanSingle");
            //set the location
            //PacManLoc = new Vector2(100, 100);

            //set the location to the center of the screen
            //The origin of the texture is the top left so if I want it truly centered I need to choose the center of the screen 
            //and then subtract half the texture height and half the texture width 
            PacManLoc = new Vector2(GraphicsDevice.Viewport.Width / 2 - PacMan.Width / 2, GraphicsDevice.Viewport.Height / 2 - PacMan.Height / 2);


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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //Move the PacMan he will move down and left right off the screen we'll deal with this later
            PacManLoc = new Vector2(PacManLoc.X + 1, PacManLoc.Y + 1);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Draw hello with the sprintFont
            spriteBatch.DrawString(Font, "Hello XNA!!", new Vector2(10, 10), Color.Black);
            //Draw PacMan
            spriteBatch.Draw(PacMan, PacManLoc, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
