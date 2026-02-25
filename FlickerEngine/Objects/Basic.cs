namespace FlickerEngine.Objects;

public class Basic {
    /// <summary>
    /// Behaves as both <c>active</c> and <c>visible</c>.
    /// </summary>
    public bool exists = true;
    
    public bool active = true;
    public bool visible = true;

    public int ID;

    /// <summary>
    /// Draws to the frame upon every draw.
    /// Automatically skips drawing if invisible.
    /// </summary>
    public virtual void Draw() {
        if (!visible || !exists)
            return;
    }

    /// <summary>
    /// Updates upon every frame.
    /// Automatically skips updating if inactive.
    /// </summary>
    /// <param name="Delta">Delta Time</param>
    public virtual void Update(double Delta) {
        if (!active || !exists)
            return;
    }

    /// <summary>
    /// Sets <c>exists</c> to <c>false</c>.
    /// </summary>
    public virtual void Kill() {
        exists = false;
    }
}