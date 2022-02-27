using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System;
using System.IO;
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
        string[] levels = new string[] {"Assets/Scene/v"+releaseVersion+"-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
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
        string[] levels = new string[] {"Assets/Scene/v"+releaseVersion+"-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
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
        string[] levels = new string[] {"Assets/Scene/v"+releaseVersion+"-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
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
        string[] levels = new string[] {"Assets/Scene/v"+releaseVersion+"-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        BuildPipeline.BuildPlayer(levels, System.IO.Path.Combine(buildPath,appName), BuildTarget.WebGL, BuildOptions.None);
    }

    public static void createUnityPackage()
    {
             GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube); 
     
              Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
             string filePath = "Assets/Prefabs/";
             FileInfo file = new FileInfo(filePath);
             file.Directory.Create();
     
             string  prefabfilename = filePath + obj.name + ".prefab";
             UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab(prefabfilename);
     
             PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
     
             string exportPath = "Assets/" + obj.name + ".unitypackage";
             
             AssetDatabase.ExportPackage(prefabfilename, exportPath, 
            ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);
       
     
    }


}