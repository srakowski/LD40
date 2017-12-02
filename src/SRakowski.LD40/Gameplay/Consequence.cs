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
    }

    class DeathConsequence : Consequence
    {
    }

    class BiteConsequence : Consequence
    {
    }

    class HarmConsequence : Consequence
    {
    }

    class DamageConsequence : Consequence
    {
    }
}
