using System.Diagnostics;
using FlickerEngine.Managers;
using FlickerEngine.Objects.Render;
using Raylib_cs;

namespace FlickerEngine;

public class Game {
    private static readonly List<ConfigFlags> _configFlags = [
        ConfigFlags.ResizableWindow,
        ConfigFlags.AlwaysRunWindow,
        ConfigFlags.Msaa4xHint
    ];

    public static int FrameWidth = 640;
    public static int FrameHeight = 480;

    public static int RenderWidth => Raylib.GetScreenWidth();
    public static int RenderHeight => Raylib.GetScreenHeight();

    private static List<Scene> Scenes = new ();

    /// <summary>
    /// Currently set ContextFlags (also known as ConfigFlags).
    /// </summary>
    public static ConfigFlags[] ContextFlags {
        get => _configFlags.ToArray();
    }
    
    public static void SetConfigFlag(ConfigFlags flag) {
        if (ContextFlags.Contains(flag))
            return;
        
        _configFlags.Add(flag);
        Raylib.SetConfigFlags(flag);
    }
    
    public static void RemoveConfigFlag(ConfigFlags flag) {
        if (!ContextFlags.Contains(flag))
            return;

        _configFlags.Remove(flag);
        Raylib.ClearWindowState(flag);
    }
    
    public static void Initialize(string Title, int Width, int Height, int FPS) {
        FrameWidth = Width;
        FrameHeight = Height;
        
        Window.Initialize(Width, Height, Title, ContextFlags);
        
        Raylib.SetTargetFPS(FPS);
        Raylib.SetExitKey(KeyboardKey.Null);
    }

    /// <summary>
    /// Starts the Game Loop.
    /// </summary>
    /// <returns>Exit Code</returns>
    public static int Execute() {
        try {
            while (!Window.ShouldClose) {
                Update();
            }
        }
        catch (Exception e) {
            Console.WriteLine($"[FAILED] {e.Message}");
            return 1;
        }

        return 0;
    }

    private static void Update() {
        Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Blank);

            int currentCount = Scenes.Count;
            for (int i = 0; i < currentCount; i++) {
                Scene Scene = Scenes[i];
                
                Scene.Update(Raylib.GetFrameTime());
                Scene.Draw();
            }
        Raylib.EndDrawing();
    }

    public static void AddScene(Scene Scene) {
        Scene.Create();
        Scenes.Add(Scene);
    }

    public static void RemoveScene(Scene Scene) {
        Scenes.Remove(Scene);
        GC.Collect();
    }
}