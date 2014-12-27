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


namespace IntroConsole
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TestInputInConsole : Microsoft.Xna.Framework.GameComponent
    {

        GameConsole gameConsole;
        InputHandler input;

        bool ShowGamePadOneInConsole;
        
        public TestInputInConsole(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            gameConsole = (GameConsole)Game.Services.GetService(typeof(IGameConsole));
            input = (InputHandler)Game.Services.GetService(typeof(IInputHandler));
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
            if (input.WasKeyPressed(Keys.G))
            {
                if (ShowGamePadOneInConsole)
                {
                    ShowGamePadOneInConsole = false;
                }
                else
                {
                    ShowGamePadOneInConsole = true;
                }
            }

            KeyboardState kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.A))
            {
                gameConsole.GameConsoleWrite("A " + gameTime.TotalGameTime.Seconds.ToString());
            }

            if (input.KeyboardState.HasReleasedKey(Keys.B))
            {
                gameConsole.GameConsoleWrite("B " + gameTime.TotalGameTime.Seconds.ToString());
            }

            if(input.WasPressed(0, InputHandler.ButtonType.X, Keys.X))
            {
                gameConsole.GameConsoleWrite("X " + gameTime.TotalGameTime.Seconds.ToString());
            }

            //Show Controller1
            if (ShowGamePadOneInConsole)
            {
                gameConsole.DebugText = input.GamePads[0].ThumbSticks.ToString();
            }

            base.Update(gameTime);
        }
    }
}