using Microsoft.Xna.Framework;
using System;

namespace SRakowski.LD40.Gameplay
{
    class Group
    {
        public const int WellBeingPointsPerSurvivor = 10;
        public const int FightingCapabilityPointsPerFigher = 10;

        public int WellBeing { get; set; }

        public int FightingCapability { get; set; }

        public int NumberOfFighters { get; set; }

        public int Size { get; set; }

        public int BuildingMaterials { get; set; }

        public Point MapLocation { get; set; }

        public static Group Create()
        {
            return new Group();
        }
    }
}
