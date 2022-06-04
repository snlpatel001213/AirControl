using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;
using Commons;

public class AutomatedBuild : MonoBehaviour
{
    // 0.1.0-AirControl+PLATFORM
    public static String releaseVersion = "v"+CommonFunctions.GET_VERSION()+"-AirControl";

    /// <summary>
    /// Buld for all the platforms
    /// </summary>
    /// <returns></returns>
    [MenuItem("Air Control/Build/BuildAll")]
    public static void BuildAll ()
    {
        BuildLinux();
        BuildLinuxHeadless();
        BuildWindows();
        BuildMac();
        // BuildWebGL();
        SwitchBuild2Default();
    }
    
    /// <summary>
    /// Build for Linux
    /// </summary>
    [MenuItem("Air Control/Build/LinuxBuild")]
    public static void BuildLinux ()
    {
        // Get filename.
        String OS = "linux";
        UnityEngine.Debug.Log("Starting build for - "+OS);
        String appName = releaseVersion;
        string buildPath = System.IO.Path.Combine("Build",CommonFunctions.GET_VERSION(),"Linux");
        string sceneName = SceneManager.GetActiveScene().name;
        string[] levels = new string[] {"Assets/Scene/"+sceneName+".unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
        BuildPipeline.BuildPlayer(levels, System.IO.Path.Combine(buildPath,appName+".x86_64"), BuildTarget.StandaloneLinux64, BuildOptions.None);
    }

    /// <summary>
    /// Build for  Linux - headless
    /// </summary>
    [MenuItem("Air Control/Build/LinuxBuildHeadless")]
    public static void BuildLinuxHeadless ()
    {
        // Get filename.
        String OS = "linux";
        UnityEngine.Debug.Log("Starting build for - "+OS);
        String appName = releaseVersion;
        string buildPath = System.IO.Path.Combine("Build",CommonFunctions.GET_VERSION(),"LinuxHeadless");
        string sceneName = SceneManager.GetActiveScene().name;
        string[] levels = new string[] {"Assets/Scene/"+sceneName+".unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
        BuildPipeline.BuildPlayer(levels, System.IO.Path.Combine(buildPath,appName+".x86_64"), BuildTarget.StandaloneLinux64, BuildOptions.EnableHeadlessMode);
    }

    /// <summary>
    /// Build for Windows
    /// </summary>
    [MenuItem("Air Control/Build/WindowsBuild")]
    public static void BuildWindows ()
    {
        // Get filename.
        String OS = "windows";
        UnityEngine.Debug.Log("Starting build for - "+OS);
        String appName = releaseVersion;
        string buildPath = System.IO.Path.Combine("Build",CommonFunctions.GET_VERSION(),"Windows");
        string sceneName = SceneManager.GetActiveScene().name;
        string[] levels = new string[] {"Assets/Scene/"+sceneName+".unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
        BuildPipeline.BuildPlayer(levels, System.IO.Path.Combine(buildPath,appName+".exe"), BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    /// <summary>
    /// Build for Mac
    /// </summary>
    [MenuItem("Air Control/Build/MacBuild")]
    public static void BuildMac ()
    {
        // Get filename.
        String OS = "mac";
        UnityEngine.Debug.Log("Starting build for - "+OS);
        String appName = releaseVersion;
        string buildPath = System.IO.Path.Combine("Build",CommonFunctions.GET_VERSION(),"Mac");
        string sceneName = SceneManager.GetActiveScene().name;
        string[] levels = new string[] {"Assets/Scene/"+sceneName+".unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
        BuildPipeline.BuildPlayer(levels, System.IO.Path.Combine(buildPath,appName+".app"), BuildTarget.StandaloneOSX, BuildOptions.None);

    }

    /// <summary>
    /// Build for WebGL
    /// </summary>
    [MenuItem("Air Control/Build/WebGLBuild")]
    public static void BuildWebGL ()
    {
        // Get filename.
        String OS = "webgl";
        UnityEngine.Debug.Log("Starting build for - "+OS);
        String appName = releaseVersion;
        string buildPath = System.IO.Path.Combine("Build",CommonFunctions.GET_VERSION(),"WebGL");
        string sceneName = SceneManager.GetActiveScene().name;
        string[] levels = new string[] {"Assets/Scene/"+sceneName+".unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.WebGL);
        BuildPipeline.BuildPlayer(levels, System.IO.Path.Combine(buildPath,appName), BuildTarget.WebGL, BuildOptions.None);
    }
    
    [MenuItem("Air Control/Build/Unitypackage")]
    static void GetAllDependenciesForScenes()
    {
        String OS = "UnityPackage";
        String appName = releaseVersion;
        string buildPath = System.IO.Path.Combine("Build",CommonFunctions.GET_VERSION(),"UnityPackage");
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        string sceneName = SceneManager.GetActiveScene().name;

        var allScenes = AssetDatabase.FindAssets("t:Scene");
        string[] allPaths = new string[allScenes.Length];
        int curSceneIndex = 0;

        foreach (var guid in allScenes)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            allPaths[curSceneIndex] = path;
            ++curSceneIndex;
        }

        var dependencies = AssetDatabase.GetDependencies(allPaths);

        var dependenciesString =  new List<string>();

        foreach (var curDependency in dependencies)
        {
            dependenciesString.Add(curDependency);
        }

        UnityEngine.Debug.Log("All dependencies for Scenes in Project: " + dependenciesString);
        AssetDatabase.ExportPackage(dependenciesString.ToArray(), System.IO.Path.Combine(buildPath,appName+".unitypackage"),
            ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
        UnityEngine.Debug.Log("Completed build for - "+OS );
    }

    [MenuItem("Air Control/Build/SnapRelease")]
    public static void SnapRelease ()
    {
        // Get filename.
        String OS = "snap";
        UnityEngine.Debug.Log("Starting build for - "+OS);
        String appName = releaseVersion;
        string buildPath = System.IO.Path.Combine(CommonFunctions.GET_VERSION(),"snap/snap");
        string sceneName = SceneManager.GetActiveScene().name;
        string[] levels = new string[] {"Assets/Scene/"+sceneName+".unity"};
        //Create directory , remove existing
        // if (Directory.Exists(buildPath))
        // {
        //     CommonFunctions.clearFolder(buildPath);
        // }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
        BuildPipeline.BuildPlayer(levels, System.IO.Path.Combine(buildPath,appName+".x86_64"), BuildTarget.StandaloneLinux64, BuildOptions.None);
    }

   
    // [MenuItem("Air Control/Build/CreateGitZip")]
    // public static void CreateGitZip()
    // {
    //     try
    //     {
    //         ProcessStartInfo procStartInfo = new ProcessStartInfo("/usr/bin/git", "archive HEAD -o ${PWD##*/}.zip");

    //         procStartInfo.RedirectStandardError = procStartInfo.RedirectStandardInput = procStartInfo.RedirectStandardOutput = true;
    //         procStartInfo.UseShellExecute = false;
    //         procStartInfo.CreateNoWindow = true;

    //         procStartInfo.WorkingDirectory = "/home/supatel/Games/New_AirControl_2020/";
            

    //         Process proc = new Process();
    //         proc.StartInfo = procStartInfo;
    //         proc.Start();

    //         StringBuilder sb = new StringBuilder();
    //         proc.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
    //         {
    //             sb.AppendLine(e.Data);
    //         };
    //         proc.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
    //         {
    //             sb.AppendLine(e.Data);
    //         };

    //         proc.BeginOutputReadLine();
    //         proc.BeginErrorReadLine();
    //         proc.WaitForExit();
    //         UnityEngine.Debug.Log($"Error in command: {sb.ToString()}");
    //     }
    //     catch (Exception objException)
    //     {
    //         UnityEngine.Debug.Log($"Error in command: {objException.Message}");
    //     }
    // }

    /// <summary>
    /// After build switch back to the original edior plat form based on the os
    /// </summary>
    public static void SwitchBuild2Default(){
        #if UNITY_EDITOR_LINUX
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
        #endif
        #if UNITY_EDITOR_OSX
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
        #endif
        #if UNITY_EDITOR_WIN
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
        #endif


    }


}