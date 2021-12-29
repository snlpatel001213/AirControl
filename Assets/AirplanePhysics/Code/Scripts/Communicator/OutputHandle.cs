using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using Newtonsoft.Json;
using AirControl;
using System.Linq;


namespace Communicator
{
    public class OutputHandle:MonoBehaviour
    {
        #region Builtin Methods
        // private SQLiteConnection connection;
        #endregion
        
        public string  ParseOutput()
        {
            var screencapture = StaticOutputSchema.ScreenCapture;
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
            });
            
            return output;
        }
    }

}
