using FlickerEngine;
using Raylib_cs;
using Sample.Scenes;

namespace Sample;

internal class MyGame {
    public static int Main(string[] args) {
        // You can set and remove a Config Flag before Initialization.
        Game.SetConfigFlag(ConfigFlags.UndecoratedWindow);
        Game.RemoveConfigFlag(ConfigFlags.UndecoratedWindow);
        
        // You can set FPS to 0 for Unlimited FPS.
        Game.Initialize("Sample Project", 640, 480, 0);
        Game.AddScene(new MainScene());
        
        // Same can be done after Initialization.
        Game.SetConfigFlag(ConfigFlags.VSyncHint);

        return Game.Execute(); // Starts the Game Loop.
    }
}