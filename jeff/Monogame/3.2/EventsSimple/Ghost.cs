using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using MonoGameLibrary.Events;

namespace EventsSimple
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ghost : DrawableSprite
    {
        public Texture2D ghostHit;
        public Texture2D ghost;
        public string strGhostTexture;

        GameConsole gameConsole;
        PacMan pacMan;
        GhostState ghostState;
        public GhostState State { get { return this.ghostState; } set { this.ghostState = value; } } 
        
        public Vector2 StartLoc;
        
        
        
        
        public Ghost(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            this.pacMan = ((Game1)game).GetPacMan();
            gameConsole = (GameConsole)game.Services.GetService(typeof(IGameConsole));
            strGhostTexture = "RedGhost";
            StartLoc = new Vector2(50, 50);
            this.ghostState = GhostState.Roving;
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

        protected override void LoadContent()
        {

            this.ghost = this.content.Load<Texture2D>(strGhostTexture);
            this.ghostHit = content.Load<Texture2D>("GhostHit");

            this.spriteTexture = ghost; 
            this.Direction = new Vector2(0, 1);
            this.Location = StartLoc;
            this.Speed = 50.0f;

            base.LoadContent();
            this.Orgin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            float turnAmount = .02f;
            switch (this.ghostState)
            {
                case GhostState.Chasing :
                    this.spriteTexture = this.ghost;
                    if (pacMan.Location.Y > this.Location.Y)
                    {
                        //this.Direction.Y = 1;
                        this.Direction.Y = MathHelper.Clamp(this.Direction.Y += turnAmount, -1, 1);
                    }
                    else
                    {
                        //this.Direction.Y = -1;
                        this.Direction.Y = MathHelper.Clamp(this.Direction.Y -= turnAmount, -1, 1);
                    }
                    if (pacMan.Location.X > this.Location.X)
                    {
                        //this.Direction.X = 1;
                        this.Direction.X = MathHelper.Clamp(this.Direction.X += turnAmount, -1, 1);
                    }
                    else
                    {
                        //this.Direction.X = -1;
                        this.Direction.X = MathHelper.Clamp(this.Direction.X -= turnAmount, -1, 1);
                    }
                    break;
                case GhostState.Evading :
                    this.spriteTexture = ghostHit;
                    if (pacMan.Location.Y > this.Location.Y)
                    {
                        this.Direction.Y = -1;
                    }
                    else
                    {
                        this.Direction.Y = 1;
                    }
                    if (pacMan.Location.X > this.Location.X)
                    {
                        this.Direction.X = -1;
                    }
                    else
                    {
                        this.Direction.X = -1;
                    }
                    break;
                case GhostState.Roving :
                    this.spriteTexture = this.ghost;
                    break;
            }
            
            //Borders
            if ((this.Location.Y + this.spriteTexture.Height/2 > graphics.GraphicsDevice.Viewport.Height) 
                ||
                (this.Location.Y - this.spriteTexture.Height / 2 < 0)
                )
            {
                this.Direction.Y *= -1;
                
            }
            if ((this.Location.X + this.spriteTexture.Width / 2 > graphics.GraphicsDevice.Viewport.Width)
                ||
                (this.Location.X - this.spriteTexture.Width/2 < 0)
                )
            {
                this.Direction.X *= -1;
                
            }

            Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);      //Simple Move

            //Collision
            if (this.Intersects(pacMan))
            {
                //PerPixel Collision
                if (this.PerPixelCollision(pacMan))
                {
                    //Pac Man Eats Ghost
                    if (this.State == GhostState.Evading)
                    {
                        this.Visible = false;
                        this.Enabled = false;
                        //TODO scoring
                    }
                    else  //Pac man dies
                    {
                        pacMan.Enabled = false;
                        pacMan.Visible = false;
                        //TODO lives
                    }
                }
                
            }
            
            gameConsole.DebugText = this.Intersects(pacMan) + "\n";
            gameConsole.DebugText += this.LocationRect + "\n";
            gameConsole.DebugText += pacMan.LocationRect;

            

            base.Update(gameTime);
        }

       
        
       

        /// <summary>Event arguments that are triggered when a controller is disconnected.</summary>
        public class GhostCollideEventArgs : EventArgs
        {
            #region Fields

            /// <summary>Specifies the index of the player that was disconnected.</summary>
            public Ghost ghost;
            public PacMan pacMan;

            #endregion


            #region Initialization


            public GhostCollideEventArgs() { }


            public GhostCollideEventArgs(Ghost ghost, PacMan pacMan)
            {
                this.ghost = ghost;
                this.pacMan = pacMan;
                ghost.spriteTexture = ghost.ghostHit;
            }


            #endregion
        }

        public void OnFoodHit(object sender, EventArgs e)
        {
            gameConsole.GameConsoleWrite(this + " OnFoodHit!");
            this.State = GhostState.Evading;
            
        }

        public void OnFoodHitTimeOut(object sender, EventArgs e)
        {
            gameConsole.GameConsoleWrite(this + " OnFoodHit Time Out");
            this.State = GhostState.Roving;
        }

        public enum GhostState { Chasing, Evading, Roving }
    }
}