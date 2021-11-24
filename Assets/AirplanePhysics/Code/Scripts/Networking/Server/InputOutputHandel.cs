using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace AirControl
{
    public class InputOutputHandel
    {
        #region Variables
        // public TCPTestServer networkUtils;
        
        // Output Struct // struct to send msg out of unity
        public struct outputStructure
        {
            public float pitch;
            public float roll;
            public float yaw;
            public float throttle;
            public float brake;
            public int flaps;
        };
        outputStructure outStruct;

        // Input Struct  // struct to receive msg to unity
        public struct inputStructure
        {
            public float pitch;
            public float roll;
            public float yaw;
            public float throttle;
            public float brake;
            public int flaps;
        };
        inputStructure inStruct;

        #endregion

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
            inputStructure inStructDeserialized =  JsonConvert.DeserializeObject<inputStructure>(receivedString);
            Debug.Log("received string  : " +  receivedString);
            // call a fucntion to set this input to the rigid body 
            //pending
        }

        // send msg out of unity
        public string  ParseOutput(AC_BaseAirplane_Input currentReadings)
        {
            // Debug.Log("listening to controls : "+currentReadings.Pitch);
            // Put data to structure
            
            outStruct.pitch = currentReadings.Pitch;
            outStruct.roll = currentReadings.Roll;
            outStruct.yaw = currentReadings.Yaw;
            outStruct.throttle = currentReadings.Throttle;
            outStruct.brake = currentReadings.Brake;
            outStruct.flaps = currentReadings.Flaps;
            
            // Convert to Json and Serialize data
            string outStructSerialized = JsonConvert.SerializeObject(outStruct);
            return outStructSerialized;

            // networkUtils.SendMessage(outStructSerialized);
        }

        #endregion

    }
}

