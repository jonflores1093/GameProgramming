using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacManGameComponent
{

    public class PacManWInputController : Microsoft.Xna.Framework.GameComponent
    {

        public Texture2D Texture;
        public Vector2 PacManLoc, PacManDir;
        public float PacManSpeed, PacManRotate;
        public SpriteEffects PacManSpriteEffects;

        //this class depends on PlayerController
        protected PlayerController controller;

        public PacManWInputController(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            controller = new PlayerController(game);
            game.Components.Add(controller);

        }

        /*
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("PacManSingle");
            PacManLoc = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
            PacManDir = new Vector2(1, 0);
            PacManSpeed = 100.0f;
            PacManRotate = 0.0f;

            base.LoadContent();
        }
        */

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
            //Elapsed time since last update
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            PacManRotate = controller.Rotate;

            //Time corrected move. MOves PacMan By PacManDiv every Second
            PacManDir = controller.Direction;
            PacManLoc += ((PacManDir * (time / 1000)) * PacManSpeed);      //Simple Move PacMan by PacManDir
            
            base.Update(gameTime);
        }
    }
}

