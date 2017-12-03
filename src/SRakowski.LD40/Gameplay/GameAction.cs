using System;

namespace SRakowski.LD40.Gameplay
{
    enum GameActionId
    {
        Invalid = 0,
        MoveNorthWest,
        MoveNorth,
        MoveNorthEast,
        MoveEast,
        MoveSouthEast,
        MoveSouth,
        MoveSouthWest,
        MoveWest,
        AcceptConsequences,
        AcceptRewards,
        IgnoreSurvivors,
        AcceptSurvivor,
        AckOutcome,
        DoneRegrouping,
        Fight,
        Flee,
        GrabAndFlee,
    }

    class GameAction
    {
        public GameActionId Id { get; set; }

        public string Description { get; set; }

        public Func<GameState, Phase> Execute { get; set; }

        public GameAction(GameActionId id, string description, Func<GameState, Phase> execute)
        {
            this.Id = id;
            this.Description = description;
            this.Execute = execute;
        }
    }
}
