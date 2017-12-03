using System;
using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Gameplay
{
    enum PointOfInterest
    {
        None = 0,
        Hospital,
        PoliceStation,
        SuperMarket,
        ConstructionArea,
    }

    abstract class Territory
    {
        public Point MapPoint { get; }

        public int PopDensityPreApocolypse { get; }

        public PointOfInterest PointOfInterest { get; }

        public bool IsSettled { get; private set; }

        public Territory(Point mapPoint)
        {
            this.MapPoint = mapPoint;
        }

        public void Settle(Group group)
        {
        }

        internal static Territory Generate(Random random, Point cityCenter, Point mapPoint)
        {
            var next = random.Next(100);
            var distance = Vector2.Distance(cityCenter.ToVector2(), mapPoint.ToVector2());
            if (distance < 3)
            {
                if (next < 2) return new RuralTerritory(mapPoint);
                else if (next >= 40) return new UrbanTerritory(mapPoint);
                else return new SubUrbanTerritory(mapPoint);
            }
            else if (distance > 10)
            {
                if (next < 80) return new RuralTerritory(mapPoint);
                else if (next >= 97) return new UrbanTerritory(mapPoint);
                else return new SubUrbanTerritory(mapPoint);
            }
            else
            {
                if (next < 20) return new RuralTerritory(mapPoint);
                else if (next >= 80) return new UrbanTerritory(mapPoint);
                else return new SubUrbanTerritory(mapPoint);
            }
        }
    }

    class UrbanTerritory : Territory
    {
        public UrbanTerritory(Point mapPoint) : base(mapPoint)
        {
        }
    }

    class SubUrbanTerritory : Territory
    {
        public SubUrbanTerritory(Point mapPoint) : base(mapPoint)
        {
        }
    }

    class RuralTerritory : Territory
    {
        public RuralTerritory(Point mapPoint) : base(mapPoint)
        {
        }
    }
}
