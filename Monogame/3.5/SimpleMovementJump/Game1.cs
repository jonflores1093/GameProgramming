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

        Vector2 GravityDir;
        float GravityAccel;
        float Friction;
        float Accel = 10;
        int jumpHeight = -200;

        bool isOnGround;

        KeyboardHandler inputKeyboard;
        //GameConsole gameConsole;

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
            PacManLoc = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            PacManDir = new Vector2(50, 0);
            PacManSpeedMax = 200;

            GravityDir = new Vector2(0, 1);
            GravityAccel = 6.0f;
            Friction = 8.0f;
            isOnGround = false;
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

            //Gravity
            PacManDir = PacManDir + (GravityDir * GravityAccel);

            UpdateKeepPacmanOnScreen();
            UpdateInputFromKeyboard();

            //gameConsole.DebugText = String.Format("PacManDir:{0}\nGravityAccel:{1}\njumpHeight:{2}",
            //    PacManDir.ToString(), GravityAccel, jumpHeight);

            base.Update(gameTime);
        }

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
            if (PacManLoc.Y > 400)
            {
                PacManLoc.Y = 400;
                PacManDir.Y = 0;
                isOnGround = true;
            }
        }

        private void UpdateInputFromKeyboard()
        {
            inputKeyboard.Update();
            if (inputKeyboard.WasKeyPressed(Keys.Up))
            {
                PacManDir = PacManDir + new Vector2(0, jumpHeight);
                isOnGround = false;
            }



            if (isOnGround)
            {


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

            if (inputKeyboard.WasKeyPressed(Keys.A))
            {
                GravityAccel = GravityAccel + 0.2f;
            }
            if (inputKeyboard.WasKeyPressed(Keys.Z))
            {
                GravityAccel = GravityAccel - 0.2f;
            }

            if (inputKeyboard.WasKeyPressed(Keys.S))
            {
                jumpHeight = jumpHeight + 10;
            }
            if (inputKeyboard.WasKeyPressed(Keys.X))
            {
                jumpHeight = jumpHeight - 10;
            }
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
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
