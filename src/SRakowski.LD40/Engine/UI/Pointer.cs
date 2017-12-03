using Microsoft.Xna.Framework;
using System;

namespace SRakowski.LD40.Engine.UI
{
    class Pointer : FormElement
    {
        SpriteRenderer _spriteRenderer;

        public Pointer(Form form,
            SpriteRenderer spriteRenderer)
            : base(form)
        {
            _spriteRenderer = spriteRenderer;
            _spriteRenderer.Origin = Vector2.Zero;
            AddComponent(spriteRenderer);
            AddComponent(new RelayBehavior(OnUpdate));
        }

        private void OnUpdate(Behavior sender)
        {
            this.TranslateTo(sender.Input.MouseState.Position.ToVector2());
        }
    }
}
