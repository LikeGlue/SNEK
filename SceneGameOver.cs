using Raylib_cs;

public class SceneGameOver : Scene
{
    public override void Load()
    {
        //Console.WriteLine("Game Over");
    }

    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            SceneManager.Load<SceneMenu>();
        }
    }

    public override void Draw()
    {
        //Console.WriteLine("Drawing menu...");
    }

    public override void UnLoad()
    {
        Console.WriteLine("Unloading menu...");
    }
}