using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame.Food
{
    public enum FoodState { NotEaten, Eaten }

    class Food : Sprite
    {
        GameConsole console;
        protected FoodState _state;

        public Color color;

        public FoodState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _state, value));

                    _state = value;
                }
            }
        }

        public Food(Game game, GameConsole console) : base(game)
        {
            this.State = FoodState.NotEaten;
            this.console = console;
            this.color = Color.White;
        }

        protected override void LoadContent()
        {
            spriteTexture = this.Game.Content.Load<Texture2D>("food");
            Speed = 0;
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            

            sb.Draw(spriteTexture,
              new Rectangle(
                  (int)Location.X,
                  (int)Location.Y,
                  (int)(spriteTexture.Width * this.Scale),
                  (int)(spriteTexture.Height * this.Scale)),
              null,
              color,
              MathHelper.ToRadians(Rotate),
              this.Origin,
              SpriteEffects,
              0);


            DrawMarkers(sb);
        }



        public void Log(string s)
        {
           
            console.GameConsoleWrite(s);
            
        }

    }
}
