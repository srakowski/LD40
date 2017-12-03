using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Engine
{
    class Transform : Component
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; } = 0f;

        public float Scale { get; set; } = 1f;
    }

    static partial class EntityExtensions
    {
        public static T TranslateTo<T>(this T self, float x, float y) where T : Entity => self.TranslateTo(new Vector2(x, y));

        public static T TranslateTo<T>(this T self, Vector2 position) where T : Entity
        {
            self.Transform.Position = position;
            return self;
        }
    }
}
