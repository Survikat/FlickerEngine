using Raylib_cs;

namespace FlickerEngine.Objects.Generic;

public class RaylibText : Basic {
    public string Text;

    public float X;
    public float Y;

    public int FontSize = 8;
    public Color Color;
    
    public RaylibText(string Text, float X, float Y, Color Color) {
        this.Text = Text;
        
        this.X = X;
        this.Y = Y;
        
        this.Color = Color;
    }

    public override void Draw() {
        base.Draw();
        
        Raylib.DrawText(Text, (int)X, (int)Y, FontSize, Color);
    }
}