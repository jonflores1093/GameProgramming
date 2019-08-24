using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace IntroRectangleCollision
{
    class PacManMouseController : GameComponent
    {
        InputHandler input;
        GameConsole console;

        public Vector2 Location;
        
        public PacManMouseController(Game game)
            : base(game)
        {
            input = (InputHandler)this.Game.Services.GetService(typeof(IInputHandler));
            if (input == null)
            {
                input = new InputHandler(this.Game);
                this.Game.Components.Add(input);
            }

            console = (GameConsole)this.Game.Services.GetService(typeof(IGameConsole));
            if (input == null)
            {
                console = new GameConsole(this.Game);
                this.Game.Components.Add(console);
            }

            Location = new Vector2(100, 100);
        }

        public override void Update(GameTime gameTime)
        {
            this.Location = input.MouseState.Position.ToVector2();
            base.Update(gameTime);
        }
    }
}
