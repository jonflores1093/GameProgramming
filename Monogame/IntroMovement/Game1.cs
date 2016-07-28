using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
////using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IntroMovement
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D PacMan;
        Vector2 PacManLoc;      //Pacman location
        Vector2 PacManDir;      //Pacman direction
        float PacManSpeed, PacManMaxSpeed, PacManAcceloration;              //speed for the PacMan Sprite in pixels per frame per second

        enum PacManMovementState { Continuious, OnKey, Momentum, Influnence };
        PacManMovementState currentMovementState;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferHeight = 720;
            //graphics.PreferredBackBufferWidth = 1280;
            graphics.ToggleFullScreen();

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
            //Center PacMan
            PacManLoc = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);
            PacManDir = new Vector2(1, 0);

            PacManSpeed = 100;
            PacManMaxSpeed = 300;
            PacManAcceloration = 2;

            font = Content.Load<SpriteFont>("Arial");
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

            //Elapsed time since last update will be used to correct movement speed
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            //Turn PacMan Around if it hits the edge of the screen
            if ((PacManLoc.X > graphics.GraphicsDevice.Viewport.Width - PacMan.Width)
                || (PacManLoc.X < 0))
            {
                PacManDir *= new Vector2(-1, 0);
            }
            if ((PacManLoc.Y > graphics.GraphicsDevice.Viewport.Height - PacMan.Height)
                || (PacManLoc.Y < 0))
            {
                PacManDir *= new Vector2(0, -1);
            }

            //Move PacMan
            //Simple move Moves PacMac by PacManDiv on every update
            //PacManLoc = PacManLoc + PacManDir * PacManSpeed;

            //Time corrected move. MOves PacMan By PacManDiv every Second
            PacManLoc = PacManLoc + ((PacManDir * PacManSpeed) * (time / 1000));      //Simple Move PacMan by PacManDir

            //Movement for next frame
            //Handle movement modes
            switch (currentMovementState)
            {

                case PacManMovementState.Continuious:
                    PacManSpeed = 200;
                    break;

                case PacManMovementState.OnKey:
                    if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    {
                        PacManSpeed = 200;
                    }
                    else
                    {
                        PacManSpeed = 0;
                    }
                    break;
                case PacManMovementState.Momentum:
                    //slowdown no keys pressed accelerate on keys
                    if (Keyboard.GetState().GetPressedKeys().Length == 0)
                    {
                        if (PacManSpeed > 0)
                        {
                            PacManSpeed -= PacManAcceloration;
                        }
                    }
                    else
                    {
                        if (PacManSpeed < PacManMaxSpeed)
                        {
                            PacManSpeed += PacManAcceloration;
                        }
                    }
                    break;
                case PacManMovementState.Influnence:

                    PacManSpeed = 1;
                    if (PacManDir.Length() - PacManSpeed > PacManMaxSpeed)
                    {
                        PacManDir = Vector2.Normalize(PacManDir) * PacManAcceloration;
                        
                    }
                    //slowdown no keys pressed
                    if (Keyboard.GetState().GetPressedKeys().Length == 0)
                    {
                        PacManDir = Vector2.Lerp(PacManDir, Vector2.Zero, .05f);
                    }
                    
                    break;
            }

            HandleInput();

            //Normalize all modes except Influence
            if (currentMovementState != PacManMovementState.Influnence)
            {
                //normalize vector
                if (PacManDir.Length() >= 1.0)
                {
                    PacManDir.Normalize();
                }
            }
            

            base.Update(gameTime);
        }

        

        private void HandleInput()
        {
            #region Ketboard Movement
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
            #endregion

            #region Keyboard MovementModes
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                currentMovementState = PacManMovementState.Continuious;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
               currentMovementState = PacManMovementState.OnKey;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                currentMovementState = PacManMovementState.Momentum;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                currentMovementState = PacManMovementState.Influnence;
            }
            #endregion

            
            
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

            spriteBatch.DrawString(font, "CurrentMode:" + currentMovementState, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "C: Continuous O:OnKeyDown M:Momentum I:Influence", new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(font, "PacManSpeed:" + PacManSpeed + " PacManMaxSpeed:" + PacManMaxSpeed , new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(font, "PacManDir:" +  PacManDir  + " Length()" + PacManDir.Length(), new Vector2(10, 70), Color.White);
            spriteBatch.DrawString(font, "PacManAcceloration:" + PacManAcceloration, new Vector2(10, 90), Color.White);
            spriteBatch.DrawString(font, "PacManLoc:" + PacManLoc, new Vector2(10, 110), Color.White);
            
            spriteBatch.Draw(PacMan, PacManLoc, Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
