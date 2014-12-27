using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IntroGameLibrary;
using IntroGameLibrary.Sprite;
using IntroGameLibrary.Sprite2;
using IntroGameLibrary.Util;
using IntroGameLibrary.Events;
using IntroGameLibrary.Particle;

namespace Particles
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ghost : DrawableSprite2, IObserver
    {
      
        public Texture2D ghostHit;
        public Texture2D ghost;
        public string strGhostTexture;
        int ghostNum;

        GameConsole gameConsole;
        PacManAnimated pacMan;
        GhostState ghostState;
        public GhostState State { get { return this.ghostState; } set { this.ghostState = value; } } 
        
        public Vector2 StartLoc;

        public static List<Ghost> Ghosts = new List<Ghost>();
        Random r;


        
        public Ghost(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            r = new Random();
            this.pacMan = ((Game1)game).pacMan;
            pacMan.Attach(this);
            gameConsole = (GameConsole)game.Services.GetService(typeof(IGameConsole));
            strGhostTexture = "RedGhost";
            StartLoc = new Vector2(50, 50);
            this.ghostState = GhostState.Roving;
            Ghost.Ghosts.Add(this);

            ghostNum = Ghosts.Count;
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // TODO: Add your initialization code here
            
            //this.Orgin = new Vector2(this.spriteTexture.Width/2, this.spriteTexture.Height/2);
            
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

            ParticleManager.Instance().ParticleSystems.Add("ghost" + ghostNum,
                new ParticleSystem(
                    1, //Min 
                    3, //Max
                    this.ghost,
                    1, 1,  //Speed
                    1, 1,  //Accel
                    1, 1, //Rot
                    2.5f, 4.0f, //Life
                    1.0f, 1.0f, //Scale
                    1));//spawns
            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            float turnAmount = .04f;

            ParticleManager.Instance().ParticleSystems["ghost" + ghostNum].Update(.1f);

           
            switch (this.ghostState)
            {
                case GhostState.Dead:
                    
                    //TODO Dead moving and dead animation.
                    //Until then
                    this.ghostState = GhostState.Roving;
                    //Pick random direction
                    Random r = new Random();
                    Vector2 v = new Vector2((float)r.NextDouble() -0.5f, (float)r.NextDouble() - 0.5f);
                    Vector2.Normalize(ref v, out v);    //Normalize
                    this.Direction = v;                 //Assign random direction
                
                    break;
                case GhostState.Chasing :
                    //Change texture if Chasing
                    if (!(this.spriteTexture == this.ghost))
                    {
                        //gameConsole.GameConsoleWrite(this.ToString() + " Chasing changed texture to spriteTexture");
                        this.spriteTexture = this.ghost;
                    }
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
                    //Change texture if evading
                    if (this.spriteTexture != this.ghostHit)
                    {
                        //gameConsole.GameConsoleWrite(this.ToString() + " Evading changed texture to ghostHit");
                        this.spriteTexture = this.ghostHit;
                    }

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

                    ParticleManager.Instance().ParticleSystems["ghost" + ghostNum].AddParticles(
                                new Vector2(this.Location.X + this.LocationRect.Width / 2,
                                    this.Location.Y + this.LocationRect.Height / 2),
                                Vector2.Zero);
                    

                    break;
                case GhostState.Roving :
                    //Change texture if Chasing
                    if (!(this.spriteTexture == this.ghost))
                    {
                        //gameConsole.GameConsoleWrite(this.ToString() + " Roving changed texture to spriteTexture");
                        this.spriteTexture = this.ghost;
                    }
                    //check if ghost can see pacman
                    Vector2 normD = Vector2.Normalize(this.Direction);
                    Vector2 p = new Vector2(this.Location.X, this.Location.Y);
                    while (p.X < graphics.GraphicsDevice.Viewport.Width &&
                          p.X > 0 &&
                          p.Y < graphics.GraphicsDevice.Viewport.Height &&
                          p.Y > 0)
                    {
                        if(pacMan.LocationRect.Contains(new Point((int)p.X, (int)p.Y)))
                        {
                            this.ghostState = GhostState.Chasing;
                            gameConsole.GameConsoleWrite(this.ToString() + " saw pacman");
                            break;
                        }
                        p += this.Direction;
                    }

                    
                    break;
            }
            
            //Borders
            if ((this.Location.Y + this.spriteTexture.Height/2 > graphics.GraphicsDevice.Viewport.Height) 
                ||
                (this.Location.Y - this.spriteTexture.Height / 2 < 0)
                )
            {
                this.Direction.Y *= -1;
                //this.ghostState = GhostState.Roving;
            }
            if ((this.Location.X + this.spriteTexture.Width / 2 > graphics.GraphicsDevice.Viewport.Width)
                ||
                (this.Location.X - this.spriteTexture.Width/2 < 0)
                )
            {
                this.Direction.X *= -1;
                //this.ghostState = GhostState.Roving;
            }

            Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);      //Simple Move

            //Collision
            if (this.Intersects(pacMan))
            {
     
                //PerPixel Collision
                if (this.PerPixelCollision(pacMan))
                {
                    if (this.ghostState == GhostState.Evading)
                    {
                        this.Visible = false;
                        this.Enabled = false;
                    }
                    else
                    {
                        if (pacMan.PacManState != PacManState.Dying)
                        {
                            pacMan.Die();
                            this.Location = new Vector2(100, 100);
                            this.ghostState = GhostState.Roving;
                        }
                    }
                }
            }
            
          

            base.Update(gameTime);
        }

        

        

        public void Evade()
        {
            this.ghostState = GhostState.Evading;
        }

        private Vector2 GetRandLocation()
        {
            Vector2 loc;
            loc.X = r.Next(Game.Window.ClientBounds.Width);
            loc.Y = r.Next(Game.Window.ClientBounds.Height);
            return loc;
        }
       

        public enum GhostState { Chasing, Evading, Roving, Dead }

        #region IObserver Members

        public void ObserverUpdate(Object sender, Object message)
        {
            if (message is PacManState)
            {
                PacManState p = (PacManState)message;
                gameConsole.GameConsoleWrite(this + " notified " + p + " from " + sender);
                if (p == PacManState.Dying)
                {
                    this.State = GhostState.Roving;
                }
                
            }
            if (message is string)
            {
                string strMessage = (string)message;
                switch (strMessage)
                {
                    case "PowerUP" :
                        this.Evade();
                        break;
                    case "PowerUP Elapsed":
                        this.ghostState = GhostState.Roving;
                        break;
                    case "Alive" :
                        if(this.Intersects(pacMan))
                        {
                            while(this.Intersects(pacMan))
                            {
                                gameConsole.GameConsoleWrite(this + " relocated ");
                                this.Location = this.GetRandLocation();
                                this.SetTranformAndRect();
                            }
                        }
                        break;

                }
            }

        }

        #endregion
    }
}