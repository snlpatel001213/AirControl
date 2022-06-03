using UnityEngine;
using System;
using Utility;
using Commons;


namespace AirControl {

    public class CommandlineArgs: MonoBehaviour {
        
        static string cmdInfo = "";

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
            /* Parsing the port number from the command line and sets the port number to the
            global variable `CommonFunctions.ServerPort` */
            defineServerPort(CommandLine);
            /* This function displays the host and port of the client */
            displayHostPortClient(CommandLine);
        }

        /// <summary>
        /// This function parses the port number from the command line and sets the port number to the
        /// global variable `CommonFunctions.ServerPort`
        /// </summary>
        /// <param name="Arguments">This is the class that contains the command line arguments.</param>
        void defineServerPort(Arguments CommandLine){
            //parse port from commandline
            if (int.TryParse(CommandLine["serverPort"], out _)) {  
                // if port provided              
                CommonFunctions.ServerPort = int.Parse(CommandLine["serverPort"]);
                Console.WriteLine("Server port is defined as : "+ CommonFunctions.ServerPort);
            } else {
                //If no port is provided, set default port
                CommonFunctions.ServerPort = 8054;
                Console.WriteLine("Server Port not defined. Selecting default: "+ CommonFunctions.ServerPort);        
            }
        }

        /// <summary>
        /// This function displays the host and port of the client
        /// </summary>
        /// <param name="Arguments">This is the object that contains the command line arguments.</param>
        void displayHostPortClient(Arguments CommandLine){
            if (CommandLine["clientIP"] != "") {  
                // if port provided              
                CommonFunctions.clientIP = CommandLine["clientIP"];
                Console.WriteLine("ClientIP is defined as : "+ CommonFunctions.clientIP);
            } else {
                //If no port is provided, set default port
                Console.WriteLine("ClientIP not defined.");        
            }
            if (CommandLine["clientPort"] != "") {  
                // if port provided              
                CommonFunctions.clientPort = CommandLine["clientPort"];
                Console.WriteLine("ClientPort is defined as : "+ CommonFunctions.ServerPort);
            } else {
                //If no port is provided, set default port
                Console.WriteLine("ClientPort not defined.");        
            }
        }
    }

}