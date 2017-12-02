using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    abstract class Resource
    {
        public bool IsScouted { get; set; }

        public int GainToWellbeing { get; set; }

        public int GainToFightingCapability { get; set; }

        public int ChanceToNabWithoutConsequence { get; set; }

        public IEnumerable<Consequence> Consequences { get; set; }
    }

    /// <summary>
    /// Clothing, Teddy Bears, etc, increases wellbeing a small amount
    /// </summary>
    class ComfortItems : Resource
    {
    }

    /// <summary>
    /// Increases wellbeing a moderate amount
    /// </summary>
    class FoodAndWater : Resource
    {
    }

    /// <summary>
    /// Increases wellbeing a lot, rare
    /// </summary>
    class MedicineAndBandages : Resource
    {
    }

    /// <summary>
    /// Boosts fighting capability, plus gives option to train new fighter
    /// </summary>
    class Weapon : Resource
    {
    }

    /// <summary>
    /// Boosts fighting capability
    /// </summary>
    class Ammo : Resource
    {
    }

    /// <summary>
    /// Options to Add to group/survivor, exile/execute?, effects TBD
    /// </summary>
    class Survivor : Resource
    {
    }

    /// <summary>
    /// This resource is needed in combination with people in order to build a settlement
    /// as the walls are required
    /// </summary>
    class BuildingMaterial : Resource
    {
    }
}
