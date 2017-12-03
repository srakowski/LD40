using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Engine
{
    class Behavior : Component
    {
        public GameTime GameTime { get; internal set; }

        public InputManager Input { get; internal set; }

        public virtual void OnUpdate() { }

        internal virtual void Update(GameTime gameTime, InputManager input)
        {
            GameTime = gameTime;
            Input = input;
            OnUpdate();
        }
    }
}
