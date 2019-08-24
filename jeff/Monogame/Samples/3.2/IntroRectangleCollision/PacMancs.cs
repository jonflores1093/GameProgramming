using MonoGameLibrary.Sprite2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IntroRectangleCollision
{
    class PacMancs : DrawableSprite2
    {

        PacManMouseController controller;
        
        public PacMancs(Game game)
            : base(game)
        {
            controller = new PacManMouseController(this.Game);
            this.Game.Components.Add(controller);
            this.ShowMarkers = true;
        }

        protected override void LoadContent()
        {
            this.spriteTexture = this.Game.Content.Load<Texture2D>("pacmanSingle");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.Location = controller.Location;
            base.Update(gameTime);
        }
    }
}
