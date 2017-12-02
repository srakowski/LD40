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
            for (int x = -9; x < 10; x++)
                for (int y = -9; y < 10; y++)
                    territories.Add(new Point(x, y), Territory.Generate(random));

            return new Map(territories);
        }

        public Territory GetTerritoryAtMapLocation(Point location) =>
            _territories[location];
    }
}
