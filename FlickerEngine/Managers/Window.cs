using Raylib_cs;

namespace FlickerEngine.Managers;

public class Window {
    public static int X {
        get {
            if (!Raylib.IsWindowReady())
                return 0;
            
            return (int)Raylib.GetWindowPosition().X;
        }
        set {
            if (!Raylib.IsWindowReady())
                return;
            
            Raylib.SetWindowPosition(value, Y);
        }
    }
    
    public static int Y {
        get {
            if (!Raylib.IsWindowReady())
                return 0;
            
            return (int)Raylib.GetWindowPosition().Y;
        }
        set {
            if (!Raylib.IsWindowReady())
                return;
            
            Raylib.SetWindowPosition(X, value);
        }
    }

    private static int _width;
    public static int Width {
        get => _width;
        set
        {
            if (!Raylib.IsWindowReady())
                return;
            
            _width = value;
            Raylib.SetWindowSize(value, _height);
        }
    }
    
    private static int _height;
    public static int Height {
        get => _height;
        set {
            if (!Raylib.IsWindowReady())
                return;
                
            _height = value;
            Raylib.SetWindowSize(_width, value);   
        }
    }

    private static string? _title;
    public static string? Title {
        get => _title;
        set {
            if (!Raylib.IsWindowReady())
                return;
            
            _title = value;
            Raylib.SetWindowTitle(value);
        }
    }

    public static bool ShouldClose {
        get {
            if (!Raylib.IsWindowReady())
                return false;

            return Raylib.WindowShouldClose();
        }
    }

    public static bool WasResized {
        get {
            if (!Raylib.IsWindowReady())
                return false;

            return Raylib.IsWindowResized();
        }
    }
    
    /// <summary>
    /// Initializes the Window for the current application.
    /// </summary>
    /// <param name="Width">Width of Window</param>
    /// <param name="Height">Height of Window</param>
    /// <param name="Title">Title of Window</param>
    /// <param name="Flags">Context Flags</param>
    public static void Initialize(int Width, int Height, string Title, ConfigFlags[] Flags)
    {
        // Set ConfigFlags for Window
        foreach (ConfigFlags flag in Flags)
            Raylib.SetConfigFlags(flag);
        
        _width = Width;
        _height = Height;

        Raylib.InitWindow(Width, Height, Title);
    }

    public static void SetSize(int Width, int Height) {
        Window.Width = Width;
        Window.Height = Height;
    }

    public static void SetPosition(int X, int Y) {
        Window.X = X;
        Window.Y = Y;
    }
}