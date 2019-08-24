using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Sprite;

namespace AnimationSimple
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PacManAnimate : Sprite
    {

        int framesizeX, framesizeY, maxFrame, currentFrame;
        float timePerFrame, AnimateTime;
        
        public PacManAnimate(Game game)
            : base(game)
        {
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.spriteTexture = this.Game.Content.Load<Texture2D>("pacmanDie");
            this.framesizeX = 54;
            this.framesizeY = 54;
            this.currentFrame = 0;
            this.maxFrame = 13;
            this.timePerFrame = .5f;

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
            AnimateTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //move to next frame
            if (AnimateTime > this.timePerFrame)
            {
                this.currentFrame++;
                this.currentFrame = this.currentFrame % (this.maxFrame);
                //reset our timer
                AnimateTime -= this.timePerFrame;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            //Don't see this the spriteTransform handles the rectangle now
            //this.locationRect = new Rectangle(
            //        (int)Location.X - (int)this.Orgin.X,
            //        (int)Location.Y - (int)this.Orgin.Y,
            //        (int)(spriteTexture.Width * this.Scale),
            //        (int)(spriteTexture.Height * this.Scale));

            
              sb.Draw(spriteTexture,
                new Rectangle(
                    (int)Location.X,
                    (int)Location.Y,
                    (int)(framesizeX),
                    (int)(framesizeX)),
                new Rectangle(
                    (int)this.currentFrame * (int)this.framesizeX, 0,
                    this.framesizeX , (int)this.framesizeY),
                Color.White,
                MathHelper.ToRadians(Rotate),
                this.Origin,
                SpriteEffects,
                0);
            
            
            DrawMarkers(sb);
    
        }
    }
}
