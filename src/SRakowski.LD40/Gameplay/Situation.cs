using System.Collections.Generic;

namespace SRakowski.LD40.Gameplay
{
    class Situation
    {
        public int NumberOfZombies { get; set; }

        public int ChanceToKillZombiesWithoutConsequence { get; set; }

        public int ChanceToRunWithoutConsequence { get; set; } 

        public IEnumerable<Consequence> Consequences { get; set; }

        public IEnumerable<Resource> ResourceToNabOrWin { get; set; }

        /// <summary>
        /// Most risky, but most potential for reward as all resources
        /// will be gained.
        /// </summary>
        public void FightZombies()
        {
        }

        /// <summary>
        /// Somewhat risky, some reward as you may get a resource.
        /// </summary>
        public void NabResourceAndRun()
        {
        }

        /// <summary>
        /// Least risky, but no resources gained and group wellbeing is diminished.
        /// </summary>
        public void Run()
        {
        }
    }
}
