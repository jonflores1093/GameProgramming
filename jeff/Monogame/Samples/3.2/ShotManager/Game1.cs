using IntroGameCollisionRotate;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShotManager
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //Declare Components
        MonoGamePacMan pacMan;
        Ghost redGhost;

        //Declare Services
        InputHandler input;
        GameConsole gameConsole;

        FPS fps;
        float elapsedTime;

        MonoGameShotManager mgsm;

        GhostManager gm;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

             input = new InputHandler(this);
            gameConsole = new GameConsole(this);
            this.Components.Add(input);
            this.Components.Add(gameConsole);

#if DEBUG

            fps = new FPS(this);
            
            this.Components.Add(fps);
            mgsm = new MonoGameShotManager(this);
            this.Components.Add(mgsm);

            gm = new GhostManager(this);
            this.Components.Add(gm);
           
#endif
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
             pacMan = new  MonoGamePacMan(this);      //Create PacMan first
            pacMan.ShowMarkers = true;
            redGhost = new Ghost(this);
            redGhost.ShowMarkers = true;
            this.Components.Add(redGhost);
            this.Components.Add(pacMan); 
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
            mgsm.TestShots();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            //Change Ghosts Scale
            //redGhost.Scale = Math.Abs((float)(1.5 * Math.Sin(elapsedTime)));
            //pacMan.Scale = .5f;
            

            //Toggle Sprite Markerz with M Key
            if(input.KeyboardState.HasReleasedKey(Keys.M))
            {
                if (pacMan.ShowMarkers)
                {
                    pacMan.ShowMarkers = false;
                    redGhost.ShowMarkers = false;
                }
                else
                {
                    pacMan.ShowMarkers = true;
                    redGhost.ShowMarkers = true;
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
            spriteBatch.Begin();
            mgsm.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public MonoGamePacMan GetPacMan()
        {
            return pacMan;
        }
    }
}
