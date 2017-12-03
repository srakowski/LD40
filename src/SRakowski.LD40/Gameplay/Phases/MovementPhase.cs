using System;
using static SRakowski.LD40.Gameplay.Direction;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class MovementPhase
    {
        public static Phase Create()
        {
            return new Phase(
                PhaseId.MovementPhaseId,
                "Advance your party to the next section of the city",
                null,
                new[]
                {
                    new GameAction(GameActionId.MoveNorthWest , "Advance North West", MoveFn(NorthWest)),
                    new GameAction(GameActionId.MoveNorth , "Advance North", MoveFn(North)),
                    new GameAction(GameActionId.MoveNorthEast , "Advance North East", MoveFn(NorthEast)),
                    new GameAction(GameActionId.MoveEast , "Advance East", MoveFn(East)),
                    new GameAction(GameActionId.MoveSouthEast , "Advance South East", MoveFn(SouthEast)),
                    new GameAction(GameActionId.MoveSouth , "Advance South", MoveFn(South)),
                    new GameAction(GameActionId.MoveSouthWest, "Advance South West", MoveFn(SouthWest)),
                    new GameAction(GameActionId.MoveWest, "Advance West", MoveFn(West)),
                });
        }

        private static Func<GameState, Phase> MoveFn(Direction direction) =>
            (gs) =>
            {
                var targetMapLocation = gs.Group.MapLocation.GetPointInDirection(direction);
                var targetTerritory = gs.Map.GetTerritoryAtMapLocation(targetMapLocation);
                if (targetTerritory is WastelandTerritory)
                {
                    // make them move again
                    return Create();
                }
                else
                {
                    gs.Group.MapLocation = targetMapLocation;
                    var situation = Situation.Generate(gs, targetTerritory);
                    return SituationPhase.Create(situation);
                }
            };
    }
}
