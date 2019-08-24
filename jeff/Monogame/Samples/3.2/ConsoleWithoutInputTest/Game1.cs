using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameLibrary.Util;

namespace ConsoleWithoutInputTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameConsole console;
        int frameCount;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            console = new GameConsole(this);
            this.Components.Add(console);
            console.ToggleConsole();
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
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            console.DebugText = string.Format("Elaspsed GameTime:{0}", gameTime.ElapsedGameTime.Milliseconds);

            frameCount += gameTime.ElapsedGameTime.Milliseconds;
            
            //Tick if greater than 1000 miliseconds
            if (frameCount > 1000)
            {
                //only tick if just passed 1000
                if (frameCount - gameTime.ElapsedGameTime.Milliseconds < 1000)
                {
                    console.GameConsoleWrite(string.Format("Tick {0} {1}", frameCount, gameTime.TotalGameTime.TotalMilliseconds));
                }
                //if passedd 2000 tock and reset
                if (frameCount > 2000)
                {
                    console.GameConsoleWrite(string.Format("Tock {0} {1}", frameCount, gameTime.TotalGameTime.TotalMilliseconds));
                    frameCount = frameCount - 2000;
                }
            }
             
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

            base.Draw(gameTime);
        }
    }
}
