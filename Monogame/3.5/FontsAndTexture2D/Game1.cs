using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FontsAndTexture2D
{
    /// <summary>
    /// Very simple monogame project uses a single font and a single texture2D 
    /// need to use the monogame pipline tool to setup the texture2D and sprintefonr see
    /// http://imamp.colum.edu/mediawiki/index.php/Hello_World_in_Monogame_using_spritefont
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declare a new SpriteFont
        SpriteFont Font;

        //Declare a Texture2D and vector2
        Texture2D PacMan;
        Vector2 PacManLoc;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

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

            //Load SpriteFont
            //Will get null object excpetion if SpriteFont1 has not been added to the Content.mgcb file
            //see http://imamp.colum.edu/mediawiki/index.php/Hello_World_in_Monogame_using_spritefont
            Font = Content.Load<SpriteFont>("SpriteFont1");

            //Locate Texture for PacMan
            //Will get null object excpetion if pacmanSingle has not been added to the Content.mgcb file
            //see http://imamp.colum.edu/mediawiki/index.php/Hello_World_in_Monogame_using_spritefont
            PacMan = Content.Load<Texture2D>("pacManSingle");
            //set the location
            //PacManLoc = new Vector2(100, 100);

            //set the location to the center of the screen
            //The origin of the texture is the top left so if I want it truly centered I need to choose the center of the screen 
            //and then subtract half the texture height and half the texture width 
            PacManLoc = new Vector2(GraphicsDevice.Viewport.Width / 2 - PacMan.Width / 2, GraphicsDevice.Viewport.Height / 2 - PacMan.Height / 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
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

            //Move the PacMan he will move down and left right off the screen we'll deal with this later
            PacManLoc = new Vector2(PacManLoc.X + 1, PacManLoc.Y + 1);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //Draw hello with the sprintFont
            spriteBatch.DrawString(Font, "Hello XNA!!", new Vector2(10, 10), Color.Black);
            //Draw PacMan
            spriteBatch.Draw(PacMan, PacManLoc, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
