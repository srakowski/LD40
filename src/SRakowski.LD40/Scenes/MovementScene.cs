using Microsoft.Xna.Framework.Input;
using SRakowski.LD40.Engine;
using SRakowski.LD40.Entities;
using SRakowski.LD40.Gameplay;
using SRakowski.LD40.Gameplay.Phases;
using System;
using System.Linq;
using CameraEntity = SRakowski.LD40.Entities.CameraEntity;

namespace SRakowski.LD40.Scenes
{
    class MovementScene : Scene
    {
        private GameState _gameState;

        private Phase _phase;

        public MovementScene(GameState gameState, Phase phase)
        {
            _gameState = gameState;
            _phase = phase;
        }

        public override void Initialize()
        {
            AddEntity(new CameraEntity());

            foreach (var territory in _gameState.Map.Territories)
                AddEntity(new TerritoryEntity(territory));

            AddEntity(new GroupEntity(_gameState.Group));

            AddEntity(new Entity().AddComponent(new RelayBehavior(HandleMovementInput)));

            //var form = new Form();
            //AddEntity(new Pointer(form, new SpriteRenderer("Sprites/pointer")));
        }

        public static Scene Create() =>
            new MovementScene(GameState.Create(), MovementPhase.Create());

        private void HandleMovementInput(Behavior sender)
        {
            if (_phase.Id != PhaseId.MovementPhaseId) throw new InvalidOperationException();
            if (sender.Input.WasKeyPressed(Keys.NumPad7)) ExecuteGameAction(GameActionId.MoveNorthWest);
            if (sender.Input.WasKeyPressed(Keys.NumPad8)) ExecuteGameAction(GameActionId.MoveNorth);
            if (sender.Input.WasKeyPressed(Keys.NumPad9)) ExecuteGameAction(GameActionId.MoveNorthEast);
            if (sender.Input.WasKeyPressed(Keys.NumPad6)) ExecuteGameAction(GameActionId.MoveEast);
            if (sender.Input.WasKeyPressed(Keys.NumPad3)) ExecuteGameAction(GameActionId.MoveSouthEast);
            if (sender.Input.WasKeyPressed(Keys.NumPad2)) ExecuteGameAction(GameActionId.MoveSouth);
            if (sender.Input.WasKeyPressed(Keys.NumPad1)) ExecuteGameAction(GameActionId.MoveSouthWest);
            if (sender.Input.WasKeyPressed(Keys.NumPad4)) ExecuteGameAction(GameActionId.MoveWest);
        }

        private void ExecuteGameAction(GameActionId id)
        {
            Exit();
            Context.SceneManager.AddScene(PhaseSceneFactory.CreateSceneForPhase(_gameState, _phase.PossibleActions.Where(pa => pa.Id == id).First()?.Execute(_gameState)));
        }
    }
}
