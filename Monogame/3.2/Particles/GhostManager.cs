using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MonoGameLibrary.Particle;


namespace Particles
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GhostManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        List<Ghost> Ghosts;
        Random r;

        

        bool addNewGhost = false;

        //List of ghosts that have collided with another ghost
        List<Ghost> collidedGhosts;

        public GhostManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            Ghosts = new List<Ghost>();
            collidedGhosts = new List<Ghost>();
            r = new Random();

            
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

       

        private Vector2 GetRandLocation()
        {
            Vector2 loc;
            loc.X = r.Next(Game.Window.ClientBounds.Width);
            loc.Y = r.Next(Game.Window.ClientBounds.Height);
            return loc;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            collidedGhosts.Clear();
            
            foreach (Ghost g in Ghosts)
            {
                //Update ghosts that are enabled
                if (g.Enabled)
                {
                    g.Update(gameTime);
                }
                else // the ghost has been eaten replace it randomly and add a new ghost
                {
                    g.Location = GetRandLocation();
                    //TODO relocate if intersects pacman or another ghost
                    g.Enabled = true;
                    g.Visible = true;

                    addNewGhost = true;
                }
            }

            if (addNewGhost)
            {

                //add new ghost
                Ghost ng = new Ghost(Game);
                ng.Initialize();
                ng.Location = GetRandLocation();
                //TODO relocate if intersects pacman or another ghost
                Ghosts.Add(ng);

                addNewGhost = false;
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            for (int i = 0; i < 4; i++)
            {
                Ghost g = new Ghost(Game);
                
                Ghosts.Add(g);
                g.Initialize();
                g.Location = GetRandLocation();
            }
            
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Ghost g in Ghosts)
            {
                if(g.Visible)
                    g.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}