using FlickerEngine;
using Raylib_cs;

namespace Sample;

internal class MainClass
{
    public static void Main(string[] args)
    {
        var app = new Application();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                Raylib.DrawText("Hello World!", 4, 4, 32, Color.Black);
            Raylib.EndDrawing();
        }
    }
}