using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SRakowski.LD40.Engine
{
    class SpriteRenderer : Renderer
    {
        private string _textureKey;

        private Entity _entity;

        public Texture2D Texture { get; set; }

        public Rectangle? SourceRectangle { get; set; }

        public Color Color { get; set; }

        private Vector2? _origin;

        public Vector2 Origin
        {
            get => _origin ?? SourceRectangle?.Center.ToVector2() ?? new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f);
            set => _origin = value;
        }

        public SpriteEffects SpriteEffects { get; set; }

        public SpriteRenderer(
            string textureKey,
            Rectangle? sourceRectangle = null,
            Color? color = null,
            Vector2? origin = null,
            SpriteEffects spriteEffects = SpriteEffects.None)
        {
            _textureKey = textureKey;
            SourceRectangle = sourceRectangle;
            Color = color ?? Color.White;
            _origin = origin;
            SpriteEffects = spriteEffects;
        }

        internal override void Activate(EngineContext context, Entity entity)
        {
            _entity = entity;
            Texture = context.Content[_textureKey] as Texture2D;
        }

        internal override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: Texture,
                position: _entity.Transform.Position,
                sourceRectangle: SourceRectangle,
                color: Color,
                rotation: _entity.Transform.Rotation,
                origin: Origin,
                scale: _entity.Transform.Scale,
                effects: SpriteEffects,
                layerDepth: 1f);
        }

        internal override Rectangle Bounds
        {
            get
            {
                var pos = (_entity.Transform.Position - Origin).ToPoint();
                return new Rectangle(pos.X, pos.Y,
                    SourceRectangle?.Width ?? Texture?.Width ?? 0,
                    SourceRectangle?.Bottom ?? Texture?.Height ?? 0
                );
            }
        }
    }
}
