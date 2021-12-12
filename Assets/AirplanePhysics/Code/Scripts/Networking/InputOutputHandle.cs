using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using  AirControl;
namespace AirControl
{
    public class InputOutputHandle
    {
        #region Builtin Methods
        // Update is called once per frame
        // void Update()
        // {
        //     Outputhandel();
        // }
        #endregion

        #region Custom Methods
        // receive msg to unity
        public void ParseInput(string receivedString)
        {
            //parse input
            var incoming =  JObject.Parse(receivedString);
            Debug.Log("received string  : " +  incoming);
            var vari_ = incoming["type"].ToString();
            Debug.Log(incoming[vari_]);
            // inStruct.(inStruct.type)
            // call a fucntion to set this input to the rigid body 
        }

        // send msg out of unity
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

