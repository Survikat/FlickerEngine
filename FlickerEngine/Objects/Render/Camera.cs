using System.Numerics;
using FlickerEngine.Managers;
using Raylib_cs;

namespace FlickerEngine.Objects.Render;

public class Camera {
    private Camera2D Lens;
    private Group Objects = new Group();
    
    public Color BGColor = Color.Blank;
    
    public int Width;
    public int Height;

    private bool _dynamicSize;
    
    /// <summary>
    /// Makes the Camera render to the full screen.
    /// </summary>
    public bool DynamicSize {
        get => _dynamicSize;
        set {
            if (!value) {
                Width = Game.FrameWidth;
                Height = Game.FrameHeight;
            }
            else {
                Width = Game.RenderWidth;
                Height = Game.RenderHeight;
            }
            
            UpdateLensOffsets();
            _dynamicSize = value;
        }
    }
    
    private float Zoom = 1.0f;
    
    public Camera(int Width, int Height, int X = 0, int Y = 0) {
        this.Width = Width;
        this.Height = Height;
        
        Lens = new Camera2D(
            new Vector2(X, Y), 
            new Vector2(0, 0),
            0f, 
            1.0f
        );
        
        UpdateLensOffsets();
    }

    private void UpdateLensOffsets() {
        Lens.Offset.X = (float)(Game.RenderWidth / 2.0);
        Lens.Offset.Y = (float)(Game.RenderHeight / 2.0);

        Lens.Target.X = (float)(Width / 2.0);
        Lens.Target.Y = (float)(Height / 2.0);
    }

    public void Draw() {
        if (Window.WasResized) {
            if (DynamicSize) {
                Width = Game.RenderWidth;
                Height = Game.RenderHeight;
            }
            
            UpdateLensOffsets();
        }
        
        float Scale = Math.Min(
            (float)Game.RenderWidth / Width,
            (float)Game.RenderHeight / Height
        );
                
        Lens.Zoom = Zoom * Scale;

        int TotalWidth = (int)(Width * Scale);
        int TotalHeight = (int)(Height * Scale);
        
        Raylib.BeginScissorMode(
            (Game.RenderWidth - TotalWidth) / 2, 
            (Game.RenderHeight - TotalHeight) / 2, 
            TotalWidth,
            TotalHeight
        );
            Raylib.BeginMode2D(Lens);
                Raylib.ClearBackground(BGColor);
                Objects.Draw();
                
                // PLACEHOLDER
                Raylib.DrawText("Hello World!", 0, 0, 32, Color.Blue);
            Raylib.EndMode2D();
        Raylib.EndScissorMode();
    }

    public void Update(double Delta) {
        Objects.Update(Delta);
    }

    public void Add(Basic Object) {
        if (Objects.Contains(Object))
            return;
        
        Objects.Add(Object);
    }

    public void Remove(Basic Object) {
        if (!Objects.Contains(Object))
            return;
        
        Objects.Remove(Object);
    }
}