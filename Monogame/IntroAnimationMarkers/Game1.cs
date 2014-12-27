using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using IntroGameLibrary;
using IntroGameLibrary.Util;

namespace IntroAnimationMarkers
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declare Components
        PacManAnimated pacMan;
        Ghost redGhost;
        Ghost purpleGhost;
        //Ghost tealGhost;
        FPS fps;


        //Declare Services
        CelAnimationManager celAnimationManager;
        InputHandler input;
        GameConsole gameConsole;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            fps = new FPS(this);
            input = new InputHandler(this);
            gameConsole = new GameConsole(this);
            celAnimationManager = new CelAnimationManager(this);
            this.Components.Add(fps);
            this.Components.Add(input);
            this.Components.Add(gameConsole);
            this.Components.Add(celAnimationManager);

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
            pacMan = new PacManAnimated(this);      //Create PacMan first
            pacMan.ShowMarkers = true;
            redGhost = new Ghost(this);
            redGhost.ShowMarkers = true;
            this.Components.Add(redGhost);
            purpleGhost = new Ghost(this);
            purpleGhost.ShowMarkers = true;
            purpleGhost.strGhostTexture = "purpleGhost";
            purpleGhost.StartLoc = new Vector2(200, 100);
            purpleGhost.State = Ghost.GhostState.Evading;
            this.Components.Add(purpleGhost);

            //tealGhost = new Ghost(this);
            //tealGhost.strGhostTexture = "tealGhost";
            //tealGhost.StartLoc = new Vector2(400, 400);
            //tealGhost.State = Ghost.GhostState.Chasing;
            //this.Components.Add(tealGhost);

            this.Components.Add(pacMan);    // Add PacMan Last
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
            Content.Load<Texture2D>("pacmanTwo");
            Content.Load<Texture2D>("pacmanDie");
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

            if(input.KeyboardState.HasReleasedKey(Keys.E))
            {
                gameConsole.GameConsoleWrite("Evade!!!"); 
                foreach (Ghost g in Ghost.Ghosts)
                {
                    g.Evade();
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public PacManAnimated GetPacMan()
        {
            return pacMan;
        }
    }
}
