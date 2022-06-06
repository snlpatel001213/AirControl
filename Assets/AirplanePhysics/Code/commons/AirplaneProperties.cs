using System.IO;
using System.Collections;
using System.Collections.Generic;
using Communicator;
using UnityEditor;
using UnityEngine;
using Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.UIElements;
using System.IO.Enumeration;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Commons{
    public static class AirplaneProperties{
        public static int getInt(string airplaneName, string property){
            string key = airplaneName+"-"+property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey[key];
                
            if(ifKeyPresent){
                return CommonFunctions.airplanePreset[key];
            }
            else {
                Debug.Log("Key Not present - "+ key);
                return -1;
            }
        }
        public static float getFloat(string airplaneName, string property){
            string key = airplaneName+"-"+property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey[key];
                
            if(ifKeyPresent){
                return CommonFunctions.airplanePreset[key];
            }
            else {
                Debug.Log("Key Not present - "+ key);
                return -1f;
            }
        }
        public static string getString(string airplaneName, string property){
            string key = airplaneName+"-"+property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey[key];
                
            if(ifKeyPresent){
                return CommonFunctions.airplanePreset[key];
            }
            else {
                Debug.Log("Key Not present - "+ key);
                return "None";
            }
        }

        public static bool readJson(string filepath)
        {
            
            // read JSON directly from a file
            StreamReader file = File.OpenText(filepath);
            JsonTextReader reader = new JsonTextReader(file);
            JObject o2 = new JObject();
            
            try{
                CommonFunctions.airplanePreset = (JObject) JToken.ReadFrom(reader);
                return true;
            }
            catch (JsonException ioExp){   
                Debug.Log(ioExp.Message);
                return false;
            } 
            
        }
        
        public static void saveJson(string filepath){
            // write JSON directly to a file
            using (FileStream fs = File.Create(filepath))
            using (StreamWriter jsonStream = new StreamWriter(fs))
            {
                var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                jsonSerializer.Serialize(jsonStream, CommonFunctions.airplanePreset);
            }
        }

        public static void initAirplaneJsonObject(){
            CommonFunctions.airplanePreset["Cessna152-cameraHeight"] = 4;
        }  
    }
       
}


