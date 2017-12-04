using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Apokalipsa
{
    class SituationState : GameState
    {
        private class Risk
        {
            public GameCard Card { get; }
            public int ChanceOfHappening { get; }
            public Risk(GameCard card, int chanceOfHappening)
            {
                this.Card = card;
                this.ChanceOfHappening = chanceOfHappening;
            }
        }

        private GameContext context;

        private string Description { get; set; }

        private List<GameCard> Resources { get; set; }

        private List<Risk> FightRisks { get; set; }

        private List<Risk> GrabAndRunRisks { get; set; }

        private List<Risk> RunRisks { get; set; }

        private int _selectedAction = 0;

        public SituationState(GameContext context)
        {
            this.context = context;
            GenerateSituation(context);
        }


        public override void Update(GameContext context, GameTime gameTime, Input input)
        {
            if (input.WasKeyPressed(Keys.Up)) _selectedAction -= 1;
            if (input.WasKeyPressed(Keys.Down)) _selectedAction += 1;
            if (_selectedAction < 0) _selectedAction = 2;
            if (_selectedAction > 2) _selectedAction = 0;
            if (input.WasKeyPressed(Keys.Enter))
            {
                var outcome = new SituationOutcome();
                outcome.Action = _selectedAction;
                if (_selectedAction == 0)
                {
                    outcome.Description = "YOU CHOSE TO FIGHT..";
                    foreach (var risk in FightRisks)
                    {
                        var check = context.Random.Next(0, 100);
                        if (check < risk.ChanceOfHappening)
                            outcome.Consequences.Add(risk.Card);
                    }
                    outcome.GainedResources.AddRange(this.Resources);
                }
                else if (_selectedAction == 1)
                {
                    outcome.Description = "YOU CHOSE TO LOOT SOME RESOURCES AND RUN...";
                    foreach (var risk in GrabAndRunRisks)
                    {
                        var check = context.Random.Next(0, 100);
                        if (check < risk.ChanceOfHappening)
                            outcome.Consequences.Add(risk.Card);
                    }
                    var gain = Resources.OrderBy(_ => context.Random.Next(100)).First();
                    outcome.GainedResources.Add(gain);
                    Resources.Remove(gain);
                    int i = 30;
                    foreach (var remainingResource in Resources)
                    {
                        var check = context.Random.Next(100);
                        if (check < (i - context.Group.MemberCount))
                            outcome.GainedResources.Add(remainingResource);
                        i -= 5;
                    }
                }
                else
                {
                    outcome.Description = "YOU CHOSE TO RUN...";
                    foreach (var risk in RunRisks)
                    {
                        var check = context.Random.Next(0, 100);
                        if (check < risk.ChanceOfHappening)
                            outcome.Consequences.Add(risk.Card);
                    }
                }
                Manager.State = new SituationOutcomeState(outcome, context);
            }
        }


        private void GenerateSituation(GameContext context)
        {
            if (context.Group.GameBoardTile.Type == GameTileType.Rural)
                Description = "As you travel through rural Kasprzak you scout a small group of ";
            else if (context.Group.GameBoardTile.Type == GameTileType.Suburban)
                Description = "As you travel through suburban Kasprzak you scout a medium group of ";
            else if (context.Group.GameBoardTile.Type == GameTileType.Urban)
                Description = "As you travel through urban Kasprzak you scout a large group of ";

            int grouptype = context.Random.Next(3);
            switch (grouptype)
            {
                case 0: Description += "zombies. "; break;
                case 1: Description += "bandits. "; break;
                case 2: Description += "thugs. "; break;
            }
            Description += "You can try fighting them, you can try looting and running away, or you can just run away...";
            Description = Description.ToUpper();

            Resources = new List<GameCard>();
            FightRisks = new List<Risk>();
            GrabAndRunRisks = new List<Risk>();
            RunRisks = new List<Risk>();

            if (context.Group.GameBoardTile.Type == GameTileType.Rural)
                AddResourcesInRange(2, 4, 10, context);
            else if (context.Group.GameBoardTile.Type == GameTileType.Suburban)
                AddResourcesInRange(3, 6, 30, context);
            else if (context.Group.GameBoardTile.Type == GameTileType.Urban)
                AddResourcesInRange(4, 8, 50, context);
        }

        private void AddResourcesInRange(int low, int high, int fightDeathAlways, GameContext context)
        {
            var cards = context.Random.Next(low, high);
            Resources.AddRange(GenerateResources(cards, context.Random));

            cards = context.Random.Next(low, high);
            FightRisks.Add(new Risk(new DeathCard(), fightDeathAlways));
            FightRisks.AddRange(GenerateFightRisks(cards, context.Group.MemberCount, context.Random));

            cards = context.Random.Next(low - 1, high - 1);
            RunRisks.AddRange(GenerateRunRisks(cards, context.Group.MemberCount, context.Random));

            GrabAndRunRisks.AddRange(RunRisks);
            GrabAndRunRisks.AddRange(GenerateGrabAndRunRisks(1, context.Group.MemberCount, context.Random));
        }

        private IEnumerable<GameCard> GenerateResources(int count, Random rand)
        {
            for (int i = 0; i < count; i++)
            {
                var value = rand.Next(0, 100);
                if (value < 40) yield return new FoodAndWaterResourceCard();
                else if (value < 50) yield return new BuildingMaterialResourceCard();
                else if (value < 60) yield return new MedicalSuppliesResourceCard();
                else if (value < 80) yield return new AmmoResourceCard();
                else if (value < 90) yield return new WeaponResourceCard();
                else if (value < 100) yield return new SurvivorCard();
            }
        }

        private IEnumerable<Risk> GenerateFightRisks(int count, int groupSize, Random rand)
        {
            for (int i = 0; i < count; i++)
            {
                int chanceTop = 80;
                chanceTop -= (rand.Next(1, 5) * groupSize);
                chanceTop = MathHelper.Clamp(chanceTop, 30, 80);
                var chance = rand.Next(20, chanceTop);

                var value = rand.Next(0, 100);
                if (value < 40) yield return new Risk(new DamageCard(), chance);
                else if (value < 80) yield return new Risk(new InjuryCard(), chance);
                else if (value < 100) yield return new Risk(new DeathCard(), chance);
            }
        }

        private IEnumerable<Risk> GenerateGrabAndRunRisks(int count, int groupSize, Random rand)
        {
            for (int i = 0; i < count; i++)
            {
                int chanceTop = 50;
                chanceTop += (rand.Next(1, 5) * groupSize);
                chanceTop = MathHelper.Clamp(chanceTop, 25, 80);
                var chance = rand.Next(15, chanceTop);

                var value = rand.Next(0, 100);
                if (value < 40) yield return new Risk(new DamageCard(), chance);
                else if (value < 80) yield return new Risk(new InjuryCard(), chance);
                else if (value < 100) yield return new Risk(new DeathCard(), chance);
            }
        }

        private IEnumerable<Risk> GenerateRunRisks(int count, int groupSize, Random rand)
        {
            for (int i = 0; i < count; i++)
            {
                int chanceTop = 30;
                chanceTop += (rand.Next(1, 5) * groupSize);
                chanceTop = MathHelper.Clamp(chanceTop, 20, 80);
                var chance = rand.Next(10, chanceTop);

                var value = rand.Next(0, 100);
                if (value < 30) yield return new Risk(new DamageCard(), chance);
                else if (value < 90) yield return new Risk(new InjuryCard(), chance);
                else if (value < 100) yield return new Risk(new DeathCard(), chance);
            }
        }

        public override void Draw(GameContext context, SpriteBatch spriteBatch)
        {
            base.Draw(context, spriteBatch);
            spriteBatch.Begin();

            int ypos = 16;
            int inc = 120;
            int xOff = 96;

            new TextSprite("ACTION PHASE:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            new TextSprite(Description).Draw(spriteBatch, context.Content, new Vector2(12, ypos + 32));

            ypos += inc;
            new TextSprite("SCOUTED RESOURCES:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));

            int i = 0;
            foreach (var card in Resources)
                card.Draw(spriteBatch, context.Content, new Vector2(12 + (i++ * 64), ypos + 32));

            ypos += inc;
            new TextSprite("FIGHT").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 0 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 0)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }

            new TextSprite("RISKS:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            i = 0;
            foreach (var risk in FightRisks)
            {
                risk.Card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
                new TextSprite(risk.ChanceOfHappening.ToString()).Draw(spriteBatch, context.Content, new Vector2(xOff + 12 + (i * 64), ypos + 88));
                i++;
            }

            ypos += inc;
            new TextSprite("LOOT").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 1 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 1)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }

            new TextSprite("RISKS:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            i = 0;
            foreach (var risk in GrabAndRunRisks)
            {
                risk.Card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
                new TextSprite(risk.ChanceOfHappening.ToString()).Draw(spriteBatch, context.Content, new Vector2(xOff + 12 + (i * 64), ypos + 88));
                i++;
            }

            ypos += inc;
            new TextSprite("RUN").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 2 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 2)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }

            new TextSprite("RISKS:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            i = 0;
            foreach (var risk in RunRisks)
            {
                risk.Card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
                new TextSprite(risk.ChanceOfHappening.ToString()).Draw(spriteBatch, context.Content, new Vector2(xOff + 12 + (i * 64), ypos + 88));
                i++;
            }

            spriteBatch.End();
        }
    }
}
