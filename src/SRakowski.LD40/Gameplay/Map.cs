using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    class Map
    {
        public Dictionary<Point, Territory> _territories;

        public IEnumerable<Territory> Territories => _territories.Values;

        protected Map(Dictionary<Point, Territory> territories) =>
            _territories = territories;

        public static Map Generate(Random random)
        {
            var territories = new Dictionary<Point, Territory>();
            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                    territories.Add(new Point(x, y), Territory.Generate(random, new Point(x, y)));

            return new Map(territories);
        }

        public Territory GetTerritoryAtMapLocation(Point location) =>
            _territories[location];
    }
}
