using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Util;
using MonoGameLibrary.Particle;

namespace Particles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public PacManAnimated pacMan;
        GhostManager gm;


        //Game Services
        CelAnimationManager celAnimationManager;
        InputHandler input;
        GameConsole console;
        List<Food> foods;
        Food food1, food2, food3, food4;

        FPS fps;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Game Services
            fps = new FPS(this);
            input = new InputHandler(this);
            console = new GameConsole(this);
            celAnimationManager = new CelAnimationManager(this);
            this.Components.Add(fps);
            this.Components.Add(input);
            this.Components.Add(console);
            this.Components.Add(celAnimationManager);

            pacMan = new PacManAnimated(this);
            gm = new GhostManager(this);

            food1 = new Food(this);
            food2 = new Food(this);
            food3 = new Food(this);
            food4 = new Food(this);

            foods = new List<Food>();
            foods.Add(food1);
            foods.Add(food2);
            foods.Add(food3);
            foods.Add(food4);

            foreach (Food f in foods)
            {
                this.Components.Add(f);


            }

            this.Components.Add(pacMan);
            this.Components.Add(gm);
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

            food1.Location = new Vector2(100, 400);
            food2.Location = new Vector2(100, 100);
            food3.Location = new Vector2(400, 400);
            food4.Location = new Vector2(400, 100);


            Texture2D ghostHit = Content.Load<Texture2D>("GhostHit");

            //Mouse Particles
            ParticleManager.Instance().ParticleSystems.Add("mouse",
                new ParticleSystem(10, 200,
                    ghostHit,
                    1, 2,  //Speed
                    1, 3,  //Accel
                    1, 10, //Rot
                    5.5f, 10.0f, //Life
                    0.2f, 0.7f, //Scale
                    10));
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

            //Mouse Particles
            Vector2 mouseLoc = new Vector2(input.MouseState.X, input.MouseState.Y);
            ParticleManager.Instance().ParticleSystems["mouse"].Update(.1f);
            ParticleManager.Instance().ParticleSystems["mouse"].AddParticles(
                              mouseLoc,
                               Vector2.Zero);

            // TODO: Add your update logic here
            foreach (Food f in foods)
            {
                if (f.Enabled)
                {
                    if (f.Intersects(pacMan))
                    {
                        pacMan.PowerUp();
                        f.Hit();
                    }
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
            
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            //spriteBatch.Begin();
            ParticleManager.Instance().Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
