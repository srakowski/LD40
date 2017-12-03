using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class AcceptRewardsPhase
    {
        internal static Phase Create(IEnumerable<ResourceCard> resourceCards, Phase next)
        {
            return new Phase(
                PhaseId.AcceptRewardsPhaseId,
                "To the victor goes the spoils...",
                resourceCards,
                new[]
                {
                    new GameAction(GameActionId.AcceptRewards, "Accept", gs => 
                    {
                        gs.Group.AddCards(resourceCards);
                        return next;
                    })
                });
        }
    }
}
