using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class TutorialSceneAutoBuilder
{
    private const string FlagRelativePath = "Settings/tutorial_autobuild.flag";
    private static double nextCheckTime;
    private static bool reportedWaiting;

    static TutorialSceneAutoBuilder()
    {
        EditorApplication.delayCall += RunIfRequested;
    }

    private static void RunIfRequested()
    {
        string flagPath = Path.Combine(Application.dataPath, FlagRelativePath);
        if (!File.Exists(flagPath))
        {
            EditorApplication.update -= RetryWhenReady;
            return;
        }

        if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
        {
            if (!reportedWaiting)
            {
                reportedWaiting = true;
                Debug.Log("Tutorial auto-build is waiting until Unity leaves Play Mode and finishes compiling.");
            }

            EditorApplication.update -= RetryWhenReady;
            EditorApplication.update += RetryWhenReady;
            return;
        }

        try
        {
            DeleteFlagAndMeta(flagPath);
            Debug.Log("Tutorial auto-build flag detected. Rebuilding tutorial scene.");
            TutorialSceneBuilder.BuildTutorialScene();
            TutorialSceneValidator.ValidateTutorialScene();
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private static void RetryWhenReady()
    {
        if (EditorApplication.timeSinceStartup < nextCheckTime)
        {
            return;
        }

        nextCheckTime = EditorApplication.timeSinceStartup + 1.0f;
        RunIfRequested();
    }

    private static void DeleteFlagAndMeta(string flagPath)
    {
        File.Delete(flagPath);

        string metaPath = flagPath + ".meta";
        if (File.Exists(metaPath))
        {
            File.Delete(metaPath);
        }
    }
}
