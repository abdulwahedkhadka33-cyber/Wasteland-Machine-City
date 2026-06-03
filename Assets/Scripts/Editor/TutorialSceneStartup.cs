using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class TutorialSceneStartup
{
    private const string ScenePath = "Assets/Scenes/Tutorial_01_AwakeningCorridor.unity";
    private const double StartupWatchSeconds = 30.0;
    private static double nextRetryTime;
    private static double watchUntilTime;

    static TutorialSceneStartup()
    {
        watchUntilTime = EditorApplication.timeSinceStartup + StartupWatchSeconds;
        EditorApplication.delayCall += OpenTutorialSceneIfUnityRestoredBackup;
        EditorApplication.update -= RetryWhenReady;
        EditorApplication.update += RetryWhenReady;
    }

    [MenuItem("Tools/Wasteland Mech City/Open Tutorial Scene")]
    public static void OpenTutorialScene()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            return;
        }

        EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
    }

    private static void OpenTutorialSceneIfUnityRestoredBackup()
    {
        if (EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorApplication.update -= RetryWhenReady;
            EditorApplication.update += RetryWhenReady;
            return;
        }

        Scene activeScene = SceneManager.GetActiveScene();
        string scenePath = activeScene.path.Replace('\\', '/');
        bool restoredTemporaryScene = scenePath.StartsWith("Temp/", System.StringComparison.OrdinalIgnoreCase);
        bool openedEmptyUntitledScene = string.IsNullOrEmpty(scenePath);

        if (!restoredTemporaryScene && !openedEmptyUntitledScene)
        {
            if (EditorApplication.timeSinceStartup > watchUntilTime)
            {
                EditorApplication.update -= RetryWhenReady;
            }

            return;
        }

        if (!System.IO.File.Exists(ScenePath))
        {
            return;
        }

        EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
    }

    private static void RetryWhenReady()
    {
        if (EditorApplication.timeSinceStartup < nextRetryTime)
        {
            return;
        }

        nextRetryTime = EditorApplication.timeSinceStartup + 0.5f;
        OpenTutorialSceneIfUnityRestoredBackup();
    }
}
