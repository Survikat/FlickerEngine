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
    
    public float Zoom = 1.0f;

    public float X = 0.0f;
    public float Y = 0.0f;

    public float ScrollX = 0.0f;
    public float ScrollY = 0.0f;

    public int RenderX;
    public int RenderY;

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
            
            _dynamicSize = value;
            UpdateLensOffsets();
        }
    }
    
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
    
    private int TotalWidth;
    private int TotalHeight;
    
    private void UpdateLensOffsets() {
        if (DynamicSize) {
            Width = Game.RenderWidth;
            Height = Game.RenderHeight;
        }
        
        float Scale = Math.Min(
            (float)Game.RenderWidth / Width,
            (float)Game.RenderHeight / Height
        );

        Lens.Zoom = Scale * Zoom;

        TotalWidth = (int)(Width * Scale);
        TotalHeight = (int)(Height * Scale);

        RenderX = ((Game.RenderWidth - TotalWidth) / 2) + (int)X;
        RenderY = ((Game.RenderHeight - TotalHeight) / 2) + (int)Y;
    }

    public void Draw(Basic[] Objects) {
        if (Window.WasResized || (TotalWidth == 0 || TotalHeight == 0))
            UpdateLensOffsets();
        
        Lens.Offset.X = (float)(Game.RenderWidth / 2.0) + X;
        Lens.Offset.Y = (float)(Game.RenderHeight / 2.0) + Y;
        
        Lens.Target.X = (float)(Width / 2.0) + ScrollX;
        Lens.Target.Y = (float)(Height / 2.0) + ScrollY;
        
        Raylib.BeginScissorMode(
            RenderX,
            RenderY, 
            TotalWidth,
            TotalHeight
        );
            Raylib.BeginMode2D(Lens);
                Raylib.ClearBackground(BGColor);
                
                foreach (Basic Object in Objects)
                    Object.Draw(this);
            Raylib.EndMode2D();
        Raylib.EndScissorMode();
    }
}