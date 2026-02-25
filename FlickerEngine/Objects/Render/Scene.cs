namespace FlickerEngine.Objects.Render;

public class Scene {
    public List<Camera> Cameras = new ();

    /// <summary>
    /// Retrieves or sets the first camera in the <c>Cameras</c> array.
    /// </summary>
    protected Camera Camera {
        get {
            if (Cameras[0] == null)
                throw new Exception("Camera not found");
            
            return Cameras[0];
        }
    }
    
    protected Scene(int? Width = null, int? Height = null) {
        if (!Width.HasValue || !Height.HasValue) {
            Width = Game.FrameWidth;
            Height = Game.FrameHeight;
        }
        
        var Cam = new Camera(Width.Value, Height.Value);
        Cameras.Add(Cam);
    }

    public virtual void Draw() {
        int currentCount = Cameras.Count;
        for (int i = 0; i < currentCount; i++) {
            Cameras[i].Draw();
        }
    }
    
    public virtual void Create() {}

    public virtual void Update(double Delta) {
        int currentCount = Cameras.Count;
        for (int i = 0; i < currentCount; i++) {
            Cameras[i].Update(Delta);
        }
    }
}