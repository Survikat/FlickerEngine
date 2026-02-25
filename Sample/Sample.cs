using FlickerEngine;
using Raylib_cs;

namespace Sample;

internal class MyGame {
    public static void Main(string[] args) {
        // You can set and remove a Config Flag before Initialization.
        Game.SetConfigFlag(ConfigFlags.UndecoratedWindow);
        Game.RemoveConfigFlag(ConfigFlags.UndecoratedWindow);
        
        Game.Initialize("Sample Project", 640, 480, 60);
        
        // Same can be done after Initialization.
        Game.SetConfigFlag(ConfigFlags.VSyncHint);
    }
}