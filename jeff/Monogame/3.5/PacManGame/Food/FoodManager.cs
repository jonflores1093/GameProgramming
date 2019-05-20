using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame.Food
{
    public class FoodManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<Food> foods;
        Vector2 foodGrid = new Vector2(15, 10);
        int xFoodOffset;
        int yFoodOffset;

        List<SuperFood> sfoods;
        Vector2 sfoodGrid = new Vector2(2, 2);
        int xsFoodOffset;
        int ysFoodOffset;

        Pac.MonogamePacMan pac;
        GameConsole console;

        SpriteBatch sb;

        public FoodManager(Game game, Pac.MonogamePacMan pac)
            : base(game)
        {
            this.pac = pac;
            this.console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            foods = new List<Food>();
            sfoods = new List<SuperFood>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            
            foods = new List<Food>();
            Vector2 startLoc = new Vector2(10, 10);

            xFoodOffset = 50;
            yFoodOffset = 50;

            for (int i = 0; i < foodGrid.X; i++)
            {
                for (int ii = 0; ii < foodGrid.Y; ii++)
                {
                    Food f = new Food(this.Game, console );
                    f.Initialize();
                    f.Location = new Vector2(startLoc.X + (xFoodOffset * i), startLoc.Y + (yFoodOffset * ii));
                    foods.Add(f);
                }
            }

            xsFoodOffset = 250;
            ysFoodOffset = 250;

            for (int i = 0; i < sfoodGrid.X; i++)
            {
                for (int ii = 0; ii < sfoodGrid.Y; ii++)
                {
                    SuperFood f = new SuperFood(this.Game, console);
                    f.Initialize();
                    f.Location = new Vector2(startLoc.X + (xsFoodOffset * i), startLoc.Y + (ysFoodOffset * ii));
                    sfoods.Add(f);
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
            

            foreach (var f in foods)
            {
                if (f.Enabled)
                {
                    f.Update(gameTime);
                    if (f.Intersects(this.pac))
                    {
                        f.Enabled = false;
                        f.Visible = false;
                        
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            foreach (Food f in foods)
            {
                if (f.Visible)
                {
                    f.Draw(sb);
                }
            }
            sb.End();
            base.Draw(gameTime);
        }
    }
}
