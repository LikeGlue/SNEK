public class Timer
{
    private float elapsedTime = 0f;
    public float duration { get; private set; }
    public bool isLooping;
    public bool isRunning { get; private set; }

    public Action? Callback { private get; set; } // private get car il n'y a que le timer qui executera le "callback", "callback" sera remplacée par une methode dans le constructeur (ici new Timer) 

    public Timer(float duration, Action? callback = null, bool isLooping = true)
    {
        this.duration = duration;
        this.isLooping = isLooping;
        this.Callback = callback;
        elapsedTime = 0f;
        isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (!isRunning)
        {
            return;
        }
        elapsedTime += deltaTime;

        if (elapsedTime >= duration)
        {
            Callback?.Invoke();
            if (isLooping)
            {
                elapsedTime = 0f;
            }
            else
            {
                Stop();
            }
        }
    }

    public void Start()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void Pause()
    {
        isRunning = false;
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void Reset()
    {
        elapsedTime = 0f;
    }

    public void setDuration(float newDuration)
    {
        duration = newDuration;
    }

    public bool IsFinished()
    {
        return elapsedTime >= duration;
    }
}

