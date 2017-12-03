using SRakowski.LD40.Engine;
using SRakowski.LD40.Gameplay;
using static SRakowski.LD40.Entities.MapHelper;

namespace SRakowski.LD40.Entities
{
    class GroupEntity : Entity
    {
        private Group _group;

        public GroupEntity(Group group)
        {
            _group = group;
            this.TranslateTo(MapPointToScreenPos(_group.MapLocation));
            AddComponent(new RelayBehavior(UpdateGroupPos));
            AddComponent(new SpriteRenderer("Sprites/groupbase"));
        }

        private void UpdateGroupPos(Behavior sender) =>
            this.TranslateTo(MapPointToScreenPos(_group.MapLocation));
    }
}
