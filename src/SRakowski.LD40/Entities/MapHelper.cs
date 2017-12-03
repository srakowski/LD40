using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Entities
{
    static class MapHelper
    {
        public static Vector2 MapPointToScreenPos(Point mapPoint)
        {
            return (mapPoint.ToVector2() * new Vector2(128f, 128f));
        }
    }
}
