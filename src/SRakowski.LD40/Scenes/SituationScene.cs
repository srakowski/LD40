using SRakowski.LD40.Engine;
using SRakowski.LD40.Gameplay;
using System;

namespace SRakowski.LD40.Scenes
{
    class SituationScene : Scene
    {
        private GameState _gameState;

        private Phase _phase;

        private Situation _situation;

        public SituationScene(GameState gameState, Phase phase)
        {
            _gameState = gameState;
            _phase = phase;
            _situation = phase.Context as Situation;
            if (_situation == null) throw new InvalidOperationException();
        }

        public override void Initialize()
        {
            AddEntity(new Entity().AddComponent(new RelayBehavior(HandleInput)));
        }

        private void HandleInput(Behavior sender)
        {
        }
    }
}
