using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Engine
{
    class Camera : Component
    {
        private Entity _entity;

        private EngineContext _context;

        internal Matrix TransformationMatrix =>
            Matrix.Identity *
            Matrix.CreateRotationZ(_entity.Transform.Rotation) *
            Matrix.CreateScale(_entity.Transform.Scale) *
            Matrix.CreateTranslation(-_entity.Transform.Position.X, -_entity.Transform.Position.Y, 0f) *
            Matrix.CreateTranslation(
                (_context.GraphicsDevice.Viewport.Width * 0.5f),
                (_context.GraphicsDevice.Viewport.Height * 0.5f),
                0f);

        public Vector2 ToWorldCoords(Vector2 coords) =>
            Vector2.Transform(coords, Matrix.Invert(this.TransformationMatrix));

        internal override void Activate(EngineContext context, Entity entity)
        {
            _entity = entity;
            _context = context;
        }
    }
}
