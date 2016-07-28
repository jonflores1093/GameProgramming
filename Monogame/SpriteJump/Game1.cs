using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Util;


namespace SpriteJump
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
        //float PacManSpeed;              //Speed the PacMan Sprite
        float PacManSpeedMax;           //Max speed for the pac man sprite

        Vector2 GravityDir;
        float GravityAccel;
        float Friction;
        float Accel = 10;
        int jumpHeight = -200;

        bool isOnGround;

        InputHandler input;
        //GameConsole gameConsole;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";

            input = new InputHandler(this);
            this.Components.Add(input);
            //gameConsole = new GameConsole(this);
            //this.Components.Add(gameConsole);
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
            PacMan = Content.Load<Texture2D>("XavierLuigi");
            PacManLoc = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            PacManDir = new Vector2(50, 0);
            PacManSpeedMax = 200;

            GravityDir = new Vector2(0, 1);
            GravityAccel = 6.0f;
            Friction = 8.0f;
            isOnGround = false;
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


            //Time corrected move. MOves PacMan By PacManDiv every Second
            PacManLoc = PacManLoc + ((PacManDir * (time / 1000)));      //Simple Move PacMan by PacManDir

            //Gravity
            PacManDir = PacManDir + (GravityDir * GravityAccel);

            //Keep PacMan On Screen
            if (
                //X right
                (PacManLoc.X >
                    graphics.GraphicsDevice.Viewport.Width - PacMan.Width)
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

            if (input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                PacManDir = PacManDir + new Vector2(0, jumpHeight);
                isOnGround = false;
            }



            if (isOnGround)
            {


                if ((!(input.KeyboardState.IsHoldingKey(Keys.Left))) &&
                    (!(input.KeyboardState.IsHoldingKey(Keys.Right))))
                {
                    if (PacManDir.X > 0)
                    {
                        PacManDir.X = Math.Max(0, PacManDir.X - Friction);
                    }
                    else
                    {
                        PacManDir.X = Math.Min(0, PacManDir.X + Friction);
                    }
                }

                if (input.KeyboardState.IsHoldingKey(Keys.Left))
                {
                    PacManDir.X = Math.Max((PacManSpeedMax * -1.0f), PacManDir.X - Accel);
                }
                if (input.KeyboardState.IsHoldingKey(Keys.Right))
                {
                    PacManDir.X = Math.Min(PacManSpeedMax, PacManDir.X + Accel);
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.A))
            {
                GravityAccel = GravityAccel + 0.2f;
            }
            if (input.KeyboardState.WasKeyPressed(Keys.Z))
            {
                GravityAccel = GravityAccel - 0.2f;
            }

            if (input.KeyboardState.WasKeyPressed(Keys.S))
            {
                jumpHeight = jumpHeight + 10;
            }
            if (input.KeyboardState.WasKeyPressed(Keys.X))
            {
                jumpHeight = jumpHeight - 10;
            }

            //gameConsole.DebugText = String.Format("PacManDir:{0}\nGravityAccel:{1}\njumpHeight:{2}",
            //    PacManDir.ToString(), GravityAccel, jumpHeight);
            

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
            spriteBatch.Draw(PacMan, PacManLoc, Color.Green);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
