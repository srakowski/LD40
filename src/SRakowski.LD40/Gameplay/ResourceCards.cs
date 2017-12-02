using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    abstract class ResourceCard
    {
        public string ScoutedDescription { get; }

        public string Description { get; }

        public IEnumerable<Consequence> GrabConsequences { get; }

        public int GainToWellbeing { get; }

        public int GainToFightingCapability { get; }
    }

    /// <summary>
    /// Clothing, Teddy Bears, etc, increases wellbeing a small amount
    /// </summary>
    class ComfortItemsResourceCard : ResourceCard
    {
    }

    /// <summary>
    /// Increases wellbeing a moderate amount
    /// </summary>
    class FoodAndWaterResourceCard : ResourceCard
    {
    }

    /// <summary>
    /// Increases wellbeing a lot, rare
    /// </summary>
    class MedicalSuppliesResourceCard : ResourceCard
    {
    }

    /// <summary>
    /// Boosts fighting capability, plus gives option to train new fighter
    /// </summary>
    class WeaponResourceCard : ResourceCard
    {
    }

    /// <summary>
    /// Boosts fighting capability
    /// </summary>
    class AmmoResourceCard : ResourceCard
    {
    }

    /// <summary>
    /// Options to Add to group/survivor, exile/execute?, effects TBD
    /// </summary>
    class SurvivorResourceCard : ResourceCard
    {
    }

    /// <summary>
    /// This resource is needed in combination with people in order to build a settlement
    /// as the walls are required
    /// </summary>
    class BuildingMaterialResourceCard : ResourceCard
    {
    }
}
