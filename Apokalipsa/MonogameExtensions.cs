using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apokalipsa
{
    static class MonogameExtensions
    {
        public static Vector2 GetOrigin(this Texture2D self) => new Vector2(self.Width * 0.5f, self.Height * 0.5f);
    }
}