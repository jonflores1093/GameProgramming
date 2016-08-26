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
    
    
    
    public interface IShotManager 
    {
        List<Shot> Shots { get; set; }
        void AddShot(Shot s);
        void RemoveShot(Shot s);
    }
    
    public class ShotManager : Microsoft.Xna.Framework.GameComponent, IShotManager
    {
        
        List<Shot> shots;
        
        public ShotManager(Game game) : base(game)
        {
            this.shots = new List<Shot>();
        }

        public void TestShots()
        {
            for (int i = 0; i < 4; i++)
			{
			    Shot s = new Shot(this.Game);
                s.Initialize();
                s.Location = new Vector2(100,100 + (10 * i));
                s.Direction = new Vector2(1,0);
                s.Speed = 600;
                
                
                this.AddShot(s);

			}
        }

        public List<Shot> Shots
        {
            get
            {
                return shots;
            }
            set
            {
                shots = value;
            }
        }

        public void AddShot(Shot s)
        {
            this.shots.Add(s);
        }

        public void RemoveShot(Shot s)
        {
            this.shots.Remove(s);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var s in Shots)
            {
                s.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (var s in Shots)
            {
                s.Draw(sb);
            }
        }
    }
}
