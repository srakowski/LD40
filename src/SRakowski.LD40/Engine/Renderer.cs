using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SRakowski.LD40.Engine
{
    abstract class Renderer : Component
    {
        protected Renderer() { }

        internal abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        internal abstract Rectangle Bounds { get; }
    }
}
