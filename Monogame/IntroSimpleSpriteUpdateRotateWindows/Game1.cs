using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace IntroSimpleSpriteUpdateRotateWindows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;

        PacMan pac, pac2;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            pac = new PacMan(this);
            this.Components.Add(pac);

            pac2 = new PacMan(this);
            this.Components.Add(pac2);
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
           

            font = Content.Load<SpriteFont>("SpriteFont1");
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
            GamePadState gamePad1State = GamePad.GetState(PlayerIndex.One);
            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            

            //UpdatePacMan(gamePad1State, time);

            base.Update(gameTime);
        }

        private void UpdatePacMan(GamePadState gamePad1State, float time)
        {
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(font, pac.PacManDir.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.Draw(pac.texture, 
                new Rectangle((int)pac.PacManLoc.X, (int)pac.PacManLoc.Y, pac.texture.Width, pac.texture.Height),
                null, 
                Color.White,
                MathHelper.ToRadians(pac.PacManRotate), 
                new Vector2(pac.texture.Width / 2, pac.texture.Height / 2),
                pac.PacManSpriteEffects,
                0);

            spriteBatch.Draw(pac2.texture,
                new Rectangle((int)pac2.PacManLoc.X+ 10, (int)pac2.PacManLoc.Y + 10, pac2.texture.Width, pac2.texture.Height),
                null,
                Color.White,
                MathHelper.ToRadians(pac2.PacManRotate),
                new Vector2(pac2.texture.Width / 2, pac2.texture.Height / 2),
                pac2.PacManSpriteEffects,
                0);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
