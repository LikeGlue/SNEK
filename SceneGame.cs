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
    private Treasure treasure;
    private Score score = new Score();
    private float scoreToReach;
    private int currentScore;
    private int gridWidth = 15;
    private int gridHeight = 8;
    private int gridSize = 48;
    private int gridX = 40;
    private int gridY = 40;
    private string gameMessage;
    private string gameMessageT; // message du trésor
    private int gameMessagePos;
    private int gameMessageTPosX;
    private float scoreIncrement;
    private float scoreIncrementCoeff;
    private Color gameMessageColor;
    private bool gameMessagePotion;
    

    //CAM
    Camera2D camera = new Camera2D();
    CameraShake camShake = new CameraShake();

    Font gameFont = Raylib.LoadFontEx("alagard.ttf", 20, null, 250);
    GameOver gameOver = new GameOver();

    private bool isRunning; // Game loop
    private bool isTreasure; // Affichage du trésor
    
    private Timer moveTimer;

    public SceneGame()
    {
        grid = new Grid<bool>(gridWidth, gridHeight, gridSize, new Vector2(gridX, gridY));
        player = new Player(new Coordinates(gridWidth/2, gridHeight/2), grid);
        potion = new Potion(grid);
        enemy = new Enemy(grid);
        treasure = new Treasure(grid);
        moveTimer = new Timer((float)player.moveSpeed, OnMoveTimerStarted);
        isRunning = true;
        isTreasure = false;

    }
     
    public override void Load()
    {
        gameMessage = "Fight your way out of the dungeon...";
        enemy.GetEnemyType();
        Coordinates[] arrayPotionTemp = player.GetBodyCoordinates().Concat(enemy.GetCoordinatesArray()).Concat(treasure.GetCoordinatesArray()).ToArray();
        potion.Respawn(arrayPotionTemp);
        Coordinates[] arrayEnemyTemp = potion.GetCoordinatesArray().Concat(player.GetBodyCoordinates()).Concat(treasure.GetCoordinatesArray()).ToArray();
        enemy.Respawn(arrayEnemyTemp);
        gameMessagePos = gridHeight * gridSize + gridSize;
        gameMessageTPosX = screenWidth/2 - 100;
        camera.Target = new Vector2(0, 0);
        camera.Offset = new Vector2(0, 0);
        camera.Rotation = 0f;
        camera.Zoom = 1f;
        scoreToReach = 100;
        scoreIncrement = 200;
        scoreIncrementCoeff = 1.2f;

    }

    public void OnMoveTimerStarted()
    {
        currentScore = score.GetScore();

        if (isRunning == true)
        {
        
            player.Move();
            

            if (player.IsCollidingWithSelf() || player.IsOutOfBounds())
            {
                Console.WriteLine("Game Over");
                isRunning = false;
                player.Pause();
            }

            if (player.IsCollidingWithTreasure(treasure) && isTreasure == true )
            {
                camShake.StartShake(0.15f, 3f);
                treasure.GetTreasureType();
                scoreToReach += scoreIncrement * scoreIncrementCoeff;
                player.SpeedUp();
                gameMessagePotion = false;


                if (treasure.currentTreasure == "legendary")
                {
                    player.heroDmg += 1;
                    gameMessageT = $"Treasure: Blessing! your weapon \n gets you +{player.heroDmg} damage";
                }
                else if (treasure.currentTreasure == "potion")
                {
                    player.playerMaxHp++;
                    for (int i = 1; i <= player.playerMaxHp - player.playerHp; i++)
                    {
                        
                            player.Grow();
                        
                    }
                    gameMessageT = $"Treasure: your Max HP increases by 1";
                }
                else if (treasure.currentTreasure == "gold")
                {
                    
                    score.AddScore(100);
                    gameMessageT = $"Treasure: you get 100 coins!";
                }
                if (isTreasure == true)
                {
                    Coordinates[] arrayTreasureTemp = player.GetBodyCoordinates().Concat(potion.GetCoordinatesArray()).Concat(treasure.GetCoordinatesArray()).ToArray();
                    treasure.Respawn(arrayTreasureTemp);
                }
                isTreasure = false;
            }

            if (player.IsCollidingWithPotion(potion))
            {

                camShake.StartShake(0.15f, 2f);
                Coordinates[] arrayTemp = player.GetBodyCoordinates().Concat(enemy.GetCoordinatesArray()).ToArray();
                potion.Respawn(arrayTemp);
                player.Grow();
                player.SpeedUp();
                gameMessagePotion = true;
                if (player.playerHp < player.playerMaxHp) { gameMessageT = $"You gain an extra HP.";}
                else {gameMessageT = $"Your HP is already full, but you still rush";}
            }

            if (player.IsCollidingWithEnemy(enemy))
            {
                camShake.StartShake(0.15f, 5f);
                player.SpeedUp();

                enemy.Combat(player,potion,score);
                if (player.playerHp <= 0) {isRunning = false; player.Pause();}
                
            }
        }
    }

    public void TreasureSpawnCheck(int score)
    {
        if (score >= scoreToReach)
        {
            isTreasure = true;
        }
    }

    public override void Update(float deltaTime)
    {
        camShake.UpdateShake(ref camera);
        moveTimer.Update(deltaTime);
        player.ChangeDirection(GetInputDirection());
        isTreasure = false;
        TreasureSpawnCheck(currentScore);
        if (isRunning == false)
        {
            gameOver.Update();
        }
        gameMessage = $"Incoming {enemy.currentEnemy}\n" +
                $"HP: {enemy.enemyHp} | DMG: {enemy.enemyDmg}";
        if (gameMessagePotion == true)
        {
            gameMessageColor = Color.Green;
        }
        else
        {
            gameMessageColor = Color.Gold;
        }
    }

    public override void Draw()
    {
        
        Raylib.BeginMode2D(camera);
        grid.Draw();
        player.Draw();
        enemy.Draw();
        potion.Draw();

        if (isTreasure == true)
        {
            treasure.Draw();
        }

        score.Draw();
        Raylib.DrawTextEx(gameFont, gameMessage, new Vector2(gridX, gameMessagePos), 20,1, Color.RayWhite);
        Raylib.DrawTextEx(gameFont, gameMessageT, new Vector2(gameMessageTPosX, gameMessagePos), 20,1, gameMessageColor);
        Raylib.DrawRectangleLines(gridX, gridY, gridWidth * gridSize, gridHeight * gridSize, Color.RayWhite);

        if (isRunning == false)
        {
            gameOver.Draw();
        }
        Raylib.EndMode2D();
    }

    public override void UnLoad()
    {
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

