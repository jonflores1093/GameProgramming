using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace IntroSimpleSpriteUpdateWindows
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
        float PacManSpeed, PacManMaxSpeed, PacManAcceleration;              //speed for the PacMan Sprite in pixels per frame per second

        bool PacManUseMomentum = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Change the frame fate to 30 Frames per second the default is 60fps
            TargetElapsedTime = TimeSpan.FromTicks(333333); // you may need to add using System; to get the TimeSpan function
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
            
            PacManSpeed = 20;          //initial pacman speed
            PacManMaxSpeed = 1000;      //maximum pacman speed
            PacManAcceleration = 2;
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
            if ( (PacManLoc.X > graphics.GraphicsDevice.Viewport.Width - PacMan.Width)
                || (PacManLoc.X < 0 )
               )
            {
                PacManDir = Vector2.Negate(PacManDir);
            }

            //Move PacMan
            //Simple move Moves PacMac by PacManDiv on every update
            PacManLoc = PacManLoc + PacManDir * PacManSpeed;      //no good not time corrected

            //Time corrected move. MOves PacMan By PacManDiv every Second
            //PacManLoc = PacManLoc + ((PacManDir * PacManSpeed) * (time/1000));      //Simple Move PacMan by PacManDir

            HandleInput();

            base.Update(gameTime);
        }

        private void HandleInput()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Down))
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
            
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                if (PacManUseMomentum)
                {
                    PacManUseMomentum = false;
                }
                else
                {
                    PacManUseMomentum = true;
                }
            }

            //normalize vector
            if (PacManDir.Length() >= 1.0)
            {
                PacManDir = Vector2.Normalize(PacManDir);
            }


            if (PacManUseMomentum)
            {
                //slowdown no keys pressed
                if (Keyboard.GetState().GetPressedKeys().Length == 0)
                {
                    if (PacManSpeed > 0)
                    {
                        PacManSpeed -= PacManAcceleration;
                    }
                }
                else
                {
                    if (PacManSpeed < PacManMaxSpeed)
                    {
                        PacManSpeed += PacManAcceleration;
                    }
                }
            }
            
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
