using System.Reflection;
using Raylib_cs;

namespace FlickerEngine.Managers.Assets;

public static class Resources {
    private static Assembly GameAssembly = null!;

    public static string[] AvailableResources {
        get {
            if (GameAssembly.Equals(null))
                return [];

            return GameAssembly.GetManifestResourceNames();
        }
    }

    public static void Initialize(Assembly Assembly) {
        GameAssembly = Assembly;
    }

    private static string FormatPathToInternal(string Path) {
        return Path.Replace("\\", ".").Replace("/", ".");
    }
    
    private static Stream GetResourceStream(string Path) {
        if (GameAssembly.Equals(null)) {
            throw new Exception("Assembly not initialized.");
        }
        
        string ?BaseName = GameAssembly.GetName().Name;
        if (BaseName is null)
            throw new Exception("Could not detect Assembly Name.");

        string FormattedPath = FormatPathToInternal(Path);
        string InternalPath = $"{BaseName}.{FormattedPath}";
        
        return GameAssembly.GetManifestResourceStream(InternalPath) ?? throw new Exception("Could not find Resource.");
    }
    
    private static Byte[] GetInternal(string Path) {
        Stream ResourceFromStream = GetResourceStream(Path);
        
        Byte[] Bytes = new byte[ResourceFromStream.Length];
        ResourceFromStream.ReadExactly(Bytes);
        
        ResourceFromStream.Dispose();
        ResourceFromStream.Close();
        
        return Bytes;
    }

    /// <summary>
    /// Retrieves an Asset from Internal Resources or File System.
    /// </summary>
    /// <param name="Path">Example: <c>Assets/Jellyfish.png</c></param>
    /// <returns>Byte Array of Content</returns>
    public static Byte[] GetAssetBytes(string Path) {
        if (AvailableResources.Contains(FormatPathToInternal(Path))) {
            return GetInternal(Path);
        }
        else if (File.Exists(Path)) {
            return File.ReadAllBytes(Path);
        }

        return [];
    }
    
    /// <summary>
    /// Retrieves a Graphic and returns the Texture.
    /// </summary>
    /// <param name="Path">Example: <c>Assets/Jellyfish.png</c></param>
    /// <returns></returns>
    public static Texture2D GetGraphic(string Path) {
        Image Image = Raylib.LoadImageFromMemory(".png", GetAssetBytes(Path));
        Texture2D Texture = Raylib.LoadTextureFromImage(Image);
        
        return Texture;
    }
}