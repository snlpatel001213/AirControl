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
        /// <summary>
        /// Get current dev version
        /// </summary>
        /// <returns></returns>
        public static string GET_VERSION(){
            string VERSION = "0.0.4";
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

        public static T DeserializeJson<T>(string path)
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }   
                
    }

}
        



