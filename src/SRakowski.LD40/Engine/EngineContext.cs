using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SRakowski.LD40.Engine
{
    class EngineContext
    {
        private Func<SpriteBatch> _spriteBatch;

        public SceneManager SceneManager { get; }

        public SpriteBatch SpriteBatch => _spriteBatch();

        public IReadOnlyDictionary<string, object> Content { get; }

        private Func<GraphicsDevice> _graphicsDevice;

        public GraphicsDevice GraphicsDevice => _graphicsDevice();

        public EngineContext(
            SceneManager sceneManager,
            Func<SpriteBatch> spriteBatch,
            IReadOnlyDictionary<string, object> content,
            Func<GraphicsDevice> graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            this.SceneManager = sceneManager;
            this.Content = content;
        }
    }
}
