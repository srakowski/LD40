using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

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
        GameStateManager _gameStateManager;

        public ApokalipsaGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            _random = new Random();
            _content = new GameContent();
            _input = new Input();
            _gameStateManager = new GameStateManager();
            _gameStateManager.State = new MoveState();
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
            _content.Load(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameStateManager.Update(new GameContext(_random, _board, _group, _content, this), gameTime, _input);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameColors.BackgroundColor);
            _gameStateManager.Draw(new GameContext(_random, _board, _group, _content, this), _sb);

            _sb.Begin();

            _sb.Draw(_content.Well, new Rectangle(683, 0, 341, 768), GameColors.BackgroundColor);
            _sb.Draw(_content.Well, new Rectangle(683, 0, 12, 768), GameColors.TextColor);

            int fighters = _group.FighterCount;
            new TextSprite("GROUP:").Draw(_sb, _content, new Vector2(707, 12));
            var startAt = new Vector2(707, 36);
            int ypos = 0, xpos = 0;
            for (int i = 0; i < _group.MemberCount; i++)
            {
                _sb.Draw(_content.GroupMemberIcon,
                startAt + new Vector2(xpos * 24, ypos * 32),
                fighters-- > 0 ? GameColors.FighterColor : GameColors.TileColor);
                xpos++;
                if (xpos >= 13)
                {
                    xpos = 0;
                    ypos++;
                }
            }

            new TextSprite("WELLNESS:").Draw(_sb, _content, new Vector2(707, 184));

            new TextSprite("RESOURCES:").Draw(_sb, _content, new Vector2(707, 344));

            ypos = 376;
            new FoodAndWaterResourceCard().Draw(_sb, _content, new Vector2(707, ypos));
            new TextSprite($"X {_group.ResourceCards.OfType<FoodAndWaterResourceCard>().Count()}")
                .Draw(_sb, _content, new Vector2(771, ypos + 24));

            new MedicalSuppliesResourceCard().Draw(_sb, _content, new Vector2(707, ypos += 76));
            new TextSprite($"X {_group.ResourceCards.OfType<MedicalSuppliesResourceCard>().Count()}")
                .Draw(_sb, _content, new Vector2(771, ypos + 24));

            new BuildingMaterialResourceCard().Draw(_sb, _content, new Vector2(707, ypos += 76));
            new TextSprite($"X {_group.ResourceCards.OfType<BuildingMaterialResourceCard>().Count()}")
                .Draw(_sb, _content, new Vector2(771, ypos + 24));

            new WeaponResourceCard().Draw(_sb, _content, new Vector2(707, ypos += 76));
            new TextSprite($"X {_group.ResourceCards.OfType<WeaponResourceCard>().Count()}")
                .Draw(_sb, _content, new Vector2(771, ypos + 24));

            new AmmoResourceCard().Draw(_sb, _content, new Vector2(707, ypos += 76));
            new TextSprite($"X {_group.ResourceCards.OfType<AmmoResourceCard>().Count()}")
                .Draw(_sb, _content, new Vector2(771, ypos + 24));


            _sb.End();

            base.Draw(gameTime);
        }
    }
}
