using Raylib_cs;
using System.Numerics;

public class SceneCredits : Scene
{
    Font fontPF = Raylib.LoadFontEx("Pixelfraktur.ttf", 100, null, 250);
    Font FontAlagard = Raylib.LoadFontEx("alagard.ttf", 20, null, 250);
    public override void Load(){}
    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            SceneManager.Load<SceneMenu>();
        }
    }
    public override void Draw()
    {
        Raylib.DrawTextEx(fontPF, "Credits", new Vector2(200, 30), fontPF.BaseSize, 1, Color.Red);
        Raylib.DrawTextEx(FontAlagard, 
            "Merci a Nicolas Animapix, Robin Exodeon,\n" +
            "Clement Citron, Pierrick Pipi972, Stevens Sax,\n" +
            "Raphael Ogaren et la promo Bogota de votre aide \n" +
            "et bonne humeur.",
            new Vector2(150, 200), 20, 1, Color.White);

        Raylib.DrawTextEx(FontAlagard, "Module 2 Gaming Campus 2025", new Vector2(220, 400), 20, 1, Color.White);
        Raylib.DrawTextEx(FontAlagard, "Press SPACE to come back to the menu", new Vector2(220, 430), 15, 1, Color.Gray);
    }
    public override void UnLoad()
    {
        Console.WriteLine("Unloading menu...");
    }
}