namespace SRakowski.LD40.Gameplay
{
    /// <summary>
    /// What happens if something goes wrong in a Situation.
    /// </summary>
    abstract class Consequence
    {
        public bool TargetsFighter { get; set; }

        public int LossToWellBeing { get; set; }

        public int LossToFightingCapability { get; set; }
    }

    class Death : Consequence
    {
    }

    class Bite : Consequence
    {
    }

    class HarmToGroup : Consequence
    {
    }

    class DamageOrLossOfFightingEquipment : Consequence
    {
    }
}
