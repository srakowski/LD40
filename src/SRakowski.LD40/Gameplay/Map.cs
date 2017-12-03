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
            int scale = 10; // 40x40ish board...
            var center = new Point(random.Next(-(scale / 3), (scale / 3) + 1), random.Next(-(scale / 3), (scale / 3) + 1));
            var territories = new Dictionary<Point, Territory>();
            for (int y = -scale - 1; y < scale; y++)
                for (int x = -scale + Math.Abs(y) - 1; x < scale - Math.Abs(y); x++)
                    territories.Add(new Point(x, y), Territory.Generate(random, center, new Point(x, y)));

            return new Map(territories);
        }

        public Territory GetTerritoryAtMapLocation(Point location) =>
            _territories.TryGetValue(location, out Territory t) ? t : null;
    }
}
