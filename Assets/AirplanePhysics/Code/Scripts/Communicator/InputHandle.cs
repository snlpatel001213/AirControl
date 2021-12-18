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
    [RequireComponent(typeof(DB_Init))]
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
            var incoming =  JObject.Parse(receivedString);
            int activeCamera = int.Parse(incoming["ActiveCamera"].ToString());
            connection = DB_Init.GetConnection();
            connection.InsertOrReplace( new DB_Schema{
                Direction = "Incoming",
                ActiveCamera = activeCamera,
            });
            
        }

        // send msg out of unity
        // this will be moved to outputHandle class
        public string  ParseOutput(AC_BaseAirplane_Input currentReadings)
        {
            StructDef.OutputDataStructure outDataStruct;
            // Put data to structure
            outDataStruct.pitch = currentReadings.Pitch;
            outDataStruct.roll = currentReadings.Roll;
            outDataStruct.yaw = currentReadings.Yaw;
            outDataStruct.throttle = currentReadings.Throttle;
            outDataStruct.brake = currentReadings.Brake;
            outDataStruct.flaps = currentReadings.Flaps;
            
            // Convert to Json and Serialize data
            string outStructSerialized = JsonConvert.SerializeObject(outDataStruct);
            return outStructSerialized;

            // networkUtils.SendMessage(outStructSerialized);
        }

        #endregion

    }
}