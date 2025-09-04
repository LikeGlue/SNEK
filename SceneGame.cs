using Raylib_cs;
using System.Numerics;

public class SceneGame : Scene
{
    const int screenWidth = 800;
    const int screenHeight = 480;
    private Grid<bool> grid;
    private Player player;
    private Potion potion;
    private Enemy enemy;
    private Score score = new Score();
    private int gridWidth = 15;
    private int gridHeight = 8;
    private int gridSize = 48;
    private int gridX = 40;
    private int gridY = 40;
    private string gameMessage;
    private int gameMessagePos;

    
    //CAM
    Camera2D camera = new Camera2D();
    CameraShake camShake = new CameraShake();

    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 20, null, 250);
    GameOver gameOver = new GameOver();

    private bool isRunning; // Game loop
    
    
    private Timer moveTimer;

    public SceneGame()
    {
        grid = new Grid<bool>(gridWidth, gridHeight, gridSize, new Vector2(gridX, gridY));
        player = new Player(new Coordinates(gridWidth/2, gridHeight/2), grid);
        potion = new Potion(grid);
        enemy = new Enemy(grid);
        moveTimer = new Timer((float)player.moveSpeed, OnMoveTimerStarted);
        isRunning = true;     
    }
     
    public override void Load()
    {
        Console.WriteLine("Loading game...");
        gameMessage = "Fight your way out of the dungeon...";
        enemy.GetEnemyType();
        Coordinates[] arrayPotionTemp = player.GetBodyCoordinates().Concat(enemy.GetCoordinatesArray()).ToArray();
        potion.Respawn(arrayPotionTemp);
        Coordinates[] arrayEnemyTemp = player.GetBodyCoordinates().Concat(potion.GetCoordinatesArray()).ToArray();
        enemy.Respawn(arrayEnemyTemp);


        gameMessagePos = gridHeight * gridSize + gridSize;
        camera.Target = new Vector2(0, 0);
        camera.Offset = new Vector2(0, 0);
        camera.Rotation = 0f;
        camera.Zoom = 1f;
    }

    public void OnMoveTimerStarted()
    {
        if (isRunning == true)
        {
            player.Move();

            if (player.IsCollidingWithSelf() || player.IsOutOfBounds())
            {
                Console.WriteLine("Game Over");
                isRunning = false;
                player.Pause();
            }

            if (player.IsCollidingWithPotion(potion))
            {
                camShake.StartShake(0.15f, 2f);
                Coordinates[] arrayTemp = player.GetBodyCoordinates().Concat(enemy.GetCoordinatesArray()).ToArray();
                
                potion.Respawn(arrayTemp);
                player.Grow();
                player.SpeedUp();

                if (player.playerHp < player.playerMaxHp) { gameMessage = $"You gain an extra HP.";}
                else {gameMessage = $"Your HP is already full.";}
            }

            if (player.IsCollidingWithEnemy(enemy))
            {
                camShake.StartShake(0.15f, 3f);
                

                player.Pause();
                enemy.Combat(player,potion,score);
                if (player.playerHp <= 0) {isRunning = false;}
                gameMessage = $"Incoming {enemy.currentEnemy}";
            }
        }
    }

    public override void Update(float deltaTime)
    {
        camShake.UpdateShake(ref camera);
        moveTimer.Update(deltaTime);

        //Console.WriteLine($"Updating game w/ DT {deltaTime}...");
        player.ChangeDirection(GetInputDirection());
        gameOver.Update();
    }

    public override void Draw()
    {
        
        Raylib.BeginMode2D(camera);
        grid.Draw();
        player.Draw();
        enemy.Draw();
        potion.Draw();
        score.Draw();
        Raylib.DrawTextEx(gameFont, gameMessage, new Vector2(gridX, gameMessagePos), 20,1, Color.RayWhite);
        Raylib.DrawRectangleLines(gridX, gridY, gridWidth * gridSize, gridHeight * gridSize, Color.RayWhite);


        if (isRunning == false)
        {
            gameOver.Draw();
        }
        Raylib.EndMode2D();
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

