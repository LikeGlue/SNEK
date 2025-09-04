using Raylib_cs;
using System.Numerics;

public class Enemy
{
    public Coordinates coordinates { get; private set; }
    private Grid<bool> grid;
    public string currentEnemy;
    public int enemyHp;
    public int enemyDmg;
    public string enemyName = "E";
    public int round = 1;
    public bool combatOn = false;
    public enum EnemyType { goblin, orc, skeleton } // à intégrer
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 50, null, 250); 

    public Enemy(Grid<bool> grid) // ** warning présent **
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
        var enemyList = new List<string> { "goblin", "orc", "skeleton" };
        int index = random.Next(enemyList.Count);
        currentEnemy = enemyList[index];
        if (currentEnemy == "goblin")
        {
            enemyHp = 2;
            enemyDmg = 2;
            enemyName = "G";
        }
        else if (currentEnemy == "orc")
        {
            enemyHp = 3;
            enemyDmg = 3;
            enemyName = "O";
        }
        else if (currentEnemy == "skeleton")
        {
            enemyHp = 1;
            enemyDmg = 1;
            enemyName = "S";
        }
    }

    public void Combat(Player player, Score score)
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

                // TO DO *** enlever un segment par dégât
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
                Console.WriteLine("Enemy slained!");
                Respawn();
                score.AddScore(50);
                player.Resume();
                enemyHp = 2;
                round = 1;
                //snake.isMoving = true;
                combatOn = false;
            }
        }
    }

    public void Respawn()
    {
        coordinates = Coordinates.Random(grid.columns, grid.rows);
        GetEnemyType();
    }

    public void Draw()
    {
        Vector2 worldPosition = grid.GridToWorld(coordinates);
        worldPosition += new Vector2(10,2); // centre la position au milieu de la cellule
        Raylib.DrawTextEx(gameFont, enemyName, new Vector2((int)worldPosition.X, (int)worldPosition.Y), grid.cellSize,1, Color.Red);
    }
}

//public enum EnemyType { goblin, orc, skeleton }