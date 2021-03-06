﻿using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    enum PhaseId
    {
        Invalid = 0,
        MovementPhaseId,
        SituationPhaseId,
        OutcomePhaseId,
        AcceptConsequencesPhaseId,
        SurvivorResolutionPhaseId,
        AcceptRewardsPhaseId,
        RegroupPhaseId,
    }

    class Phase
    {
        public PhaseId Id { get; }

        public string Description { get; }

        public object Context { get; set; }

        public IEnumerable<GameAction> PossibleActions { get; }

        public Phase(PhaseId id, string description, object context,  IEnumerable<GameAction> possibleActions)
        {
            Id = id;
            Description = description;
            Context = context;
            PossibleActions = possibleActions;
        }
    }
}
