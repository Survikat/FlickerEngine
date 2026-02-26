using System.Numerics;
using FlickerEngine.Managers.Assets;
using FlickerEngine.Managers.Sprites;
using FlickerEngine.Objects.Render;
using Raylib_cs;

namespace FlickerEngine.Objects.Sprites;

// TO BE IMPLEMENTED.
public class Sprite : Basic {
    private Texture2D Texture;
    
    public Color Tint = Color.White;
    public float Alpha = 1.0f;

    private int GraphicWidth;
    private int GraphicHeight;

    public int Width;
    public int Height;
    
    public float X;
    public float Y;

    private float _scale = 1f;
    public float Scale {
        get => _scale;
        set {
            _scale = value;
            UpdateAnimator();
        }
    }
    
    // private float _rotation;

    public Animator Animator = new ();
    private Rectangle Frame;
    
    public Sprite(float X, float Y, string Path, bool Animated = false, int FrameWidth = 0, int FrameHeight = 0) {
        Texture = Resources.GetGraphic(Path);
        
        this.X = X;
        this.Y = Y;

        Frame.Width = GraphicWidth = this.Width = Texture.Width;
        Frame.Height = GraphicHeight = this.Height = Texture.Height;

        if (Animated) {
            Frame.Width = FrameWidth;
            Frame.Height = FrameHeight;
        }

        UpdateAnimator();
    }

    private Rectangle ScaledRect;
    private void UpdateAnimator() {
        ScaledRect = new Rectangle(
            Frame.X, 
            Frame.Y, 
            (int)(Frame.Width * Scale), 
            (int)(Frame.Height * Scale)
        );
        
        Texture.Width = (int)(GraphicWidth * Scale);
        Texture.Height = (int)(GraphicHeight * Scale);
        
        Animator.TextureSize = new Vector2(Texture.Width, Texture.Height);
    }

    public override void Draw(Camera Camera) {
        base.Draw(Camera);

        Raylib.DrawTextureRec(Texture, ScaledRect, new Vector2(X, Y), Raylib.ColorAlpha(Tint, Alpha));
    }

    public override void Update(float Delta) {
        base.Update(Delta);

        Rectangle? Rect = new Rectangle(
            ScaledRect.X,
            ScaledRect.Y,
            ScaledRect.Width,
            ScaledRect.Height
        );
        
        ScaledRect = Animator.Update(Rect.Value, Delta);
        Rect = null;
    }

    public override void Dispose() {
        base.Dispose();
        
        Raylib.UnloadTexture(Texture); // Remove Later
        Animator.Dispose();
    }
}