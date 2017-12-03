using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class AcceptRewardsPhase
    {
        internal static Phase Create(IEnumerable<ResourceCard> resourceCards, Phase next)
        {
            return new Phase(
                PhaseId.AcceptRewards,
                "To the victor goes the spoils...",
                resourceCards,
                new[]
                {
                    new GameAction("Accept", gs => 
                    {
                        gs.Group.AddCards(resourceCards);
                        return next;
                    })
                });
        }
    }
}
