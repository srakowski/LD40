using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

// Ahh-poh-kah-leepsa
namespace Apokalipsa
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ApokalipsaGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _sb;
        Random _random;
        GameContent _content;
        GameBoard _board;
        Group _group;
        Input _input;

        public ApokalipsaGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            _random = new Random();
            _content = new GameContent();
            _input = new Input();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _board = GameBoard.Generate(_random, _content);
            _group = new Group(_content);
            _board.PlaceGroupOnInitialGameTile(_group);
        }

        protected override void LoadContent()
        {
            _sb = new SpriteBatch(GraphicsDevice);
            _content.GroupIcon = Content.Load<Texture2D>("group");
            _content.HexTile = Content.Load<Texture2D>("hextile");
            _content.RuralIcon = Content.Load<Texture2D>("rural");
            _content.UrbanIcon = Content.Load<Texture2D>("urban");
            _content.SuburbanIcon = Content.Load<Texture2D>("suburban");
            _content.SettlementIcon = Content.Load<Texture2D>("settlement");
            _content.Direction = Content.Load<Texture2D>("direction");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_input.WasKeyPressed(Keys.Left)) _group.ChangeBearingLeft();
            if (_input.WasKeyPressed(Keys.Right)) _group.ChangeBearingRight();
            if (_input.WasKeyPressed(Keys.Enter)) _group.ExecuteMoveToTargetGameTile();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameColors.BackgroundColor);

            var groupPos = _group.GameBoardTile.CalculateDrawAt(_content);
            _sb.Begin(transformMatrix: 
                Matrix.Identity *
                Matrix.CreateTranslation((GraphicsDevice.Viewport.Width * 0.5f), 256f, 0f) *
                Matrix.CreateTranslation(-groupPos.X, -groupPos.Y, 0f));

                _board.Draw(_sb);
                _group.Draw(_sb);

            _sb.End();

            base.Draw(gameTime);
        }
    }
}
