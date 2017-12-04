using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Apokalipsa
{
    enum GroupBearing
    {
        East = 0,
        NorthEast = -55,
        NorthWest = -125,
        West = 180,
        SouthEast = 55,
        SouthWest = 125,
    }

    class Group : IGamePiece
    {
        private GameContent _content;

        private GameTile _tile;

        public int MemberCount => SurvivorCards.Count();

        public int FighterCount => SurvivorCards.Where(sc => sc.Fighter).Count();

        public bool AtFullWellness => Wellness == 12;

        public int Wellness { get; set; } = 12;

        public List<GameCard> ResourceCards { get; set; } = new List<GameCard>();

        public List<SurvivorCard> SurvivorCards { get; set; } = new List<SurvivorCard>();

        public List<SurvivorCard> SettledSurvivorCards { get; set; } = new List<SurvivorCard>();

        public GameTile GameBoardTile
        {
            get => _tile;
            set
            {
                _tile = value;
                if (_tile != null) FixBearing(-1);
            }
        }

        public GroupBearing Bearing { get; set; }

        internal void Desert()
        {
            var card = this.SurvivorCards.OrderBy(sc => sc.Fighter).FirstOrDefault();
            if (card == null) return;
            this.SurvivorCards.Remove(card);
        }

        public Group(GameContent content)
        {
            _content = content;
            this.SurvivorCards.Add(new SurvivorCard(true));
            this.SurvivorCards.Add(new SurvivorCard());
            this.SurvivorCards.Add(new SurvivorCard());
        }

        internal bool TryUpWellness1(GameCard[] neededCards)
        {
            var listCards = new List<GameCard>();
            var survivorCards = new List<SurvivorCard>();
            bool haveResources = AquireCards(neededCards, listCards, survivorCards);
            if (!haveResources)
                return false;
            this.Wellness += 1;
            this.Wellness = MathHelper.Clamp(this.Wellness, 0, 12);
            return true;
        }

        private bool AquireCards(GameCard[] neededCards, List<GameCard> listCards, List<SurvivorCard> survivorCards)
        {
            var haveResources = true;
            foreach (var card in neededCards)
            {
                if (card is SurvivorCard sc)
                {
                    var next = sc.Fighter ? 
                        SurvivorCards.Where(c => c.Fighter).FirstOrDefault() :
                        SurvivorCards.Where(c => !c.Fighter).FirstOrDefault();
                    if (next == null)
                    {
                        ResourceCards.AddRange(listCards);
                        SurvivorCards.AddRange(survivorCards);
                        haveResources = false;
                        break;
                    }
                    SurvivorCards.Remove(next);
                    survivorCards.Add(sc);
                }
                else
                {
                    var next = ResourceCards.Where(c => c.GetType() == card.GetType()).FirstOrDefault();
                    if (next == null)
                    {
                        ResourceCards.AddRange(listCards);
                        SurvivorCards.AddRange(survivorCards);
                        haveResources = false;
                        break;
                    }
                    ResourceCards.Remove(next);
                    listCards.Add(next);
                }
            }

            return haveResources;
        }

        internal bool TryUpWellness3(GameCard[] neededCards)
        {
            var listCards = new List<GameCard>();
            var survivorCards = new List<SurvivorCard>();
            bool haveResources = AquireCards(neededCards, listCards, survivorCards);
            if (!haveResources)
                return false;
            this.Wellness += 3;
            this.Wellness = MathHelper.Clamp(this.Wellness, 0, 12);
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var tile = GameBoardTile;
            var drawAt = tile.CalculateDrawAt(_content);

            spriteBatch.Draw(
                _content.GroupIcon,
                drawAt,
                null,
                GameColors.GroupColor,
                0f,
                _content.GroupIcon.GetOrigin(),
                1f,
                SpriteEffects.None,
                1f);

            spriteBatch.Draw(
                _content.Direction,
                drawAt,
                null,
                GameColors.GroupColor,
                MathHelper.ToRadians((int)Bearing),
                _content.Direction.GetOrigin() * new Vector2(-1f, 1f),
                1f,
                SpriteEffects.None,
                1f);
        }

        internal bool TryUpgradeFighter(GameCard[] neededCards)
        {
            var listCards = new List<GameCard>();
            var survivorCards = new List<SurvivorCard>();
            bool haveResources = AquireCards(neededCards, listCards, survivorCards);
            if (!haveResources)
                return false;
            survivorCards.FirstOrDefault().Fighter = true;
            SurvivorCards.AddRange(survivorCards);
            return true;
        }

        internal bool TrySettle(GameCard[] neededCards)
        {
            var listCards = new List<GameCard>();
            var survivorCards = new List<SurvivorCard>();
            bool haveResources = AquireCards(neededCards, listCards, survivorCards);
            if (!haveResources)
                return false;

            if (!SurvivorCards.Any()) // maybe if on last settlemnt
            {
                ResourceCards.AddRange(listCards);
                SurvivorCards.AddRange(survivorCards);
                return false;
            }

            SettledSurvivorCards.AddRange(survivorCards);
            this.GameBoardTile.Settle();
            return true;
        }

        internal void Attrition()
        {
            this.Wellness -= 1;
            if (this.Wellness < 0) this.Wellness = 0;
        }

        private static GroupBearing[] _bearings { get; } =
            new[] {
                GroupBearing.East,
                GroupBearing.NorthEast,
                GroupBearing.NorthWest,
                GroupBearing.West,
                GroupBearing.SouthWest,
                GroupBearing.SouthEast,
            };

        internal GameCard Damage(GameContext context)
        {
            var cardToLose = ResourceCards.OrderBy(_ => context.Random.Next(100)).FirstOrDefault();
            if (cardToLose != null)
                ResourceCards.Remove(cardToLose);
            return cardToLose;
        }

        internal void Injury(GameContext context)
        {
            this.Wellness -= 1;
            if (this.Wellness < 0) this.Wellness = 0;
        }

        internal void Death(GameContext context)
        {
            var value = this.SurvivorCards.OrderBy(s => s.Fighter).FirstOrDefault();
            if (value == null) return;
            this.SurvivorCards.Remove(value);
        }

        public void ExecuteMoveToTargetGameTile()
        {
            var target = GetTargetGameTile();
            GameBoardTile.PickupPiece(this);
            target.PlacePiece(this);
        }

        public void ChangeBearingLeft() => ChangeBearing(1);

        public void ChangeBearingRight() => ChangeBearing(-1);
        
        private void ChangeBearing(int direction)
        {
            var currentIdx = _bearings.ToList().IndexOf(Bearing);
            currentIdx += direction;
            if (currentIdx < 0) currentIdx = _bearings.Length - 1;
            if (currentIdx >= _bearings.Length) currentIdx = 0;
            Bearing = _bearings[currentIdx];
            FixBearing(direction);
        }

        private void FixBearing(int direction)
        {
            if (GetTargetGameTile() == null) ChangeBearing(direction);
        }

        private GameTile GetTargetGameTile()
        {
            var tile = this.GameBoardTile;
            var point = Point.Zero;
            if (Bearing == GroupBearing.West) point = tile.Position + new Point(-1, 0);
            if (Bearing == GroupBearing.NorthWest) point = tile.Position + new Point(-1, -1);
            if (Bearing == GroupBearing.NorthEast) point = tile.Position + new Point(0, -1);
            if (Bearing == GroupBearing.East) point = tile.Position + new Point(1, 0);
            if (Bearing == GroupBearing.SouthEast) point = tile.Position + new Point(1, 1);
            if (Bearing == GroupBearing.SouthWest) point = tile.Position + new Point(0, 1);
            return tile.GameBoard.GetGameTileAt(point);
        }
    }
}
