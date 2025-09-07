using System.Numerics;
using Raylib_cs;

public class Treasure
{
    public Coordinates coordinates { get; private set; }
    private Grid<bool> grid;

    public string currentTreasure;
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 50, null, 250);


    public Treasure(Grid<bool> grid)
    {
        this.grid = grid;
        coordinates = Coordinates.Random(grid.columns, grid.rows);
    }

    public void GetTreasureType()
    {
        var random = new Random();
        var treasureList = new List<string> { "legendary", "potion", "gold" };
        int index = random.Next(treasureList.Count);
        currentTreasure = treasureList[index];
    }

    public Coordinates[] GetCoordinatesArray()
    {
        Coordinates[] array = { coordinates };
        return array;
    }


    public void Respawn(Coordinates[] preventCoordinates)
    {
        Console.WriteLine("Treasure SPAWN");
        Coordinates newCoordinate;
        do
        {
            newCoordinate = Coordinates.Random(grid.columns, grid.rows);

        } while (Array.Exists(preventCoordinates, c => c.Equals(newCoordinate)));


        coordinates = newCoordinate;
    }

    public void Draw()
    {
        Vector2 worldPosition = grid.GridToWorld(coordinates);
        worldPosition += new Vector2(6, 3); // centre la position au milieu de la cellule
        Raylib.DrawTextEx(gameFont, "T", new Vector2((int)worldPosition.X, (int)worldPosition.Y), grid.cellSize, 1, Color.Gold);
    }
}