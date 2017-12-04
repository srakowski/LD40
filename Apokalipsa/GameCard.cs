using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apokalipsa
{
    abstract class GameCard
    {
        private int CARDW = 48;
        private int CARDH = 64;

        private readonly int _frame;

        protected GameCard(int frame) => _frame = frame;

        public void Draw(SpriteBatch spriteBatch, GameContent content, Vector2 drawAt)
        {
            var source = new Rectangle(_frame * CARDW, 0, CARDW, CARDH);
            spriteBatch.Draw(
                content.Cards,
                drawAt,
                source,
                this is SurvivorCard s && s.Fighter ? GameColors.FighterColor : GameColors.CardColor,
                0f,
                Vector2.Zero, //new Vector2(CARDW * 0.5f, CARDH * 0.5f),
                1f,
                SpriteEffects.None,
                0f);
        }
    }

    /// <summary>
    /// Increases wellbeing a moderate amount
    /// </summary>
    class FoodAndWaterResourceCard : GameCard
    {
        public FoodAndWaterResourceCard() : base(0)
        {
        }
    }

    /// <summary>
    /// Increases wellbeing a lot, rare
    /// </summary>
    class MedicalSuppliesResourceCard : GameCard
    {
        public MedicalSuppliesResourceCard() : base(1)
        {
        }
    }

    /// <summary>
    /// Boosts fighting capability, plus gives option to train new fighter
    /// </summary>
    class WeaponResourceCard : GameCard
    {
        public WeaponResourceCard() : base(2)
        {
        }
    }

    /// <summary>
    /// Boosts fighting capability
    /// </summary>
    class AmmoResourceCard : GameCard
    {
        public AmmoResourceCard() : base(3)
        {
        }
    }

    /// <summary>
    /// This resource is needed in combination with people in order to build a settlement
    /// as the walls are required
    /// </summary>
    class BuildingMaterialResourceCard : GameCard
    {
        public BuildingMaterialResourceCard() : base(4)
        {
        }
    }

    /// <summary>
    /// Survivors
    /// </summary>
    class SurvivorCard : GameCard
    {
        public bool Fighter { get; set; }
        public SurvivorCard(bool fighter = false) : base(5)
        {
            this.Fighter = fighter;
        }
    }


    class DeathCard : GameCard
    {
        public DeathCard() : base(6)
        {
        }
    }


    class InjuryCard : GameCard
    {
        public InjuryCard() : base(7)
        {
        }
    }


    class DamageCard : GameCard
    {
        public DamageCard() : base(8)
        {
        }
    }

}
