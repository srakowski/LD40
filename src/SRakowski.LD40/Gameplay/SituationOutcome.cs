using System.Collections.Generic;
using System.Linq;

namespace SRakowski.LD40.Gameplay
{
    abstract class SituationOutcome
    {
        public IEnumerable<Consequence> Consequences { get; }

        public IEnumerable<ResourceCard> Rewards { get; }

        protected SituationOutcome(
            IEnumerable<Consequence> consequences,
            IEnumerable<ResourceCard> rewards)
        {
            this.Consequences = consequences;
            this.Rewards = rewards;
        }
    }

    class FightOutcome : SituationOutcome
    {
        public FightOutcome(IEnumerable<Consequence> consequences, IEnumerable<ResourceCard> rewards) 
            : base(consequences, rewards)
        {
        }
    }

    class GrabAndFleeOutcome : SituationOutcome
    {
        public GrabAndFleeOutcome(IEnumerable<Consequence> consequences, ResourceCard reward) 
            : base(consequences, new[] { reward })
        {
        }
    }

    class FleeOutcome : SituationOutcome
    {
        public FleeOutcome(IEnumerable<Consequence> consequences) 
            : base(consequences, Enumerable.Empty<ResourceCard>())
        {
        }
    }
}
