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
            CommonFunctions.airplanePreset["Cessna152/cameraHeight"] = 12;//
            CommonFunctions.airplanePreset["Cessna152/cameraDistance"] = 25;//
            CommonFunctions.airplanePreset["Cessna152/minHeaightFromGround"] = 4;//
            //characteristics
            CommonFunctions.airplanePreset["Cessna152/maxMPH"] = 150;//
            CommonFunctions.airplanePreset["Cessna152/rbLerpSpeed"] = 0.1;//
            CommonFunctions.airplanePreset["Cessna152/maxLiftPower"] = 200;//
            CommonFunctions.airplanePreset["Cessna152/flapLiftPower"] = 100;//
            CommonFunctions.airplanePreset["Cessna152/dragFactor"] = 0.01;//
            CommonFunctions.airplanePreset["Cessna152/flapDragFactor"] = 0.0005;//
            CommonFunctions.airplanePreset["Cessna152/pitchSpeed"] = 5000;//
            CommonFunctions.airplanePreset["Cessna152/rollSpeed"] = 4000;//
            CommonFunctions.airplanePreset["Cessna152/yawSpeed"] = 5000; //
            // Airplane Controller
            CommonFunctions.airplanePreset["Cessna152/airplaneWeight"] = 1200; //
            //control surface
            CommonFunctions.airplanePreset["Cessna152/smoothSpeed"] = 4; //
            CommonFunctions.airplanePreset["Cessna152/maxAngle"] = 30;
            //engine
            CommonFunctions.airplanePreset["Cessna152/maxForce"] = 3000;//
            CommonFunctions.airplanePreset["Cessna152/maxRPM"] = 3500;//
            CommonFunctions.airplanePreset["Cessna152/shutOffSpeed"] = 2;//
            //fuel 
            CommonFunctions.airplanePreset["Cessna152/fuelCapacity"] = 25;
            CommonFunctions.airplanePreset["Cessna152/fuelBurnRate"] = 25;
            //ground effect
            CommonFunctions.airplanePreset["Cessna152/groundDistance"] = 3; //
            CommonFunctions.airplanePreset["Cessna152/liftForce"] = 50; //
            CommonFunctions.airplanePreset["Cessna152/maxSpeed"] = 15; //
            //input
            CommonFunctions.airplanePreset["Cessna152/throttleSpeed"] = 0.5;//
            //wheel
            CommonFunctions.airplanePreset["Cessna152/brakePower"] = 500; //
            CommonFunctions.airplanePreset["Cessna152/steerAngle"] = 20; //
            
            ////////////////// F4UCorsair ///////////////////////////
            CommonFunctions.airplanePreset["F4UCorsair/cameraHeight"] = 12;//
            CommonFunctions.airplanePreset["F4UCorsair/cameraDistance"] = 25;//
            CommonFunctions.airplanePreset["F4UCorsair/minHeaightFromGround"] = 4;//
            //characteristics
            CommonFunctions.airplanePreset["F4UCorsair/maxMPH"] = 150;//
            CommonFunctions.airplanePreset["F4UCorsair/rbLerpSpeed"] = 0.1;//
            CommonFunctions.airplanePreset["F4UCorsair/maxLiftPower"] = 200;//
            CommonFunctions.airplanePreset["F4UCorsair/flapLiftPower"] = 100;//
            CommonFunctions.airplanePreset["F4UCorsair/dragFactor"] = 0.01;//
            CommonFunctions.airplanePreset["F4UCorsair/flapDragFactor"] = 0.0005;//
            CommonFunctions.airplanePreset["F4UCorsair/pitchSpeed"] = 5000;//
            CommonFunctions.airplanePreset["F4UCorsair/rollSpeed"] = 4000;//
            CommonFunctions.airplanePreset["F4UCorsair/yawSpeed"] = 5000; //
            // Airplane Controller
            CommonFunctions.airplanePreset["F4UCorsair/airplaneWeight"] = 1200; //
            //control surface
            CommonFunctions.airplanePreset["F4UCorsair/smoothSpeed"] = 4; //
            CommonFunctions.airplanePreset["F4UCorsair/maxAngle"] = 30;
            //engine
            CommonFunctions.airplanePreset["F4UCorsair/maxForce"] = 3000;//
            CommonFunctions.airplanePreset["F4UCorsair/maxRPM"] = 3500;//
            CommonFunctions.airplanePreset["F4UCorsair/shutOffSpeed"] = 2;//
            //fuel 
            CommonFunctions.airplanePreset["F4UCorsair/fuelCapacity"] = 25;
            CommonFunctions.airplanePreset["F4UCorsair/fuelBurnRate"] = 25;
            //ground effect
            CommonFunctions.airplanePreset["F4UCorsair/groundDistance"] = 3; //
            CommonFunctions.airplanePreset["F4UCorsair/liftForce"] = 50; //
            CommonFunctions.airplanePreset["F4UCorsair/maxSpeed"] = 15; //
            //input
            CommonFunctions.airplanePreset["F4UCorsair/throttleSpeed"] = 0.5;//
            //wheel
            CommonFunctions.airplanePreset["F4UCorsair/brakePower"] = 500; //
            CommonFunctions.airplanePreset["F4UCorsair/steerAngle"] = 20; //
        } 
 
    }
       
}


