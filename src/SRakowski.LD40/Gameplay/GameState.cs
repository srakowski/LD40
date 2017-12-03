using System;

namespace SRakowski.LD40.Gameplay
{
    class GameState
    {
        public Random Rng { get; }
        public Group Group { get; }
        public Map Map { get; }

        public GameState(Random rng, Group group, Map map)
        {
            Group = group;
            Map = map;
        }

        public static GameState Create(int? seed = null)
        {
            var rng = seed.HasValue ? new Random(seed.Value) : new Random();
            return new GameState(
                rng,
                Group.Create(),
                Map.Generate(rng)
            );
        }
    }
}
