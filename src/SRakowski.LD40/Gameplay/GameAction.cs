using System;

namespace SRakowski.LD40.Gameplay
{
    class GameAction
    {
        public string Description { get; set; }

        public Func<GameState, Phase> Execute { get; set; }

        public GameAction(string description, Func<GameState, Phase> execute)
        {
            this.Description = description;
            this.Execute = execute;
        }
    }
}
