using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame.Food
{
    class SuperFood : Food
    {
        public SuperFood(Game game, GameConsole console) :base(game, console)
        {
            this.Scale = 2;
        }
    }
}
