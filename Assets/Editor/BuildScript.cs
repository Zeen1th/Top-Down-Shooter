using UnityEditor;
using UnityEngine;

public class BuildScript
{
    [MenuItem("Build/Build Project")]
    public static void PerformBuild()
    {
        string[] scenes = { "Assets/Scenes/Main.unity" };
        string path = "Builds/StandaloneWindows64/MyGame.exe";

        BuildPipeline.BuildPlayer(scenes, path, BuildTarget.StandaloneWindows64, BuildOptions.None);
        Debug.Log("Build completed.");
    }
}
