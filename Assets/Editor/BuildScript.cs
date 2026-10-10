using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildScript
{
    public static void BuildLinux()
    {
        string[] scenes = {
            "Assets/Scenes/Menu.unity",
            "Assets/Scenes/MainGame.unity"
        };

        BuildReport report = BuildPipeline.BuildPlayer(
            scenes,
            "Builds/Linux/MosquitoOverkill.x86_64",
            BuildTarget.StandaloneLinux64,
            BuildOptions.None
        );

        Debug.Log("Build result: " + report.summary.result);
    }
}