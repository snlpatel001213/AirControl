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
    
    [MenuItem("Air Control/Build/BuildAll")]
    public static void BuildAll ()
    {
        BuildLinux();
        BuildWindows();
        BuildMac();
        BuildWebGL();
    }
    
    [MenuItem("Air Control/Build/LinuxBuild")]
    public static void BuildLinux ()
    {
        // Get filename.
        String OS = "linux";
        String appName = releaseVersion;
        string buildPath = "Build/Linux";
        string[] levels = new string[] {"Assets/Scene/v0.0.6-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        BuildPipeline.BuildPlayer(levels, Path.Join(buildPath,appName+".x86_64"), BuildTarget.StandaloneLinux64, BuildOptions.None);
    }
    [MenuItem("Air Control/Build/WindowsBuild")]
    public static void BuildWindows ()
    {
        // Get filename.
        String OS = "windows";
        String appName = releaseVersion;
        string buildPath = "Build/Windows";
        string[] levels = new string[] {"Assets/Scene/v0.0.6-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        BuildPipeline.BuildPlayer(levels, Path.Join(buildPath,appName+".exe"), BuildTarget.StandaloneWindows, BuildOptions.None);
    }
    [MenuItem("Air Control/Build/MacBuild")]
    public static void BuildMac ()
    {
        // Get filename.
        String OS = "mac";
        String appName = releaseVersion;
        string buildPath = "Build/Mac";
        string[] levels = new string[] {"Assets/Scene/v0.0.6-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        BuildPipeline.BuildPlayer(levels, Path.Join(buildPath,appName+".app"), BuildTarget.StandaloneOSX, BuildOptions.None);
    }
    [MenuItem("Air Control/Build/WebGLBuild")]
    public static void BuildWebGL ()
    {
        // Get filename.
        String OS = "webgl";
        String appName = releaseVersion;
        string buildPath = "Build/WebGL";
        string[] levels = new string[] {"Assets/Scene/v0.0.6-AirControl.unity"};
        //Create directory , remove existing
        if (Directory.Exists(buildPath))
        {
            CommonFunctions.clearFolder(buildPath);
        }
        DirectoryInfo di  = Directory.CreateDirectory(buildPath);
        // Build player.
        BuildPipeline.BuildPlayer(levels, Path.Join(buildPath,appName), BuildTarget.WebGL, BuildOptions.None);
    }


}
