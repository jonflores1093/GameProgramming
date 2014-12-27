using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using IntroGameLibrary.Util;


namespace IntroChaseEvade
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GhostManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        List<Ghost> Ghosts;
        Random r;

        InputHandler input;
        GameConsole console;

        public GhostManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            Ghosts = new List<Ghost>();
            r = new Random(System.DateTime.Now.Millisecond);

            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            console = (GameConsole)game.Services.GetService(typeof(IGameConsole));
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if(input.WasKeyPressed(Keys.C))
            {
                this.ChangeState(Ghost.GhostState.Chasing);
                console.GameConsoleWrite("Ghosts state changed to Chasing");
            }
            if (input.WasKeyPressed(Keys.R))
            {
                this.ChangeState(Ghost.GhostState.Roving);
                console.GameConsoleWrite("Ghosts state changed to Roving");
            }
            if (input.WasKeyPressed(Keys.E))
            {
                this.ChangeState(Ghost.GhostState.Evading);
                console.GameConsoleWrite("Ghosts state changed to Evading");
            }
            if (input.WasKeyPressed(Keys.D))
            {
                this.ChangeState(Ghost.GhostState.Dead);
                console.GameConsoleWrite("Ghosts state changed to Dead");
            }
            
            foreach (Ghost g in Ghosts)
            {
                g.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected void ChangeState(Ghost.GhostState ghostState)
        {
            foreach (Ghost g in Ghosts)
            {
                g.State = ghostState;
            }

        }

        private void AddGhost()
        {
            AddGhost("RedGhost");
        }

        private void AddGhost(string TextureName)
        {
            Ghost g = new Ghost(Game);
            g.strGhostTexture = TextureName;
            g.Initialize();
            g.Location = g.GetRandLocation();
            
            //no overlapping
            foreach (Ghost otherGhost in Ghosts)
            {
                while (g.Intersects(otherGhost))
                {
                    g.Location = g.GetRandLocation();
                }
            }
            g.Scale = 1.0f;
            g.Enabled = true;
            g.Visible = true;
            Ghosts.Add(g);
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
            foreach (Ghost g in Ghosts)
            {
                g.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}