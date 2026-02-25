using FlickerEngine.Objects.Render;
using Raylib_cs;

namespace Sample.Scenes;

public class MainScene : Scene {
    public override void Create() {
        Camera.BGColor = Color.White;
        
        base.Create();
    }

    // Not Recommended Behavior, but possible.
    // Since it doesn't render to a Camera, what is rendered won't automatically fit to the frame.
    public override void Draw() {
        base.Draw();
        
        Raylib.DrawFPS(8, 8);
    }

    public override void Update(double Delta) {
        base.Update(Delta);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter)) {
            Camera.DynamicSize = !Camera.DynamicSize;
        }
    }
}