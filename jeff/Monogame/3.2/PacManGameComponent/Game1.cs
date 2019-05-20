using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace PacManGameComponent
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Input Handler
        InputHandler input;
        
        PacMan pac;

        PacManWInputController pacCont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            input = new InputHandler(this);

            pac = new PacMan(this);
            this.Components.Add(pac);
            pac.Enabled = true;
            

            pacCont = new PacManWInputController(this);
            this.Components.Add(pacCont);
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
            pac.Texture = this.Content.Load<Texture2D>("PacManSingle");
            pac.PacManLoc = new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2);
            pac.PacManDir = new Vector2(1, 0);
            pac.PacManSpeed = 100.0f;
            pac.PacManRotate = 0.0f;

            pacCont.Texture = this.Content.Load<Texture2D>("PacManSingle");
            pacCont.PacManLoc = new Vector2((this.GraphicsDevice.Viewport.Width / 2 )+ 100, (this.GraphicsDevice.Viewport.Height / 2) + 100);
            pacCont.PacManSpeed = 100.0f;
            
            
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
            
            //Draw pac
            spriteBatch.Draw(pac.Texture,
                new Rectangle(
                    (int)pac.PacManLoc.X,
                    (int)pac.PacManLoc.Y,
                    (int)(pac.Texture.Width),
                    (int)(pac.Texture.Height)),
                null,
                Color.White,
                MathHelper.ToRadians(pac.PacManRotate),
                Vector2.Zero,
                SpriteEffects.None,
                0);
            

            //Draw PacCont
            spriteBatch.Draw(pacCont.Texture,
                new Rectangle(
                    (int)pacCont.PacManLoc.X,
                    (int)pacCont.PacManLoc.Y,
                    (int)(pacCont.Texture.Width),
                    (int)(pacCont.Texture.Height)),
                null,
                Color.White,
                MathHelper.ToRadians(pacCont.PacManRotate),
                Vector2.Zero,
                SpriteEffects.None,
                0);
            spriteBatch.End();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
