using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.InteropServices.WindowsRuntime;
using Commons;
using Communicator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Commons
{
    public static class AirplaneProperties
    {
        /// <summary>
        /// Json read Int 
        /// </summary>
        /// <param name="airplaneName">Airplane name</param>
        /// <param name="property">Airplane property</param>
        /// <returns></returns>
        public static int getInt(string airplaneName, string property)
        {
            string key = airplaneName + "/" + property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey(key);

            if (ifKeyPresent)
            {
                return (int)CommonFunctions.airplanePreset[key];
            }
            else
            {
                Debug.Log("Key Not present - " + key);
                return -1;
            }
        }
        /// <summary>
        /// Json read Float 
        /// </summary>
        /// <param name="airplaneName">Airplane name</param>
        /// <param name="property">Airplane property</param>
        /// <returns></returns>
        public static float getFloat(string airplaneName, string property)
        {
            string key = airplaneName + "/" + property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey(key);

            if (ifKeyPresent)
            {
                return (float)CommonFunctions.airplanePreset[key];
            }
            else
            {
                Debug.Log("Key Not present - " + key);
                return -1f;
            }
        }
        /// <summary>
        /// Json read String 
        /// </summary>
        /// <param name="airplaneName">Airplane name</param>
        /// <param name="property">Airplane property</param>
        /// <returns></returns>
        public static string getString(string airplaneName, string property)
        {
            string key = airplaneName + "/" + property;
            bool ifKeyPresent = CommonFunctions.airplanePreset.ContainsKey(key);

            if (ifKeyPresent)
            {
                return (string)CommonFunctions.airplanePreset[key];
            }
            else
            {
                Debug.unityLogger.LogError("Key Not present : {0} ", key);
                return "None";
            }
        }
        /// <summary>
        /// Read Json From file
        /// </summary>
        /// <param name="filepath">absolute file path </param>
        /// <returns></returns>
        public static bool readJson(string filepath)
        {

            // read JSON directly from a file
            StreamReader file = File.OpenText(filepath);
            JsonTextReader reader = new JsonTextReader(file);

            try
            {
                CommonFunctions.jsonPreset = (JObject)JToken.ReadFrom(reader);
                return true;
            }
            catch (JsonException ioExp)
            {
                Debug.LogError(ioExp.Message);
                return false;
            }

        }

        /// <summary>
        /// Save Json
        /// </summary>
        /// <param name="filepath"></param>
        public static void saveJson(string filepath)
        {
            // write JSON directly to a file
            using (FileStream fs = File.Create(filepath))
            using (StreamWriter jsonStream = new StreamWriter(fs))
            {
                var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                jsonSerializer.Formatting = Formatting.Indented;
                //reset the priority to 1
                CommonFunctions.airplanePreset["General/priority"] = 1;
                jsonSerializer.Serialize(jsonStream, CommonFunctions.airplanePreset);
            }
        }

        /// <summary>
        /// Default properties of the Airplane
        /// </summary>
        public static void initAirplaneJsonObject()
        {
            CommonFunctions.airplanePreset["General/docversion"] = CommonFunctions.GET_VERSION();
            CommonFunctions.airplanePreset["General/priority"] = 1; // if priority of json is higher json will override default setting
            CommonFunctions.airplanePreset["General/activeAirplane"] = "Cessna152"; // if priority of json is higher json will override default setting
            CommonFunctions.airplanePreset["Cessna152/cameraHeight"] = 6;// Height of camera
            CommonFunctions.airplanePreset["Cessna152/cameraDistance"] = 12;// Camera distance
            CommonFunctions.airplanePreset["Cessna152/minHeaightFromGround"] = 4;// Min height of camera from ground when aiplane lands
            //characteristics
            CommonFunctions.airplanePreset["Cessna152/maxMPH"] = 150;// Max speed of the airplane
            CommonFunctions.airplanePreset["Cessna152/rbLerpSpeed"] = 0.1; // Controls accelearation
            CommonFunctions.airplanePreset["Cessna152/maxLiftPower"] = 100;// Control takeoff power of the airplane
            CommonFunctions.airplanePreset["Cessna152/flapLiftPower"] = 500;// Lift force by flap
            CommonFunctions.airplanePreset["Cessna152/dragFactor"] = 0.01;// Drag by air pressure
            CommonFunctions.airplanePreset["Cessna152/flapDragFactor"] = 0.001;// Drag when flap is used
            CommonFunctions.airplanePreset["Cessna152/pitchSpeed"] = 5000;// Elevator sufarce effectivness
            CommonFunctions.airplanePreset["Cessna152/rollSpeed"] = 4000;// Aleron surface effectivness
            CommonFunctions.airplanePreset["Cessna152/yawSpeed"] = 5000; // Rudder lever effectivness
            // Airplane Controller 
            CommonFunctions.airplanePreset["Cessna152/airplaneWeight"] = 900; // Mass of the Airplane in pounds
            //control surface
            CommonFunctions.airplanePreset["Cessna152/smoothSpeed"] = 4; // Control surface speed
            CommonFunctions.airplanePreset["Cessna152/maxAngle"] = 30; // Max angle of the control surface
            //engine
            CommonFunctions.airplanePreset["Cessna152/maxRPM"] = 4500; // RPM of the engine force will be calculated by equarion $F_i = C_t\rho\omega^2_{max}D^4$
            CommonFunctions.airplanePreset["Cessna152/shutOffSpeed"] = 2; // shutdown rate when the engien is shutoff
            CommonFunctions.airplanePreset["Cessna152/propellerSpan"] = 1.6; // span of the propeller in meters
            //fuel 
            CommonFunctions.airplanePreset["Cessna152/fuelCapacity"] = 29; // fuel capacity of the Airplane in galloons
            CommonFunctions.airplanePreset["Cessna152/fuelBurnRate"] = 6.1; // consumpltion rate Gallons/hr 
            //ground effect
            CommonFunctions.airplanePreset["Cessna152/groundDistance"] = 3; // Distance from ground when the ground effect starts, generally its equal to wingspan
            CommonFunctions.airplanePreset["Cessna152/liftForce"] = 50; // lift force generated by the ground effect
            CommonFunctions.airplanePreset["Cessna152/maxSpeed"] = 15; // max pseed for ground effect
            //input
            CommonFunctions.airplanePreset["Cessna152/throttleSpeed"] = 0.1;// Throttle Speed
            //wheel
            CommonFunctions.airplanePreset["Cessna152/brakePower"] = 500; // Brake Power
            CommonFunctions.airplanePreset["Cessna152/steerAngle"] = 20; // steer angle of the wheel

            ////////////////// F4UCorsair ///////////////////////////
            CommonFunctions.airplanePreset["F4UCorsair/cameraHeight"] = 25;//
            CommonFunctions.airplanePreset["F4UCorsair/cameraDistance"] = 25;//
            CommonFunctions.airplanePreset["F4UCorsair/minHeaightFromGround"] = 20;//
            //characteristics
            CommonFunctions.airplanePreset["F4UCorsair/maxMPH"] = 360;//
            CommonFunctions.airplanePreset["F4UCorsair/rbLerpSpeed"] = 0.1;//
            CommonFunctions.airplanePreset["F4UCorsair/maxLiftPower"] = 200;//
            CommonFunctions.airplanePreset["F4UCorsair/flapLiftPower"] = 1000;//
            CommonFunctions.airplanePreset["F4UCorsair/dragFactor"] = 0.01;//
            CommonFunctions.airplanePreset["F4UCorsair/flapDragFactor"] = 0.0005;//
            CommonFunctions.airplanePreset["F4UCorsair/pitchSpeed"] = 25000;//
            CommonFunctions.airplanePreset["F4UCorsair/rollSpeed"] = 25000;//
            CommonFunctions.airplanePreset["F4UCorsair/yawSpeed"] = 25000; //
            // Airplane Controller
            CommonFunctions.airplanePreset["F4UCorsair/airplaneWeight"] = 2500; //
            //control surface
            CommonFunctions.airplanePreset["F4UCorsair/smoothSpeed"] = 4; //
            CommonFunctions.airplanePreset["F4UCorsair/maxAngle"] = 30;
            //engine
            CommonFunctions.airplanePreset["F4UCorsair/maxRPM"] = 6500;//
            CommonFunctions.airplanePreset["F4UCorsair/shutOffSpeed"] = 2.5;//
            CommonFunctions.airplanePreset["F4UCorsair/propellerSpan"] = 2.2; //
            //fuel 
            CommonFunctions.airplanePreset["F4UCorsair/fuelCapacity"] = 75;
            CommonFunctions.airplanePreset["F4UCorsair/fuelBurnRate"] = 12;
            //ground effect
            CommonFunctions.airplanePreset["F4UCorsair/groundDistance"] = 6; //
            CommonFunctions.airplanePreset["F4UCorsair/liftForce"] = 150; //
            CommonFunctions.airplanePreset["F4UCorsair/maxSpeed"] = 30; //
            //input
            CommonFunctions.airplanePreset["F4UCorsair/throttleSpeed"] = 0.06;//
            //wheel
            CommonFunctions.airplanePreset["F4UCorsair/brakePower"] = 500; //
            CommonFunctions.airplanePreset["F4UCorsair/steerAngle"] = 25; //
        }

    }

}


