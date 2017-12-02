using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    class Group
    {
        public const int WellBeingPointsPerSurvivor = 10;
        public const int FightingCapabilityPointsPerFigher = 10;

        private readonly List<ResourceCard> _resourceCards;

        public int WellBeing { get; set; }

        public int FightingCapability { get; set; }

        public int NumberOfFighters { get; set; }

        public int Size { get; set; }

        public Point MapLocation { get; set; }

        protected Group()
        {
            _resourceCards = new List<ResourceCard>();
        }

        public static Group Create()
        {
            return new Group();
        }

        internal void AddCards(IEnumerable<ResourceCard> resourceCards) =>
            _resourceCards.AddRange(resourceCards);

        internal void AddSurvivor(Survivor survivor)
        {
            throw new NotImplementedException();
        }
    }
}
