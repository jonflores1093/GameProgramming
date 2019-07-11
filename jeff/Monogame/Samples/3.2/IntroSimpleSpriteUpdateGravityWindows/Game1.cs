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

namespace IntroSimpleSpriteUpdateGravityWindows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D PacMan;
        Vector2 PacManLoc, PacManDir;
        float PacManSpeed;              //Speed the PacMan Sprite
        float PacManSpeedMax;           //Max speed for the pac man sprite

        Vector2 GravityDir;
        float GravityAccel;

        public Game1()
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
            PacMan = Content.Load<Texture2D>("pacmanSingle");
            PacManLoc = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            PacManDir = new Vector2(1, 0);
            PacManSpeed = 10;
            PacManSpeedMax = 20;

            GravityDir = new Vector2(0, 1);
            GravityAccel = 1.8f;
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

            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            //Gravity
            PacManDir = PacManDir + GravityDir;
            if (PacManSpeed < PacManSpeedMax)
                PacManSpeed = PacManSpeed + GravityAccel;
            else
            {
                PacManSpeed = PacManSpeedMax;
            }


            //Time corrected move. Moves PacMan By PacManDiv every Second
            PacManLoc = PacManLoc + ((PacManDir * (time / 1000)) * PacManSpeed);  //Simple Move PacMan by PacManDir


            //Keep PacMan On Screen
            //X right
            if (PacManLoc.X >
                    graphics.GraphicsDevice.Viewport.Width - PacMan.Width)
            {
                //Negate X
                PacManDir = PacManDir * new Vector2(-1,1);
                PacManLoc.X = graphics.GraphicsDevice.Viewport.Width - PacMan.Width;
            }

            //X left
            if  (PacManLoc.X < 0)
            {
                //Negate X
                PacManDir = PacManDir * new Vector2(-1, 1);
                PacManLoc.X = 0;
            }

            //Y top
            if (PacManLoc.Y > 
                    graphics.GraphicsDevice.Viewport.Height - PacMan.Height) 
            {
                //Negate Y
                PacManDir = PacManDir * new Vector2(1,-1);
                PacManLoc.Y = graphics.GraphicsDevice.Viewport.Height - PacMan.Height;
            }

            //Y bottom
            if (PacManLoc.Y < 0 )
            {
                //Negate Y
                PacManDir = PacManDir * new Vector2(1,-1);
                PacManLoc.Y = 0;
            }   
            
            


            base.Update(gameTime);
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
            spriteBatch.Draw(PacMan, PacManLoc, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
