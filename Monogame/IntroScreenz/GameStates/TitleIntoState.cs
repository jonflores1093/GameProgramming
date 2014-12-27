using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IntroGameLibrary;
using IntroGameLibrary.Util;
using IntroGameLibrary.State;

namespace IntroScreenz
{
    public interface ITitleIntroState : IGameState { }
    
    public sealed class TitleIntroState : BaseGameState, ITitleIntroState
    {
        private Texture2D texture;

        public TitleIntroState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(ITitleIntroState), this);
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                OurGame.Exit();

            //Startbutton or enter
            if (Input.WasPressed(0, InputHandler.ButtonType.Start, Keys.Enter))
            {
                // push our start menu onto the stack
                GameManager.PushState(OurGame.StartMenuState.Value);
            }

            //Start with spacebar
            if(Input.KeyboardState.WasKeyPressed(Keys.Space))
            {
                // push our start menu onto the stack
                GameManager.PushState(OurGame.StartMenuState.Value);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 pos = new Vector2(100, 100);
            OurGame.SpriteBatch.Draw(texture, pos, Color.White);
            base.Draw(gameTime);
            OurGame.GraphicsDevice.Clear(Color.Black);
        }

        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>(@"Textures\titleIntro");

            base.LoadContent();
        }
    }
}
