using SRakowski.LD40.Gameplay;
using SRakowski.LD40.Gameplay.Phases;
using System;
using System.Linq;

namespace SRakowski.LD40.ConsolePrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameState = GameState.Create();
            var currentPhase = MovementPhase.Create();
            while (currentPhase != null)
            {
                Console.WriteLine($"{currentPhase.Name}: {currentPhase.Description}");
                var actions = currentPhase.PossibleActions.ToList();
                int i = 1;
                actions.ForEach(act => Console.WriteLine($"{i++}: {act.Description}"));
                int selectIdx = -1;
                while (selectIdx < 0)
                {
                    Console.Write($"Choice(1-{actions.Count})?: ");
                    string data = Console.ReadLine();
                    if (int.TryParse(data, out int value))
                    {
                        int actIdx = value - 1;
                        if (actIdx >= 0 && actIdx < actions.Count)
                            selectIdx = actIdx;
                    }
                }
                currentPhase = actions[selectIdx].Execute(gameState);
            }
        }
    }
}
