using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntroGameCollisionRotate;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite2;

namespace ShotManager
{
    public class Shot : Sprite2
    {

        float elapsedtime;

        public Shot(Game game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            this.spriteTexture = this.Game.Content.Load<Texture2D>("shot");
            this.Location = new Vector2(100, 100);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.elapsedtime = gameTime.ElapsedGameTime.Milliseconds;

            this.Location += (this.Direction * this.Speed) * (elapsedtime / 1000);

            base.Update(gameTime);
        }
    }
}
