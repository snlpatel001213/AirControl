using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite4Unity3d;
using  AirControl;
using SqliteDB;

namespace Communicator
{
    
    public class InputHandle:MonoBehaviour
    {
        #region Builtin Methods
        private SQLiteConnection connection;
        #endregion

        #region Custom Methods
        // receive msg to unity
        public void ParseInput(string receivedString)
        {
            // Parse input
            var inputJson =  JObject.Parse(receivedString);
            // MsgType can be Transaction or Continuous
            string MsgType = inputJson["MsgType"].ToString();
            Debug.Log("MsgType : "+ MsgType);
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

                connection = DB_Init.GetConnection();
                // insert in to database
                connection.InsertOrReplace(new DB_EternalInput{
                    MsgType = "Incoming",
                    InputControlType = inputControlType,
                    // Airplane properties
                    Throttle = throttle,
                    StickyThrottle = stickyThrottle,
                    Pitch = pitch,
                    Roll = roll,
                    Yaw = yaw,
                    Brake = brake,
                    Flaps = flaps,

                });
                connection.Commit();
            }
            if (MsgType=="Transcation") // if operation type is transaction
            {   
                bool levelReload = bool.Parse(inputJson["LevelReload"].ToString());
                Debug.Log("Level reload value "+ levelReload);
                //Camera
                int activeCamera = int.Parse(inputJson["ActiveCamera"].ToString());
                //input type
                string inputControlType = inputJson["InputControlType"].ToString();
                
                connection = DB_Init.GetConnection();
                connection.InsertOrReplace(new DB_Transactions{
                    MsgType = "Transcation",
                    InputControlType = inputControlType,
                    // Camrera control
                    ActiveCamera = activeCamera,
                    //level reset
                    LevelReload = levelReload
                });
                connection.Commit();

            }
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