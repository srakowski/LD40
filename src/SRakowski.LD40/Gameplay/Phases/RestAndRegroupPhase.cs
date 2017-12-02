using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class RestAndRegroupPhase
    {
        internal static Phase Create(Territory territory, Phase next)
        {
            var actions = new List<GameAction>();



            return new Phase(
                "Rest and Regroup",
                "Use resource cards and or settle this territory",
                null,
                new[]
                {
                    new GameAction("Next", gs => next)
                });
        }
    }
}
