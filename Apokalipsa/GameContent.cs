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
    }
}
