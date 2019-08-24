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

namespace RotateScaleAnimation
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ghost : DrawableSprite2
    {
        Texture2D ghostHit;
        Texture2D ghost;

        GameConsole gameConsole;
        PacMan pacMan;
        
        public Ghost(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            this.pacMan = ((Game1)game).GetPacMan();
            gameConsole = (GameConsole)game.Services.GetService(typeof(IGameConsole));
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

            this.ghost = this.content.Load<Texture2D>("RedGhost");
            this.ghostHit = content.Load<Texture2D>("GhostHit");

            this.spriteTexture = ghost; 
            this.Direction = new Vector2(0, 1);
            this.Location = new Vector2(350, 50);
            this.Speed = 50.0f;

            
            base.LoadContent();
            this.Orgin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            // Extract collision data
            this.SpriteTextureData =
                new Color[this.spriteTexture.Width * this.spriteTexture.Height];
            this.spriteTexture.GetData(this.SpriteTextureData);

            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);      //Simple Move
            if ((this.Location.Y > graphics.GraphicsDevice.Viewport.Height) 
                || 
                (this.Location.Y < 0)
                )
            {
                this.Direction.Y *= -1;
            }


            //Collision
            if (this.Intersects(pacMan))
            {
                bool PerPixel = false;
                //PerPixel Collision
                if (this.PerPixelCollision2(pacMan))
                {
                    gameConsole.GameConsoleWrite("Ghost PerPixel PacMan " + gameTime.TotalGameTime);
                    this.spriteTexture = ghostHit;
                    PerPixel = true;
                }
                else
                {
                    //if ghost is hit but perpixel now fails
                    if (this.spriteTexture == ghostHit)
                    {
                        this.spriteTexture = ghost;
                    }
                }
                if (!PerPixel)
                {
                    gameConsole.GameConsoleWrite("Ghost Intersects PacMan " + gameTime.TotalGameTime);
                }
            }
            else
            {
                this.spriteTexture = ghost;    
            }
            //gameConsole.DebugText = this.Intersects(pacMan) + "\n";
            //gameConsole.DebugText += this.LocationRect + "\n";
            //gameConsole.DebugText = "\n" + pacMan.Orgin.ToString();
            //gameConsole.DebugText += "\n" + pacMan.Location.ToString();
            //gameConsole.DebugText += "\n" + pacMan.LocationRect.ToString();

            

            

            base.Update(gameTime);
        }

        public void Hit()
        {
            this.spriteTexture = ghostHit;
        }
    }
}