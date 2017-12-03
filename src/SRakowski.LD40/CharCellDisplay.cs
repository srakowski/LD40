using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SRakowski.LD40.Scenes;

namespace SRakowski.LD40
{
    class CharCellDisplay
    {
        private int _rows = 16; // orig 46
        private int _cols = 83;
        private const int CHARW = 12;
        private const int CHARH = 16;

        private Vector2 _pos;

        private char?[,] _cells;

        private Texture2D _font;

        public CharCellDisplay(Vector2 pos, int rows, int cols, Texture2D font)
        {
            _font = font;
            _pos = pos;
            _rows = rows;
            _cols = cols;
            _cells = new char?[_rows, _cols];
        }

        public void SetChar(int row, int col, char value)
        {
            int c = col % _cols;
            int r = row % _rows;
            _cells[r, c] = value;
        }

        public void Clear()
        {
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _cols; c++)
                    _cells[r, c] = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var charDim = new Vector2(CHARW, CHARH);
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _cols; c++)
                {
                    var cell = _cells[r, c];
                    if (!cell.HasValue) continue;
                    spriteBatch.Draw(
                        texture: _font,
                        position: _pos + (charDim * new Vector2(c, r)),
                        sourceRectangle: GetSourceRectFor(cell.Value),
                        color: GameColors.ForegroundColor
                        );
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
