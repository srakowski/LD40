using System.Collections.Generic;
using System.Linq;

namespace SRakowski.LD40.Gameplay
{
    abstract class SituationOutcome
    {
        public Situation Situation { get; }

        public string Description { get; }

        public IEnumerable<Consequence> Consequences { get; }

        public IEnumerable<Reward> Rewards { get; }

        protected SituationOutcome(
            Situation situation,
            string description,
            IEnumerable<Consequence> consequences,
            IEnumerable<Reward> rewards)
        {
            this.Description = description;
            Consequences = consequences;
            this.Rewards = rewards;
        }
    }

    class FightOutcome : SituationOutcome
    {
        public FightOutcome(Situation situation, string description, IEnumerable<Consequence> consequences, IEnumerable<Reward> rewards) 
            : base(situation, description, consequences, rewards)
        {
        }
    }

    class GrabAndFleeOutcome : SituationOutcome
    {
        public GrabAndFleeOutcome(Situation situation, string description, IEnumerable<Consequence> consequences, Reward reward) 
            : base(situation, description, consequences, new[] { reward })
        {
        }
    }

    class FleeOutcome : SituationOutcome
    {
        public FleeOutcome(Situation situation, string description, IEnumerable<Consequence> consequences) 
            : base(situation, description, consequences, Enumerable.Empty<Reward>())
        {
        }
    }
}
