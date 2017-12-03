using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SRakowski.LD40.Engine
{
    class StringRenderer : Renderer
    {
        private Entity _entity;

        private string _fontKey;

        public SpriteFont SpriteFont { get; set; }

        public string Text { get; set; }

        public Color Color { get; set; }

        private Vector2? _origin;

        public Vector2 Origin
        {
            get => _origin ?? Vector2.Zero;
            set => _origin = value;
        }

        public SpriteEffects SpriteEffects { get; set; }

        public StringRenderer(
            string fontKey,
            string text,
            Color? color = null,
            Vector2? origin = null,
            SpriteEffects spriteEffects = SpriteEffects.None)
        {
            _fontKey = fontKey;
            Text = text;
            Color = color ?? Color.White;
            _origin = origin;
            SpriteEffects = spriteEffects;
        }

        internal override void Activate(EngineContext context, Entity entity)
        {
            SpriteFont = context.Content[_fontKey] as SpriteFont;
            _entity = entity;
        }

        internal override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                SpriteFont,
                Text,
                _entity.Transform.Position,
                Color,
                _entity.Transform.Rotation,
                Origin,
                _entity.Transform.Scale,
                SpriteEffects,
                1f);
        }

        internal override Rectangle Bounds
        {
            get
            {
                var pos = (_entity.Transform.Position - Origin).ToPoint();
                var measure = (SpriteFont?.MeasureString(Text)) ?? Vector2.Zero;
                return new Rectangle(pos.X, pos.Y, (int)measure.X, (int)measure.Y);
            }
        }
    }
}
