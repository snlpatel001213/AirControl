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
            CommonFunctions.ActiveAirplane =  activeAirplane.ToString();
            //Loading settings from json
            AirplaneProperties.initAirplaneJsonObject();
            Debug.Log(CommonFunctions.airplanePreset);
            if (CommonFunctions.ifExists(CommonFunctions.presetFilepath)){
                if  (!AirplaneProperties.readJson(CommonFunctions.presetFilepath)){
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
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }    

}


