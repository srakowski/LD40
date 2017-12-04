using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Apokalipsa
{
    enum GameTileType
    {
        Rural,
        Suburban,
        Urban
    }

    interface IGamePiece
    {
        GameTile GameBoardTile { get; set; }
    }

    class GameTile
    {
        public GameBoard GameBoard { get; set; }

        public Point Position { get; }

        public GameTileType Type { get; }

        public IGamePiece Occupant { get; private set; }

        public bool IsOccupied => Occupant != null;

        public bool IsSettled { get; private set; }

        public GameTile(Point position, GameTileType type)
        {
            Position = position;
            Type = type;
            Occupant = null;
        }

        public void PlacePiece(IGamePiece piece)
        {
            Occupant = piece;
            piece.GameBoardTile = this;
        }

        internal void PickupPiece(IGamePiece piece)
        {
            Occupant = null;
            piece.GameBoardTile = null;
        }

        public Vector2 CalculateDrawAt(GameContent content)
        {
            var xPos = (this.Position.X * content.HexTile.Width) + (-this.Position.Y * (content.HexTile.Width * 0.5f));
            var yPos = this.Position.Y * (content.HexTile.Height * 0.75f);
            var drawAt = new Vector2(xPos, yPos);
            return drawAt;
        }

        public void Settle()
        {
            this.IsSettled = true;
        }
    }

    class GameBoard
    {
        private Random _random;

        private GameContent _content;

        public Dictionary<Point, GameTile> _tiles = new Dictionary<Point, GameTile>();

        public GameBoard(
            Random random,
            GameContent content,
            Dictionary<Point, GameTile> tiles)
        {
            _random = random;
            _content = content;
            _tiles = tiles;
            foreach (var tile in _tiles.Values)
                tile.GameBoard = this;
        }

        internal void PlaceGroupOnInitialGameTile(Group group)
        {
            var initialTile =_tiles.Values
                .Where(t => t.Type == GameTileType.Rural)
                .OrderBy(_ => _random.Next(100))
                .First();

            initialTile.PlacePiece(group);
            group.GameBoardTile = initialTile;
        }

        internal GameTile GetGameTileAt(Point point) =>
            _tiles.TryGetValue(point, out GameTile tile) ? tile : null;

        // DRAW

        public void Draw(SpriteBatch spriteBatch)
        {
            var tilesToDraw = _tiles.Values.ToArray();
            foreach (var tile in tilesToDraw)
                DrawGameTile(spriteBatch, tile);
        }

        private void DrawGameTile(SpriteBatch spriteBatch, GameTile tile)
        {
            var origin = _content.HexTile.GetOrigin();
            Vector2 drawAt = tile.CalculateDrawAt(_content);

            // Draw the hex tile
            spriteBatch.Draw(
                _content.HexTile,
                drawAt,
                null,
                GameColors.TileColor,
                0f,
                origin,
                1f,
                SpriteEffects.None,
                0f);

            // Draw it's icon if not occupied
            if (!tile.IsOccupied)
            {
                var icon =
                    tile.IsSettled ? _content.SettlementIcon :
                    tile.Type == GameTileType.Urban ? _content.UrbanIcon :
                    tile.Type == GameTileType.Suburban ? _content.SuburbanIcon :
                    tile.Type == GameTileType.Rural ? _content.RuralIcon :
                    _content.HexTile;

                spriteBatch.Draw(
                    icon,
                    drawAt,
                    null,
                    tile.IsSettled ? GameColors.GroupColor : GameColors.TileColor,
                    0f,
                    icon.GetOrigin(),
                    1f,
                    SpriteEffects.None,
                    1f);
            }
        }

        // GENERATE MAP

        public static GameBoard Generate(Random random, GameContent content)
        {
            var tiles = new Dictionary<Point, GameTile>();
            var originTilePosition = new Point(0, 0);
            tiles.Add(originTilePosition, new GameTile(originTilePosition, GameTileType.Urban));
            AddLayer(GameTileType.Urban, tiles);
            AddLayer(GameTileType.Suburban, tiles);
            AddLayer(GameTileType.Suburban, tiles);
            AddLayer(GameTileType.Rural, tiles);
            AddLayer(GameTileType.Rural, tiles);
            return new GameBoard(random, content, tiles);
        }

        private static void AddLayer(GameTileType type, Dictionary<Point, GameTile> tiles)
        {
            var existingTiles = tiles.Values.ToArray();
            foreach (var tile in existingTiles)
                GenerateAdjacentTiles(tile, type, tiles);
        }

        private static void GenerateAdjacentTiles(GameTile tile, GameTileType type, Dictionary<Point, GameTile> tiles)
        {
            // West
            var point = tile.Position + new Point(-1, 0);
            AddTileAtPointIfItDoesntExist(type, tiles, point);

            // NorthWest
            point = tile.Position + new Point(-1, -1);
            AddTileAtPointIfItDoesntExist(type, tiles, point);

            // NorthEast
            point = tile.Position + new Point(0, -1);
            AddTileAtPointIfItDoesntExist(type, tiles, point);

            // East
            point = tile.Position + new Point(1, 0);
            AddTileAtPointIfItDoesntExist(type, tiles, point);

            // SouthEast
            point = tile.Position + new Point(1, 1);
            AddTileAtPointIfItDoesntExist(type, tiles, point);

            // SouthWest
            point = tile.Position + new Point(0, 1);
            AddTileAtPointIfItDoesntExist(type, tiles, point);
        }

        private static void AddTileAtPointIfItDoesntExist(GameTileType type, Dictionary<Point, GameTile> tiles, Point point)
        {
            if (tiles.ContainsKey(point)) return;
            tiles.Add(point, new GameTile(point, type));
        }
    }
}
