using System.Numerics;
using Raylib_cs;

namespace FlickerEngine.Managers.Sprites;

public struct AnimationData {
    public string Name;
    public int[] Frames;
    public int FPS;
    
    public AnimationData (string Name, int[] Frames, int FPS = 1) {
        this.Name = Name;
        this.Frames = Frames;
        this.FPS = FPS;
    }
}

public class Animator : IDisposable {
    private AnimationData ?CurrentAnim;
    private Dictionary<String, AnimationData> Animations = [];
    
    public Vector2 TextureSize = new (0, 0);

    public Animator() {
        var Idle = new AnimationData();
        Idle.Name = "Idle";
        Idle.Frames = [];
        
        Animations.Add("default", Idle);
        CurrentAnim = Idle;
    }

    public void Add(AnimationData Animation) {
        Animations.Add(Animation.Name, Animation);
    }

    public void Remove(string Name) {
        Animations.Remove(Name);
    }

    public void Play(string Name) {
        CurFrame = 0;
        Elapsed = 0;
        
        CurrentAnim = Animations[Name];
    }

    public int CurFrame;
    private float Elapsed;

    public Rectangle Update(Rectangle SpriteFrame, float Delta) {
        if (CurrentAnim is null)
            return SpriteFrame;
        
        AnimationData CurAnim = CurrentAnim.Value;
        
        float FrameTime = 1f / CurAnim.FPS;
        Elapsed += Delta;
        
        if (Elapsed >= FrameTime) {
            Elapsed -= FrameTime;
            ++CurFrame;
        }
        
        if (CurFrame > CurAnim.Frames.Length - 1) {
            Elapsed = 0;
            CurFrame = 0;
        }

        int Columns = (int)TextureSize.X / (int)SpriteFrame.Width;
        int FrameX = (CurFrame % Columns) * (int)SpriteFrame.Width;
        int FrameY = (CurFrame / Columns) * (int)SpriteFrame.Height;
        
        SpriteFrame.X = FrameX;
        SpriteFrame.Y = FrameY;

        return SpriteFrame;
    }

    public void Dispose() {
        Animations.Clear();
        CurrentAnim = null;
    }
}