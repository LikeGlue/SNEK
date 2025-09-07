using System.Numerics;
using Raylib_cs;

public class Score
{
    private int currentScore;
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 50, null, 250);
    public void AddScore(int score)
    {
        currentScore += score;
        Console.WriteLine("Add score" + currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void Draw()
    {
        Raylib.DrawTextEx(gameFont,$"COINS: {GetScore()}",new Vector2( 40,10),20,1,Color.White);
    }
}