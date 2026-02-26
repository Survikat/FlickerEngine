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
        
        Raylib.DrawText("You can't see me...\nUnless you resize\nthe Window.", 8, 
            Game.RenderHeight - (24 * 3), 24, Color.White);
    }

    private float Speed = 30;
    
    public override void Update(float Delta) {
        base.Update(Delta);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter)) {
            Camera.DynamicSize = !Camera.DynamicSize;
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.Escape)) {
            Console.WriteLine("FLICKER: Closing all and Reopening Scene");
            
            Game.SwitchScene(new MainScene());
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