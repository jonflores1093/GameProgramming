using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite2;

namespace IntroRectangleCollision
{

    class Ghost : DrawableSprite2
    {
        Texture2D hitGhost;
        Texture2D tealGhost;
        
        public Ghost(Game game)
            : base(game)
        {
            this.ShowMarkers = true;
        }

        protected override void LoadContent()
        {
            this.tealGhost = this.Game.Content.Load<Texture2D>("TealGhost");
            this.hitGhost = this.Game.Content.Load<Texture2D>("GhostHit");
            SwitchToNormalGhost();
            base.LoadContent();
        }

        public void SwitchToGhostHit()
        {
            this.spriteTexture = hitGhost;
        }

        public void SwitchToNormalGhost()
        {
            this.spriteTexture = tealGhost;
        }
    }
}
