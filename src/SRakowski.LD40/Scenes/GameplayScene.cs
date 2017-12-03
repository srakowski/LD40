using SRakowski.LD40.Engine;
using SRakowski.LD40.Engine.UI;
using SRakowski.LD40.Entities;
using SRakowski.LD40.Gameplay;
using SRakowski.LD40.Gameplay.Phases;
using CameraEntity = SRakowski.LD40.Entities.CameraEntity;

namespace SRakowski.LD40.Scenes
{
    class GameplayScene : Scene
    {
        private GameState _gameState;

        private Phase _currentPhase;

        public GameplayScene(GameState gameState)
        {
            this._gameState = gameState;
            this._currentPhase = MovementPhase.Create();
        }

        public override void Initialize()
        {
            var form = new Form();
            AddEntity(new Pointer(form, new SpriteRenderer("Sprites/pointer")));

            AddEntity(new CameraEntity());
            foreach (var territory in _gameState.Map.Territories)
                AddEntity(new TerritoryEntity(territory));
        }

        public static Scene Create() =>
            new GameplayScene(GameState.Create());

    }
}
