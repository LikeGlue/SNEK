using Raylib_cs;
using System.Numerics;

public class SceneGame : Scene
{
    private Grid<bool> grid;
    private Snake snake;
    private Apple apple;
    private Enemy enemy;
    private Score score = new Score();
    private int gridWidth = 15;
    private int gridHeight = 8;
    private int gridSize = 48;
    private int gridX = 40;
    private int gridY = 40;
    private string gameMessage;
    private int gameMessagePos;
    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 20, null, 250);

    private Timer moveTimer;

    public SceneGame()
    {

        grid = new Grid<bool>(gridWidth, gridHeight, gridSize, new Vector2(gridX, gridY));
        snake = new Snake(new Coordinates(gridWidth/2, gridHeight/2), grid);
        apple = new Apple(grid);
        enemy = new Enemy(grid);
        moveTimer = new Timer((float)snake.moveSpeed, OnMoveTimerStarted);
    }
     
    public override void Load()
    {
        Console.WriteLine("Loading game...");
        gameMessage = "Fight your way out of the dungeon...";
        enemy.GetEnemyType();
        gameMessagePos = gridHeight * gridSize + gridSize;
    }

    public void OnMoveTimerStarted()
    {
        snake.Move();

        if (snake.IsCollidingWithSelf() || snake.IsOutOfBounds())
        {
            Console.WriteLine("Game Over");
        }

        if (snake.IsCollidingWithApple(apple))
        {
            gameMessage = $"You drank a potion and gained an extra HP.";
            apple.Respawn();
            snake.Grow();
            snake.SpeedUp();
            // Game controller to do(add score(1000))
        }

        if (snake.IsCollidingWithEnemy(enemy))
        {
            
            snake.Pause();
            enemy.Combat(snake, score);
            gameMessage = $"Incoming {enemy.currentEnemy}";
        }

        //if (snake. > 6)
        //{
        //    snake.playerHp = 6;
        //}

    }

    public override void Update(float deltaTime)
    {

        moveTimer.Update(deltaTime);

        //Console.WriteLine($"Updating game w/ DT {deltaTime}...");
        snake.ChangeDirection(GetInputDirection());
        
    }

    public override void Draw()
    {
        grid.Draw();
        snake.Draw();
        enemy.Draw();
        apple.Draw();
        score.Draw();
        Raylib.DrawTextEx(gameFont, gameMessage, new Vector2(gridX, gameMessagePos), 20,1, Color.RayWhite);
        Raylib.DrawRectangleLines(gridX, gridY, gridWidth * gridSize, gridHeight * gridSize, Color.RayWhite);
    }

    public override void UnLoad()
    {
        Console.WriteLine("Unloading game...");
    }

    private Coordinates GetInputDirection()
    {
        Coordinates dir = Coordinates.zero;
        if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up)) dir = Coordinates.up;
        if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down)) dir = Coordinates.down;
        if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left)) dir = Coordinates.left;
        if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right)) dir = Coordinates.right;

        return dir;
    }
}