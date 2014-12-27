using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using IntroGameLibrary.Util;

namespace WindowsPhoneGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont spriteFont;  //Simple font
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

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            //Support More Orientations
            graphics.SupportedOrientations = DisplayOrientation.Portrait |
                                    DisplayOrientation.LandscapeLeft |
                                    DisplayOrientation.LandscapeRight;

            //Add event
            this.Window.OrientationChanged += new EventHandler<EventArgs>(Window_OrientationChanged);

            //FPS fps = new FPS(this);
            //this.Components.Add(fps);

            //InputHandler input = new InputHandler(this);
            //this.Components.Add(input);

            //GameConsole console = new GameConsole(this);
            //this.Components.Add(console);
        }

        void Window_OrientationChanged(object sender, EventArgs e)
        {
            switch (this.Window.CurrentOrientation)
            {
                case (DisplayOrientation.Portrait) :
                    GravityDir = new Vector2(1, 0);
                    break;
                case (DisplayOrientation.LandscapeLeft) :
                    GravityDir = new Vector2(0, 1);
                    break;
                case (DisplayOrientation.LandscapeRight) :
                    GravityDir = new Vector2(0, 1);
                    break;
            }
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
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
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
            updatePacManBounce(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "Hello XNA!", new Vector2(100, 100), Color.White);
            spriteBatch.Draw(PacMan, PacManLoc, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        protected void updatePacManBounce(GameTime gameTime)
        {
            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //Touchy Stuff
            Rectangle rect = new Rectangle((int)PacManLoc.X,
                (int)PacManLoc.Y, PacMan.Width, PacMan.Height); //get pacman rectangle
            Vector2 touchLoc;
            
            

            //Use Gravity to move the pacaman 
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
                PacManDir = PacManDir * new Vector2(-1, 1);
                PacManLoc.X = graphics.GraphicsDevice.Viewport.Width - PacMan.Width;
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
                    graphics.GraphicsDevice.Viewport.Height - PacMan.Height)
            {
                //Negate Y
                PacManDir = PacManDir * new Vector2(1, -1);
                PacManLoc.Y = graphics.GraphicsDevice.Viewport.Height - PacMan.Height;
            }

            //Y bottom
            if (PacManLoc.Y < 0)
            {
                //Negate Y
                PacManDir = PacManDir * new Vector2(1, -1);
                PacManLoc.Y = 0;
            }

            //Touch pacman
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if ((tl.State == TouchLocationState.Pressed)
                        || (tl.State == TouchLocationState.Moved))
                {
                    touchLoc = new Vector2(tl.Position.X, tl.Position.Y);
                    if (rect.Contains((int)touchLoc.X, (int)touchLoc.Y))
                    {
                        //I should figure out acceleration 
                        PacManSpeed = 10;

                        //and touch direction
                        TouchLocation tlo = tl;
                        tl.TryGetPreviousLocation(out tlo);
                        Vector2 dir = new Vector2(tl.Position.X, tl.Position.Y) - new Vector2(tlo.Position.X, tlo.Position.Y);
                        PacManDir = dir;

                        //Move the pacman to the touch
                        PacManLoc = new Vector2(tl.Position.X - PacMan.Width / 2, tl.Position.Y - PacMan.Height / 2);
                    }

                }
            }
            
        }
    }
}
