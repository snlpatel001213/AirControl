using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AirControl;
using System.Net.Sockets;
using Communicator;

namespace Communicator
{
    /// <summary>
    /// Handle inbound request from the TCP socket
    /// </summary>
    public class InputHandle:MonoBehaviour
    {
        #region Builtin Methods
        #endregion

        #region Custom Methods
        // receive msg to unity
        /// <summary>
        /// Parse input coming from network
        /// </summary>
        /// <param name="inputJson">Input Json object</param>
        public void ParseInput(JObject inputJson)
        {
                string MsgType = inputJson["MsgType"].ToString();

                #region ControlInput
                if (MsgType=="ControlInput")
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
                    StaticControlSchema.InputControlType = inputControlType;
                    StaticControlSchema.MsgType = "ControlInput";
                    StaticControlSchema.InputControlType = inputControlType;
                    // Airplane properties
                    StaticControlSchema.Throttle = throttle;
                    StaticControlSchema.StickyThrottle = stickyThrottle;
                    StaticControlSchema.Pitch = pitch;
                    StaticControlSchema.Roll = roll;
                    StaticControlSchema.Yaw = yaw;
                    StaticControlSchema.Brake = brake;
                    StaticControlSchema.Flaps = flaps;
                }
                #endregion

                #region  Camera
                else if (MsgType=="Camera") // if operation type is transaction
                {   
                    Debug.Log("Registered under camera section");
                    //input type
                    bool isActive = bool.Parse(inputJson["IsActive"].ToString());
                    //Scene Camera
                    int activeCamera = int.Parse(inputJson["ActiveCamera"].ToString());
                    bool isCapture = bool.Parse(inputJson["IsCapture"].ToString());
                    int captureCamera = int.Parse(inputJson["CaptureCamera"].ToString());
                    int captureType = int.Parse(inputJson["CaptureType"].ToString());
                    int captureWidth = int.Parse(inputJson["CaptureWidth"].ToString());
                    int captureHeight = int.Parse(inputJson["CaptureHeight"].ToString());
                    
                    //primary key
                    StaticCameraSchema.MsgType = "Camera";
                    // Camrera control
                    StaticCameraSchema.IsActive = isActive; 
                    StaticCameraSchema.ActiveCamera = activeCamera;
                    // which screen to capture
                    StaticCameraSchema.IsCapture = isCapture;
                    StaticCameraSchema.CaptureCamera = captureCamera;
                    StaticCameraSchema.CaptureType = captureType;
                    StaticCameraSchema.CaptureWidth = captureWidth;
                    StaticCameraSchema.CaptureHeight = captureHeight;

                }
                #endregion

                #region  Level
                else if (MsgType=="Level") // if operation type is transaction
                {   
                    bool levelReload = bool.Parse(inputJson["LevelReload"].ToString());
                    //set sun location
                    bool isActive = bool.Parse(inputJson["IsActive"].ToString());
                    
                    //primary key
                    StaticLevelSchema.MsgType = "Level";
                    //level reset
                    StaticLevelSchema.LevelReload = levelReload;
                    StaticLevelSchema.IsActive = isActive;

                }
                #endregion

                #region  TOD
                else if (MsgType=="TOD") // if operation type is transaction
                {   
                    //set sun location
                    float sunLatitude = float.Parse(inputJson["SunLatitude"].ToString());
                    float sunLongitude = float.Parse(inputJson["SunLongitude"].ToString());
                    int hour = int.Parse(inputJson["Hour"].ToString());
                    int minute = int.Parse(inputJson["Minute"].ToString());
                    bool isActive = bool.Parse(inputJson["IsActive"].ToString());
                    
                    //primary key
                    StaticTODSchema.MsgType = "TOD";
                    //set sun location
                    StaticTODSchema.SunLatitude = sunLatitude;
                    StaticTODSchema.SunLongitude =sunLongitude;
                    StaticTODSchema.Hour = hour;
                    StaticTODSchema.Minute = minute;
                    StaticTODSchema.IsActive = isActive;

                }
                #endregion

                #region  Weather
                else if (MsgType=="Weather") // if operation type is transaction
                {   
                    //set sun location
                    bool isClouds = bool.Parse(inputJson["IsClouds"].ToString());
                    bool isFog = bool.Parse(inputJson["IsFog"].ToString());
                    
                    //primary key
                    StaticWeatherSchema.MsgType = "Weather";
                    //set if clouds are enabled
                    StaticWeatherSchema.IsClouds = isClouds;
                    //set if fog ia enabled
                    StaticWeatherSchema.IsFog =isFog;

                }
                #endregion
                
                #region Audio
                else if (MsgType=="Audio") // if operation type is transaction
                {   
                    //input type
                    bool isActive = bool.Parse(inputJson["IsActive"].ToString());
                    bool enableAudio = bool.Parse(inputJson["EnableAudio"].ToString());
                    
                    //primary key
                    StaticAudioSchema.MsgType = "Audio";
                     // Camrera control
                    StaticAudioSchema.IsActive = isActive; 
                    //set if fog ia enabled
                    StaticAudioSchema.EnableAudio =enableAudio;
                }
                #endregion

                #region UI
                else if (MsgType=="UI") // if operation type is transaction
                {   
                    //input type
                    bool isActive = bool.Parse(inputJson["IsActive"].ToString());
                    bool showUIElements = bool.Parse(inputJson["ShowUIElements"].ToString());
                    
                    //primary key
                    StaticUISchema.MsgType = "UI";
                     // Camrera control
                    StaticUISchema.IsActive = isActive; 
                    //set if clouds are enabled
                    StaticUISchema.ShowUIElements = showUIElements;
                }
                #endregion



                #region Lidar
                else if (MsgType=="Lidar") // if operation type is transaction
                {   
                    float range = float.Parse(inputJson["Range"].ToString());
                    int density = int.Parse(inputJson["Density"].ToString());
                    
                    //primary key
                    StaticLidarSchema.MsgType = "Weather";
                    //set if clouds are enabled
                    StaticLidarSchema.Range = range;
                    //set if fog ia enabled
                    StaticLidarSchema.Density =density;

                }
                #endregion

                #region Fuel
                else if (MsgType=="Fuel") // if operation type is transaction
                {   
                    // blank for now
                }
                #endregion

                #region StartUp
                else if (MsgType=="StartUp") // if operation type is transaction
                {   
                    // blank for now
                }
                #endregion

                // gived output if called
                #region Output
                else if (MsgType=="Output") // if operation type is transaction
                {   
                    // Do nothing output API will receive output
                }
                #endregion


            
        }    

        #endregion

    }
}