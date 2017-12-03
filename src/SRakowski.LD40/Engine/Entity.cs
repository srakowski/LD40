using System.Collections.Generic;
using System.Linq;

namespace SRakowski.LD40.Engine
{
    class Entity
    {
        private EngineContext _context = null;

        public Transform Transform { get; } = new Transform();

        private IEnumerable<Component> _components = Enumerable.Empty<Component>();

        public IEnumerable<Component> Components => _components;

        public Entity() => AddComponent(Transform);

        public Entity AddComponent(Component component)
        {
            if (_context != null) component.Activate(_context, this);
            _components = _components.Concat(new[] { component });
            return this;
        }

        internal Entity Activate(EngineContext context)
        {
            _context = context;
            foreach (var component in _components)
                component.Activate(context, this);
            return this;
        }
    }

    static partial class EntityExtensions
    {
    }
}
