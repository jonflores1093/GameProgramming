using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IntroGameCollisionRotate;
using MonoGameLibrary.Util;

namespace ShotManager
{
    class GhostManager : DrawableGameComponent
    {

        List<Ghost> ghosts;
        InputHandler input;
        
        public GhostManager(Game game)
            : base(game)
        {
            this.ghosts = new List<Ghost>();

            input = (InputHandler)this.Game.Services.GetService<IInputHandler>();
            if (input == null)
            {
                input = new InputHandler(this.Game);
            }
        }


        public override void Update(GameTime gameTime)
        {
            foreach (var ghost in ghosts)
            {
                //Check ghosts
            }

            //hadle input
            if(input.KeyboardState.HasReleasedKey(Keys.G))
            {
                Ghost g = new Ghost(this.Game);
                g.Initialize();
                //g.Direction = g.GetRandomDirection();
                //g.Location = g.GetRandLocation();
                this.AddGhost(g);
            }
            base.Update(gameTime);
        }

        public Ghost AddGhost()
        {
            Ghost g = new Ghost(this.Game);
            g.Initialize();
            return this.AddGhost(g);
        }

        public Ghost AddGhost(Ghost g)
        {
            this.ghosts.Add(g);
            this.Game.Components.Add(g);
            return g;
        }
    }
}
