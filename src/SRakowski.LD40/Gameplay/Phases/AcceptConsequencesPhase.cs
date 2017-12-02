using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class AcceptConsequencesPhase
    {
        internal static Phase Create(IEnumerable<Consequence> consequences, Phase next)
        {
            return new Phase(
                "Consequences",
                "Something went wrong...", 
                consequences,
                new[]
                {
                    new GameAction("Accept", gs => 
                    {
                        foreach (var consequence in consequences) consequence.Apply(gs);
                        return next;
                    })
                });
        }
    }
}
