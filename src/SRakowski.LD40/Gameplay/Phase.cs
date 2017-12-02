using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    class Phase
    {
        public string Name { get; }

        public string Description { get; }

        public object Context { get; set; }

        public IEnumerable<GameAction> PossibleActions { get; }

        public Phase(string name, string description, object context,  IEnumerable<GameAction> possibleActions)
        {
            Name = name;
            Description = description;
            PossibleActions = possibleActions;
        }
    }
}
