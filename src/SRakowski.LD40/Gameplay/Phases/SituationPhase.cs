using System;
using System.Collections.Generic;
using System.Linq;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class SituationPhase
    {
        public static Phase Create(Situation situation)
        {
            var actions = new List<GameAction>();
            actions.Add(new GameAction("Fight", FightFn(situation)));
            actions.AddRange(situation.Rewards.Select(c => GrabAndFleeGameAction(situation, c)));
            actions.Add(new GameAction("Flee", FleeFn(situation)));
            return new Phase(
                "Situation",
                "Assess and take action",
                situation,
                actions);
        }

        private static Func<GameState, Phase> FightFn(Situation situation) =>
            gs => OutcomePhase.Create(situation.Fight(gs));

        private static GameAction GrabAndFleeGameAction(Situation situation, Reward card) =>
            new GameAction($"Grab the {card.ScoutedDescription} and Flee", GrabAndFleeFn(situation, card));

        private static Func<GameState, Phase> GrabAndFleeFn(Situation situation, Reward card) =>
            gs => OutcomePhase.Create(situation.GrabAndFlee(gs, card));

        private static Func<GameState, Phase> FleeFn(Situation situation) =>
            gs => OutcomePhase.Create(situation.Flee(gs));
    }
}
