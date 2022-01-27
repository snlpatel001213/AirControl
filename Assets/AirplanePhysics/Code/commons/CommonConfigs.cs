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
        static int counter=0;
        /// <summary>
        /// Get current dev version please update this version before each build and relases
        /// </summary>
        /// <returns></returns>
        public static string GET_VERSION(){
            string VERSION = "0.0.5";
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

        public static int Counter{
            get{return counter;}
            set{counter = value;}
        }
                
    }

}
        



