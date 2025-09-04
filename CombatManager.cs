using Raylib_cs;

public class CombatManager
{
    private enum CombatState
    {
        Combat,
        Pause,
        NextRound,
        Leave
    }

    private CombatState currentState = CombatState.Combat;
    private int round = 1;
    public bool HasExited => currentState == CombatState.Leave;

    public void Update()
    {
        switch (currentState)
        {
            case CombatState.Combat:
                if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                {
                    currentState = CombatState.Pause;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.L))
                {
                    currentState = CombatState.Leave;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.R)) // Simulate end of round
                {
                    currentState = CombatState.NextRound;
                }
                break;

            case CombatState.Pause:
                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    currentState = CombatState.Combat;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.L))
                {
                    currentState = CombatState.Leave;
                }
                break;

            case CombatState.NextRound:
                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    round++;
                    currentState = CombatState.Combat;
                }
                break;

            case CombatState.Leave:
                // Transition out
                break;
        }
    }

    public void Draw()
    {
        switch (currentState)
        {
            case CombatState.Combat:
                Raylib.ClearBackground(Color.DarkGreen);
                Raylib.DrawText($"Round {round}: Combat ongoing! Press R to end round, ESC to pause.", 100, 280, 20, Color.Lime);
                break;

            case CombatState.Pause:
                Raylib.ClearBackground(Color.DarkGray);
                Raylib.DrawText("Game Paused. Press ENTER to continue or L to leave.", 200, 280, 20, Color.Yellow);
                break;

            case CombatState.NextRound:
                Raylib.ClearBackground(Color.DarkPurple);
                Raylib.DrawText($"Round {round} complete. Press ENTER to start next round.", 160, 280, 20, Color.Orange);
                break;

            case CombatState.Leave:
                Raylib.ClearBackground(Color.Black);
                Raylib.DrawText("Leaving combat... Returning to map.", 220, 280, 20, Color.Red);
                break;
        }

        
    }
}
