using System.IO;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    
    public static class CommonFunctions
    {


        // max reward 
        // if you chnage this value, put the same value at Scripts/Communicator/NetworkCommunicator.cs
        public static float maxR = 0;
        // request counter
        public static int counter=0;
        public static string clientIP = "None";
        public static string clientPort = "None";
        public static int serverPort=5000; // field 
        public static string activeAirplane = "None";
        #region Properties

        #region preset
        public static string  persistentDataPath = Application.streamingAssetsPath;
        public static string presetFilename = "AirplaneProperties.json";
        public static JObject airplanePreset = new JObject();
        public static JObject jsonPreset = new JObject();

        public static string presetFilepath = System.IO.Path.Combine(persistentDataPath,presetFilename);
        #endregion
        
        /// <summary>
        /// Getting and setting max reward
        /// </summary>
        /// <value></value>
        public static float MaxR{
            get{return maxR;}
            set{maxR = value;}
        }

        /// <summary>
        /// getting and setting host and port of the client
        /// </summary>
        /// <value></value>
        public static string ClientIP{
            get{return clientIP;}
            set{clientIP = value;}
        }

        public static string ClientPort{
            get{return clientPort;}
            set{clientPort = value;}
        }

        public static string ActiveAirplane{
            get{return activeAirplane;}
            set{activeAirplane = value;}
        }

        /// <summary>
        /// Defining server port
        /// </summary>
        
        public static int ServerPort   // property
        {
            get { return serverPort; }   // get method
            set { serverPort = value; }  // set method
        }
        #endregion

        /// <summary>
        /// Get current dev version please update this version before each build and relases
        /// </summary>
        /// <returns></returns>
        public static string GET_VERSION(){
            string VERSION = "1.5.0";
            return VERSION;
        } 

        public static bool ifExists(string path){
            if (File.Exists(path)){
                return true;
            } 
            return false;
        }

        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            try{
            
                if (File.Exists(path))    
                {    
                    // If file found, delete it    
                    File.Delete(path);   
                    Console.WriteLine("File deleted : {path}");    
                }    
                else 
                    Debug.Log("File not found : {path}");    
            }    
            catch (IOException ioExp)    
            {    
                Debug.Log(ioExp.Message);    
            } 
        }
        /// <summary>
        /// Template function - Deserialize Json file
        /// </summary>
        /// <param name="path">Path to the file </param>
        /// <typeparam name="T">Class to which it will be serialized</typeparam>
        /// <returns></returns>
        public static T DeserializeJson<T>(string path)
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        } 

        /// <summary>
        /// Input output counter for debug only
        /// </summary>
        /// <value></value>
        public static int Counter{
            get{return counter;}
            set{counter = value;}
        }

        /// <summary>
        /// To delete the files and then the folder 
        /// </summary>
        /// <param name="FolderName"></param>
        public static void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach(FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }
    }
}
        



