using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Apokalipsa
{
    class SituationOutcome
    {
        public int Action { get; set; }
        public List<GameCard> Consequences { get; set; } = new List<GameCard>();
        public List<GameCard> GainedResources { get; internal set; } = new List<GameCard>();
        public string Description { get; internal set; }
    }

    class SituationOutcomeState : GameState
    {
        private SituationOutcome outcome;
        private GameContext context;
        bool _hasAcked = false;

        public SituationOutcomeState(SituationOutcome outcome, GameContext context)
        {
            this.outcome = outcome;
            this.context = context;
        }

        public override void Update(GameContext context, GameTime gameTime, Input input)
        {
            //if (input.WasKeyPressed(Keys.Left)) context.Group.ChangeBearingLeft();
            //if (input.WasKeyPressed(Keys.Right)) context.Group.ChangeBearingRight();
            if (input.WasKeyPressed(Keys.Enter))
            {
                if (!_hasAcked)
                {
                    foreach (var card in outcome.GainedResources)
                    {
                        if (card is SurvivorCard) context.Group.MemberCount++;
                        else context.Group.ResourceCards.Add(card);
                    }
                    foreach (var card in outcome.Consequences)
                    {
                        if (card is DeathCard) context.Group.Death(context);
                        if (card is InjuryCard) context.Group.Injury(context);
                        if (card is DamageCard)
                        {
                            var cardToLose = context.Group.Damage(context);
                            if (cardToLose == null)
                                context.Group.Injury(context);
                        }
                    }
                    _hasAcked = true;
                }
                else
                {

                }
            }
        }

        public override void Draw(GameContext context, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            int inc = 120;
            int ypos = 16;
            new TextSprite(outcome.Description).Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            ypos += 32;

            int i = 0;
            if (outcome.GainedResources.Any())
            {
                new TextSprite("GAINED RESOURCES:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                foreach (var card in outcome.GainedResources)
                    card.Draw(spriteBatch, context.Content, new Vector2(12 + (i++ * 64), ypos + 32));
                ypos += inc;
            }
            if (outcome.Consequences.Any())
            {
                new TextSprite("CONSEQUENCES:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                i = 0;
                foreach (var card in outcome.Consequences)
                    card.Draw(spriteBatch, context.Content, new Vector2(12 + (i++ * 64), ypos + 32));
                ypos += inc;
            }
            if (!outcome.Consequences.Any() && !outcome.GainedResources.Any())
            {
                new TextSprite("YOU GAINED NOTHING, BUT YOU ALSO LOST NOTHING").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                ypos += 32;
            }

            new TextSprite("PRESS ENTER TO ACKNOWLEDGE...").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            ypos += 32;

            if (_hasAcked)
            {
                new TextSprite("PRESS ENTER TO CONTINUE").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            }

            //int ypos = 16;
            //int inc = 120;
            //int xOff = 96;

            //new TextSprite("THREAT DESCRIPTION:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            //new TextSprite(Description).Draw(spriteBatch, context.Content, new Vector2(12, ypos + 32));

            //ypos += inc;
            //new TextSprite("SCOUTED RESOURCES:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));

            //int i = 0;
            //foreach (var card in Resources)
            //    card.Draw(spriteBatch, context.Content, new Vector2(12 + (i++ * 64), ypos + 32));

            //ypos += inc;
            //new TextSprite("FIGHT").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            //spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 0 ? GameColors.TextColor : GameColors.TileColor);
            //if (_selectedAction == 0)
            //{
            //    spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            //}

            //new TextSprite("RISKS:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            //i = 0;
            //foreach (var risk in FightRisks)
            //{
            //    risk.Card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
            //    new TextSprite(risk.ChanceOfHappening.ToString()).Draw(spriteBatch, context.Content, new Vector2(xOff + 12 + (i * 64), ypos + 88));
            //    i++;
            //}

            //ypos += inc;
            //new TextSprite("LOOT").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            //spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 1 ? GameColors.TextColor : GameColors.TileColor);
            //if (_selectedAction == 1)
            //{
            //    spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            //}

            //new TextSprite("RISKS:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            //i = 0;
            //foreach (var risk in GrabAndRunRisks)
            //{
            //    risk.Card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
            //    new TextSprite(risk.ChanceOfHappening.ToString()).Draw(spriteBatch, context.Content, new Vector2(xOff + 12 + (i * 64), ypos + 88));
            //    i++;
            //}

            //ypos += inc;
            //new TextSprite("RUN").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            //spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 2 ? GameColors.TextColor : GameColors.TileColor);
            //if (_selectedAction == 2)
            //{
            //    spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            //}

            //new TextSprite("RISKS:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            //i = 0;
            //foreach (var risk in RunRisks)
            //{
            //    risk.Card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
            //    new TextSprite(risk.ChanceOfHappening.ToString()).Draw(spriteBatch, context.Content, new Vector2(xOff + 12 + (i * 64), ypos + 88));
            //    i++;
            //}

            spriteBatch.End();
        }
    }
}
