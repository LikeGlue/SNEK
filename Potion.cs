using System.Numerics;
using Raylib_cs;

public class Potion
{
    public Coordinates coordinates { get; private set; }
    private Grid<bool> grid;
    //private float radius = 0.4f;
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 50, null, 250);


    public Potion(Grid<bool> grid)
    {
        this.grid = grid;
        coordinates = Coordinates.Random(grid.columns, grid.rows);
    }

    
    public void Respawn(Coordinates[] preventCoordinates) // passer tableau de coordonnées interdites en paramètre (perso, ennemis, etc.)
                          // utiliser methode queue pour retourner tableau - boucle while tant que coordonnées de body et ennemis non atteints.
                          // Concatenation de tableau 
                        
    {
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
        Raylib.DrawTextEx(gameFont, "P", new Vector2((int)worldPosition.X,(int)worldPosition.Y), grid.cellSize,1,Color.Green);
    }
}