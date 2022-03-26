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
        public static int counter=0;

        #region Properties
        public static float MaxR{
            get{return maxR;}
            set{maxR = value;}
        }
        #endregion

        /// <summary>
        /// Get current dev version please update this version before each build and relases
        /// </summary>
        /// <returns></returns>
        public static string GET_VERSION(){
            string VERSION = "1.2.0";
            return VERSION;
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
        



