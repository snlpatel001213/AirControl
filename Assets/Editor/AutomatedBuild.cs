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
        BuildWindows();
        BuildMac();
        BuildWebGL();
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
        string buildPath = "Build/Linux";
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
    /// Build for Windows
    /// </summary>
    [MenuItem("Air Control/Build/WindowsBuild")]
    public static void BuildWindows ()
    {
        // Get filename.
        String OS = "windows";
        UnityEngine.Debug.Log("Starting build for - "+OS);
        String appName = releaseVersion;
        string buildPath = "Build/Windows";
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
        string buildPath = "Build/Mac";
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
        string buildPath = "Build/WebGL";
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
        string buildPath = "Build/UnityPackage";
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        string sceneName = SceneManager.GetActiveScene().name;
        var dependencies = AssetDatabase.GetDependencies("Assets/Scene/"+sceneName+".unity");

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