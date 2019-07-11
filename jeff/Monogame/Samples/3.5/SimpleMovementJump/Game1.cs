using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SimpleMovementJump
{
    /// <summary>
    /// Simple Movement For Jumping
    /// Uses a simple class called KeyboardHandler for input
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D PacMan;
        Vector2 PacManLoc, PacManDir;
        float PacManSpeedMax;           //Max speed for the pac man sprite

        Vector2 GravityDir;             //Direction for gravity
        float GravityAccel;             //Acceloration from gravity
        float Friction;                 //Friction to slow down when no input is set
        float Accel = 10;               //Acceloration
        int jumpHeight = -200;          //Jump impulse

        bool isOnGround;                //Hack to stop falling at a certian point 

        KeyboardHandler inputKeyboard;  //Instance of class that handles keyboard input

        SpriteFont font;
        string OutputData;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            inputKeyboard = new KeyboardHandler();
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
            //Place pacman at the center of the screen
            PacManLoc = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            //In the sample Direction also has magnitude
            PacManDir = new Vector2(50, 0);

            PacManSpeedMax = 200;

            GravityDir = new Vector2(0, 1);     //Gravity direction starts as down
            GravityAccel = 200.0f;            
            Friction = 10.0f;
            isOnGround = false; //Techical Debt

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

            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            //Time corrected move. MOves PacMan By PacManDiv every Second
            PacManLoc = PacManLoc + ((PacManDir * (time / 1000)));      //Simple Move PacMan by PacManDir

            //Gravity also affects pacman
            PacManDir = PacManDir + (GravityDir * GravityAccel) * (time / 1000);

            UpdateKeepPacmanOnScreen();
            UpdateInputFromKeyboard();

            OutputData = string.Format("PacDir:{0}\nPacLoc:{1}\nGravityDir:{2}\nGravityAccel:{3}\nTime:{4}\njumpHeight:{5}", PacManDir.ToString(),
                PacManLoc.ToString(), GravityDir.ToString(), GravityAccel.ToString(), time, jumpHeight);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Keeps pac man on screen
        /// </summary>
        private void UpdateKeepPacmanOnScreen()
        {
            //Keep PacMan On Screen
            if (
                //X right
                (PacManLoc.X >
                    GraphicsDevice.Viewport.Width - PacMan.Width)
                ||
                //X left
                (PacManLoc.X < 0)
                )
            {
                //Negate X
                PacManDir = PacManDir * new Vector2(-1, 1);
            }

            //Y stop at 400
            //Hack Floor location is hard coded
            //TODO viloates single resposibilty principle should be moved to it's own method
            if (PacManLoc.Y > 400) //HACK
            {
                PacManLoc.Y = 400;
                PacManDir.Y = 0;
                isOnGround = true;
            }
        }


        private void UpdateInputFromKeyboard()
        {
            inputKeyboard.Update();
            //Jump
            if (inputKeyboard.WasKeyPressed(Keys.Up))
            {
                PacManDir = PacManDir + new Vector2(0, jumpHeight);
                isOnGround = false; //remove onGround bool so gravity will kick in again
            }
            
            if (isOnGround)
            {
                //Allows left and right movement on ground 
                //This way you cannot change direction in the air this is a design descision
                if ((!(inputKeyboard.IsHoldingKey(Keys.Left))) &&
                    (!(inputKeyboard.IsHoldingKey(Keys.Right))))
                {
                    if (PacManDir.X > 0) //If the pacman has a positive direction in X(moving right)
                    {
                        PacManDir.X = Math.Max(0, PacManDir.X - Friction); //Reduce X by friction amount but don't go below 0 
                    }
                    else //Else pacman has a negative direction.X (moving left)
                    {
                        
                        PacManDir.X = Math.Min(0, PacManDir.X + Friction); //Add friction amount until X is 0
                    }
                    //Zero X is stopped so if you're no holding a key friction will slow down the movement until pacman stops
                }

                //If keys left or Right key is down acceorate up to make speed
                if (inputKeyboard.IsHoldingKey(Keys.Left))
                {
                    PacManDir.X = Math.Max((PacManSpeedMax * -1.0f), PacManDir.X - Accel); 
                }
                if (inputKeyboard.IsHoldingKey(Keys.Right))
                {
                    PacManDir.X = Math.Min(PacManSpeedMax, PacManDir.X + Accel);
                }
            }

#if DEBUG
            /* 
             * These settings are for testing gravity values and should be removed before final game release
             * The preprocessor directive #if DEBIG will remove this section when built in release mode
             */
            //A key changes gravity up
            if (inputKeyboard.WasKeyPressed(Keys.A))
            {
                GravityAccel = GravityAccel + 0.2f;
            }
            //Z key changes gravity down
            if (inputKeyboard.WasKeyPressed(Keys.Z))
            {
                GravityAccel = GravityAccel - 0.2f;
            }
            //S key changes jump up
            if (inputKeyboard.WasKeyPressed(Keys.S))
            {
                jumpHeight = jumpHeight + 10;
            }
            if (inputKeyboard.WasKeyPressed(Keys.X))
            {
                jumpHeight = jumpHeight - 10;
            }
            /*
             * We could also change gravity direction here
             */
        #endif
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(PacMan, PacManLoc, Color.Green);

            /*
             * Draw parameters on screen
             */

            spriteBatch.DrawString(font, OutputData , new Vector2(10, 10), Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
