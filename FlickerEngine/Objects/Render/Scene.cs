namespace FlickerEngine.Objects.Render;

public class Scene : IDisposable {
    public List<Camera> Cameras = new ();
    private List<Basic> Objects = new ();

    /// <summary>
    /// Retrieves or sets the first camera in the <c>Cameras</c> array.
    /// </summary>
    public Camera Camera {
        get {
            if (Cameras.Count == 0 || Cameras[0] == null)
                throw new Exception("Camera not found");
            
            return Cameras[0];
        }
        set => Cameras[0] = value;
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
        Cameras.ForEach(Cam => {
            if (Cam.Visible) {
                Basic[] RenderedObjects = Objects.FindAll(Obj => {
                    return (Obj.Alive && Obj.Cameras.Contains(Cam) && Obj.Visible);
                }).ToArray();
                
                Camera.Draw(RenderedObjects);
            }
        });
    }
    
    public virtual void Create() { }

    public virtual void Update(float Delta) {
        Basic[] CurrentObjects = Objects.ToArray();
        foreach (Basic Object in CurrentObjects) {
            if (Object.Alive)
                continue;
            
            Objects.Remove(Object);
        }
    }

    /// <summary>
    /// Adds the given object to the scene.
    /// </summary>
    /// <param name="Object">Any Object</param>
    public void Add(Basic Object) {
        Object.Camera ??= this.Camera;
        Objects.Add(Object);
    }

    public void Dispose() {
        Cameras.Clear();
        
        Objects.ForEach(Obj => Obj.Dispose());
        Objects.Clear();
        
        // Apparently a bad practice.
        // GC.Collect(1);
    }
}