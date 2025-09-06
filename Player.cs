using Raylib_cs;
using System;
using System.Numerics;

//####################################
//TO DO:
// - Turn based combat
// - Move enemies
// - Add dungeon door or treasure on condition
//####################################


public class Player
{
    Grid<bool> grid;

    Queue<Coordinates> body = new Queue<Coordinates>(); // Corps du snake
    Coordinates direction = Coordinates.right;
    Coordinates nextDirection;

    public Coordinates head => lastOrNull(); //body.Last();
    public double moveSpeed { get; private set; } = 0.3;
    private bool isGrowing = false;
    public bool isMoving = true;
    public bool isFighting = false;
    public int playerHp => body.Count();    
    public int playerMaxHp = 6;
    public int heroDmg;
    public int playerSegments;
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 50, null, 250);

    public string name;
    public string weapon;


    public Player(Coordinates start, Grid<bool> grid, int startSize = 6)
    {
        this.grid = grid;
        for (int i = startSize - 1; i >= 0; i--)
        {
            body.Enqueue(start - direction * i); // On de la tête -VERS-> la queue
        }
        nextDirection = direction;
    }

    public Coordinates lastOrNull()
    {
        if (body.Count == 0)
        {
            return Coordinates.zero;
        }
        return body.Last();
    }

    public bool CheckQueue()
    {
        return body.Any();
    }

    public void takeDamage()
    {
        if (CheckQueue())
        {
            body.Dequeue();
        }
    }

    public void Pause()
    {
        
        isFighting = true;
        isGrowing = false;
        isMoving = false;
        
    }

    public void Resume()
    {
        isGrowing = false;
        isMoving = true;
        isFighting = false;
        //nextDirection = direction;
    }


    public void Move()
    {
        if (body.Count > 0)
        { 
            if (isMoving && !isFighting)
            {
                direction = nextDirection;
                body.Enqueue(body.Last() + direction);
                if (!isGrowing)
                {
                    body.Dequeue();
                }
                else
                {
                    isGrowing = false;
                }
            }
        }
        else
        {
            Pause();
        }
    }

    public void StopBodyTrail()
    {
        direction = Coordinates.zero;
        if (CheckQueue())
        {
            body.Any();
        }
    }

    public void Draw()
    {
        foreach (Coordinates segment in body)
        {
            Vector2 position = grid.GridToWorld(segment);
            Raylib.DrawTextEx(gameFont, "@", new Vector2((int)position.X + 6, (int)position.Y - 3), grid.cellSize,1, Color.White);
            Raylib.DrawTextEx(gameFont,$"Hero hp: {playerHp}/{playerMaxHp} ", new Vector2(150, 10), 20,1, Color.White);

        }
    }

    public bool IsCollidingWithPotion(Potion potion)
    {
        return head == potion.coordinates;
    }

    public bool IsCollidingWithTreasure(Treasure treasure)
    {
        return head == treasure.coordinates;
    }

    public bool IsCollidingWithEnemy(Enemy enemy)
    {
        return head == enemy.coordinates;
    }

    public bool IsCollidingWithSelf()
    {
        return body.Count != body.Distinct().Count(); 
        // In C#, Distinct().Count() is a method chain used to count the number of unique elements in a collection,
        // such as an array or a list. The Distinct() method filters out duplicate values,
        // and Count() then returns the total number of these unique elements.
        // Ici, la méthode vérifie si les éléments ne sont pas egaux => *il faut que la classe implémente bien l'override de Equals*
    }

    public bool IsOutOfBounds()
    {
        return head.column < 0 || head.column >= grid.columns || head.row < 0 || head.row >= grid.rows;
    }

    public void Grow()
    {
        isGrowing = true;
        if (playerHp >= playerMaxHp)
        {
            isGrowing = false;
        }
    }

    public void SpeedUp()
    {
        moveSpeed *= 0.9; // couplé à un timer, + le timer est court + c'est rapide - *à tester*
    }

    public void ChangeDirection(Coordinates newDirection)
    {
        if (newDirection == -direction) 
        {
            return; // empêche la direction inverse
        }
        if (newDirection == Coordinates.zero) // empêche la direction de ne pas changer
        {
            return;
        }
        nextDirection = newDirection;
    }

    public Coordinates[] GetBodyCoordinates()
    {
        return body.ToArray();
    }


}