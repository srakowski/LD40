﻿using System.Linq;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class OutcomePhase
    {
        public static Phase Create(SituationOutcome outcome)
        {
            Phase next = MovementPhase.Create();

            if (outcome is FightOutcome)
                next = RestAndRegroupPhase.Create(outcome.Situation.Territory, next);

            if (outcome.Rewards.OfType<ResourceCard>().Any())
                next = AcceptRewardsPhase.Create(outcome.Rewards.OfType<ResourceCard>(), next);

            if (outcome.Rewards.OfType<Survivor>().Any())
                next = NewSurvivorResoutionPhase.Create(outcome.Rewards.OfType<Survivor>(), next);

            if (outcome.Consequences.Any())
                next = AcceptConsequencesPhase.Create(outcome.Consequences, next);

            return new Phase(
                "Outcome",
                outcome.Description,
                null,
                new[]
                {
                    new GameAction("Ok", gs => next)
                });
        }
    }
}
