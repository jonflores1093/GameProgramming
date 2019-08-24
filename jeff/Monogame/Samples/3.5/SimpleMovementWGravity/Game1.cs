using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimpleMovementWGravity
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D PacMan;
        Vector2 PacManLoc;      //Pacman location
        Vector2 PacManDir;      //Pacman direction
        float PacManSpeed; //speed for the PacMan Sprite in pixels per frame per second

        //Gravity
        //float PacManSpeedMax;           //Max speed for the pac man sprite
        Vector2 GravityDir;             //Gravity Direction normalized victor
        float GravityAccel;             //Gavity Acceloration

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferHeight = 720;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.ToggleFullScreen();  //changes to fullscreen

            //Change the framerate of the game to 30 frames per second
            //This is used to show how time changes animation speed or better yet that is shouldn't
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
            PacManDir = new Vector2(1, 0);

            //Pacman spped 
            PacManSpeed = 10;
            //PacManSpeedMax = 20;

            GravityDir = new Vector2(1, 0);
            GravityAccel = 1.8f;

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
            //PacManLoc = PacManLoc + PacManDir * PacManSpeed;

            //Gavity before move
            UpdateGravity();

            //Time corrected move. MOves PacMan By PacManDiv every Second
            PacManLoc = PacManLoc + ((PacManDir * PacManSpeed) * (time / 1000));      //Simple Move PacMan by PacManDir

            UpatePacmanKeepOnScreen();

            //Don't stop if no keys
            //UpdatePacmanSpeed();

            UpdateKeyboardInput();


            base.Update(gameTime);
        }

        private void UpdateGravity()
        {
            //Gravity
            PacManDir = PacManDir + (GravityDir * GravityAccel) ;
        }

        private void UpatePacmanKeepOnScreen()
        {
            //Keep PacMan On Screen
            //Turns around and stays at edges
            //X right
            if (PacManLoc.X >
                    GraphicsDevice.Viewport.Width - PacMan.Width)
            {
                //Negate X
                PacManDir = PacManDir * new Vector2(-1, 1);
                PacManLoc.X = GraphicsDevice.Viewport.Width - PacMan.Width;
            }

            //X left
            if (PacManLoc.X < 0)
            {
                //Negate X
                PacManDir = PacManDir * new Vector2(-1, 1);
                PacManLoc.X = 0;
            }

            //Y top
            if (PacManLoc.Y >
                    GraphicsDevice.Viewport.Height - PacMan.Height)
            {
                //Negate Y
                PacManDir = PacManDir * new Vector2(1, -1);
                PacManLoc.Y = GraphicsDevice.Viewport.Height - PacMan.Height;
            }

            //Y bottom
            if (PacManLoc.Y < 0)
            {
                //Negate Y
                PacManDir = PacManDir * new Vector2(1, -1);
                PacManLoc.Y = 0;
            }
        }

        private void UpdatePacmanSpeed()
        {
            //Speed for next frame
            if (Keyboard.GetState().GetPressedKeys().Length > 0) //If there is any key press the legth of the Array of keys returned by GetPressedKeys wil be greater that 0
            {
                PacManSpeed = 200;  //Key down pacman has speed
            }
            else
            {
                PacManSpeed = 0;    //No key down stop
            }

        }

        private void UpdateKeyboardInput()
        {
            #region Keyboard Movement
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                GravityDir = new Vector2(0, 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                GravityDir = new Vector2(0, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                GravityDir = new Vector2(1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                GravityDir = new Vector2(-1, 0);
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
            spriteBatch.Draw(PacMan, PacManLoc, Color.White);
            spriteBatch.DrawString(font,
                string.Format("Speed:{0}\nDir:{1}\nGravityDir:{2}\nGravtyAccel:{3}",
                PacManSpeed, PacManDir, GravityDir, GravityAccel),
                new Vector2(10, 10),
                Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
