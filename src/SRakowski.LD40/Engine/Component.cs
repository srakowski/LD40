namespace SRakowski.LD40.Engine
{
    abstract class Component
    {
        internal virtual void Activate(EngineContext context, Entity entity) { }
    }
}
