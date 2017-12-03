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

        internal static Territory Generate(Random random, Point mapPoint)
        {
            return new RuralTerritory(mapPoint);
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

    class WastelandTerritory : Territory
    {
        public WastelandTerritory(Point mapPoint) : base(mapPoint)
        {
        }
    }
}
