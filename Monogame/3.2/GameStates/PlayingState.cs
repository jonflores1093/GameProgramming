#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IntroGameLibrary;
using IntroGameLibrary.Util;
using IntroGameLibrary.State;
#endregion

namespace IntroObserver
{
    public interface IPlayingState : IGameState { }

    class PlayingState : BaseGameState, IPlayingState
    {
        SpriteFont font;
        //PacManAnimated pacMan;

        public PacManAnimated pacMan;
        GhostManager gm;
        List<Food> foods;
        Food food1, food2, food3, food4;
        
        public PlayingState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IPlayingState), this);

            //pacMan = new PacManAnimated(OurGame);
            //pacMan.ShowMarkers = true;
            //OurGame.Components.Add(pacMan);
            //pacMan.Visible = false;

            pacMan = new PacManAnimated(this.Game);
            gm = new GhostManager(this.Game);

            food1 = new Food(this.Game);
            food2 = new Food(this.Game);
            food3 = new Food(this.Game);
            food4 = new Food(this.Game);

            foods = new List<Food>();
            foods.Add(food1);
            foods.Add(food2);
            foods.Add(food3);
            foods.Add(food4);

            foreach (Food f in foods)
            {
                game.Components.Add(f);
                

            }

            game.Components.Add(pacMan);
            game.Components.Add(gm);
            setAllInvisible();
            setAllDisable();
            
        }

        public void setAllInvisible()
        {
            pacMan.Visible = false;
            gm.Visible = false;
            foreach (Food f in foods)
            {
                f.Visible = false;
            }

        }

        public void setAllVisible()
        {
            pacMan.Visible = true; ;
            gm.Visible = true;
            foreach (Food f in foods)
            {
                f.Visible = true;
            }

        }

        public void setAllDisable()
        {
            pacMan.Enabled = false;
            gm.Enabled = false;
            foreach (Food f in foods)
            {
                f.Enabled = false;
            }

        }

        public void setAllEnable()
        {
            pacMan.Enabled = true;
            gm.Enabled = true;
            foreach (Food f in foods)
            {
                f.Enabled = true;
            }

        }

        

        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                GameManager.PushState(OurGame.PausedState.Value);
            
            //TODO add pausedstate
            foreach (Food f in foods)
            {
                if (f.Enabled)
                {
                    if (f.Intersects(pacMan))
                    {
                        pacMan.PowerUp();
                        f.Hit();
                    }
                }
            }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
           
            base.Draw(gameTime);
        }

        protected override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);

            //handled change to paused state
            if (GameManager.State == OurGame.PausedState)
            {
                //just set enabled to false;
                this.Enabled = false;
                //pacMan.Enabled = false;
                //setAllVisible();
                setAllDisable();
            }
            else if (GameManager.State != this.Value)
            {
                //change to any other state
                Visible = true;
                Enabled = false;
                //pacMan.Visible = false;
                //Call Load or add components
                
                
            }
            else
            {
                //add the pacman to the game
                //pacMan.Visible = true;
                //pacMan.Enabled = true;
                //Call Unload or remove components
                setAllVisible();
                setAllEnable();
            }

            
            
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>(@"Arial");

            food1.Location = new Vector2(100, 400);
            food2.Location = new Vector2(100, 100);
            food3.Location = new Vector2(400, 400);
            food4.Location = new Vector2(400, 100);

            base.LoadContent();
        }
    }
}
