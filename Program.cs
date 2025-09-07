using Raylib_cs;

//   WISHLIST:
// - Turn based combat
// - Move enemies
// - Add dungeon door on condition

// Generation du personnage et du système de combat basé sur "DUNGEON CRAVVL" de Rémy Gautreau. ==> hobbyhyena.itch.io

const int screenWidth = 800;
const int screenHeight = 480;

Raylib.InitWindow(screenWidth, screenHeight, "DUNGEON SNEK");
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
