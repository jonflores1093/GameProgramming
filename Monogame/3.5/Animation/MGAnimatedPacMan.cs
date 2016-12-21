using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    class MGAnimatedPacMan : Pac.MonogamePacMan
    {
        SpriteAnimation PacManMoving;
        SpriteAnimation PacManDying;

        public MGAnimatedPacMan(Game game) : base (game)
        {

        }


        public override void Update(GameTime gameTime)
        {
            switch(this.PacMan.State)
            {
                case Pac.PacManState.Still:
                        this.spriteAnimationAdapter.PauseAnimation(PacManMoving);
                    break;
                case Pac.PacManState.Chomping:
                    if (this.spriteAnimationAdapter.CurrentAnimation != PacManMoving)
                        this.spriteAnimationAdapter.CurrentAnimation = PacManMoving;
                    if (this.spriteAnimationAdapter.CurrentAnimation.IsPaused)
                        this.spriteAnimationAdapter.ResumeAmination(PacManMoving);

                    break;
                case Pac.PacManState.Dying:
                    this.spriteAnimationAdapter.CurrentAnimation = PacManDying;
                    
                    //Only play animation once
                    if (spriteAnimationAdapter.GetLoopCount() > 1)
                    {
                        this.PacState = Pac.PacManState.Spawning;
                    }
                    break;

                case Pac.PacManState.Spawning:
                    //If i ha an animation for spawning I'd play it here
                    //this.spriteAnimationAdapter.CurrentAnimation = PacManSpawning
                    //I don't so I'll just go to pac man still
                    this.PacState = Pac.PacManState.Still;
                    
                    break;
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            PacManMoving = new SpriteAnimation("PacManMoving", "PacManTwo", 5, 2, 1);
            spriteAnimationAdapter.AddAnimation(PacManMoving);

            PacManDying = new SpriteAnimation("PacManDying", "PacManDie", 5, 13, 1);
            spriteAnimationAdapter.AddAnimation(PacManDying);

            Location = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2, this.Game.GraphicsDevice.Viewport.Height / 2);
            Direction = new Vector2(1, 0);
            Speed = 100.0f;
            Rotate = 0.0f;
            base.LoadContent();

            //Use CurrentLocationRect cannot use spriteTexture because of animation
            this.Origin = new Vector2(this.spriteAnimationAdapter.CurrentLocationRect.Width / 2,
                this.spriteAnimationAdapter.CurrentLocationRect.Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        internal void Die(GameComponent comp)
        {
            this.PacState = Pac.PacManState.Dying;
        }
    }
}
