using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using IntroGameCollisionRotate;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShotManager
{
    public class MonoGameShotManager : ShotManager
    {
        List<Shot> ShotsToRemove;

        public MonoGameShotManager(Game game)
            : base(game)
        {
            ShotsToRemove = new List<Shot>();
        }

        public override void Update(GameTime gameTime)    
        {
            //Monogame specific

            //Remove shots that are off screen
            
            foreach(Shot s in Shots)
            {
                if(s.Location.X > this.Game.GraphicsDevice.Viewport.Width)
                {
                    ShotsToRemove.Add(s);
                }
                //TODO left top bottom

            }

            foreach (Shot str in ShotsToRemove)
            {
                this.Shots.Remove(str);
            }
            ShotsToRemove.Clear();

            base.Update(gameTime);
        }
    }
}
