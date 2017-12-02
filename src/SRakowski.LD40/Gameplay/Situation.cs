using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    class Situation
    {
        public string ThreatDescription { get; set; }

        public IEnumerable<Consequence> FightConsequences { get; set; }

        public IEnumerable<Consequence> FleeConsequences { get; set; }

        public IEnumerable<ResourceCard> Rewards { get; set; }

        protected Situation(
            string threatDescription,
            IEnumerable<Consequence> consequences,
            IEnumerable<ResourceCard> rewards)
        {
            FightConsequences = consequences;
            Rewards = rewards;
        }

        /// <summary>
        /// Most risky, but most potential for reward as all resources
        /// will be gained.
        /// </summary>
        public SituationOutcome Fight(GameState gs)
        {
            return new FightOutcome(FightConsequences, Rewards);
        }

        /// <summary>
        /// Somewhat risky, some reward as you may get a resource.
        /// </summary>
        public SituationOutcome GrabAndFlee(GameState gs, ResourceCard card)
        {
            return new GrabAndFleeOutcome(card.GrabConsequences, card);
        }

        /// <summary>
        /// Least risky, but no resources gained and group wellbeing is diminished.
        /// </summary>
        public SituationOutcome Flee(GameState gs)
        {
            return new FleeOutcome(FleeConsequences);
        }

        /// <summary>
        /// Notes:
        /// Chances to succeed at any particular situation action without consequence should be affected
        /// by the size of the group. 
        /// </summary>
        public static Situation Generate(GameState gs, Territory territory)
        {
            return new Situation(
                "Zombie Horde",
                new[]
                {
                    new BiteConsequence(),
                },
                new[]
                {
                    new FoodAndWaterResourceCard(),
                });
        }
    }
}
