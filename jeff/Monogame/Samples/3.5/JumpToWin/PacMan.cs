using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;

namespace JumpToWin
{
    class PacMan : DrawableSprite
    {

        Vector2 GravityDir;
        float GravityAccel;
        float Friction;
        float Accel = 10;
        int jumpHeight = -300;

        bool isOnGround;


        InputHandler input;
        private float SpeedMax;

        public PacMan(Game game)
            : base(game)
        {
            input = (InputHandler)game.Services.GetService<InputHandler>();
            if(input == null)
            {
                input = new InputHandler(game);
                game.Components.Add(input);
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteTexture = this.Game.Content.Load<Texture2D>("pacManSingle");
            
            GravityDir = new Vector2(0, 1);
            GravityAccel = 6.0f;
            Friction = 8.0f;
            SpeedMax = 200;
            isOnGround = false;
        }

        public override void Update(GameTime gameTime)
        {
            
            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            //Time corrected move. MOves PacMan By PacManDiv every Second
            this.Location = this.Location + ((this.Direction * (time / 1000)));      //Simple Move PacMan by PacManDir

            //Gravity
            this.Direction = this.Direction + (GravityDir * GravityAccel);

            
            UpdateInputFromKeyboard();



            base.Update(gameTime);
        }

        

        private void UpdateInputFromKeyboard()
        {
            
            if (input.KeyboardState.WasKeyPressed(Keys.Up))
            {
                this.Direction = this.Direction + new Vector2(0, jumpHeight);
                isOnGround = false;
            }
            
            if (isOnGround)
            {
                if ((!(input.KeyboardState.IsHoldingKey(Keys.Left))) &&
                    (!(input.KeyboardState.IsHoldingKey(Keys.Right))))
                {
                    if (this.Direction.X > 0) //If the pacman has a positive direction in X(moving right)
                    {
                        this.Direction.X = Math.Max(0, this.Direction.X - Friction); //Reduce X by friction amount but don't go below 0 
                    }
                    else //Else pacman has a negative direction.X (moving left)
                    {
                        this.Direction.X = Math.Min(0, this.Direction.X + Friction); //Add friction amount until X is 0
                    }
                    //Zero X is stopped so if you're no holding a key friction will slow down the movement until pacman stops
                }

                //If keys left or Right key is down acceorate up to make speed
                if (input.KeyboardState.IsHoldingKey(Keys.Left))
                {
                    this.Direction.X = Math.Max((SpeedMax * -1.0f), this.Direction.X - Accel);
                }
                if (input.KeyboardState.IsHoldingKey(Keys.Right))
                {
                    this.Direction.X = Math.Min(SpeedMax, this.Direction.X + Accel);
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
        }
        
    }
}
