using Raylib_cs;
using System.Numerics;

public class GameOver
{
    Font gameOverFont = Raylib.LoadFontEx("alagard.ttf", 100, null, 250);
    Color gameOverBg = new Color(255, 255, 255, 150);
    const int screenWidth = 800;
    const int screenHeight = 480;

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            SceneManager.Load<SceneGame>();
        }
    }

    public void Draw()
    {   
        Raylib.DrawRectangle(0, 0, screenWidth, screenHeight, gameOverBg);
        Raylib.DrawTextEx(gameOverFont, "GAME OVER", new Vector2(110, 150), 100, 1, Color.Red);
        Raylib.DrawTextEx(gameOverFont, "press SPACE to start over ", new Vector2(140,300), 40, 1, Color.Black);

    }
}