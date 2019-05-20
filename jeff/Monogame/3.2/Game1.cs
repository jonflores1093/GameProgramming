#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

using IntroGameLibrary.Util;
using IntroGameLibrary;
using IntroGameLibrary.State;


namespace IntroObserver
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Public SpriteBatch
        public SpriteBatch SpriteBatch { get { return this.spriteBatch; } }

        //Playable Game Components
        

        //Game Services
        GameConsole console;
        CelAnimationManager celAnimationManager;
        InputHandler input;
        

        #region GameStates
        private GameStateManager gameManager;

        public ITitleIntroState TitleIntroState; //First State
        public IStartMenuState StartMenuState;
        public IPlayingState PlayingState;
        public IPausedState PausedState;
        //Into Screen

        #endregion

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Game Services
            input = new InputHandler(this);
            console = new GameConsole(this);
            celAnimationManager = new CelAnimationManager(this);
            this.Components.Add(input);
            this.Components.Add(console);
            this.Components.Add(celAnimationManager);

            

            

            #region GameStates
            gameManager = new GameStateManager(this);
            TitleIntroState = new TitleIntroState(this);
            StartMenuState = new StartMenuState(this);
            PlayingState = new PlayingState(this);
            PausedState = new PausedState(this);

            gameManager.ChangeState(TitleIntroState.Value);
            #endregion
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed )
                Exit();

            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }

        internal PacManAnimated GetPacMan()
        {
            return ((PlayingState)PlayingState).pacMan;
        }
    }
}
