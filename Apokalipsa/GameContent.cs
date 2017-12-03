using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Apokalipsa
{
    class GameContent
    {
        public Texture2D HexTile { get; set; }
        public Texture2D UrbanIcon { get; set; }
        public Texture2D SuburbanIcon { get; set; }
        public Texture2D RuralIcon { get; set; }
        public Texture2D GroupIcon { get; set; }
        public Texture2D SettlementIcon { get; internal set; }
        public Texture2D Direction { get; internal set; }
        public Texture2D Cards { get; set; }
        public Texture2D Font { get; set; }
        public Texture2D Well { get; private set; }
        public Texture2D GroupMemberIcon { get; private set; }
        public Texture2D WellnessIcon { get; private set; }

        public void Load(ContentManager content)
        {
            var _content = this;
            _content.GroupIcon = content.Load<Texture2D>("group");
            _content.HexTile = content.Load<Texture2D>("hextile");
            _content.RuralIcon = content.Load<Texture2D>("rural");
            _content.UrbanIcon = content.Load<Texture2D>("urban");
            _content.SuburbanIcon = content.Load<Texture2D>("suburban");
            _content.SettlementIcon = content.Load<Texture2D>("settlement");
            _content.Direction = content.Load<Texture2D>("direction");
            _content.Cards = content.Load<Texture2D>("cards");
            _content.Font = content.Load<Texture2D>("font");
            _content.Well = content.Load<Texture2D>("well");
            _content.GroupMemberIcon = content.Load<Texture2D>("groupmember");
            _content.WellnessIcon = content.Load<Texture2D>("wellness");
        }
    }
}
