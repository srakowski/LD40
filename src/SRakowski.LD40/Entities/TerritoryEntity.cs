using Microsoft.Xna.Framework;
using SRakowski.LD40.Engine;
using SRakowski.LD40.Gameplay;

namespace SRakowski.LD40.Entities
{
    class TerritoryEntity : Entity
    {
        private Territory _territory;

        public TerritoryEntity(Territory territory)
        {
            _territory = territory;
            this.TranslateTo((territory.MapPoint.ToVector2() * new Vector2(128f, 128f)));
            AddComponent(new SpriteRenderer("Sprites/territorybase"));
        }
    }
}
