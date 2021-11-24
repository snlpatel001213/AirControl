using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace AirControl
{
    public class InputOutputHandel
    {
        #region Variables
        public AC_BaseAirplane_Input currentReadings;
        // public TCPTestServer networkUtils;
        
        // Output Struct // struct to send msg out of unity
        struct outputStructure
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
        struct inputStructure
        {
            
        }

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
        void InputHandel()
        {

        }
        // send msg out of unity
        public string  Outputhandel()
        {
            // Debug.Log("listening to controls : "+currentReadings.Pitch);
            // Put data to structure
            
            Debug.Log("listening to controls : "+currentReadings.Pitch);
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

