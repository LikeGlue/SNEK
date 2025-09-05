using Raylib_cs;
using System.Numerics;

public class SceneMenu : Scene
{

    Font fontTTF = Raylib.LoadFontEx("Pixelfraktur.ttf", 200, null, 250);

    public override void Load()
    {
        //Console.WriteLine("Loading menu...");
    }

    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            SceneManager.Load<SceneIntro>();
        }
    }

    public override void Draw()
    {
        //Console.WriteLine("Drawing menu...");
        Raylib.DrawTextEx(fontTTF,"SNEK", new Vector2(70, 150), fontTTF.BaseSize, -15, Color.Red);
        

    }

    public override void UnLoad()
    {
        Console.WriteLine("Unloading menu...");
    }
}