using FlickerEngine;
using FlickerEngine.Objects.Generic;
using FlickerEngine.Objects.Render;
using Raylib_cs;

namespace Sample.Scenes;

public class MainScene : Scene {
    public override void Create() {
        Camera.BGColor = Color.White;
        
        base.Create();

        var HelloWorld = new RaylibText("Hello World", 128, 128, Color.Blue);
        HelloWorld.FontSize = 32;
        Add(HelloWorld);
    }

    // Not Recommended Behavior, but possible.
    // Since it doesn't render to a Camera, what is rendered won't automatically fit to the frame.
    public override void Draw() {
        base.Draw();
        
        float FPS = 1 / Raylib.GetFrameTime();
        Raylib.DrawText($"FPS: {FPS:F2}", 8, 8, 24, Color.Green);
    }

    private float Speed = 30;
    
    public override void Update(float Delta) {
        base.Update(Delta);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter)) {
            Camera.DynamicSize = !Camera.DynamicSize;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Escape)) {
            Console.WriteLine("Closing and Reopening Scene");
            
            Game.AddScene(new MainScene());
            Game.RemoveScene(this);
        }
        
        bool TargetScrolling = true;
        if (Raylib.IsKeyDown(KeyboardKey.LeftControl))
            TargetScrolling = false;

        if (Raylib.IsKeyDown(KeyboardKey.Up)) {
            if (TargetScrolling)
                Camera.ScrollY -= Speed * Delta;
            else
                Camera.Y -= Speed * Delta;
        }
        else  if (Raylib.IsKeyDown(KeyboardKey.Down)) {
            if (TargetScrolling)
                Camera.ScrollY += Speed * Delta;
            else
                Camera.Y += Speed * Delta;
        }

        if (Raylib.IsKeyDown(KeyboardKey.Left)) {
            if (TargetScrolling)
                Camera.ScrollX -= Speed * Delta;
            else
                Camera.X -= Speed * Delta;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Right)) {
            if (TargetScrolling)
                Camera.ScrollX += Speed * Delta;
            else
                Camera.X += Speed * Delta;
        }
    }
}