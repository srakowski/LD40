using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SRakowski.LD40.Engine
{
    class SceneManager : DrawableGameComponent
    {
        private List<Scene> _scenes = new List<Scene>();

        private InputManager _input;

        private EngineContext _context;

        public SceneManager(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            _input = new InputManager();
        }

        internal void Activate()
        {
            _context = Game.Services.GetService<EngineContext>();
            var scenes = _scenes.ToArray();
            foreach (var scene in scenes)
                scene.Activate(_context);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _input.Update();
            var scenes = _scenes.ToArray();
            var lastScene = scenes.Last();
            foreach (var scene in scenes)
                scene.Update(gameTime, _input, isTopMost: lastScene == scene);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var scenes = _scenes.ToArray();
            var lastScene = scenes.Last();
            foreach (var scene in scenes)
                scene.Draw(gameTime, isTopMost: lastScene == scene);
        }

        public void AddScene(Scene scene)
        {
            scene.Initialize();
            _scenes.Add(scene);
            if (_context != null)
                scene.Activate(_context);
        }

        public void RemoveScene(Scene scene) =>
            _scenes.Remove(scene);
    }
}
 