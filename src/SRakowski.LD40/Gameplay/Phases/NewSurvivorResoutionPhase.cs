using System;
using System.Collections.Generic;
using System.Linq;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class SurvivorResoutionPhase
    {
        internal static Phase Create(IEnumerable<Survivor> survivors, Phase next)
        {
            var actions = new List<GameAction>();
            actions.AddRange(survivors.Select(s => IncorporateSurvivorIntoGroupAction(s, survivors, next)));
            actions.Add(new GameAction("Get rid of them", gs => next));
            return new Phase(
                PhaseId.SurvivorResolution,
                "What will you do with them?",
                survivors,
                actions);
        }

        private static GameAction IncorporateSurvivorIntoGroupAction(Survivor survivor, IEnumerable<Survivor> survivors, Phase next) =>
            new GameAction(
                $"Incorporate {survivor.Name} into your group", 
                IncorporateSurvivorFn(survivor, survivors, next));

        private static Func<GameState, Phase> IncorporateSurvivorFn(Survivor survivor, IEnumerable<Survivor> survivors, Phase next) =>
            gs =>
            {
                gs.Group.AddSurvivor(survivor);
                var remainingSurvivors = survivors.Where(s => s != survivor);
                return remainingSurvivors.Any() ?
                    Create(remainingSurvivors, next) :
                    next;
            };
    }
}
