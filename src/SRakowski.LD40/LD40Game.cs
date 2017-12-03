using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SRakowski.LD40.Engine;
using SRakowski.LD40.Scenes;
using System.Collections.Generic;

namespace SRakowski.LD40
{
    public class LD40Game : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<string, object> loadedContent;
        SceneManager sceneManager;
        EngineContext context;

        public LD40Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";

            loadedContent = new Dictionary<string, object>();
            sceneManager = new SceneManager(this);
            context = new EngineContext(
                sceneManager,
                () => spriteBatch,
                loadedContent,
                () => GraphicsDevice
                );


            sceneManager.AddScene(new BackgroundScene());
            //sceneManager.AddScene(new MainMenuScene());
            sceneManager.AddScene(MovementScene.Create());

            Services.AddService(context);
            Components.Add(sceneManager);
        }

        protected override void Initialize()
        {
            base.Initialize();
            sceneManager.Activate();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            loadedContent
                .Load<Texture2D>("Sprites/cardbase", Content)
                .Load<Texture2D>("Sprites/groupbase", Content)
                .Load<Texture2D>("Sprites/territorybase", Content)
                .Load<Texture2D>("Sprites/panel", Content)
                .Load<Texture2D>("Sprites/playbutton", Content)
                .Load<SpriteFont>("Fonts/ui", Content)
                .Load<Texture2D>("Sprites/pointer", Content);
        }

        protected override void UnloadContent()
        {
            loadedContent.Clear();
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameColors.BackgroundColor);
            base.Draw(gameTime);
        }
    }

    static partial class Extensions
    {
        public static Dictionary<string, object> Load<T>(this Dictionary<string, object> self, string key, ContentManager content)
        {
            self.Add(key, content.Load<T>(key));
            return self;
        }
    }
}
