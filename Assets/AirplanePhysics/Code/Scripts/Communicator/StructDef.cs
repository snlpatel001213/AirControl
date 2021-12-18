using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Communicator
{
    
    public static class StructDef
    {
        
        #region Variables        
        // Output Struct // struct to send msg out of unity

        public struct OutputDataStructure
        {
            public float pitch;
            public float roll;
            public float yaw;
            public float throttle;
            public float brake;
            public int flaps;
        };
        // OutputDataStructure outDataStruct;

        // Input Struct  // struct to receive msg to unity
        public struct InputDataStruct
        {
            public float pitch;
            public float roll;
            public float yaw;
            public float throttle;
            public float brake;
            public int flaps;
        };

        public struct Camera{
            public bool setActive;
        };

        public struct Init{
            public string airplaneName;
            public float maxRPM;
        }

        public struct InputStruct{
            public string type;
            public InputDataStruct inData;
            public Camera camera;
            public Init init;
        };


        #endregion

    }
}