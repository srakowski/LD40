using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Engine
{
    abstract class Scene
    {
        private EngineContext _context = null;

        protected EngineContext Context => _context;

        private IEnumerable<Entity> _entities = Enumerable.Empty<Entity>();

        public IEnumerable<Entity> Entities => _entities;

        public abstract void Initialize();

        public virtual void OnActivated() { }

        public Scene AddEntity(Entity entity)
        {
            if (_context != null) entity.Activate(_context);
            _entities = _entities.Concat(new[] { entity });
            return this;
        }

        internal Scene Activate(EngineContext context)
        {
            _context = context;
            foreach (var entity in _entities)
                entity.Activate(context);
            OnActivated();
            return this;
        }

        internal void Update(GameTime gameTime, InputManager input, bool isTopMost)
        {
            if (_context == null) return;

            var behaviors = Entities.SelectMany(e => e.Components.OfType<Behavior>());
            foreach (var behavior in behaviors)
                behavior.Update(gameTime, input);
        }

        internal void Draw(GameTime gameTime, bool isTopMost)
        {
            if (_context == null) return;

            var camera = Entities.SelectMany(e => e.Components.OfType<Camera>()).FirstOrDefault();
            var renderers = Entities.SelectMany(e => e.Components.OfType<Renderer>());
            Context.SpriteBatch.Begin(transformMatrix: camera?.TransformationMatrix);
            foreach (var renderer in renderers)
                renderer.Draw(gameTime, Context.SpriteBatch);
            Context.SpriteBatch.End();
        }

        public void Exit() =>
            Context.SceneManager.RemoveScene(this);
    }
}
