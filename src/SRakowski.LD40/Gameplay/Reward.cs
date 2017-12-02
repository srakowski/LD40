using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    abstract class Reward
    {
        public string Description { get; }

        // Effects when consumed
        public int GainToWellbeing { get; }
        public int GainToFightingCapability { get; }

        // Used when in situation
        public string ScoutedDescription { get; }
        public IEnumerable<Consequence> GrabConsequences { get; }

        public Reward(
            string description,
            string scoutedDescription,
            IEnumerable<Consequence> grabConsequences)
        {
            Description = description;
            ScoutedDescription = scoutedDescription;
            GrabConsequences = grabConsequences;
        }
    }

    /// <summary>
    /// Options to Add to group/survivor, exile/execute?, effects TBD
    /// </summary>
    class Survivor : Reward
    {
        public string Name { get; }

        public Survivor(string name, string scoutedDescription, IEnumerable<Consequence> grabConsequences)
            : base("Survivor", scoutedDescription, grabConsequences)
        {
            Name = name;
        }
    }

    abstract class ResourceCard : Reward
    {
        public ResourceCard(string description, string scoutedDescription, IEnumerable<Consequence> grabConsequences) 
            : base(description, scoutedDescription, grabConsequences)
        {
        }
    }

    /// <summary>
    /// Clothing, Teddy Bears, etc, increases wellbeing a small amount
    /// </summary>
    class ComfortItemsResourceCard : ResourceCard
    {
        public ComfortItemsResourceCard(string scoutedDescription, IEnumerable<Consequence> grabConsequences) 
            : base("Comfort Items", scoutedDescription, grabConsequences)
        {
        }
    }

    /// <summary>
    /// Increases wellbeing a moderate amount
    /// </summary>
    class FoodAndWaterResourceCard : ResourceCard
    {
        public FoodAndWaterResourceCard(string scoutedDescription, IEnumerable<Consequence> grabConsequences) 
            : base("Food and Water", scoutedDescription, grabConsequences)
        {
        }
    }

    /// <summary>
    /// Increases wellbeing a lot, rare
    /// </summary>
    class MedicalSuppliesResourceCard : ResourceCard
    {
        public MedicalSuppliesResourceCard(string scoutedDescription, IEnumerable<Consequence> grabConsequences) 
            : base("Medical Supplies", scoutedDescription, grabConsequences)
        {
        }
    }

    /// <summary>
    /// Boosts fighting capability, plus gives option to train new fighter
    /// </summary>
    class WeaponResourceCard : ResourceCard
    {
        public WeaponResourceCard(string scoutedDescription, IEnumerable<Consequence> grabConsequences) 
            : base("Weapon", scoutedDescription, grabConsequences)
        {
        }
    }

    /// <summary>
    /// Boosts fighting capability
    /// </summary>
    class AmmoResourceCard : ResourceCard
    {
        public AmmoResourceCard(string scoutedDescription, IEnumerable<Consequence> grabConsequences) 
            : base("Ammunition", scoutedDescription, grabConsequences)
        {
        }
    }

    /// <summary>
    /// This resource is needed in combination with people in order to build a settlement
    /// as the walls are required
    /// </summary>
    class BuildingMaterialResourceCard : ResourceCard
    {
        public BuildingMaterialResourceCard(string scoutedDescription, IEnumerable<Consequence> grabConsequences) 
            : base("Building Material", scoutedDescription, grabConsequences)
        {
        }
    }
}
