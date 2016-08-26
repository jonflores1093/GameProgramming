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


namespace IntroSimpleSpriteUpdateRotateWindows
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PacMan : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public Texture2D texture;
        public Vector2 PacManLoc, PacManDir;
        public float PacManSpeed, PacManRotate;
        public SpriteEffects PacManSpriteEffects;
        
        public PacMan(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }


        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("PacManSingle");
            PacManLoc = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
            PacManDir = new Vector2(1, 0);
            PacManSpeed = 100.0f;
            PacManRotate = 0.0f;

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            

            //Input for update from analog stick
            GamePadState gamePad1State = GamePad.GetState(PlayerIndex.One);
            #region LeftStick
            Vector2 pacStickDir = Vector2.Zero;
            if (gamePad1State.ThumbSticks.Left.Length() > 0.0f)
            {

                pacStickDir = gamePad1State.ThumbSticks.Left;
                pacStickDir.Y *= -1;      //Invert Y Axis

                float RotationAngle = (float)Math.Atan2(
                    gamePad1State.ThumbSticks.Left.X,
                    gamePad1State.ThumbSticks.Left.Y);

                PacManRotate = (float)MathHelper.ToDegrees(RotationAngle - (float)(Math.PI / 2));




                //Time corrected move. MOves PacMan By PacManDiv every Second
                //PacManLoc += ((PacManDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir


            }
            #endregion

            //Update for input from DPad
            #region DPad
            Vector2 PacManDPadDir = Vector2.Zero;
            if (gamePad1State.DPad.Left == ButtonState.Pressed)
            {
                //Orginal Position is Right so flip X
                PacManDPadDir += new Vector2(-1, 0);
            }
            if (gamePad1State.DPad.Right == ButtonState.Pressed)
            {
                //Original Position is Right
                PacManDPadDir += new Vector2(1, 0);
            }
            if (gamePad1State.DPad.Up == ButtonState.Pressed)
            {
                //Up
                PacManDPadDir += new Vector2(0, -1);
            }
            if (gamePad1State.DPad.Down == ButtonState.Pressed)
            {
                //Down
                PacManDPadDir += new Vector2(0, 1);
            }
            if (PacManDPadDir.Length() > 0)
            {

                //Angle in radians from vector
                float RotationAngleKey = (float)Math.Atan2(
                        PacManDPadDir.X,
                        PacManDPadDir.Y * -1);
                //Find angle in degrees
                PacManRotate = (float)MathHelper.ToDegrees(
                    RotationAngleKey - (float)(Math.PI / 2)); //rotated right already

                //Normalize NewDir to keep agled movement at same speed as horilontal/Vert
                PacManDPadDir = Vector2.Normalize(PacManDPadDir);


                //move the PacMan
                //PacManLoc += ((PacManDPadDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir
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

                //PacManLoc += ((PacManKeyDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir
            }
            #endregion
#endif
            PacManDir = PacManKeyDir + PacManDPadDir + pacStickDir;
            if (PacManDir.Length() > 0)
            {
                PacManDir = Vector2.Normalize(PacManDir);
            }
            PacManLoc += ((PacManDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir

            //Keep PacMan On Screen
            if (PacManLoc.X > Game.GraphicsDevice.Viewport.Width - (texture.Width / 2))
            {
                PacManLoc.X = Game.GraphicsDevice.Viewport.Width - (texture.Width / 2);
            }
            if (PacManLoc.X < (texture.Width / 2))
                PacManLoc.X = (texture.Width / 2);

            if (PacManLoc.Y > Game.GraphicsDevice.Viewport.Height - (texture.Height / 2))
                PacManLoc.Y = Game.GraphicsDevice.Viewport.Height - (texture.Height / 2);

            if (PacManLoc.Y < (texture.Height / 2))
                PacManLoc.Y = (texture.Height / 2);


            
            base.Update(gameTime);
        }
    }
}
