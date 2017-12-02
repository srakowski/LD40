using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    class Situation
    {
        public string ThreatDescription { get; set; }

        public Territory Territory { get; set; }

        public IEnumerable<Consequence> FightConsequences { get; set; }

        public IEnumerable<Consequence> FleeConsequences { get; set; }

        public IEnumerable<Reward> Rewards { get; set; }

        protected Situation(
            string threatDescription,
            Territory territory,
            IEnumerable<Consequence> fightConsequences,
            IEnumerable<Consequence> fleeConsequences,
            IEnumerable<Reward> rewards)
        {
            FightConsequences = fightConsequences;
            FleeConsequences = fleeConsequences;
            Rewards = rewards;
        }

        /// <summary>
        /// Most risky, but most potential for reward as all resources
        /// will be gained.
        /// </summary>
        public SituationOutcome Fight(GameState gs)
        {
            return new FightOutcome(this, "You fought...", FightConsequences, Rewards);
        }

        /// <summary>
        /// Somewhat risky, some reward as you may get a resource.
        /// </summary>
        public SituationOutcome GrabAndFlee(GameState gs, Reward card)
        {
            return new GrabAndFleeOutcome(this, "You grabbed and fleed...", card.GrabConsequences, card);
        }

        /// <summary>
        /// Least risky, but no resources gained and group wellbeing is diminished.
        /// </summary>
        public SituationOutcome Flee(GameState gs)
        {
            return new FleeOutcome(this, "You fleed...", FleeConsequences);
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
                territory,
                new[]
                {
                    new BiteConsequence(),
                },
                new[]
                {
                    new DamageConsequence(),
                },
                new[]
                {
                    new FoodAndWaterResourceCard("Box With Banana On It", new [] {
                        new HarmConsequence(),
                    }),
                });
        }
    }
}
