using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Apokalipsa
{
    class GameContext
    {
        public Random Random { get; }
        public GameBoard Board { get; }
        public Group Group { get; }
        public GameContent Content { get; }
        public Game Game { get; }
        public Action Reset { get; }

        public GameContext(Random random, GameBoard board, Group group, GameContent content, Game game, Action reset)
        {
            this.Random = random;
            this.Board = board;
            this.Group = group;
            this.Content = content;
            this.Game = game;
            this.Reset = reset;
        }
    }

    abstract class GameState
    {
        public GameStateManager Manager { get; set; }
        public virtual void Update(GameContext context, GameTime gameTime, Input input) { }
        public virtual void Draw(GameContext context, SpriteBatch spriteBatch) { }
    }

    class GameStateManager
    {
        private GameState _state;

        public GameState State
        {
            get => _state;
            set
            {
                _state = value;
                _state.Manager = this;
            }
        }

        public void Update(GameContext context, GameTime gameTime, Input input) => 
            State.Update(context, gameTime, input);

        public void Draw(GameContext context, SpriteBatch spriteBatch) =>
            State.Draw(context, spriteBatch);
    }

    class MoveState : GameState
    {
        public override void Update(GameContext context, GameTime gameTime, Input input)
        {
            if (input.WasKeyPressed(Keys.Left)) context.Group.ChangeBearingLeft();
            if (input.WasKeyPressed(Keys.Right)) context.Group.ChangeBearingRight();
            if (input.WasKeyPressed(Keys.Enter))
            {
                context.Group.ExecuteMoveToTargetGameTile();
                if (!context.Group.GameBoardTile.IsSettled)
                {
                    Manager.State = new SituationState(context);
                }
            }
        }

        public override void Draw(GameContext context, SpriteBatch spriteBatch)
        {
            var groupPos = context.Group.GameBoardTile.CalculateDrawAt(context.Content);
            spriteBatch.Begin(transformMatrix:
                Matrix.Identity *
                Matrix.CreateTranslation((context.Game.GraphicsDevice.Viewport.Width * 0.33f), (context.Game.GraphicsDevice.Viewport.Height * 0.5f), 0f)
                * Matrix.CreateTranslation(-groupPos.X, 0f, 0f)
                );

            context.Board.Draw(spriteBatch);
            context.Group.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
            new TextSprite("MOVEMENT PHASE:").Draw(spriteBatch, context.Content, new Vector2(12, 16));
            new TextSprite("THE CITY OF KASPRZAK").Draw(spriteBatch, context.Content, new Vector2(12, 40));
            new TextSprite($"YOU HAVE SETTLED {context.Group.SettledSurvivorCards.Count} OF 30").Draw(spriteBatch, context.Content, new Vector2(12, 64));
            spriteBatch.End();
        }
    }
}
