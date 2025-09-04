using Raylib_cs;
using System.Numerics;

public class SceneIntro : Scene
{

    Font fontIntro = Raylib.LoadFontEx("alagard.ttf", 40, null, 250);
    public Player player;
    
    int framesCounter = 0;
    string message;
    string name;
    string weapon;

    public override void Load()
    {
        Console.WriteLine("Loading intro...");
        name = player.WeaponGeneration();
        weapon = player.NameGeneration();
        message = $"Your name is {name}, and you are a prisonner of this dungeon, with your {weapon} you must find a way out, but beware of the creatures below, for bla bla bla...";


    }

    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            SceneManager.Load<SceneGame>();
        }

        if(Raylib.IsKeyDown(KeyboardKey.Space))
        {
            framesCounter += 8;
        }

        else
        {
            framesCounter += 1;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            framesCounter = 0;
        }
    }

    public override void Draw()
    {
        //Console.WriteLine("Drawing menu...");
        //Raylib.DrawText(message.SubText(0, framesCounter/10), 70, 150, 40, Color.White);
        Raylib.DrawText("TEST SCENE INTRO", 10, 10, 50, Color.White);
        Raylib.DrawText($"Player name: {name} | Weapon name: {weapon}", 10, 10, 50, Color.White);

    }

    public override void UnLoad()
    {
        Console.WriteLine("Unloading menu...");
    }
}