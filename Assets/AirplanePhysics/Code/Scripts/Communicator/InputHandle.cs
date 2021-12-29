using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using  AirControl;
using Communicator;

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
                StaticInputSchema.InputControlType = inputControlType;
                StaticInputSchema.MsgType = "Incoming";
                StaticInputSchema.InputControlType = inputControlType;
                // Airplane properties
                StaticInputSchema.Throttle = throttle;
                StaticInputSchema.StickyThrottle = stickyThrottle;
                StaticInputSchema.Pitch = pitch;
                StaticInputSchema.Roll = roll;
                StaticInputSchema.Yaw = yaw;
                StaticInputSchema.Brake = brake;
                StaticInputSchema.Flaps = flaps;
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
                StaticTransactionSchema.MsgType = "Transcation";
                StaticTransactionSchema.InputControlType = inputControlType;
                // Camrera control
                StaticTransactionSchema.ActiveCamera = activeCamera;
                //level reset
                StaticTransactionSchema.LevelReload = levelReload;
                //set sun location
                StaticTransactionSchema.SunLatitude = sunLatitude;
                StaticTransactionSchema.SunLongitude =sunLongitude;
                StaticTransactionSchema.SunHour = sunHour;
                StaticTransactionSchema.SunMinute = sunMinute;
                StaticTransactionSchema.IsActive = isActive;
                // which screen to capture
                StaticTransactionSchema.CaptureScreen = captureScreen;
                StaticTransactionSchema.ScreenCaptureType = screenCaptureType;

            }
            #endregion
            
            # region StartUp
            else if (MsgType=="StartUp") // if operation type is transaction
            {   
                // blank for now
            }
            #endregion
        }    

        #endregion

    }
}