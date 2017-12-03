using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apokalipsa
{
    struct TextSprite
    {
        private const int CHARW = 12;
        private const int CHARH = 16;

        public string Text { get; set; }

        public int WrapAt { get; set; } 

        public TextSprite(string value = "", int wrapAt = 40)
        {
            Text = value;
            WrapAt = 40;
        }

        public void Draw(SpriteBatch spriteBatch, GameContent content, Vector2 drawAt)
        {
            int x = 0;
            int y = 0;
            if (Text == null) return;
            var charDim = new Vector2(CHARW, CHARH);
            for (int c = 0; c < Text.Length; c++)
            {
                var cell = Text[c];
                if (cell == '\n')
                {
                    x = 0;
                    y++;
                    continue;
                }

                spriteBatch.Draw(
                    texture: content.Font,
                    position: drawAt + (charDim * new Vector2(x++, y)),
                    sourceRectangle: GetSourceRectFor(cell),
                    color: GameColors.TextColor
                    );

                if (x >= WrapAt && cell == ' ')
                {
                    x = 0;
                    y++;
                }
            }
        }

        private Rectangle? GetSourceRectFor(char value)
        {
            var r = value / 32;
            var c = value % 32;
            return new Rectangle(c * CHARW, r * CHARH, CHARW, CHARH);
        }
    }
}
