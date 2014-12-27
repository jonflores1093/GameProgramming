using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IntroPacManComponent
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FoodManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<food> foods;
        Vector2 foodGrid = new Vector2(10, 10);
        int xOffset;
        int yOffset;
        Game g;

        SpriteBatch sb;
        
        public FoodManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            g = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            foods = new List<food>();
            Vector2 startLoc = new Vector2(10,10);

            xOffset = 50;
            yOffset = 50;

            for (int i = 0; i < foodGrid.X; i++)
            {
                for (int ii = 0; ii < foodGrid.Y; ii++)
                {
                    food f = new food(g);
                    f.Initialize();
                    f.Location = new Vector2(startLoc.X + (xOffset * i), startLoc.Y + (yOffset * ii));
                    foods.Add(f);
                }
            }
            sb = new SpriteBatch(Game.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            foreach (food f in foods)
            {
                f.Draw(sb);
            }
            sb.End();
            base.Draw(gameTime);
        }
    }
}