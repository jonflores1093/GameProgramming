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
using MonoGameLibrary.Sprite2;
using MonoGameLibrary.Util;
using ShotManager;
using MonoGameLibrary.Sprite;

namespace IntroGameCollisionRotate
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ghost : DrawableSprite2
    {

        public Texture2D ghostHit;
        public Texture2D ghost;
        public string strGhostTexture;

        GameConsole gameConsole;

        GhostState ghostState;
        public GhostState State { get { return this.ghostState; } set { this.ghostState = value; } }

        public Vector2 StartLoc;

       
        public MonoGamePacMan pacMan;
        Random r;

        public Ghost(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            this.pacMan = ((Game1)game).GetPacMan();
            //pacMan.Attach(this);
            gameConsole = (GameConsole)game.Services.GetService(typeof(IGameConsole));
            strGhostTexture = "RedGhost";
            this.ghostState = GhostState.Roving;
            
            r = new Random();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // TODO: Add your initialization code here
            this.ShowMarkers = true;
            this.Orgin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }

        protected override void LoadContent()
        {

            this.ghost = this.content.Load<Texture2D>(strGhostTexture);
            this.ghostHit = content.Load<Texture2D>("GhostHit");
            this.spriteTexture = ghost;
            this.Direction = new Vector2(0, 1);
            this.Location = new Vector2(100, 100);
            this.Speed = 100.0f;

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            float turnAmount = .04f;

            switch (this.ghostState)
            {
                case GhostState.Dead:

                    //TODO Dead moving and dead animation.
                    //Until then
                    this.ghostState = GhostState.Roving;
                    //Pick random direction
                    this.Direction = this.GetRandomDirection(); ;                 //Assign random direction


                    break;
                case GhostState.Chasing:
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
                case GhostState.Evading:
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
                    break;
                case GhostState.Roving:
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
                        if (pacMan.LocationRect.Contains(new Point((int)p.X, (int)p.Y)))
                        {
                            this.ghostState = GhostState.Chasing;
                            gameConsole.GameConsoleWrite(this.ToString() + " saw pacman");
                            break;
                        }
                        p += this.Direction;
                    }

                    break;
            }

            Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);      //Simple Move

            //Borders Keep Ghost on the Screen
            if ((this.Location.Y + this.spriteTexture.Height / 2 > graphics.GraphicsDevice.Viewport.Height)
                ||
                (this.Location.Y - this.spriteTexture.Height / 2 < 0)
                )
            {
                this.Direction.Y *= -1;
                this.ghostState = GhostState.Roving;
            }
            if ((this.Location.X + this.spriteTexture.Width / 2 > graphics.GraphicsDevice.Viewport.Width)
                ||
                (this.Location.X - this.spriteTexture.Width / 2 < 0)
                )
            {
                this.Direction.X *= -1;
                this.ghostState = GhostState.Roving;
            }

            //Collision
            if (this.Intersects(pacMan))
            {
                //gameConsole.GameConsoleWrite(this.ToString() + " Intersects PacsMan" + gameTime.TotalGameTime.Seconds + "." + gameTime.TotalGameTime.Milliseconds);
                //PerPixel Collision
                if (this.PerPixelCollision(pacMan))
                {
                    //gameConsole.GameConsoleWrite(this.ToString() + " Pixels touched PacsMan" + gameTime.TotalGameTime.Seconds + "." + gameTime.TotalGameTime.Milliseconds);
                    this.ghostState = GhostState.Dead;
                }
            }

            base.Update(gameTime);
        }

        public Vector2 GetRandomDirection()
        {
            Vector2 v = new Vector2((float)r.NextDouble() - 0.5f, (float)r.NextDouble() - 0.5f);
            Vector2.Normalize(ref v, out v);    //Normalize
            return v;
        }

        public Vector2 GetRandLocation()
        {
            System.Threading.Thread.Sleep(1);
            Vector2 loc;
            loc.X = r.Next(Game.Window.ClientBounds.Width - this.spriteTexture.Width) + this.spriteTexture.Width;
            loc.Y = r.Next(Game.Window.ClientBounds.Height - this.spriteTexture.Height) + this.spriteTexture.Height;
            return loc;
        }

        public void Evade()
        {
            this.ghostState = GhostState.Evading;
        }

        public enum GhostState { Chasing, Evading, Roving, Dead }


    }
    
}