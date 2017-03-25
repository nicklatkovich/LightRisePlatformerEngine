using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using LightRise.BaseClasses;
using System.Collections.Generic;
using System;

namespace LightRise.MapEditor {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainThread : Game {

        public static readonly Point MAP_SIZE;
        static MainThread( ) {

            // ATTENSION: MAP_SIZE must be only positive
            MAP_SIZE = new Point(64, 64);

        }

        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        public Point MousePosition = new Point( );
        public Point MousePreviousPosition;

        Camera Cam;

        Map[ ][ ] Maps;

        Point SelectedMap;
        Point SelectedPoint;

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Mouse.WindowHandle = Window.Handle;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize( ) {
            Maps = SimpleUtils.Create2DArray(1, 1, new Map((uint)MAP_SIZE.X, (uint)MAP_SIZE.Y));
            Maps[0][0].Randomize( );
            Cam = new Camera(new Vector2(0, 0), new Vector2(32f, 32f));
            SimpleUtils.Init(GraphicsDevice);

            base.Initialize( );
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent( ) {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent( ) {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState( ).IsKeyDown(Keys.Escape))
                Exit( );

            MousePreviousPosition = MousePosition;
            MouseState MouseState = Mouse.GetState( );
            MousePosition = MouseState.Position;
            Vector2 MouseWorldPosition = MousePosition.Vector2( ) / Cam.Scale + Cam.Position;
            SelectedMap = MouseWorldPosition.FloorToPoint( ) / MAP_SIZE;
            SelectedPoint = MouseWorldPosition.FloorToPoint( ).Mod(MAP_SIZE);

            if (MouseState.RightButton == ButtonState.Pressed) {
                Cam.Position = Cam.Position + (MousePreviousPosition - MousePosition).Vector2() / Cam.Scale;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin( );
            for (uint i = 0; i < Maps.Length; i++) {
                for (uint j = 0; j < Maps[i].Length; j++) {
                    Maps[i][j].Draw(SpriteBatch, new Camera(Cam.Position - new Vector2(MAP_SIZE.X * i, MAP_SIZE.Y * j), Cam.Scale));
                }
            }
            SpriteBatch.Draw(SimpleUtils.WhiteRect, new Rectangle(Cam.WorldToWindow(SelectedPoint.Vector2( )), Cam.Scale.RoundToPoint( )), new Color(0, 0, 255, 127));
            SpriteBatch.End( );

            base.Draw(gameTime);
        }
    }
}
