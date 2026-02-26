using System.Diagnostics;
using Raylib_cs;

namespace FlickerEngine.Objects.Render.Debugger;

public static class DebuggerOverlay {
    // Internal
    private static bool firstRun = true;
    private static float UpdateTimer;

    private static float FPSCounter;
    private static float FPSPeak;

    private static float FPSToDisplay;
    private static float LastFPSAverage;
    
    private static float MemoryUsage;
    private static float MemoryPeak;

    private static int X = 12;
    private static int Y = 12;
    
    public static void Draw(float Delta) {
        UpdateTimer += Delta;
        
        FPSCounter = 1 / Delta;
        if (FPSCounter >= FPSPeak || Single.IsInfinity(FPSPeak))
            FPSPeak = FPSCounter;

        // Update FPS Display
        if (UpdateTimer >= 0.1 || firstRun) {
            FPSToDisplay = (float)Math.Round(FPSCounter, 0);
            LastFPSAverage = (FPSCounter + FPSPeak) / 2;
            
            MemoryUsage = (float)Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
            MemoryPeak = (float)Process.GetCurrentProcess().PeakWorkingSet64 / 1024 / 1024;

            if (firstRun) 
                firstRun = false;
            
            UpdateTimer = 0;
        }
        
        int TextSize = Math.Min(Math.Max((Game.RenderWidth / 640) * 12, 12), 24);
        
        Raylib.DrawText($"FPS: {FPSToDisplay:0000}", X, Y, TextSize + 4, Color.Green);
        Raylib.DrawText($"Average: {LastFPSAverage:0000}", X + (TextSize * 6), Y, TextSize + 4, Color.Green);
        
        Raylib.DrawText($"Mem: {MemoryUsage:F2}MiB/{MemoryPeak:F2}MiB", X, Y + TextSize, TextSize + 4, Color.SkyBlue);
    }
}