using Raylib_cs;
using System.Numerics;

public class SceneMenu : Scene
{
    Font fontAlagard = Raylib.LoadFontEx("alagard.ttf", 180, null, 250);

    public override void Load(){}

    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            SceneManager.Load<SceneIntro>();
        }
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            SceneManager.Load<SceneCredits>();
        }
    }

    public override void Draw()
    {
        Raylib.DrawTextEx(fontAlagard, "Dungeon", new Vector2(60,50), fontAlagard.BaseSize, -15, Color.Red);
        Raylib.DrawTextEx(fontAlagard, "Snek", new Vector2(270, 190), fontAlagard.BaseSize, -15, Color.Red);

        Raylib.DrawTextEx(fontAlagard, " Press\nENTER\nto start", new Vector2(200, 380), 25, 1, Color.White);
        Raylib.DrawTextEx(fontAlagard, "\t\t Press\n\t\tSPACE\nfor credits", new Vector2(450, 380), 25, 1, Color.White);
    }

    public override void UnLoad()
    {
        Console.WriteLine("Unloading menu...");
    }
}