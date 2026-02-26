using FlickerEngine.Managers;
using FlickerEngine.Objects.Render;
using FlickerEngine.Objects.Render.Debugger;
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
            
            Window.Close();
            return 1;
        }

        Window.Close();
        return 0;
    }
    
    private static void Update() {
        Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Blank);

            List<Scene> ScenesToUpdate = Scenes.ToList();
            ScenesToUpdate.ForEach(Scene => {
                Scene.Update(Raylib.GetFrameTime());
                
                if (!Scene.Equals(null))
                    Scene.Draw();
            });

            // Handled this way to prevent flickering.
            if (ScenesToRemove.Count > 0) {
                var OldScenes = ScenesToRemove.ToList();
                OldScenes.ForEach(Old => { Scenes.Remove(Old); Old.Dispose(); });
            
                ScenesToRemove.Clear();
                Scenes.Sort();
            }
            
            DebuggerOverlay.Draw(Raylib.GetFrameTime());
        Raylib.EndDrawing();
    }

    public static void AddScene(Scene Scene) {
        Scene.Create();
        Scenes.Add(Scene);
    }

    private static List<Scene> ScenesToRemove = new ();
    public static void RemoveScene(Scene Scene) {
        ScenesToRemove.Add(Scene);
    }

    /// <summary>
    /// Opens the given scene before closing all others.
    /// </summary>
    /// <param name="Scene">Any New Scene</param>
    public static void SwitchScene(Scene Scene) {
        List<Scene> OldScenes = Scenes.ToList();
        
        AddScene(Scene);
        ScenesToRemove = OldScenes;
    }
}