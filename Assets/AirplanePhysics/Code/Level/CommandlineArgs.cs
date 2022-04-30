 using UnityEngine;
 using System.Collections;
 using System;

 /// <summary>
 ///  open CMDTest.app/ --args -myarg andstuff and more
 /// </summary>
 public class CommandlineArgs : MonoBehaviour 
 {    
     static string cmdInfo = "";
 
     void Awake () 
     {
         string[] arguments = Environment.GetCommandLineArgs();
         foreach(string arg in arguments)
         {
             cmdInfo += arg.ToString() + "\n ";
             Debug.Log(cmdInfo);
         }
     }
     
     void OnGUI()
     {
         Rect r = new Rect(5,5, 800, 500);
         GUI.Label(r, cmdInfo);
     }
 }
