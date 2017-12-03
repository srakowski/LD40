using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Apokalipsa
{
    enum GroupBearing
    {
        East = 0,
        NorthEast = -55,
        NorthWest = -125,
        West = 180,
        SouthEast = 55,
        SouthWest = 125,
    }

    class Group : IGamePiece
    {
        private GameContent _content;

        private GameTile _tile;

        public int MemberCount { get; set; } = 3;

        public int FighterCount { get; set; } = 1;

        public int Heath { get; set; } = 10;

        public List<GameCard> ResourceCards { get; set; } = new List<GameCard>();

        public GameTile GameBoardTile
        {
            get => _tile;
            set
            {
                _tile = value;
                if (_tile != null) FixBearing(-1);
            }
        }

        public GroupBearing Bearing { get; set; }

        public Group(GameContent content)
        {
            _content = content;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var tile = GameBoardTile;
            var drawAt = tile.CalculateDrawAt(_content);

            spriteBatch.Draw(
                _content.GroupIcon,
                drawAt,
                null,
                GameColors.GroupColor,
                0f,
                _content.GroupIcon.GetOrigin(),
                1f,
                SpriteEffects.None,
                1f);

            spriteBatch.Draw(
                _content.Direction,
                drawAt,
                null,
                GameColors.GroupColor,
                MathHelper.ToRadians((int)Bearing),
                _content.Direction.GetOrigin() * new Vector2(-1f, 1f),
                1f,
                SpriteEffects.None,
                1f);
        }

        private static GroupBearing[] _bearings { get; } =
            new[] {
                GroupBearing.East,
                GroupBearing.NorthEast,
                GroupBearing.NorthWest,
                GroupBearing.West,
                GroupBearing.SouthWest,
                GroupBearing.SouthEast,
            };

        internal GameCard Damage(GameContext context)
        {
            var cardToLose = ResourceCards.OrderBy(_ => context.Random.Next(100)).FirstOrDefault();
            if (cardToLose != null)
                ResourceCards.Remove(cardToLose);
            return cardToLose;
        }

        internal void Injury(GameContext context)
        {
            this.Heath -= 1;
            if (this.Heath < 0) this.Heath = 0;
        }

        internal void Death(GameContext context)
        {
            this.MemberCount--;
            if (this.MemberCount < 0) this.MemberCount = 0;
            if (this.MemberCount < this.FighterCount) this.FighterCount--;
        }

        public void ExecuteMoveToTargetGameTile()
        {
            var target = GetTargetGameTile();
            GameBoardTile.PickupPiece(this);
            target.PlacePiece(this);
        }

        public void ChangeBearingLeft() => ChangeBearing(1);

        public void ChangeBearingRight() => ChangeBearing(-1);
        
        private void ChangeBearing(int direction)
        {
            var currentIdx = _bearings.ToList().IndexOf(Bearing);
            currentIdx += direction;
            if (currentIdx < 0) currentIdx = _bearings.Length - 1;
            if (currentIdx >= _bearings.Length) currentIdx = 0;
            Bearing = _bearings[currentIdx];
            FixBearing(direction);
        }

        private void FixBearing(int direction)
        {
            if (GetTargetGameTile() == null) ChangeBearing(direction);
        }

        private GameTile GetTargetGameTile()
        {
            var tile = this.GameBoardTile;
            var point = Point.Zero;
            if (Bearing == GroupBearing.West) point = tile.Position + new Point(-1, 0);
            if (Bearing == GroupBearing.NorthWest) point = tile.Position + new Point(-1, -1);
            if (Bearing == GroupBearing.NorthEast) point = tile.Position + new Point(0, -1);
            if (Bearing == GroupBearing.East) point = tile.Position + new Point(1, 0);
            if (Bearing == GroupBearing.SouthEast) point = tile.Position + new Point(1, 1);
            if (Bearing == GroupBearing.SouthWest) point = tile.Position + new Point(0, 1);
            return tile.GameBoard.GetGameTileAt(point);
        }
    }
}
