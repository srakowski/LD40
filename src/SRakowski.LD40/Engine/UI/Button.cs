using Microsoft.Xna.Framework.Input;
using System;

namespace SRakowski.LD40.Engine.UI
{
    class Button : FormElement
    {
        Renderer _renderer;

        Action _onClick;

        public Button(Form form, Renderer renderer)
            : base(form)
        {
            _renderer = renderer;
            AddComponent(renderer);
            AddComponent(new RelayBehavior(Behavior));
        }

        internal Entity OnClick(Action onClick)
        {
            _onClick = onClick;
            return this;
        }

        private void Behavior(Behavior sender)
        {
            if (_renderer.Bounds.Contains(sender.Input.MouseState.Position) &&
                sender.Input.PrevMouseState.LeftButton == ButtonState.Pressed &&
                sender.Input.MouseState.LeftButton == ButtonState.Released)
                _onClick?.Invoke();
        }
    }
}
