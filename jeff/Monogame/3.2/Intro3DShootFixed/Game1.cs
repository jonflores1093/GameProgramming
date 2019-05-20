using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MonoGameLibrary;
using MonoGameLibrary.ThreeD;
using MonoGameLibrary.Util;


namespace Intro3dShootFixed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        VertexPositionNormalTexture[] line;  // Our start and end points.
       
        //BasicEffect basicEffect;             // Standard Effect to draw with
        //VertexDeclaration vertexDeclaration; // Our format for the line.

        #region 3D
        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;

        BasicEffect basicEffect;

        Mesh mesh;

        #endregion


        Texture2D checkerTexture;

        InputHandler input;
        GameConsole console;
        FPS fps;
        public FirstPersonCamera camera;        //Meshes need refence to camers

        Baron baron;
        QuadDrawer quadDrawer;
        MonkeyShots monkeyShots;

        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            input = new InputHandler(this);
            this.Components.Add(input);
            fps = new FPS(this);
            this.Components.Add(fps);
            console = new GameConsole(this);
            this.Components.Add(console);
            camera = new FirstPersonCamera(this);
            this.Services.AddService(typeof(ICamera), camera);
            this.Components.Add(camera);

            baron = new Baron(this);
            this.Components.Add(baron);

            mesh = new Mesh(this);
            mesh.Pitch = 55;
            mesh.Location = new Vector3(0, 20.0f, -50.0f);
            mesh.Direction = new Vector3(1, 0, 0);
            mesh.Rotation = new Vector3(10, 0, 0);
            mesh.Scale = 10.0f;
            this.Components.Add(mesh);

            monkeyShots = new MonkeyShots(this);
            this.Components.Add(monkeyShots);
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
            InitializeTransform();
            InitializeEffect();
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
            quadDrawer = new QuadDrawer(graphics.GraphicsDevice);
            checkerTexture = Content.Load<Texture2D>("checker");

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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            Viewport viewport = device.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;

            device.Clear(Color.CornflowerBlue);

            // Compute camera matrices.
            viewMatrix = Matrix.CreateLookAt(camera.Position,
                                              camera.Position + camera.CameraForward,
                                              camera.CameraUp);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(1, aspectRatio,
                                                                    1, 100);

            // Set alpha blending renderstates.
            //RenderState renderState = device.RenderState;

            //renderState.AlphaBlendEnable = true;
            //renderState.SourceBlend = Blend.SourceAlpha;
            //renderState.DestinationBlend = Blend.InverseSourceAlpha;

            //renderState.AlphaTestEnable = true;
            //renderState.AlphaFunction = CompareFunction.Greater;
            //renderState.ReferenceAlpha = 128;

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;


            // Draw the checkered ground polygon.
            Matrix groundTransform = Matrix.CreateScale(200) *
                                     Matrix.CreateRotationX(MathHelper.PiOver2) *
                                     Matrix.CreateTranslation(new Vector3(0,0,-100));

            quadDrawer.DrawQuad(checkerTexture, 32, groundTransform, camera.View, camera.Projection);

            // Draw the game entities.
            baron.Draw(quadDrawer, camera.Position, camera.View, camera.Projection);
            //cat.Draw(quadDrawer, cameraPosition, view, projection);
            //dog.Draw(quadDrawer, cameraPosition, view, projection);

            monkeyShots.DrawShots(gameTime);

            
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {

                
                DrawLine();
                pass.Apply();
            }

            

            base.Draw(gameTime);
        }

        /// <summary>
        /// Initializes the transforms used by the game.
        /// </summary>
        private void InitializeTransform()
        {
            worldMatrix = Matrix.CreateTranslation(new Vector3(-1.5f, -0.5f, 0.0f));

            viewMatrix = Matrix.CreateLookAt(
                new Vector3(0.0f, 0.0f, 7.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                Vector3.Up
                );

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45),
                (float)graphics.GraphicsDevice.Viewport.Width /
                (float)graphics.GraphicsDevice.Viewport.Height,
                1.0f, 100.0f
                );
        }

        /// <summary>
        /// Initializes the effect (loading, parameter setting, and technique selection)
        /// used by the game.
        /// </summary>
        private void InitializeEffect()
        {
            basicEffect = new BasicEffect(graphics.GraphicsDevice);
            basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);

            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
        }

        private void DrawLine()
        {
            basicEffect.View = camera.View;
            basicEffect.Projection = camera.Projection;
            
            line = new VertexPositionNormalTexture[2];

            /*
            line[0] = new VertexPositionNormalTexture(new Vector3(0, 0, 0),
                                                         Vector3.Forward,
                                                         Vector2.One
                                                       );


            line[1] = new VertexPositionNormalTexture(new Vector3(0, 100, 0),
                                                         Vector3.Forward,
                                                         Vector2.One
                                                       );
            */
            line[0] = new VertexPositionNormalTexture(new Vector3(0,50,0),
                                                         Vector3.Forward,
                                                         Vector2.One
                                                       );


            line[1] = new VertexPositionNormalTexture( camera.TransformedReference,
                                                         Vector3.Forward,
                                                         Vector2.One
                                                       );

            
            graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
            PrimitiveType.LineList,
            line,
            0,  // vertex buffer offset to add to each element of the index buffer
            2,  // number of vertices in pointList
            new short[2] { 0, 1 },  // the index buffer
            0,  // first index element to read
            1   // number of primitives to draw

            );
        }
    }
}
