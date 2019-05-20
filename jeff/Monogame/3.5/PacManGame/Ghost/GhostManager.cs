using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost
{
    class GhostManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<MonogameGhost> Ghosts;
        Random r;

        InputHandler input;
        GameConsole console;

        Pac.MonogamePacMan pac;

        public GhostManager(Game game, Pac.MonogamePacMan pac)
           : base(game)
        {
            this.pac = pac;
            Ghosts = new List<MonogameGhost>();
            r = new Random(System.DateTime.Now.Millisecond);

            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            console = (GameConsole)game.Services.GetService(typeof(IGameConsole));
        }

        public override void Update(GameTime gameTime)
        {
            
            if (input.WasKeyPressed(Keys.C))
            {
                this.ChangeState(GhostState.Chasing);
                console.GameConsoleWrite("Ghosts state changed to Chasing");
            }
            if (input.WasKeyPressed(Keys.R))
            {
                this.ChangeState(GhostState.Roving);
                console.GameConsoleWrite("Ghosts state changed to Roving");
            }
            if (input.WasKeyPressed(Keys.E))
            {
                this.ChangeState(GhostState.Evading);
                console.GameConsoleWrite("Ghosts state changed to Evading");
            }
            if (input.WasKeyPressed(Keys.D))
            {
                this.ChangeState(GhostState.Dead);
                console.GameConsoleWrite("Ghosts state changed to Dead");
            }

            foreach (MonogameGhost g in Ghosts)
            {
                g.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected void ChangeState(GhostState ghostState)
        {
            foreach (MonogameGhost g in Ghosts)
            {
                g.Ghost.State = ghostState;
            }

        }

        private void AddGhost()
        {
            AddGhost("RedGhost");
        }

        private void AddGhost(string TextureName)
        {
            MonogameGhost g = new MonogameGhost(Game, this.pac);
            g.strGhostTexture = TextureName;
            g.Initialize();
            g.Location = g.GetRandLocation();
            g.SetTranformAndRect(); //Ghost location changed and update wasn't called to we need to update the rectagle

            //no overlapping
            foreach (MonogameGhost otherGhost in Ghosts)
            {
                while (g.Intersects(otherGhost))
                {
                    g.Location = g.GetRandLocation();
                    g.SetTranformAndRect(); //Ghost location changed and update wasn't called to we need to update the rectagle
                }
            }
            g.Scale = 1.0f;
            g.Enabled = true;
            g.Visible = true;
            Ghosts.Add(g);  //not as game compents
            console.GameConsoleWrite(string.Format("Added {0} Ghost Number {1}", TextureName, this.Ghosts.Count));
        }

        protected override void LoadContent()
        {

            LoadThreeGhosts();
            base.LoadContent();
        }

        private void LoadThreeGhosts()
        {
            AddGhost("RedGhost");
            AddGhost("TealGhost");
            AddGhost("PurpleGhost");

        }

        public override void Draw(GameTime gameTime)
        {
            foreach (MonogameGhost g in Ghosts)
            {
                g.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }

}

