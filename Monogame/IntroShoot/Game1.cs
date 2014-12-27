using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace IntroShoot
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
        float PacManSpeed, PacManRotate;
        SpriteEffects PacManSpriteEffects;
        
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
            PacManSpeed = 100.0f;
            PacManRotate = 0.0f;
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
            GamePadState gamePad1State = GamePad.GetState(PlayerIndex.One);
            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            PacManSpriteEffects = SpriteEffects.None;       //Default Sprite Effects

            UpdatePacMan(gamePad1State, time);

            base.Update(gameTime);
        }

        private void UpdatePacMan(GamePadState gamePad1State, float time)
        {
            //Input for update from analog stick
            #region LeftStick
            if (gamePad1State.ThumbSticks.Left.Length() > 0.0f)
            {

                PacManDir = gamePad1State.ThumbSticks.Left;
                PacManDir.Y *= -1;      //Invert Y Axis

                float RotationAngle = (float)Math.Atan2(
                    gamePad1State.ThumbSticks.Left.X,
                    gamePad1State.ThumbSticks.Left.Y);

                PacManRotate = (float)MathHelper.ToDegrees(RotationAngle - (float)(Math.PI / 2));




                //Time corrected move. MOves PacMan By PacManDiv every Second
                PacManLoc += ((PacManDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir

                
            }
            #endregion

            //Update for input from DPad
            #region DPad
            Vector2 PacManDDir = Vector2.Zero;
            if (gamePad1State.DPad.Left == ButtonState.Pressed)
            {
                //Orginal Position is Right so flip X
                PacManDDir += new Vector2(-1, 0);
            }
            if (gamePad1State.DPad.Right == ButtonState.Pressed)
            {
                //Original Position is Right
                PacManDDir += new Vector2(1, 0);
            }
            if (gamePad1State.DPad.Up == ButtonState.Pressed)
            {
                //Up
                PacManDDir += new Vector2(0, -1);
            }
            if (gamePad1State.DPad.Down == ButtonState.Pressed)
            {
                //Down
                PacManDDir += new Vector2(0, 1);
            }
            if (PacManDDir.Length() > 0)
            {

                //Angle in radians from vector
                float RotationAngleKey = (float)Math.Atan2(
                        PacManDDir.X,
                        PacManDDir.Y * -1);
                //Find angle in degrees
                PacManRotate = (float)MathHelper.ToDegrees(
                    RotationAngleKey - (float)(Math.PI / 2)); //rotated right already

                //Normalize NewDir to keep agled movement at same speed as horilontal/Vert
                PacManDDir = Vector2.Normalize(PacManDDir);
                PacManDir += PacManDDir;

                //move the pacman
                PacManLoc += ((PacManDDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir
            }
            #endregion
                
            //Update for input from Keyboard
#if !XBOX360
            #region KeyBoard
            KeyboardState keyboardState = Keyboard.GetState();

            Vector2 PacManKeyDir = new Vector2(0, 0);
            
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                //Flip Horizontal

                PacManKeyDir += new Vector2(-1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                //No new sprite Effects

                PacManKeyDir += new Vector2(1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {

                PacManKeyDir += new Vector2(0, -1);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {

                PacManKeyDir += new Vector2(0, 1);
            }
            if (PacManKeyDir.Length() > 0)
            {

                float RotationAngleKey = (float)Math.Atan2(
                        PacManKeyDir.X,
                        PacManKeyDir.Y * -1);

                PacManRotate = (float)MathHelper.ToDegrees(
                    RotationAngleKey - (float)(Math.PI / 2));

                //Normalize NewDir to keep agled movement at same speed as horilontal/Vert
                PacManKeyDir = Vector2.Normalize(PacManKeyDir);
                 PacManDir += PacManKeyDir;

                PacManLoc += ((PacManKeyDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir
            }
            #endregion
#endif
            //Keep PacMan On Screen
            if (PacManLoc.X > graphics.GraphicsDevice.Viewport.Width - (PacMan.Width / 2))
                PacManLoc.X = graphics.GraphicsDevice.Viewport.Width - (PacMan.Width / 2);

            if (PacManLoc.X < (PacMan.Width /2))
                PacManLoc.X = (PacMan.Width /2);

            if (PacManLoc.Y > graphics.GraphicsDevice.Viewport.Height - (PacMan.Height / 2))
                PacManLoc.Y = graphics.GraphicsDevice.Viewport.Height - (PacMan.Height / 2);

            if (PacManLoc.Y < (PacMan.Height /2 ))
                PacManLoc.Y = (PacMan.Height /2 );

            //Shoot
            if (gamePad1State.Buttons.A == ButtonState.Pressed)
            {
                Shot s = new Shot(this);
                
                s.Location = PacManLoc;
                s.Direction = PacManDir;
                s.Speed = PacManSpeed * 10;
                this.Components.Add(s);
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
            spriteBatch.Draw(PacMan, 
                new Rectangle((int)PacManLoc.X, (int)PacManLoc.Y, PacMan.Width, PacMan.Height),
                null, 
                Color.White,
                MathHelper.ToRadians(PacManRotate), 
                new Vector2(PacMan.Width / 2, PacMan.Height / 2),
                PacManSpriteEffects,
                0);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
