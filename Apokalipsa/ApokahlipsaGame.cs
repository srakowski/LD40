using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;

// Ehh-poh-kah-leepsa
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
        Song song;

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
            _gameStateManager.State = new TitleState();
        }

        protected override void Initialize()
        {
            base.Initialize();
            NewGame();
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
        }

        protected override void LoadContent()
        {
            _sb = new SpriteBatch(GraphicsDevice);
            _content.Load(Content);
            song = Content.Load<Song>("song");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!_group.SurvivorCards.Any())
            {
                _gameStateManager.State = new GameOverState(hasWon: false);
            }
            else if (_group.SettledSurvivorCards.Count >= 30)
            {
                _gameStateManager.State = new GameOverState(hasWon: true);
            }

            _gameStateManager.Update(new GameContext(_random, _board, _group, _content, this, NewGame), gameTime, _input);

            base.Update(gameTime);
        }

        private void NewGame()
        {
            _board = GameBoard.Generate(_random, _content);
            _group = new Group(_content);
            _board.PlaceGroupOnInitialGameTile(_group);
            //_group.ResourceCards.AddRange(new GameCard[]
            //{
            //    new BuildingMaterialResourceCard(),
            //    new BuildingMaterialResourceCard(),
            //    new BuildingMaterialResourceCard(),
            //    new BuildingMaterialResourceCard(),
            //    new BuildingMaterialResourceCard(),
            //    new BuildingMaterialResourceCard(),
            //    new BuildingMaterialResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new FoodAndWaterResourceCard(),
            //    new WeaponResourceCard(),
            //    new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),new WeaponResourceCard(),
            //    new AmmoResourceCard(),new AmmoResourceCard(),new AmmoResourceCard(),new AmmoResourceCard(),new AmmoResourceCard(),new AmmoResourceCard(),new AmmoResourceCard(),new AmmoResourceCard(),new AmmoResourceCard(),
            //});
            //_group.SurvivorCards.AddRange(new[]
            //{
            //    new SurvivorCard(true),
            //    new SurvivorCard(true),
            //    new SurvivorCard(true),
            //    new SurvivorCard(true),new SurvivorCard(true),new SurvivorCard(true),
            //    new SurvivorCard(true),
            //    new SurvivorCard(),
            //    new SurvivorCard(),
            //    new SurvivorCard(),
            //    new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),
            //    new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),new SurvivorCard(),
            //});
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameColors.BackgroundColor);
            _gameStateManager.Draw(new GameContext(_random, _board, _group, _content, this, NewGame), _sb);

            if (_gameStateManager.State is TitleState) return;

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
                fighters-- > 0 ? GameColors.FighterColor : GameColors.TextColor);
                xpos++;
                if (xpos >= 13)
                {
                    xpos = 0;
                    ypos++;
                }
            }

            new TextSprite("WELLNESS:").Draw(_sb, _content, new Vector2(707, 184));
            startAt = new Vector2(707, 208);
            ypos = 0; xpos = 0;
            for (int i = 0; i < _group.Wellness; i++)
            {
                _sb.Draw(_content.WellnessIcon,
                startAt + new Vector2(xpos * 38, ypos * 38),GameColors.TextColor);
                xpos++;
                if (xpos >= 6)
                {
                    xpos = 0;
                    ypos++;
                }
            }

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
