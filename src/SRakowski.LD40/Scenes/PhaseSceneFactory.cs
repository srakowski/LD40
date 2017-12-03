using SRakowski.LD40.Engine;
using SRakowski.LD40.Gameplay;
using System;

namespace SRakowski.LD40.Scenes
{
    static class PhaseSceneFactory
    {
        internal static Scene CreateSceneForPhase(GameState state, Phase phase)
        {
            if (phase.Id == PhaseId.MovementPhaseId) return new MovementScene(state, phase);
            if (phase.Id == PhaseId.SituationPhaseId) return new SituationScene(state, phase);
            throw new NotImplementedException();
        }
    }
}
