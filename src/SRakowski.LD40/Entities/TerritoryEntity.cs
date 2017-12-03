using SRakowski.LD40.Engine;
using SRakowski.LD40.Gameplay;
using static SRakowski.LD40.Entities.MapHelper;

namespace SRakowski.LD40.Entities
{
    class TerritoryEntity : Entity
    {
        private Territory _territory;

        public TerritoryEntity(Territory territory)
        {
            _territory = territory;
            (this).TranslateTo(MapPointToScreenPos(territory.MapPoint));
            AddComponent(new SpriteRenderer("Sprites/territorybase"));
        }
    }
}
