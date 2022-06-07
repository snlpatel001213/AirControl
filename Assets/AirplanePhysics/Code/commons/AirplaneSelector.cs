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

namespace AirControl {
    public class AirplaneSelector : MonoBehaviour
    {
        public enum availableAirplanes { Cessna152, F4UCorsair };
        public availableAirplanes activeAirplane;


        void Awake()
        {

            // Selecting active airplane and disabling the rest
            foreach (int i in Enum.GetValues(typeof(availableAirplanes)))  
            {  
                if (i!=activeAirplane){
                    // disable prefabs
                    
                }
            }  


            CommonFunctions.ActiveAirplane =  activeAirplane.ToString();
            //Loading settings from json
            AirplaneProperties.initAirplaneJsonObject();
            Debug.Log(CommonFunctions.airplanePreset);
            if (CommonFunctions.ifExists(CommonFunctions.presetFilepath)){
                // json is not present Write it to disk
                if  (!AirplaneProperties.readJson(CommonFunctions.presetFilepath)) // tocheck if the json is there but empty
                {
                    try{
                        AirplaneProperties.saveJson(CommonFunctions.presetFilepath);
                    }
                        catch (IOException ioExp){    
                        Debug.Log(ioExp.Message);   
                    } 
                }
            }
            else{
                try{
                    AirplaneProperties.saveJson(CommonFunctions.presetFilepath);
                }
                catch (IOException ioExp){    
                    Debug.Log(ioExp.Message);   
                } 
            }
            CheckRecency();
        }

        // Update is called once per frame
        void CheckRecency()
        {
            //check aircontrolversion
            var jsonAirControlVersion = CommonFunctions.jsonPreset["General/airControlVersion"];
            var currentAirControlVersion = CommonFunctions.airplanePreset["General/airControlVersion"];
            if (jsonAirControlVersion != currentAirControlVersion){
                Debug.Log("Saved version and current version are different, Writting defaults to the json");
                AirplaneProperties.saveJson(CommonFunctions.presetFilepath);
            }
            //check priority
            var jsonPriority = CommonFunctions.jsonPreset["General/priority"];
            var currentPriority = CommonFunctions.airplanePreset["General/priority"];
            if (jsonPriority > currentPriority ){ 
                // if the jsonDocVersion greater then read properties from json
                Debug.Log("json doc version has higher priority, reading value from json");
                CommonFunctions.airplanePreset = CommonFunctions.jsonPreset;
            }
            else{
                // if the jsonDocVersion is lower then default then read properties from default
                Debug.Log("json doc version has lower priority, reading value from defaults");                
            }
            
        }
    }    

}


