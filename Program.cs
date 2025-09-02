using Raylib_cs;

const int screenWidth = 800;
const int screenHeight = 480;

Raylib.InitWindow(screenWidth, screenHeight, "SNEK");
Raylib.SetTargetFPS(60);


SceneManager.Load<SceneMenu>();

while (!Raylib.WindowShouldClose())
{   //UPDATE
    SceneManager.Update(Raylib.GetFrameTime());





    //DRAW
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);

    SceneManager.Draw();

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
