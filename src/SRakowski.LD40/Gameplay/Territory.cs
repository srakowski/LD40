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

        public void Settle(Group group)
        {
        }
    }

    class UrbanTerritory : Territory
    {
    }

    class SubUrbanTerritory : Territory
    {
    }

    class RuralTerritory : Territory
    {
    }
}
