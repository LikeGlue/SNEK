using Raylib_cs;
using System.Numerics;
public class Enemy
{
    public Coordinates coordinates { get; private set; }
    private Grid<bool> grid;
    public string currentEnemy;
    public int enemyHp;
    public int enemyDmg;
    public int enemyPts;
    public string enemyName = "E";
    public int round = 1;
    public bool combatOn = false;
    public Coordinates playerCoordinates;
    public string gameMessage;
    
    public enum EnemyType { goblin, orc, skeleton } // à intégrer
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 50, null, 250);

    public Enemy(Grid<bool> grid)
    {
        this.grid = grid;
        coordinates = Coordinates.Random(grid.columns, grid.rows);
        GetEnemyType();
    }

    public Coordinates[] GetCoordinatesArray()
    {
        Coordinates[] array = { coordinates };
        return array;
    }

    public void GetEnemyType()
    {
        var random = new Random();
        var enemyList = new List<string> { "goblin", "orc", "skeleton", "giant spider", "vampire", "troll" };
        int index = random.Next(enemyList.Count);
        currentEnemy = enemyList[index];
        if (currentEnemy == "skeleton")
        {
            enemyHp = 1;
            enemyDmg = 1;
            enemyPts = 10;
            enemyName = "S";
        }
        else if (currentEnemy == "goblin")
        {
            enemyHp = 2;
            enemyDmg = 2;
            enemyPts = 20;
            enemyName = "G";
        }
        else if (currentEnemy == "orc")
        {
            enemyHp = 3;
            enemyDmg = 3;
            enemyPts = 30;
            enemyName = "O";
        }
        else if (currentEnemy == "giant spider")
        {
            enemyHp = 3;
            enemyDmg = 3;
            enemyPts = 40;
            enemyName = "X";
        }
        else if (currentEnemy == "vampire")
        {
            enemyHp = 4;
            enemyDmg = 4;
            enemyPts = 50;
            enemyName = "V";
        }
        else if (currentEnemy == "troll")
        {
            enemyHp = 4;
            enemyDmg = 4;
            enemyPts = 60;
            enemyName = "T";
        }
    }

    public void Combat(Player player, Potion potion, Score score)
    {
        Console.WriteLine("Combat started");
        combatOn = true;
        while (combatOn)
        {
            Random rnd = new Random();
            int playerDmg = rnd.Next(1, 7);
            int playerRoll = rnd.Next(1, 7);
            Console.WriteLine($"Combat round {round} | Hero hp: {player.playerHp} | Enemy hp: {enemyHp} | Enemy Type: {currentEnemy} ");
            Console.WriteLine($"You roll {playerRoll}...");
            round += 1;

            if (playerRoll >= enemyDmg)
            {
                enemyHp -= playerDmg;

                Console.WriteLine($"The enemy is hit and takes {playerDmg} damage.");
            }
            else
            {
                for (int i = 1; i <= enemyDmg; i++)
                {
                    player.takeDamage();
                }

                Console.WriteLine($"You are hit and take {enemyDmg} damage.");
            }
            if (player.playerHp <= 0)
            {
                Console.WriteLine("You died!");
                Console.WriteLine("Game Over");
                combatOn = false;
            }
            if (enemyHp <= 0)
            {
                if (currentEnemy == "skeleton") { score.AddScore(10); }
                else if (currentEnemy == "goblin") { score.AddScore(20); }
                else if (currentEnemy == "orc") { score.AddScore(30); }
                else if (currentEnemy == "giant spider") { score.AddScore(40); }
                else if (currentEnemy == "vampire") { score.AddScore(50); }
                else if (currentEnemy == "troll") { score.AddScore(60); }
                
                gameMessage = "Enemy slained!";
                Coordinates[] arrayTemp = player.GetBodyCoordinates().Concat(potion.GetCoordinatesArray()).ToArray();
                Respawn(arrayTemp);
                
                
                round = 1;
                combatOn = false;
            }
        }
    }
    public void Respawn(Coordinates[] preventCoordinates) 
    {
        Coordinates newCoordinate;
        do
        {
            newCoordinate = Coordinates.Random(grid.columns, grid.rows);

        } while (Array.Exists(preventCoordinates, c => c.Equals(newCoordinate)));


        coordinates = newCoordinate;
        GetEnemyType();
    }

    public void Draw()
    {
        Vector2 worldPosition = grid.GridToWorld(coordinates);
        worldPosition += new Vector2(10,2); // centre la position au milieu de la cellule
        Raylib.DrawTextEx(gameFont, enemyName, new Vector2((int)worldPosition.X, (int)worldPosition.Y), grid.cellSize,1, Color.Red);
    }
}