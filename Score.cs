using System.Numerics;
using Raylib_cs;

public class Score
{
    private int currentScore;
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 50, null, 250);

    public void AddScore(int points)
    {
        currentScore += points;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void Draw()
    {
        Raylib.DrawTextEx(gameFont,$"Coins: {GetScore()}",new Vector2( 40,10),20,1,Color.White);
    }
}