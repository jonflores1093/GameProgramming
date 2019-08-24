using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace Collision
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Sevices from MonogameLibrary.Util
        InputHandler input;     //Input Handler
        GameConsole console;    //Game Console for logging
        ScoreService score;     //Score Service to keep track or game score

        //Game Components
        PacMan pac;             //Subclasses of MonogameLibrary.DrawableSprite
        Ghost tealGhost;        //Subclasses of MonogameLibrary.DrawableSprite

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //create instance of services
            input = new InputHandler(this);
            console = new GameConsole(this);
            score = new ScoreService(this);

            //Add components to game
            this.Components.Add(input);     
            this.Components.Add(console);
            this.Components.Add(score);

            //Pacman and Ghost depend on the services to add them next
            pac = new PacMan(this);
            pac.ShowMarkers = true;     //show markers for collision
            this.Components.Add(pac);

            tealGhost = new Ghost(this);
            tealGhost.ShowMarkers = true;
            this.Components.Add(tealGhost);
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

            


            //Check for Collision very simple test for rectangle collision
            if (pac.Intersects(tealGhost))
            {
                //hit
                console.GameConsoleWrite("pac intersects teal ghost");
                
                //Maybe try per pixel collision here 
                //It's a good idea to do rectagle collision first no need to look at pixels if rectangle don't intersect
                if(pac.PerPixelCollision(tealGhost))
                {
                    console.GameConsoleWrite("pac pixel collision with teal ghost");
                    tealGhost.Hit();
                }
            }
            else
            {
                tealGhost.Chase();
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

            

            base.Draw(gameTime);
        }
    }
}
