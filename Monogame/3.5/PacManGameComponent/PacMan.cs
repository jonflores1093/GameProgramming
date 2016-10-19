using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite2;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacManGameComponent
{
    public enum PacState
    {
        Spawning, Stopped, Chomping, Dying, Dead, PoweredUp
    }

    public class PacMan : DrawableSprite2
    {
        //Services
        protected GameConsole console;
        protected ScoreService score;

        //State
        protected PacState pacstate;
        public PacState Pacstate
        {
            get { return pacstate; }
            protected set { this.pacstate = value; }
        }

        public PacMan(Game game)
            : base(game)
        {
            //PacMan Depends on some game sevices
            #region Dependancy Services

            console = (GameConsole)game.Services.GetService(typeof(IGameConsole));
            //Make sure input service exists
            if (console == null)
            {
                console = new GameConsole(this.Game);
                console.Initialize();
                this.Game.Services.AddService(console);
            }

            score = (ScoreService)game.Services.GetService<IScoreService>();
            //Make sure score service exists
            if (score == null)
            {
                score = new ScoreService(this.Game);
                this.Game.Services.AddService(score);
            }
            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (this.Pacstate)
            {
                case PacState.Chomping:
                case PacState.Stopped:
                    UpdatePacMan(lastUpdateTime, gameTime);
                    break;
            }


        }

        protected override void LoadContent()
        {
            this.spriteTexture = content.Load<Texture2D>("pacManSingle");

            this.Location = new Vector2(300, 300);
            this.Speed = 100;
            base.LoadContent();
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            this.Scale = 1;
            this.Pacstate = PacState.Stopped;
        }

        protected virtual void UpdatePacMan(float lastUpdateTime, GameTime gameTime)
        {
            console.DebugText = this.Pacstate.ToString();   
        }
        

    }

}

