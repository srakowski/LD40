using System;
using static SRakowski.LD40.Gameplay.Direction;

namespace SRakowski.LD40.Gameplay.Phases
{
    static class MovementPhase
    {
        public static Phase Create()
        {
            return new Phase(
                "Move",
                "Advance your party to the next section of the city",
                null,
                new[]
                {
                    new GameAction("Advance North West", MoveFn(NorthWest)),
                    new GameAction("Advance North", MoveFn(North)),
                    new GameAction("Advance North East", MoveFn(NorthEast)),
                    new GameAction("Advance East", MoveFn(East)),
                    new GameAction("Advance South East", MoveFn(SouthEast)),
                    new GameAction("Advance South", MoveFn(South)),
                    new GameAction("Advance South West", MoveFn(SouthWest)),
                    new GameAction("Advance West", MoveFn(West)),
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
