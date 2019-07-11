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
using IntroGameLibrary.Sprite;
using IntroGameLibrary.Util;
using IntroGameLibrary.Sprite2;


namespace IntroObserver
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    /// 
    // A delegate type for hooking up change notifications.
    public delegate void FoodHitEventHandler(object sender, EventArgs e);


    public class Food : DrawableSprite2
    {
        InputHandler input;

        static int EatenCount = 0;

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        
        public event FoodHitEventHandler FoodHitTimeOut;

        

        // Invoke the FoodHitHit event;
        public virtual void OnFoodHitTimeOut(EventArgs e)
        {
            if (FoodHitTimeOut != null)
                FoodHitTimeOut(this, e);
        }

        
        System.Timers.Timer foodTimer = new System.Timers.Timer(5000);

        public Food(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            //this.showMarkers = true;

             // This creates a new timer that will fire every second (1000 milliseconds)
            foodTimer.Elapsed += new System.Timers.ElapsedEventHandler(foodTimer_Elapsed);   
        }

        void foodTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Visible = true;
            this.Enabled = true;
            EatenCount--;
            //No more powerfoods eaten
            if (EatenCount == 0)
            {
                this.OnFoodHitTimeOut(EventArgs.Empty);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Hit()
        {
            this.Visible = false;
            this.Enabled = false;
            foodTimer.Start();
            EatenCount++;                       //Add 1 to static counter
            
        }
        
        protected override void LoadContent()
        {
            
            
            base.LoadContent();
            spriteTexture = content.Load<Texture2D>("20px_1trans");
            Location = new Vector2(10, 10);
            
           
            this.Orgin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            
           
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        
    }
}
