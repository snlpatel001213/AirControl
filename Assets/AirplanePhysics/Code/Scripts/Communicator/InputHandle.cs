using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using  AirControl;
using SqliteDB;

namespace Communicator
{
    
    public class InputHandle:MonoBehaviour
    {
        #region Builtin Methods
        #endregion

        #region Custom Methods
        // receive msg to unity
        
        public void ParseInput(string receivedString)
        {
            // Parse input
            var inputJson =  JObject.Parse(receivedString);
            // MsgType can be Transaction or Continuous
            string MsgType = inputJson["MsgType"].ToString();
            # region Input
            if (MsgType=="Incoming")
            {
                //input type
                string inputControlType = inputJson["InputControlType"].ToString();
                
                // Airplane Properties
                float throttle = float.Parse(inputJson["Throttle"].ToString());
                float stickyThrottle = float.Parse(inputJson["StickyThrottle"].ToString());
                float pitch = float.Parse(inputJson["Pitch"].ToString());
                float roll = float.Parse(inputJson["Roll"].ToString());
                float yaw = float.Parse(inputJson["Yaw"].ToString());
                float brake = float.Parse(inputJson["Brake"].ToString());
                int flaps = int.Parse(inputJson["Flaps"].ToString());

                // connection = DB_Init.GetConnection();
                // insert in to static class 
                DB_StaticEternalInput.InputControlType = inputControlType;
                DB_StaticEternalInput.MsgType = "Incoming";
                DB_StaticEternalInput.InputControlType = inputControlType;
                // Airplane properties
                DB_StaticEternalInput.Throttle = throttle;
                DB_StaticEternalInput.StickyThrottle = stickyThrottle;
                DB_StaticEternalInput.Pitch = pitch;
                DB_StaticEternalInput.Roll = roll;
                DB_StaticEternalInput.Yaw = yaw;
                DB_StaticEternalInput.Brake = brake;
                DB_StaticEternalInput.Flaps = flaps;
            }
            #endregion

            #region  Transaction
            else if (MsgType=="Transcation") // if operation type is transaction
            {   
                bool levelReload = bool.Parse(inputJson["LevelReload"].ToString());
                //Camera
                int activeCamera = int.Parse(inputJson["ActiveCamera"].ToString());
                //input type
                string inputControlType = inputJson["InputControlType"].ToString();
                //set sun location
                float sunLatitude = float.Parse(inputJson["SunLatitude"].ToString());
                float sunLongitude = float.Parse(inputJson["SunLongitude"].ToString());
                int sunHour = int.Parse(inputJson["SunHour"].ToString());
                int sunMinute = int.Parse(inputJson["SunMinute"].ToString());
                bool isActive = bool.Parse(inputJson["IsActive"].ToString());
                bool captureScreen = bool.Parse(inputJson["CaptureScreen"].ToString());
                int screenCaptureType = int.Parse(inputJson["ScreenCaptureType"].ToString());
                
                //primary key
                DB_StaticTransactions.MsgType = "Transcation";
                DB_StaticTransactions.InputControlType = inputControlType;
                // Camrera control
                DB_StaticTransactions.ActiveCamera = activeCamera;
                //level reset
                DB_StaticTransactions.LevelReload = levelReload;
                //set sun location
                DB_StaticTransactions.SunLatitude = sunLatitude;
                DB_StaticTransactions.SunLongitude =sunLongitude;
                DB_StaticTransactions.SunHour = sunHour;
                DB_StaticTransactions.SunMinute = sunMinute;
                DB_StaticTransactions.IsActive = isActive;
                // which screen to capture
                DB_StaticTransactions.CaptureScreen = captureScreen;
                DB_StaticTransactions.ScreenCaptureType = screenCaptureType;

            }
            #endregion
            
            # region StartUp
            else if (MsgType=="StartUp") // if operation type is transaction
            {   
                // blank for now
            }
            #endregion
        }    
        

        // send msg out of unity
        // this will be moved to outputHandle class
        // public string  ParseOutput(AC_BaseAirplane_Input currentReadings)
        // {
        //     StructDef.OutputDataStructure outDataStruct;
        //     // Put data to structure
        //     outDataStruct.pitch = currentReadings.Pitch;
        //     outDataStruct.roll = currentReadings.Roll;
        //     outDataStruct.yaw = currentReadings.Yaw;
        //     outDataStruct.throttle = currentReadings.Throttle;
        //     outDataStruct.brake = currentReadings.Brake;
        //     outDataStruct.flaps = currentReadings.Flaps;
            
        //     // Convert to Json and Serialize data
        //     string outStructSerialized = JsonConvert.SerializeObject(outDataStruct);
        //     return outStructSerialized;

        //     // networkUtils.SendMessage(outStructSerialized);
        // }

        #endregion

    }
}