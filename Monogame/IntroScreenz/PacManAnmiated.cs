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

namespace IntroScreenz
{
    public class PacManAnimated : DrawableAnimatableSprite
    {

        InputHandler input;
        public PacManState pacManState;
        SpriteAnimation PacManMoving;
        SpriteAnimation PacManDying;
        
        public PacManAnimated(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            pacManState = PacManState.Stopped;
        }

        public override void Initialize()
        {

            base.Initialize();
            this.Orgin = new Vector2(this.spriteAnimationAdapter.CurrentLocationRect.Width / 2,
                this.spriteAnimationAdapter.CurrentLocationRect.Height / 2);
        }

        protected override void LoadContent()
        {

            PacManMoving = new SpriteAnimation("PacManMoving", "PacManTwo", 5, 2, 1);
            spriteAnimationAdapter.AddAnimation(PacManMoving);

            PacManDying = new SpriteAnimation("PacManDying", "PacManDie", 5, 13, 1);
            spriteAnimationAdapter.AddAnimation(PacManDying);
            
            Location = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            Direction = new Vector2(1, 0);
            Speed = 100.0f;
            Rotate = 0.0f;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            GamePadState gamePad1State = input.GamePads[0];
            UpdatePacMan(gamePad1State, lastUpdateTime);
            base.Update(gameTime);
        }

        private void UpdatePacMan(GamePadState gamePad1State, float lastUpdateTime)
        {
            //Dying pacman can't move
            if (pacManState == PacManState.Dying)
            {
                if (spriteAnimationAdapter.GetLoopCount() > 0)
                {
                    //spriteAnimationAdapter.PauseAnimation(PacManDying);
                    //spriteAnimationAdapter.ResetAnimation(PacManDying);
                    spriteAnimationAdapter.CurrentAnimation = PacManMoving;
                    this.locationRect = new Rectangle(100, 100, this.spriteAnimationAdapter.CurrentTexture.Width, this.spriteAnimationAdapter.CurrentTexture.Height);
                    this.pacManState = PacManState.Stopped;
                }
                return;
            }
            
            pacManState = PacManState.Stopped;

            //Input for update from analog stick
            #region LeftStick
            if (gamePad1State.ThumbSticks.Left.Length() > 0.0f)
            {
                pacManState = PacManState.Moving;               //Change State;
                
                Direction = gamePad1State.ThumbSticks.Left;
                Direction.Y *= -1;      //Invert Y Axis

                float RotationAngle = (float)Math.Atan2(
                    gamePad1State.ThumbSticks.Left.X,
                    gamePad1State.ThumbSticks.Left.Y);

                Rotate = (float)MathHelper.ToDegrees(RotationAngle - (float)(Math.PI / 2));


                //Time corrected move. MOves PacMan By PacManDiv every Second
                Location += ((Direction * (lastUpdateTime / 1000)) * Speed);      //Simple Move PacMan by PacManDir

                //Keep PacMan On Screen
                if (Location.X > graphics.GraphicsDevice.Viewport.Width - (this.locationRect.Width / 2))
                    Location.X = graphics.GraphicsDevice.Viewport.Width - (this.locationRect.Width / 2);

                if (Location.X < (this.locationRect.Width / 2))
                    Location.X = (this.locationRect.Width / 2);
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
                pacManState = PacManState.Moving;               //Change State;

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
                pacManState = PacManState.Moving;               //Change State;

                float RotationAngleKey = (float)Math.Atan2(
                        PacManKeyDir.X,
                        PacManKeyDir.Y * -1);

                Rotate = (float)MathHelper.ToDegrees(
                    RotationAngleKey - (float)(Math.PI / 2));


                Location += ((PacManKeyDir * (lastUpdateTime / 1000)) * Speed);      //Simple Move PacMan by PacManDir
            }
            #endregion
#endif

            //Animation
            switch(pacManState)
            {
                case PacManState.Moving :
                    spriteAnimationAdapter.ResumeAmination(PacManMoving);
                    break;
                case PacManState.Stopped :
                    spriteAnimationAdapter.PauseAnimation(PacManMoving);
                    break;
                
            }

        }

        public void Die()
        {
            this.pacManState = PacManState.Dying;
            this.spriteAnimationAdapter.CurrentAnimation = PacManDying;
            this.spriteAnimationAdapter.ResetAnimation(PacManDying);
            this.spriteAnimationAdapter.ResumeAmination(PacManDying);
            
        }

    }

    public enum PacManState { Stopped, Moving, Dying };
}
