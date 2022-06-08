using System.IO;
using System.Collections;
using System.Collections.Generic;
using Communicator;
using UnityEditor;
using UnityEngine;
using Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Enumeration;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Globalization;

namespace Commons{
    public static class AirplaneProperties{

        // public static Jobject jsonContent;

        public static int getInt(string airplaneName, string property){
            string key = airplaneName+"/"+property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey(key);
                
            if(ifKeyPresent){
                return (int)CommonFunctions.airplanePreset[key];
            }
            else {
                Debug.Log("Key Not present - "+ key);
                return -1;
            }
        }
        public static float getFloat(string airplaneName, string property){
            string key = airplaneName+"/"+property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey(key);
                
            if(ifKeyPresent){
                return (float)CommonFunctions.airplanePreset[key];
            }
            else {
                Debug.Log("Key Not present - "+ key);
                return -1f;
            }
        }
        public static string getString(string airplaneName, string property){
            string key = airplaneName+"/"+property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey(key);
                
            if(ifKeyPresent){
                return (string)CommonFunctions.airplanePreset[key];
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
            JObject jsonContent = new JObject();
            
            try{
                CommonFunctions.jsonPreset = (JObject) JToken.ReadFrom(reader);
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
                jsonSerializer.Formatting = Formatting.Indented;
                //reset the priority to 1
                CommonFunctions.airplanePreset["General/priority"] =1;
                jsonSerializer.Serialize(jsonStream, CommonFunctions.airplanePreset);
            }
        }

        public static void initAirplaneJsonObject(){
            CommonFunctions.airplanePreset["General/docversion"] = CommonFunctions.GET_VERSION();
            CommonFunctions.airplanePreset["General/priority"] = 5;
            CommonFunctions.airplanePreset["Cessna152/cameraHeight"] = 12;
            CommonFunctions.airplanePreset["Cessna152/cameraDistance"] = 25;
            CommonFunctions.airplanePreset["Cessna152/minHeaightFromGround"] = 4;
            //characteristics
            CommonFunctions.airplanePreset["Cessna152/maxMPH"] = 12;
            CommonFunctions.airplanePreset["Cessna152/rbLerpSpeed"] = 25;
            CommonFunctions.airplanePreset["Cessna152/maxLiftPower"] = 4;
            CommonFunctions.airplanePreset["Cessna152/dragFactor"] = 12;
            CommonFunctions.airplanePreset["Cessna152/flapDragFactor"] = 25;
            CommonFunctions.airplanePreset["Cessna152/pitchSpeed"] = 4;
            CommonFunctions.airplanePreset["Cessna152/rollSpeed"] = 12;
            CommonFunctions.airplanePreset["Cessna152/yawSpeed"] = 25;
            // Airplane Controller
            CommonFunctions.airplanePreset["Cessna152/airplaneWeight"] = 4;
            //control surface
            CommonFunctions.airplanePreset["Cessna152/smoothSpeed"] = 12;
            CommonFunctions.airplanePreset["Cessna152/maxAngle"] = 25;
            //engine
            CommonFunctions.airplanePreset["Cessna152/maxForce"] = 12;
            CommonFunctions.airplanePreset["Cessna152/maxRPM"] = 25;
            CommonFunctions.airplanePreset["Cessna152/shutOffSpeed"] = 25;
            //fuel 
            CommonFunctions.airplanePreset["Cessna152/fuelCapacity"] = 25;
            CommonFunctions.airplanePreset["Cessna152/fuelBurnRate"] = 25;
            //ground effect
            CommonFunctions.airplanePreset["Cessna152/groundDistance"] = 25;
            CommonFunctions.airplanePreset["Cessna152/liftForce"] = 25;
            CommonFunctions.airplanePreset["Cessna152/groundDistance"] = 25;
            CommonFunctions.airplanePreset["Cessna152/maxSpeed"] = 25;
            //input
            CommonFunctions.airplanePreset["Cessna152/throttleSpeed"] = 25;
            //wheel
            CommonFunctions.airplanePreset["Cessna152/brakePower"] = 25;
            CommonFunctions.airplanePreset["Cessna152/steerAngle"] = 25;
            CommonFunctions.airplanePreset["Cessna152/brakePower"] = 25;
            
            // CommonFunctions.airplanePreset["Cessna152/cameraDistance"] = 12;
        } 
 
    }
       
}


