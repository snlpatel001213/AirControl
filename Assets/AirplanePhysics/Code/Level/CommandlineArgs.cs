using UnityEngine;
using System;
using Utility;
using AirControl;


namespace AirControl {

    public class CommandlineArgs: MonoBehaviour {
        
        static string cmdInfo = "";
        LevelControl level =  new LevelControl();
        
        /// <summary>
        /// It takes the command line arguments, parses them, and then calls the ParseIt function
        /// </summary>
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
        /// Parse command line variable here
        /// </summary>
        /// <param name="CommandLine"></param>
        void ParseIt(Arguments CommandLine) {
            //parse port from commandline
            if (CommandLine["port"] != null) {                
                Console.WriteLine("port value: " +CommandLine["port"]);
                level.ServerPort = int.Parse(CommandLine["port"]);
            } else {
                Console.WriteLine("port not defined !");
                //set default port
                level.ServerPort = 8053;
            }

            Console.Out.WriteLine("Arguments parsed. Press a key");
        }
    }

}