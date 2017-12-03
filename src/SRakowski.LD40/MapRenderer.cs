using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SRakowski.LD40.Gameplay;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRakowski.LD40
{
    class MapRenderer
    {
        private Map _map;
        Texture2D tileBase;
        Texture2D tileOutline;
        Texture2D urbanIco;
        private Texture2D suburb;
        private Texture2D rural;
        private List<TerritoryRenderer> _renderers = new List<TerritoryRenderer>();

        public MapRenderer(Map map)
        {
            _map = map;
            foreach (var territory in map.Territories)
                _renderers.Add(new TerritoryRenderer(territory, _map));
        }

        public void LoadContent(ContentManager content)
        {
            tileBase = content.Load<Texture2D>("tileBase");
            tileOutline = content.Load<Texture2D>("tileBot");
            urbanIco = content.Load<Texture2D>("urban");
            suburb = content.Load<Texture2D>("suburb");
            rural = content.Load<Texture2D>("ruralico");
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tileBase, new Vector2(100, 100), GameColors.BackgroundColor);
            //spriteBatch.Draw(tileBot, new Vector2(100, 100), GameColors.ForegroundColor);
            foreach (var tr in _renderers)
                tr.Draw(spriteBatch, tileOutline, tileBase, urbanIco, suburb, rural);
        }
    }

    class TerritoryRenderer
    {
        private Territory territory;

        private Map map;

        private Vector2 _drawAt;

        public TerritoryRenderer(Territory territory, Map map)
        {
            this.territory = territory;
            this.map = map;
            _drawAt = MapHelper.MapPointToScreenPos(territory.MapPoint);
        }

        internal void Draw(SpriteBatch spriteBatch, Texture2D tileOutline, Texture2D tileBase, Texture2D urban, Texture2D suburb, Texture2D rural)
        {
            if (map.GetTerritoryAtMapLocation(territory.MapPoint.GetPointInDirection(territory.MapPoint.Y, Direction.West)) == null)
                DrawTheTerritory(spriteBatch, tileOutline, new Vector2(0, 16), new Rectangle(0, 16, 32, 32));

            if (map.GetTerritoryAtMapLocation(territory.MapPoint.GetPointInDirection(territory.MapPoint.Y, Direction.NorthWest)) == null)
                DrawTheTerritory(spriteBatch, tileOutline, new Vector2(0,0), new Rectangle(0, 0, 32, 16));

            if (map.GetTerritoryAtMapLocation(territory.MapPoint.GetPointInDirection(territory.MapPoint.Y, Direction.NorthEast)) == null)
                DrawTheTerritory(spriteBatch, tileOutline, new Vector2(32, 0), new Rectangle(32, 0, 32, 16));

            if (map.GetTerritoryAtMapLocation(territory.MapPoint.GetPointInDirection(territory.MapPoint.Y, Direction.SouthWest)) == null)
                DrawTheTerritory(spriteBatch, tileOutline, new Vector2(0, 48), new Rectangle(0, 48, 32, 16));

            if (map.GetTerritoryAtMapLocation(territory.MapPoint.GetPointInDirection(territory.MapPoint.Y, Direction.SouthEast)) == null)
                DrawTheTerritory(spriteBatch, tileOutline, new Vector2(32,48), new Rectangle(32, 48, 32, 16));

            if (map.GetTerritoryAtMapLocation(territory.MapPoint.GetPointInDirection(territory.MapPoint.Y, Direction.East)) == null)
                DrawTheTerritory(spriteBatch, tileOutline, new Vector2(32,16), new Rectangle(32, 16, 32, 32));

            spriteBatch.Draw(
                texture: tileBase,
                position: _drawAt,
                color: GameColors.BackgroundColor,
                origin: tileBase.Bounds.Center.ToVector2(),
                scale: new Vector2(0.90f, 0.90f));

            spriteBatch.Draw(
                texture: territory is RuralTerritory ? rural :
                    territory is UrbanTerritory ? urban :
                    territory is SubUrbanTerritory ? suburb : tileBase,
                position: _drawAt,
                color: GameColors.TileColor,
                origin: urban.Bounds.Center.ToVector2());
        }

        private void DrawTheTerritory(SpriteBatch spriteBatch, Texture2D tileOutline, Vector2 offset, Rectangle? r)
        {
            spriteBatch.Draw(
                texture: tileOutline,
                position: _drawAt + offset,
                color: GameColors.TileColor,
                origin: tileOutline.Bounds.Center.ToVector2(),
                sourceRectangle: r);
        }
    }

    static class MapHelper
    {
        public static Vector2 MapPointToScreenPos(Point mapPoint)
        {
            return (mapPoint.ToVector2() * new Vector2(64f, 48f)) + new Vector2(Math.Abs(mapPoint.Y) % 2 == 1 ? 32f : 0f, 0f);
        }
    }
}
