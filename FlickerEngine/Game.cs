using FlickerEngine.Managers;
using Raylib_cs;

namespace FlickerEngine;

public class Game {
    private static readonly List<ConfigFlags> _configFlags = [
        ConfigFlags.ResizableWindow,
        ConfigFlags.AlwaysRunWindow,
        ConfigFlags.Msaa4xHint
    ];
    
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
        Window.Initialize(Width, Height, Title, ContextFlags);
        Raylib.SetTargetFPS(FPS);
        
        while (!Window.ShouldClose) { Update(); }
    }

    private static void Update() {
        Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Blank);
        Raylib.EndDrawing();
    }
}