using MonoGameLibrary.Sprite2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AngleAiming
{
    class Ghost : DrawableSprite2
    {
        public Ghost(Game game)
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

            // TODO: Add your initialization code here
            this.ShowMarkers = true;
            this.Orgin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height); //Middle bottom
        }

        protected override void LoadContent()
        {

            this.spriteTexture = content.Load<Texture2D>("GhostHit");
            this.Direction = new Vector2(0, 1);

            this.Speed = 0;

            base.LoadContent();
        }

        public enum GhostState { Chasing, Evading, Roving, Dead }

    }
}
