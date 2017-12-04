using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apokalipsa
{
    class RestState : GameState
    {
        private int _selectedAction = 0;

        private string Message { get; set; } = "";

        public override void Update(GameContext context, GameTime gameTime, Input input)
        {
            if (input.WasKeyPressed(Keys.Up))
            {
                _selectedAction -= 1;
                Message = "";
            }
            if (input.WasKeyPressed(Keys.Down)) 
            {
                _selectedAction += 1;
                Message = "";
            }
            if (_selectedAction < 0) _selectedAction = 4;
            if (_selectedAction > 4) _selectedAction = 0;
            if (input.WasKeyPressed(Keys.Enter))
            {
                if (_selectedAction == 0)
                {
                    this.Manager.State = new MoveState();
                }
                else if (_selectedAction == 1)
                {
                    if (!context.Group.TryUpWellness1(CostFor1Wellness())) Message = "NOT ENOUGH RESOURCES TO PERFORM THIS ACTION!";
                }
                else if (_selectedAction == 2)
                {
                    if (!context.Group.TryUpWellness3(CostFor3Wellness())) Message = "NOT ENOUGH RESOURCES TO PERFORM THIS ACTION!";
                }
                else if (_selectedAction == 3)
                {
                    if (!context.Group.TryUpgradeFighter(FighterUpgradeCost())) Message = "NOT ENOUGH RESOURCES TO PERFORM THIS ACTION!";
                }
                else if (_selectedAction == 4)
                {
                    if (!context.Group.TrySettle(CostToSettle(context))) Message = "NOT ENOUGH RESOURCES TO PERFORM THIS ACTION!";
                }
            }
        }

        public override void Draw(GameContext context, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            int ypos = 16;
            int inc = 120;
            int xOff = 96;

            new TextSprite("REST AND REGROUP PHASE:").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            new TextSprite("IN THIS PHASE YOU MAY SPEND RESOURCE CARDS").Draw(spriteBatch, context.Content, new Vector2(12, ypos + 32));

            ypos += 76;
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 0 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 0)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }
            new TextSprite("PACK UP AND HEAD OUT").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos + 48));


            ypos += inc;
            //new TextSprite("").Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 1 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 1)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }
            new TextSprite("RESTORE 1 WELLNESS, COST:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            int i = 0;
            foreach (var card in CostFor1Wellness())
            {
                card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
            }

            ypos += inc;
            //new TextSprite().Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 2 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 2)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }
            new TextSprite("RESTORE 3 WELLNESS, COST:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            i = 0;
            foreach (var card in CostFor3Wellness())
            {
                card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
            }

            ypos += inc;
            //new TextSprite().Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 3 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 3)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }
            new TextSprite("UPGRADE GROUP MEMBER TO FIGHTER, COST:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            i = 0;
            foreach (var card in FighterUpgradeCost())
            {
                card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
                i++;
            }


            ypos += inc;
            //new TextSprite().Draw(spriteBatch, context.Content, new Vector2(12, ypos));
            spriteBatch.Draw(context.Content.HexTile, new Vector2(12, ypos + 24), _selectedAction == 4 ? GameColors.TextColor : GameColors.TileColor);
            if (_selectedAction == 4)
            {
                spriteBatch.Draw(context.Content.GroupIcon, new Vector2(28, ypos + 40), GameColors.GroupColor);
            }
            new TextSprite("CREATE A SETTLEMENT ON THIS TILE, COST:").Draw(spriteBatch, context.Content, new Vector2(xOff, ypos));
            i = 0;
            GameCard[] costToSettle = CostToSettle(context);

            foreach (var card in costToSettle)
            {
                card.Draw(spriteBatch, context.Content, new Vector2(xOff + (i * 64), ypos + 24));
                i++;
                if (i >= 8)
                {
                    i = 0;
                    ypos += 76;
                }
            }

            spriteBatch.End();
        }

        private static FoodAndWaterResourceCard[] CostFor1Wellness()
        {
            return new[] { new FoodAndWaterResourceCard() };
        }

        private static MedicalSuppliesResourceCard[] CostFor3Wellness()
        {
            return new[] { new MedicalSuppliesResourceCard() };
        }

        private static GameCard[] FighterUpgradeCost()
        {
            return new GameCard[] { new WeaponResourceCard(), new AmmoResourceCard(), new AmmoResourceCard(), new AmmoResourceCard(), new SurvivorCard() };
        }

        private static GameCard[] CostToSettle(GameContext context)
        {
            return context.Group.GameBoardTile.Type == GameTileType.Rural ? new GameCard[] {
                    new BuildingMaterialResourceCard(), new FoodAndWaterResourceCard(),
                    new SurvivorCard(true), new SurvivorCard(), new SurvivorCard() } :
                context.Group.GameBoardTile.Type == GameTileType.Suburban ? new GameCard[] {
                    new BuildingMaterialResourceCard(), new BuildingMaterialResourceCard(), new FoodAndWaterResourceCard(),new FoodAndWaterResourceCard(),
                    new SurvivorCard(true), new SurvivorCard(true), new SurvivorCard(), new SurvivorCard(), new SurvivorCard(),
                } :
                context.Group.GameBoardTile.Type == GameTileType.Urban ? new GameCard[]
                {
                    new BuildingMaterialResourceCard(), new BuildingMaterialResourceCard(),
                    new BuildingMaterialResourceCard(), new FoodAndWaterResourceCard(),new FoodAndWaterResourceCard(),new FoodAndWaterResourceCard(),
                    new SurvivorCard(true), new SurvivorCard(true), new SurvivorCard(true), new SurvivorCard(), new SurvivorCard(),
                    new SurvivorCard(), new SurvivorCard(), new SurvivorCard(), new SurvivorCard(),
                } :
                throw new Exception();
        }
    }
}
