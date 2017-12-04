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
            if (input.WasKeyPressed(Keys.Enter))
            {
                if (!_hasAcked)
                {
                    if (context.Group.Wellness == 0)
                        context.Group.Desert();

                    if (outcome.Action != 0)
                        context.Group.Attrition();

                    foreach (var card in outcome.GainedResources)
                    {
                        if (card is SurvivorCard scard) context.Group.SurvivorCards.Add(scard);
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
                    if (outcome.Action != 0) Manager.State = new MoveState();
                    else Manager.State = new RestState();
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
            else
            {
                new TextSprite("NO RESOURCE CARDS WERE GAINED").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                ypos += 32;
            }

            if (outcome.Consequences.Any())
            {
                new TextSprite("CONSEQUENCES:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                i = 0;
                foreach (var card in outcome.Consequences)
                    card.Draw(spriteBatch, context.Content, new Vector2(12 + (i++ * 64), ypos + 32));
                ypos += inc;
            }
            else
            {
                new TextSprite("NO HARM HAS BEFELL YOUR GROUP").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                ypos += 32;
            }

            if (outcome.Action != 0)
            {
                new TextSprite("WELLNESS POINTS DECREASED BY 1 BECAUSE YOU CANNOT STOP TO REST. " +
                    "YOU MUST FIGHT TO REST.").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                ypos += 64;
            }

            if (context.Group.Wellness == 0)
            {
                new TextSprite("WELLNESS IS AT 0. A MEMBER OF YOUR GROUP HAS DESERTED")
                    .Draw(spriteBatch, context.Content, new Vector2(12, ypos));
                ypos += 64;
            }

            new TextSprite("PRESS ENTER TO ACKNOWLEDGE...").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            ypos += 32;

            if (_hasAcked)
            {
                new TextSprite("PRESS ENTER TO CONTINUE...").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            }

            spriteBatch.End();
        }
    }
}
