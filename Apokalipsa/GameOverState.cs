using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Apokalipsa
{
    class GameOverState : GameState
    {
        public bool _hasWon = false;

        public GameOverState(bool hasWon)
        {
            _hasWon = hasWon;
        }

        public override void Update(GameContext context, GameTime gameTime, Input input)
        {
            if (input.WasKeyPressed(Keys.Enter))
            {
                context.Reset();
                Manager.State = new MoveState();
            }
        }

        public override void Draw(GameContext context, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (_hasWon)
            {
                new TextSprite(
                    "VICTORY.\n\n" +
                    "YOU HAVE SUCCESSFULLY RESETTLED THE CITY OF KASPRZAK.\n\n" +
                    "WELL DONE." +
                    "\n\n" +
                    "ENTER STARTS A NEW GAME..."
                    ).Draw(spriteBatch, context.Content, new Vector2(12, 16));
            }
            else
            {
                new TextSprite(
                    "YOUR ENTIRE GROUP HAS DIED OR DESERTED YOU.\n\n" +
                    "YOU HAVE FAILED TO RESETTLE THE CITY OF KASPRZAK.\n\n" +
                    "\n" +
                    "GAME OVER MAN. GAME OVER." +
                    "\n" +
                    "ENTER STARTS A NEW GAME..."
                    ).Draw(spriteBatch, context.Content, new Vector2(12, 16));
            }
            spriteBatch.End();
        }
    }

    class TitleState : GameState
    {
        public override void Update(GameContext context, GameTime gameTime, Input input)
        {
            if (input.WasKeyPressed(Keys.Enter))
                Manager.State = new MoveState();
        }

        public override void Draw(GameContext context, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            new TextSprite(
                "APOKALIPSA\n\n" +
                "CREATED BY SHAWN RAKOWSKI\n" +
                "FOR LUDUM DARE 40\n\n" +
                "CONTROLS ARE ARROW KEYS\n" +
                "PRESS ENTER TO START.."
                ).Draw(spriteBatch, context.Content, new Vector2(96, 260));
            spriteBatch.End();
        }
    }
}
