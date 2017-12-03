using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class AcceptConsequencesPhase
    {
        internal static Phase Create(IEnumerable<Consequence> consequences, Phase next)
        {
            return new Phase(
                PhaseId.AcceptConsequencesPhaseId,
                "Something went wrong...", 
                consequences,
                new[]
                {
                    new GameAction(GameActionId.AcceptConsequences, "Accept", gs => 
                    {
                        foreach (var consequence in consequences) consequence.Apply(gs);
                        return next;
                    })
                });
        }
    }
}
