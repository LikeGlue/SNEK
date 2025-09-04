using Raylib_cs;
using System.Numerics;

public class CameraShake
{
    private float timer;
    private float shakeDuration;
    private float shakeMagnitude;

    public void StartShake(float duration, float magnitude)
    {
        timer = 0;
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void UpdateShake(ref Camera2D camera2D)
    { 
        if (timer < shakeDuration)
        {
            timer += Raylib.GetFrameTime();
        }

        Random random = new Random();
        if (timer < shakeDuration)
        {
            camera2D.Offset.X = (float)random.NextDouble() * shakeMagnitude;
            camera2D.Offset.Y = (float)random.NextDouble() * shakeMagnitude;
        }
        Console.WriteLine($"CAM SHAKE timer: {timer}");
    }  
}
