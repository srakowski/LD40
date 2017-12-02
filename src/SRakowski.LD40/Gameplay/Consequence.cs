namespace SRakowski.LD40.Gameplay
{
    /// <summary>
    /// What happens if something goes wrong in a Situation.
    /// </summary>
    abstract class Consequence
    {
        public int ChanceToHappen { get; set; }

        public bool TargetsFighter { get; set; }

        public int LossToWellBeing { get; set; }

        public int LossToFightingCapability { get; set; }

        public abstract void Apply(GameState gs);
    }

    class DeathConsequence : Consequence
    {
        public override void Apply(GameState gs)
        {
        }
    }

    class BiteConsequence : Consequence
    {
        public override void Apply(GameState gs)
        {
        }
    }

    class HarmConsequence : Consequence
    {
        public override void Apply(GameState gs)
        {
        }
    }

    class DamageConsequence : Consequence
    {
        public override void Apply(GameState gs)
        {
        }
    }
}
