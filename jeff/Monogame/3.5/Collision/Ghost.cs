using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision
{

    public enum GhostState
    {
        Spawning, Stopped, Roving, Chasing, Evading, Dying, Dead
    }

    class Ghost : DrawableSprite
    {
        
        //Static
        protected static short ghostCount;

        //State
        protected GhostState ghoststate;

        //Protected
        Texture2D ghostTexture, hitTexture;     //two textures one for regular ghost and one used when ghost is hit by pacman

        public GhostState Ghoststate
        {
            get { return ghoststate; }
            protected set { this.ghoststate = value; }
        }

        public Ghost(Game game)
            : base(game)
        {
            ghostCount++;
        }

        protected override void LoadContent()
        {
            this.ghostTexture = this.Game.Content.Load<Texture2D>("TealGhost");
            this.hitTexture = this.Game.Content.Load<Texture2D>("GhostHit");
            this.spriteTexture = ghostTexture;

            this.Location = new Vector2(100 + (100 * ghostCount), 100 ); //offset based on ghostcount
            this.Speed = 100;
            base.LoadContent();
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            this.Scale = 1;
            this.Ghoststate = GhostState.Stopped;
        }

        public override void Update(GameTime gameTime)
        {

            
            UpdateGhostTexture();

            base.Update(gameTime);
        }
        /// <summary>
        /// Changes ghost texture based on state
        /// </summary>
        private void UpdateGhostTexture()
        {
            switch(this.Ghoststate)
            {
                case GhostState.Chasing:
                case GhostState.Evading:
                case GhostState.Roving:
                case GhostState.Spawning:
                case GhostState.Stopped:
                    this.spriteTexture = ghostTexture;
                    break;
                case GhostState.Dead:
                case GhostState.Dying:
                    this.spriteTexture = hitTexture;
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public virtual void Hit()
        {
            this.ghoststate = GhostState.Dead;
        }

        public virtual void Chase()
        {
            this.ghoststate = GhostState.Chasing;
        }
    }
}
