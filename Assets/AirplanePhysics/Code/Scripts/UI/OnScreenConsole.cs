using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Show log On screen
/// https://answers.unity.com/questions/125049/is-there-any-way-to-view-the-console-in-a-build.html
/// </summary>
 public class OnScreenConsole : MonoBehaviour
 {
     string myLog = "Use key `l` to enable or disable onscreen logs";
     string filename = "";
     bool doShow = true;
     int kChars = 700;
     void OnEnable() { Application.logMessageReceived += Log; }
     void OnDisable() { Application.logMessageReceived -= Log; }
     void Update() { if (Input.GetKeyDown(KeyCode.L)) { doShow = !doShow; } }
     
    
    /// <summary>
    /// This is a simple logging function that will write to the console and to a file.
    /// </summary>
    /// <param name="logString"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>

    public void Log(string logString, string stackTrace, LogType type)
    {
        // for onscreen...
         myLog = myLog + "\n" + logString;
         if (myLog.Length > kChars) { myLog = myLog.Substring(myLog.Length - kChars); }
        // No logs will be writtten to the disk to reduce performace battneck
        // Below is the experimental code to write logs to file
        //  if (filename == "")
        //  {
        //      string d = System.Environment.GetFolderPath(
        //         System.Environment.SpecialFolder.Desktop) + "/Aircontrol_Logs";
        //      System.IO.Directory.CreateDirectory(d);
             
        //      string r = DateTime.Now.ToString("MM-dd-yyyy");
        //      filename = d + "/log-" + r + ".txt";
        //      Debug.Log("File Path" + filename);
        //  }
        //  try { System.IO.File.AppendAllText(filename, logString + "\n"); }
        //  catch { }
    }

    /// <summary>
    /// This is the code that draws the console on the screen.
    /// </summary>    
    void OnGUI()
    {
        if (!doShow) { return; }
        GUI.backgroundColor = new Color(1f, 1f, 1f, 0f);    
        // GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
        //     new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
        GUI.TextArea(new Rect(10, 10, 500, 200), myLog);
    }
 }