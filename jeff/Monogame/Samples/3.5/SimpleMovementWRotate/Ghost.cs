using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMovementWRotate
{
    class Ghost : Sprite
    {
        public Ghost(Game game) : base (game)
        {

        }

        internal void StartMovin()
        {
            this.Direction = new Vector2(1, 1);
        }
    }
}
