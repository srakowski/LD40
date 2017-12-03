using Microsoft.Xna.Framework.Input;

namespace SRakowski.LD40.Engine
{
    class InputManager
    {
        public MouseState PrevMouseState { get; private set; }
        public MouseState MouseState { get; private set; }

        public InputManager()
        {
            PrevMouseState = new MouseState();
            MouseState = new MouseState();
        }

        internal void Update()
        {
            PrevMouseState = MouseState;
            MouseState = Mouse.GetState();
        }
    }
}
