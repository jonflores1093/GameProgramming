using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IntroGameLibrary;
using IntroGameLibrary.Sprite;
using IntroGameLibrary.Util;

namespace EventsSimple
{
    public class PacMan : DrawableSprite
    {

        InputHandler input;
        GameConsole gameConsole;
        
        public PacMan(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            gameConsole = (GameConsole)game.Services.GetService(typeof(IGameConsole));
            this.showMarkers = true;
        }

        public override void Initialize()
        {
            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            
            base.LoadContent();
            spriteTexture = content.Load<Texture2D>("pacmanSingle");
            Location = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            Direction = new Vector2(1, 0);
            Speed = 100.0f;
            Rotate = 0.0f;
            this.Orgin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
           
        }

        public override void Update(GameTime gameTime)
        {

            GamePadState gamePad1State = input.GamePads[0];
            UpdatePacMan(gamePad1State, lastUpdateTime);
            base.Update(gameTime);
        }
        
        private void UpdatePacMan(GamePadState gamePad1State, float lastUpdateTime)
        {
            //Input for update from analog stick
            #region LeftStick
            if (gamePad1State.ThumbSticks.Left.Length() > 0.0f)
            {

                Direction = gamePad1State.ThumbSticks.Left;
                Direction.Y *= -1;      //Invert Y Axis

                float RotationAngle = (float)Math.Atan2(
                    gamePad1State.ThumbSticks.Left.X,
                    gamePad1State.ThumbSticks.Left.Y);

                Rotate = (float)MathHelper.ToDegrees(RotationAngle - (float)(Math.PI / 2));


                //Time corrected move. MOves PacMan By PacManDiv every Second
                Location += ((Direction * (lastUpdateTime / 1000)) * Speed);      //Simple Move PacMan by PacManDir

                //Keep PacMan On Screen
                if (Location.X > graphics.GraphicsDevice.Viewport.Width - (spriteTexture.Width / 2))
                    Location.X = graphics.GraphicsDevice.Viewport.Width - (spriteTexture.Width / 2);

                if (Location.X < (spriteTexture.Width / 2))
                    Location.X = (spriteTexture.Width / 2);
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
                Rotate = (float)MathHelper.ToDegrees(
                    RotationAngleKey - (float)(Math.PI / 2)); //rotated right already

                //move the pacman
                Location += ((PacManDDir * (lastUpdateTime / 1000)) * Speed);      //Simple Move PacMan by PacManDir
            }
            #endregion

            //Update for input from Keyboard
#if !XBOX360
            #region KeyBoard
            

            Vector2 PacManKeyDir = new Vector2(0, 0);

            if (input.KeyboardState.IsKeyDown(Keys.Left))
            {
                //Flip Horizontal

                PacManKeyDir += new Vector2(-1, 0);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Right))
            {
                //No new sprite Effects

                PacManKeyDir += new Vector2(1, 0);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Up))
            {

                PacManKeyDir += new Vector2(0, -1);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Down))
            {

                PacManKeyDir += new Vector2(0, 1);
            }
            if (PacManKeyDir.Length() > 0)
            {

                float RotationAngleKey = (float)Math.Atan2(
                        PacManKeyDir.X,
                        PacManKeyDir.Y * -1);

                Rotate = (float)MathHelper.ToDegrees(
                    RotationAngleKey - (float)(Math.PI / 2));


                Location += ((PacManKeyDir * (lastUpdateTime / 1000)) * Speed);      //Simple Move PacMan by PacManDir
            }
            #endregion
#endif


        }

        public void OnFoodHit(object sender, EventArgs e)
        {
            gameConsole.GameConsoleWrite(this + " OnFoodHit!");
        }

    }
}
