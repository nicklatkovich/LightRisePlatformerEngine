using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using LightRise.BaseClasses;
using LightRise.WinUtilsLib;
using System;
using System.Collections.Generic;

namespace LightRise.Main {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainThread : Game {

        Action script1;
        Action script2;
        public GraphicsDeviceManager Graphics { get; protected set; }
        public SpriteBatch SpriteBatch;
        SpineObject SpineInstance;
        Texture2D Back;

        public List<Instance> Instances = new List<Instance>( );
        public List<Instance> GUIes = new List<Instance>( );

        RenderTarget2D[ ] Renders;
        public Map Map { get; protected set; }
        Camera Cam;
        public Player Player { get; protected set; }
        public HackScreen HackScreen;
        public HackScreen FirstHackScreen;
        public HackScreen SecondHackScreen;
        public SpriteFont HackFont;
        public Texture2D Terminal;
        Door Door1;
        Door Door2;

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this);
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            IsMouseVisible = true;
#if DEBUG
            Graphics.IsFullScreen = false;
            Graphics.PreferredBackBufferWidth = 1024;
            Graphics.PreferredBackBufferHeight = 640;
#else
            Graphics.IsFullScreen = true;
            Graphics.PreferredBackBufferWidth = displayMode.Width;
            Graphics.PreferredBackBufferHeight = displayMode.Height;
#endif
            Content.RootDirectory = "Content";
        }

        public int Width { get { return Graphics.PreferredBackBufferWidth; } }
        public int Height { get { return Graphics.PreferredBackBufferHeight; } }
        public Point Size { get { return new Point(Width, Height); } }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize( ) {
            Tuple<Map, Point> tuple = WinUtils.LoadMap("Content/SampleFloor.lrmap");
            Map = tuple.Item1;
            Player = new Player(tuple.Item2);
            Player.SetHero(GraphicsDevice, 1 / 600f);
            Cam = new Camera(new Vector2(0, 0), new Vector2(32f, 32f));
            Instances.Add(new FirstComp(Player.GridPosition + new Point(29, 0), GraphicsDevice));
            Instances.Add(new SecondComp(Player.GridPosition + new Point(5, 0), GraphicsDevice));
            SimpleUtils.Init(GraphicsDevice);
            // TODO: Renders will be used for more fust drawing of the background... Later
            Renders = new RenderTarget2D[4];
            for (uint i = 0; i < Renders.Length; i++) {
                Renders[i] = new RenderTarget2D(GraphicsDevice, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            }

            base.Initialize( );
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent( ) {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpineInstance = new SpineObject(GraphicsDevice, "Sample", 1, new Vector2(20, 10));
            HackFont = Content.Load<SpriteFont>("HackFont");
            Terminal = Content.Load<Texture2D>("Terminal");
            FirstHackScreen = new FirstHack(HackFont, SpriteBatch, Terminal, Instances[0] as Comp);
            FirstHack.Items = Player.Items;
            SecondHackScreen = new SecondHack(HackFont, SpriteBatch, Terminal, Instances[1] as Comp);
            SecondHack.Items = Player.Items;
            Back = Content.Load<Texture2D>("SampleFloorBG");
            Texture2D doorText = Content.Load<Texture2D>("door");
            Texture2D computerTex = Content.Load<Texture2D>("Computer");
            (Instances[0] as Comp).texture = computerTex;
            (Instances[1] as Comp).texture = computerTex;
            Door1 = new Door(doorText, Player.GridPosition + new Point(26, -1), Instances[0] as Comp, Map);
            Door2 = new Door(doorText, Player.GridPosition + new Point(3, -1), Instances[1] as Comp, Map);
            (Instances[0] as Comp).Allowed = true;
            script1 = delegate ()
            {
                (Instances[0] as Comp).Allowed = false;
            };
            Player.GridPosition += new Point(4, 0);

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
            StepState State = new StepState(gameTime, Keyboard.GetState( ), Mouse.GetState( ));

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed /*|| State.Keyboard.IsKeyDown(Keys.Escape)*/)
                Exit( );

            float cam_spd = 0.1f;
            float dx = (State.Keyboard.IsKeyDown(Keys.Right) ? cam_spd : 0) - (State.Keyboard.IsKeyDown(Keys.Left) ? cam_spd : 0);
            float dy = (State.Keyboard.IsKeyDown(Keys.Down) ? cam_spd : 0) - (State.Keyboard.IsKeyDown(Keys.Up) ? cam_spd : 0);
            Cam.Position = new Vector2(Cam.Position.X + dx, Cam.Position.Y + dy);
            Player.Hero.Update(gameTime);

            Player.Step(State);
            if (Player.GridPosition.X > 38 && script1 != null)
            {
                script1();
                script1 = null;
            }
            Cam.Position = Player.Position - Size.ToVector2( ) / Cam.Scale / 2f;
            if (HackScreen != null)
                HackScreen.Update(gameTime, State);

            try {
                foreach (var a in Instances) {
                    a.Update(State);
                }
                foreach (var a in GUIes) {
                    a.Update(State);
                }
            } catch { }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            //SpriteBatch.Begin(transformMatrix: Matrix.CreateOrthographic(Cam.Scale.X, Cam.Scale.Y, -0.1f, 1f));
            //SpriteBatch.Begin(transformMatrix: Matrix.CreateOrthographicOffCenter(new Rectangle((int)(Cam.Position.X - Cam.Scale.X / 2), (int)(Cam.Position.Y - Cam.Scale.Y / 2), (int)Cam.Scale.X, (int)Cam.Scale.Y), 1f, 1000f));
            SpriteBatch.Begin( );
            SpriteBatch.Draw(Back, new Rectangle(Cam.WorldToWindow(new Point(9, 1).ToVector2( )), (new Point(80, 34).ToVector2( ) * Cam.Scale / 2f).ToPoint( )), Color.White);
            //Map.Draw(SpriteBatch, Cam);
            Door1.Draw(SpriteBatch, Cam);
            Door2.Draw(SpriteBatch, Cam);
            foreach (var a in Instances) {
                a.Draw(SpriteBatch, Cam);
            }
            try {
                SpriteBatch.End( );
            } catch (InvalidOperationException) { }

            Player.Draw(SpriteBatch, Cam);

            SpriteBatch.Begin( );
            foreach (var a in GUIes) {
                a.Draw(SpriteBatch, Cam);
            }
            SpriteBatch.End( );
            if (HackScreen != null)
                HackScreen.Draw(Cam);

            base.Draw(gameTime);
        }
    }
}
