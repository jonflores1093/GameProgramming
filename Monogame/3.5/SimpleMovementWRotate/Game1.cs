using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SimpleMovementWRotate
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D PacMan;
        Vector2 PacManOrgin;    //Orgin for Drawing
        Vector2 PacManLoc;      //Pacman location
        Vector2 PacManDir;      //Pacman direction
        float PacManSpeed;      //speed for the PacMan Sprite in pixels per frame per second
        float PacManRotate;


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

            PacMan = Content.Load<Texture2D>("pacmanSingle");
            //Set PacMan Location to center of screen
            PacManLoc = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);
            //Vector for pacman direction
            //notice this vector has no magnitude it's noramlized
            PacManDir = new Vector2(1, 0);

            //Orgin shoud be center of texture
            PacManOrgin = new Vector2(PacMan.Width / 2, PacMan.Height / 2);


            //Pacman spped 
            PacManSpeed = 100;

            font = Content.Load<SpriteFont>("SpriteFont1");
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

            //Time corrected move. MOves PacMan By PacManDiv every Second
            PacManLoc = PacManLoc + ((PacManDir * PacManSpeed) * (time / 1000));      //Simple Move PacMan by PacManDir

            UpatePacmanKeepOnScreen();

            UpdatePacmanSpeed();

            UpdateKeyboardInput();

            //Angle in radians from vector
            float RotationAngleKey = (float)System.Math.Atan2(
                    PacManDir.X,
                    PacManDir.Y * -1);
            //Find angle in degrees
            PacManRotate = (float)MathHelper.ToDegrees(
                RotationAngleKey - (float)(Math.PI / 2)); //rotated right already


            base.Update(gameTime);
        }

        private void UpatePacmanKeepOnScreen()
        {
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
        }

        private void UpdatePacmanSpeed()
        {
            //Speed for next frame
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                PacManSpeed = 200;
            }
            else
            {
                PacManSpeed = 0;
            }

        }
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
                PacManDir.Normalize();
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

            //spriteBatch.Draw(PacMan, PacManLoc, Color.White);

            //Use longer spriteBatch Draw call to include rotation
            //Wrong orgin rotates around top left corner
            spriteBatch.Draw(PacMan,  //texture2D
                new Rectangle(        //Create rectange to draw to
                    (int)PacManLoc.X,
                    (int)PacManLoc.Y,
                    (int)(PacMan.Width),
                    (int)(PacMan.Height)),
                null,   //no source rectangle
                Color.White,
                MathHelper.ToRadians(PacManRotate), //rotation in radians
                Vector2.Zero,   //0,0 is top left
                SpriteEffects.None,
                0);

            //spriteBatch.Draw(PacMan,  //texture2D
            //    new Rectangle(        //Create rectange to draw to
            //        (int)PacManLoc.X,
            //        (int)PacManLoc.Y,
            //        (int)(PacMan.Width),
            //        (int)(PacMan.Height)),
            //    null,   //no source rectangle
            //    Color.White,
            //    MathHelper.ToRadians(PacManRotate), //rotation in radians
            //    PacManOrgin,   //correct orgin center of texture
            //    SpriteEffects.None,
            //    0);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
