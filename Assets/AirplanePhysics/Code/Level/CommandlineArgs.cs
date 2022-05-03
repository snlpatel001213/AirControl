using UnityEngine;
using System;
using Utility;
using Commons;


namespace AirControl {

    public class CommandlineArgs: MonoBehaviour {
        
        static string cmdInfo = "";
        LevelControl level =  new LevelControl();

        // link current airplane to the active airplane 
        public GameObject currentAirplane;
        public GameObject ActiveAirplane;
        
        /// <summary>
        /// It takes the command line arguments, parses them, and then calls the ParseIt function
        /// </summary>
        void Awake() {
            string[] arguments = Environment.GetCommandLineArgs();
            Arguments CommandLine = new Arguments(arguments);
            ParseIt(CommandLine);

            if (true){
                ActiveAirplane = currentAirplane;
            }

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
            if (int.TryParse(CommandLine["port"], out _)) {  
                // if port provided              
                CommonFunctions.ServerPort = int.Parse(CommandLine["port"]);
            } else {
                //If no port is provided, set default port
                CommonFunctions.ServerPort = 8054;
                Console.WriteLine("No Port not defined. Selecting default: "+ CommonFunctions.ServerPort);        
            }

            Console.Out.WriteLine("Arguments parsed. Press a key");
        }
    }

}