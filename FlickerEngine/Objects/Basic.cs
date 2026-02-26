using FlickerEngine.Objects.Render;

namespace FlickerEngine.Objects;

public class Basic : IDisposable {
    /// <summary>
    /// Behaves as both <c>active</c> and <c>visible</c>.
    /// If false, and the Object is part of a Scene, it will be removed next frame.
    /// </summary>
    public bool Alive = true;
    
    public bool Active = true;
    public bool Visible = true;
    
    public List<Camera> Cameras = [];
    public Camera? Camera {
        get {
            if (Cameras.Count == 0)
                return null;
            
            return Cameras[0];
        }
        set {
            if (value is not null) {
                if (Cameras.Count == 0) {
                    Cameras.Add(value);
                }
                else {
                    Cameras[0] = value;
                }
            }
        }
    }

    /// <summary>
    /// Draws to the frame upon every draw.
    /// Automatically skips drawing if invisible.
    /// </summary>
    public virtual void Draw() {
        if (!Visible || !Alive)
            return;
    }

    /// <summary>
    /// Updates upon every frame.
    /// Automatically skips updating if inactive.
    /// </summary>
    /// <param name="Delta">Delta Time</param>
    public virtual void Update(float Delta) {
        if (!Active || !Alive)
            return;
    }

    /// <summary>
    /// Sets <c>exists</c> to <c>false</c>.
    /// </summary>
    public virtual void Kill() {
        Alive = false;
    }

    public virtual void Dispose() {
        Cameras.Clear();
    }
}