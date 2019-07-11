using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SimpleUpdateMovement
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D PacMan;
        Vector2 PacManLoc;
        Vector2 PacManDir;
        int PacManSpeed;
        
        SpriteFont font;        //Font created as a spritefont using the mgcb pipeline tool

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Uncomment these line to chanhe the window resolution
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            //Uncomment to change to full screen
            //graphics.ToggleFullScreen();  //changes to fullscreen

            //Change the framerate of the game to 30 frames per second
            //This is used to show how time changes animation speed or better yet that is shouldn't we will test this in class
            //TargetElapsedTime = TimeSpan.FromTicks(333333);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

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

            PacMan = Content.Load<Texture2D>("pacManSingle");
            //Set PacMan Location to center of screen
            PacManLoc = new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
            //Vector for pacman direction
            //notice this vector has no magnitude it's noramlized
            PacManDir = new Vector2(0, 1);

            //Pacman speed 
            PacManSpeed = 200;
            
            font = Content.Load<SpriteFont>("Arial");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
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

            //Elapsed time since last update will be used to correct movement speed
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //Move PacMan
            //Simple move Moves PacMac by PacManDiv on every update
            //PacManLoc = PacManLoc + (PacManDir * PacManSpeed);

            //Time corrected move. MOves PacMan By PacManDiv every Second
            PacManLoc = PacManLoc + ((PacManDir * PacManSpeed) * (time / 1000));      //Simple Move PacMan by PacManDir

            UpatePacmanKeepOnScreen();

            UpdatePacmanSpeed();

            UpdateKeyboardInput();

            base.Update(gameTime);
        }

        private void UpatePacmanKeepOnScreen()
        {
            //Turn PacMan Around if it hits the edge of the screen
            if (PacManLoc.X > GraphicsDevice.Viewport.Width - PacMan.Width)   
            {
                PacManDir *= new Vector2(-1, 0);
                PacManLoc.X = 0;
            }
            if((PacManLoc.X < 0))
            {
                PacManDir *= new Vector2(-1, 0);
                
                PacManLoc.X = GraphicsDevice.Viewport.Width - PacMan.Width;
            }
            if ((PacManLoc.Y > GraphicsDevice.Viewport.Height - PacMan.Height)
                || (PacManLoc.Y < 0))
            {
                PacManDir *= new Vector2(0, -1);
            }
        }

        /// <summary>
        /// Changes pacmans speec bases on if a key is pressed or not
        /// </summary>
        private void UpdatePacmanSpeed()
        {
            //Speed for next frame
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                //If a key is press set the speed to 200
                PacManSpeed = 200;
            }
            else
            {
                PacManSpeed = 0;    //no key no speed if we didn't do this pacman would continue going the direction of the previous
                                    //key press even after it has been released
            }
            
        }

        /// <summary>
        /// Uses Keyboard Getstate to test for keys press on each update
        /// </summary>
        private void UpdateKeyboardInput()
        {
            #region Keyboard Movement
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                PacManDir += new Vector2(0, 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                PacManDir += new Vector2(0, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PacManDir += new Vector2(1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                PacManDir += new Vector2(-1, 0);
            }

            //normalize vector
            if (PacManDir.Length() >= 1.0)
            {
                PacManDir.Normalize(); //keep the direction vector between -1 and 1
            }
            #endregion
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(PacMan, PacManLoc, Color.White);   //Draw the pacman
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
