
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using MonoGameLibrary;
using MonoGameLibrary.State;
#endregion

namespace IntroScreenz
{
    public partial class BaseGameState : GameState
    {
        protected Game1 OurGame;
        protected ContentManager Content;

        public BaseGameState(Game game)
            : base(game)
        {
            Content = game.Content;
            OurGame = (Game1)game;
        }
    }
}


