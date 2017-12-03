using System;

namespace SRakowski.LD40.Engine
{
    class RelayBehavior : Behavior
    {
        private Action<RelayBehavior> _onUpdate;

        public RelayBehavior(Action<Behavior> onUpdate) =>
            _onUpdate = onUpdate;

        public override void OnUpdate() => _onUpdate(this);
    }
}
