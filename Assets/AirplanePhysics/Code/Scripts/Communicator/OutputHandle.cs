using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using Newtonsoft.Json;
using AirControl;
using System.Linq;


namespace Communicator
{
    /// <summary>
    ///  Handle outbound request from the TCP socket 
    /// </summary>
    public class OutputHandle:MonoBehaviour
    {
        #region Builtin Methods
        // private SQLiteConnection connection;
        #endregion
        /// <summary>
        /// Prepare output object and return json string to be dispatched
        /// </summary>
        /// <returns>Output json string</returns>
        public string  ParseOutput()
        {
            ref var screencapture = ref StaticOutputSchema.ScreenCapture;
            // to avoid null retun from Screencapture
            if (screencapture == null){
                screencapture =  new byte[0];
            }
            
            string output = JsonConvert.SerializeObject(new OutputSchema{
                BankAngle = StaticOutputSchema.BankAngle,
                PitchAngle = StaticOutputSchema.PitchAngle,
                AGL = StaticOutputSchema.AGL,
                MSL = StaticOutputSchema.MSL,
                CurrentRPM = StaticOutputSchema.CurrentRPM,
                MaxRPM = StaticOutputSchema.MaxRPM,
                MaxPower = StaticOutputSchema.MaxPower,
                CurrentPower = StaticOutputSchema.CurrentPower,
                CurrentSpeed = StaticOutputSchema.CurrentSpeed,
                ScreenCapture = screencapture,
                LidarPointCloud = StaticOutputSchema.LidarPointCloud,
                currentLocation_x = StaticOutputSchema.currentLocation_x,
                currentLocation_y = StaticOutputSchema.currentLocation_y,
                currentLocation_z = StaticOutputSchema.currentLocation_z,
                IfCollision = StaticOutputSchema.IfCollision
            }, new PrimitiveToStringConverter());
            
            return output;
        }
        /// <summary>
        /// Just Log output if the entire output is not required
        /// </summary>
        /// <returns>Prepare output object and return json string to be dispatched</returns>
        public string LogOutput()
        {
            string LogOutput = JsonConvert.SerializeObject(new Logger{ 
                    Log =  StaticLogger.Log,
                }, new PrimitiveToStringConverter()
            );
            return LogOutput;
        }
    }

}
