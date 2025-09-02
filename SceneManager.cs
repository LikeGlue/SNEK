public static class SceneManager
{
    private static Scene? currentScene;

    public static void Load<T>() where T : Scene, new() // Le type générique (T = scene générique par ex.) est -enfant- de Scene , new() pou l'instancier soi-même
    {
        if (currentScene != null) // CurrentScene existe déja alors Unload sinon ordre suivant du code...
        {
            currentScene.UnLoad();
        }
        currentScene = new T(); // Instanciation de nouvelle scene
        currentScene.Load();
    }

    public static void Update(float deltaTime)
    {
        currentScene?.Update(deltaTime);
    }

    public static void Draw()
    {
        currentScene?.Draw();
    }
}