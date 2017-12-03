using Microsoft.Xna.Framework.Input;

namespace Apokalipsa
{
    class Input
    {
        public MouseState PrevMouseState { get; private set; }
        public MouseState MouseState { get; private set; }

        public KeyboardState PrevKeyboardState { get; private set; }
        public KeyboardState KeyboardState { get; private set; }

        public Input()
        {
            PrevMouseState = new MouseState();
            MouseState = new MouseState();
            PrevKeyboardState = new KeyboardState();
            KeyboardState = new KeyboardState();
        }

        internal void Update()
        {
            PrevMouseState = MouseState;
            MouseState = Mouse.GetState();
            PrevKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
        }

        internal bool WasKeyPressed(Keys key) => PrevKeyboardState.IsKeyUp(key) && KeyboardState.IsKeyDown(key);
    }
}
