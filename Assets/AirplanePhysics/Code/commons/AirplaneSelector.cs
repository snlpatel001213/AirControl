using System.IO;
using UnityEngine;
using Commons;
using System;



namespace AirControl {
    public class AirplaneSelector : MonoBehaviour
    {
        public enum availableAirplanes { Cessna152, F4UCorsair };
        public availableAirplanes activeAirplane;
     
        
        

        void Awake()
        {
            // Activate selected plane and deactivate others
            // if it is not running in editor then select airplane supplied by the json/default config

            foreach(availableAirplanes currentAirplane in Enum.GetValues(typeof(availableAirplanes))){
                // disable all the non selected airplanes
                if (activeAirplane != currentAirplane)
                {
                    Debug.Log("Disabled Airplane : "+currentAirplane);
                    GameObject.Find(currentAirplane.ToString()).SetActive(false);
                }
                else
                {
                    Debug.Log("Active Airplane : "+ activeAirplane);
                    GameObject.Find(activeAirplane.ToString()).SetActive(true);
                }
            }

            //disable all audio listners
            AudioListener [] listeners = GameObject.FindObjectsOfType<AudioListener>();
            foreach(var eachListner in listeners){
                eachListner.enabled  =  false;
            }

            // log the streaming asset path 
            Debug.Log("Assets will be saved at : "+ Application.streamingAssetsPath);
            
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
// once everything is done, load 
#if !UNITY_EDITOR
    CommonFunctions.ActiveAirplane = (string)CommonFunctions.airplanePreset["General/activeAirplane"];
#elif UNITY_EDITOR
    CommonFunctions.ActiveAirplane =  activeAirplane.ToString();
#endif
        }

        /// <summary>
        /// Check recency of the avialble document at streamingAssetsPath
        /// and the default settings for airplane
        /// </summary>
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
            var jsonPriority = (int)CommonFunctions.jsonPreset["General/priority"];
            var currentPriority = (int)CommonFunctions.airplanePreset["General/priority"];
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


