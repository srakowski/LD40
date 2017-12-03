using SRakowski.LD40.Engine;
using CameraComponent = SRakowski.LD40.Engine.Camera;

namespace SRakowski.LD40.Entities
{
    class CameraEntity : Entity
    {
        public CameraEntity()
        {
            AddComponent(new CameraComponent());
        }
    }
}
