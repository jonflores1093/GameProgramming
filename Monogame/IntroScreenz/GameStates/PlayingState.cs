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

namespace IntroScreenz
{
    public interface IPlayingState : IGameState { }

    class PlayingState : BaseGameState, IPlayingState
    {
        SpriteFont font;
        PacManAnimated pacMan;
        
        public PlayingState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IPlayingState), this);

            pacMan = new PacManAnimated(OurGame);
            pacMan.ShowMarkers = true;
            OurGame.Components.Add(pacMan);
            pacMan.Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                GameManager.PushState(OurGame.PausedState.Value);
            
            //TODO add pausedstate
            
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
                pacMan.Enabled = false;
            }
            else if (GameManager.State != this.Value)
            {
                //change to any other state
                Visible = true;
                Enabled = false;
                pacMan.Visible = false;
                //Call Load or add components
                
            }
            else
            {
                //add the pacman to the game
                pacMan.Visible = true;
                pacMan.Enabled = true;
                //Call Unload or remove components
            }

            
            
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>(@"Arial");

            base.LoadContent();
        }
    }
}
