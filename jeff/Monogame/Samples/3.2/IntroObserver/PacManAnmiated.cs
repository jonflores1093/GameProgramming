using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;
using MonoGameLibrary.Sprite2;
using MonoGameLibrary.Util;

namespace IntroObserver
{
    public class PacManAnimated : DrawableAnimatableSprite2, ISubject
    {

        InputHandler input;
        GameConsole gameConsole;
        PacManState pacManState;
        public PacManState PacManState
        {
            get { return pacManState; }
            set
            {
                if (!(pacManState == value))
                {
                    gameConsole.GameConsoleWrite(this + " changing state to " + value );
                    pacManState = value;
                    Notify();       //Notify observers of change
                }
            }
        }
        SpriteAnimation PacManMoving;
        SpriteAnimation PacManDying;


        System.Timers.Timer powerTimer = new System.Timers.Timer(5000);

        
        public PacManAnimated(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            gameConsole = (GameConsole)game.Services.GetService(typeof(IGameConsole));
            PacManState = PacManState.Spawning;
            observers = new List<IObserver>();

            powerTimer.Elapsed += new System.Timers.ElapsedEventHandler(powerTimer_Elapsed); 
        }

        void powerTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Notify("PowerUP Elapsed");
            

        }

        public override void Initialize()
        {

            base.Initialize();
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

            //Use CurrentLocationRect cannot use spriteTexture because of animation
            this.Orgin = new Vector2(this.spriteAnimationAdapter.CurrentLocationRect.Width / 2,
                this.spriteAnimationAdapter.CurrentLocationRect.Height / 2);

            // Extract collision data
            this.SpriteTextureData =
                new Color[this.spriteAnimationAdapter.CurrentTexture.Width * this.spriteAnimationAdapter.CurrentTexture.Height];
            this.spriteTexture = this.spriteAnimationAdapter.CurrentTexture;
            this.spriteTexture.GetData(this.SpriteTextureData);
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
            if (PacManState == PacManState.Dying)
            {
                if (spriteAnimationAdapter.GetLoopCount() > 0)
                {
                    //spriteAnimationAdapter.PauseAnimation(PacManDying);
                    //spriteAnimationAdapter.ResetAnimation(PacManDying);
                    spriteAnimationAdapter.CurrentAnimation = PacManMoving;
                    //TODO move pacman to a location without a ghost
                    this.locationRect = new Rectangle(100, 100, this.spriteAnimationAdapter.CurrentTexture.Width, this.spriteAnimationAdapter.CurrentTexture.Height);
                    this.PacManState = PacManState.Stopped;
                    this.Notify("Alive");
                }
                return;
            }
            
            PacManState = PacManState.Stopped;

            //Input for update from analog stick
            #region LeftStick
            if (gamePad1State.ThumbSticks.Left.Length() > 0.0f)
            {
                PacManState = PacManState.Moving;               //Change State;
                
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
                PacManState = PacManState.Moving;               //Change State;

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
                PacManState = PacManState.Moving;               //Change State;

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
            switch(PacManState)
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
            this.PacManState = PacManState.Dying;
            this.spriteAnimationAdapter.CurrentAnimation = PacManDying;
            this.spriteAnimationAdapter.ResetAnimation(PacManDying);
            this.spriteAnimationAdapter.ResumeAmination(PacManDying);
            
        }


        #region ISubject Members
        List<IObserver> observers;

        public void Attach(IObserver o)
        {

            this.observers.Add(o);
        }

        public void Deatach(IObserver o)
        {

            this.observers.Remove(o);
        }

        public void Notify()
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, this.PacManState);
            }
        }

        public void Notify(string message)
        {
            foreach (IObserver o in observers)
            {
                o.ObserverUpdate(this, message);
            }
        }

        #endregion

        internal void PowerUp()
        {
            this.Notify("PowerUP");
            powerTimer.Start();

        }

        
    }

    public enum PacManState { Spawning, Stopped, Moving, Dying };
}
