using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SRakowski.LD40.Engine;
using SRakowski.LD40.Entities;
using SRakowski.LD40.Gameplay;
using SRakowski.LD40.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRakowski.LD40
{
    class NewGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D font;
        Texture2D groupIco;
        CharCellDisplay commDisplay;
        Texture2D vertBar;
        private Texture2D horzBar;
        GameState gameState;
        MapRenderer mapRenderer;
        
        public NewGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            gameState = GameState.Create();
            mapRenderer = new MapRenderer(gameState.Map);
        }

        protected override void Initialize()
        {
            base.Initialize();
            int i = 0;
            commDisplay.SetChar(0, i++, 'F');
            commDisplay.SetChar(0, i++, 'I');
            commDisplay.SetChar(0, i++, 'G');
            commDisplay.SetChar(0, i++, 'H');
            commDisplay.SetChar(0, i++, 'T');
            i = 0;
            commDisplay.SetChar(1, i++, 'G');
            commDisplay.SetChar(1, i++, 'R');
            commDisplay.SetChar(1, i++, 'A');
            commDisplay.SetChar(1, i++, 'B');
            commDisplay.SetChar(1, i++, ' ');
            commDisplay.SetChar(1, i++, 'A');
            commDisplay.SetChar(1, i++, 'N');
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<Texture2D>("font");
            mapRenderer.LoadContent(Content);
            groupIco = Content.Load<Texture2D>("groupico");
            vertBar = Content.Load<Texture2D>("vertbar");
            horzBar = Content.Load<Texture2D>("horzbar");
            commDisplay = new CharCellDisplay(new Vector2(12, 31 * 16), 16, 83, font);
        }

        protected override void UnloadContent()
        {
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
            spriteBatch.Begin(transformMatrix: Matrix.Identity *
            Matrix.CreateTranslation(
                (GraphicsDevice.Viewport.Width * 0.5f),
                (GraphicsDevice.Viewport.Height * 0.5f),
                0f));
            mapRenderer.Draw(spriteBatch);
            // spriteBatch.Draw(groupIco, new Vector2(116, 116), GameColors.ForegroundColor);
            //spriteBatch.Draw(horzBar, new Vector2(12, 480), GameColors.ForegroundColor);
            //spriteBatch.Draw(vertBar, new Vector2(732, 16), GameColors.ForegroundColor);
            //commDisplay.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
