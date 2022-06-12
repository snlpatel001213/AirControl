using System.IO;
using UnityEngine;
using Commons;
using System;
using UnityEngine.Rendering;

namespace AirControl {
    public class AirplaneSelector : MonoBehaviour
    {
        public enum availableAirplanes { Cessna152, F4UCorsair };
        public availableAirplanes activeAirplane;
        
        void Awake()
        {
            //Loading settings from json
            AirplaneProperties.initAirplaneJsonObject();
            JsonOps();
            CheckRecency();
            // once everything is done, load 
            #if !UNITY_EDITOR
                CommonFunctions.ActiveAirplane = (string)CommonFunctions.airplanePreset["General/activeAirplane"];
                Debug.Log("Active Airplane selected from config : "+ CommonFunctions.ActiveAirplane);
            #elif UNITY_EDITOR
                CommonFunctions.ActiveAirplane =  activeAirplane.ToString();
                Debug.Log("Active Airplane selected from editor : "+ CommonFunctions.ActiveAirplane);
            #endif  
            // Activate selected plane and deactivate others
            // if it is not running in editor then select airplane supplied by the json/default config
            Debug.Log("Active Airplane : "+ CommonFunctions.ActiveAirplane);
            GameObject.Find(CommonFunctions.ActiveAirplane).SetActive(true);
            foreach(availableAirplanes currentAirplane in Enum.GetValues(typeof(availableAirplanes))){
                // disable all the non selected airplanes
                if (CommonFunctions.ActiveAirplane != currentAirplane.ToString())
                {
                    Debug.Log("Disabled Airplane : "+currentAirplane);
                    GameObject.Find(currentAirplane.ToString()).SetActive(false);
                }
            }

            //disable all audio listners
            AudioListener [] listeners = GameObject.FindObjectsOfType<AudioListener>();
            foreach(var eachListner in listeners){
                eachListner.enabled  =  false;
            }

            // log the streaming asset path 
            Debug.Log("Assets will be saved at : "+ Application.streamingAssetsPath);

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
                Debug.Log("Json doc version has higher priority, reading value from json");
                CommonFunctions.airplanePreset = CommonFunctions.jsonPreset;
            }
            else if (jsonPriority <= currentPriority ){
                // if the jsonDocVersion is lower then default then read properties from default
                Debug.Log("json doc version has lower priority, reading value from defaults");                
            }
            
        }
        void JsonOps(){
            Debug.Log(CommonFunctions.airplanePreset);
            if (CommonFunctions.ifExists(CommonFunctions.presetFilepath)){
                // json is not present Write it to disk
                Debug.Log("Reading Json from : "+ CommonFunctions.presetFilepath);
                bool readJson = AirplaneProperties.readJson(CommonFunctions.presetFilepath);
                if (readJson){
                    Debug.Log("Reading fron Json successful : \n " + CommonFunctions.jsonPreset);
                }
                else // tocheck if the json is there but empty
                {
                    try{
                        AirplaneProperties.saveJson(CommonFunctions.presetFilepath);
                    }
                        catch (IOException ioExp){    
                        Debug.LogError(ioExp.Message);   
                    } 
                }
            }
            else{
                try{
                    AirplaneProperties.saveJson(CommonFunctions.presetFilepath);
                }
                catch (IOException ioExp){    
                    Debug.LogError(ioExp.Message);   
                } 
            }
        }
    }    

}


