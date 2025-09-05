using Raylib_cs;
using System.Numerics;

public class SceneIntro : Scene
{
    public Player player;
    
    int framesCounter = 0;
    string introMessage;
    string startMessage;
    string speedMessage;
    string name;
    string weapon;
    bool startMessageBool;

    float blinkTimer = 0.0f;
    bool textVisible = true;

    Font introFont = Raylib.LoadFontEx("alagard.ttf", 25, null, 250);

    public string NameGeneration()
    {
        var random = new Random();

        var listName1 = new List<string> { "Hil", "Val", "Gol", "Gar", "Bro", "Del" };
        var listName2 = new List<string> { "", "de", "ro", "do", "du", "vo" };
        var listName3 = new List<string> { "ric", "val", "gol", "gar", "gor", "sil" };

        int index1 = random.Next(listName1.Count);
        int index2 = random.Next(listName2.Count);
        int index3 = random.Next(listName3.Count);

        string name = listName1[index1] + listName2[index2] + listName3[index3];

        return name;
    }

    public string WeaponGeneration()
    {
        var random = new Random();

        var weaponList = new List<string> { "dagger", "sword", "axe", "flail", "whip", "bow" };
        int weaponIndex = random.Next(weaponList.Count);

        string weapon = weaponList[weaponIndex];
        return weapon;
    }

    public override void Load()
    {
        Console.WriteLine("Loading intro...");
        float deltaTime = Raylib.GetFrameTime();

        name = NameGeneration();
        weapon = WeaponGeneration();
        introMessage =
            $"You are {name},          \n" +
            $"and with your {weapon} at hand       \n" +
            $"you take on this deep dark dungeon\n" +
            $"filled with bounty, gold and glory.       \n" +
            $"\n" +
            $"But beware of the creatures below,\n" +
            $"       for death...\n" +
            $"\n" +
            $"      \t\t\t\t\t\t\t\t..awaits";

        startMessage = "Press ENTER to start your journey";
        speedMessage = "Hold SPACE";
        startMessageBool = false;

    }

    public override void Update(float deltaTime)
    {
        //Console.WriteLine(framesCounter);
        if(Raylib.IsKeyDown(KeyboardKey.Space))
        {
            framesCounter += 8;
        }

        else
        {
            framesCounter += 1;
        }

        if (framesCounter >= 900)
        {
            startMessageBool = true;
            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                SceneManager.Load<SceneGame>();
            }

        }

        blinkTimer += deltaTime;
        if (blinkTimer >= 0.7f)
        {
            textVisible = !textVisible;
            blinkTimer = 0.0f;
        }
    }

    public override void Draw()
    {
        //Console.WriteLine("Drawing menu...");
        //Raylib.DrawText(message.SubText(0, framesCounter/10), 70, 150, 40, Color.White);
        
        Raylib.DrawTextEx(introFont, introMessage.SubText(0, framesCounter/4),new Vector2(150,100), 25,1, Color.White);
        if (startMessageBool == true && textVisible)
        {
            Raylib.DrawTextEx(introFont, startMessage, new Vector2(170, 400), 25, 1, Color.Gold);
        }

        if (startMessageBool == false)
        {
            Raylib.DrawTextEx(introFont, speedMessage, new Vector2(680,450), 15, 1, Color.Gray);
        }


    }

    public override void UnLoad()
    {
        Console.WriteLine("Unloading menu...");
    }
}