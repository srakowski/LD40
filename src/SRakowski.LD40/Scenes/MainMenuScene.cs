using SRakowski.LD40.Engine;
using SRakowski.LD40.Engine.UI;

namespace SRakowski.LD40.Scenes
{
    class MainMenuScene : Scene
    {
        public override void Initialize()
        {
            //AddEntity(
            //    new TextInput("Enter a name for your group: ")
            //        .TranslateTo(100, 100)
            //);

            var form = new Form();

            AddEntity(
                new Button(form, new SpriteRenderer("Sprites/playbutton"))
                    .TranslateTo(100, 100)
                    .OnClick(() =>
                    {
                        Exit();
                        Context.SceneManager.AddScene(MovementScene.Create());
                    })
            );

            AddEntity(new Pointer(form, new SpriteRenderer("Sprites/pointer")));
        }
    }
}
