using UnityEngine;
using System;
using Utility;
using AirControl;

/// <summary>
///  open CMDTest.app/ --args -myarg andstuff and more
/// https://www.codeproject.com/Articles/3111/C-NET-Command-Line-Arguments-Parser
/// </summary>
namespace AirControl {

    public class CommandlineArgs: MonoBehaviour {
        
        static string cmdInfo = "";
        LevelControl level =  new LevelControl();
        
        void Awake() {
            string[] arguments = Environment.GetCommandLineArgs();
            Arguments CommandLine = new Arguments(arguments);
            ParseIt(CommandLine);
        }

        /// <summary>
        /// just for debug, use as given below. Assign value to the `cmdInfo` variable
        /// cmdInfo = "port value: " +CommandLine["port"]
        /// </summary>
        void OnGUI() {
        
            Rect r = new Rect(5, 5, 800, 500);
            GUI.Label(r, cmdInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CommandLine"></param>
        
        void ParseIt(Arguments CommandLine) {
            //parse port from commandline
            if (CommandLine["port"] != null) {                
                Console.WriteLine("port value: " +CommandLine["port"]);
                level.ServerPort = int.Parse(CommandLine["port"]);
            } else {
                Console.WriteLine("port not defined !");
            }

            Console.Out.WriteLine("Arguments parsed. Press a key");
        }
    }

}