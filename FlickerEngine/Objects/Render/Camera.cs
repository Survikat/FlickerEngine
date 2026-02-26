using System.Numerics;
using FlickerEngine.Managers;
using Raylib_cs;

namespace FlickerEngine.Objects.Render;

public class Camera {
    private Camera2D Lens;

    public Color BGColor = Color.Blank;
    public bool Visible = true;
    
    public int Width;
    public int Height;

    public float X = 0.0f;
    public float Y = 0.0f;

    public float ScrollX = 0.0f;
    public float ScrollY = 0.0f;

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
        Lens.Offset.X = (float)(Game.RenderWidth / 2.0) + X;
        Lens.Offset.Y = (float)(Game.RenderHeight / 2.0) + Y;

        Lens.Target.X = (float)(Width / 2.0) + ScrollX;
        Lens.Target.Y = (float)(Height / 2.0) + ScrollY;
    }

    public void Draw(Basic[] Objects) {
        if (DynamicSize && Window.WasResized) {
            Width = Game.RenderWidth;
            Height = Game.RenderHeight;
        }
        
        UpdateLensOffsets();
        
        float Scale = Math.Min(
            (float)Game.RenderWidth / Width,
            (float)Game.RenderHeight / Height
        );
                
        Lens.Zoom = Zoom * Scale;

        int TotalWidth = (int)(Width * Scale);
        int TotalHeight = (int)(Height * Scale);
        
        Raylib.BeginScissorMode(
            ((Game.RenderWidth - TotalWidth) / 2) + (int)X,
            ((Game.RenderHeight - TotalHeight) / 2) + (int)Y, 
            TotalWidth,
            TotalHeight
        );
            Raylib.BeginMode2D(Lens);
                Raylib.ClearBackground(BGColor);
                
                foreach (Basic Object in Objects)
                    Object.Draw();
            Raylib.EndMode2D();
        Raylib.EndScissorMode();
    }
}