using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using Newtonsoft.Json;
using AirControl;
using System.Linq;
using Commons;

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
        public void  ParseOutput(ref string outputmsg)
        {
            // To avoid null retun from Screencapture
            // To not to send the screencapture if `IsCapture` is to `false`, to save bandwidth
            ref var screencapture = ref StaticOutputSchema.ScreenCapture;
            if (screencapture == null || StaticCameraSchema.IsCapture == false){
                screencapture =  new byte[0];
                
            }

            // to avoid null retun from Screencapture
            var lidarPointCloud = StaticOutputSchema.LidarPointCloud;
            if (lidarPointCloud == null){
                lidarPointCloud =  new float[0];
            }
            
            
            outputmsg = JsonConvert.SerializeObject(new OutputSchema{
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
                LidarPointCloud = lidarPointCloud,
                Latitude = StaticOutputSchema.Latitude,
                Longitude = StaticOutputSchema.Longitude,
                IfCollision = StaticOutputSchema.IfCollision,
                Reward = StaticOutputSchema.Reward,
                Counter = StaticOutputSchema.Counter,
                CollisionObject = StaticOutputSchema.CollisionObject,
                IsFlying =  StaticOutputSchema.IsFlying,
                IsGrounded =  StaticOutputSchema.IsGrounded,
                IsTaxiing =  StaticOutputSchema.IsTaxiing,
                PosXAbs =  StaticOutputSchema.PosXAbs,
                PosYAbs =  StaticOutputSchema.PosYAbs,
                PosZAbs =  StaticOutputSchema.PosZAbs,
                PosXRel =  StaticOutputSchema.PosXRel,
                PosYRel =  StaticOutputSchema.PosYRel,
                PosZRel =  StaticOutputSchema.PosZRel,
                RotXAbs = StaticOutputSchema.RotXAbs,
                RotYAbs = StaticOutputSchema.RotYAbs,
                RotZAbs = StaticOutputSchema.RotZAbs,
                RotXRel = StaticOutputSchema.RotXRel,
                RotYRel = StaticOutputSchema.RotYRel,
                RotZRel = StaticOutputSchema.RotZRel,
                AngularXVelocity = StaticOutputSchema.AngularXVelocity,
                AngularYVelocity = StaticOutputSchema.AngularYVelocity,
                AngularZVelocity = StaticOutputSchema.AngularZVelocity,
                LinearXVelocity = StaticOutputSchema.LinearXVelocity,
                LinearYVelocity = StaticOutputSchema.LinearYVelocity,
                LinearZVelocity = StaticOutputSchema.LinearZVelocity,
                AngularXAcceleration = StaticOutputSchema.AngularXAcceleration,
                AngularYAcceleration = StaticOutputSchema.AngularYAcceleration,
                AngularZAcceleration = StaticOutputSchema.AngularZAcceleration,
                LinearXAcceleration = StaticOutputSchema.LinearXAcceleration,
                LinearYAcceleration = StaticOutputSchema.LinearYAcceleration,
                LinearZAcceleration = StaticOutputSchema.LinearZAcceleration,
                
            }, new PrimitiveToStringConverter());
            StaticOutputSchema.Counter ++; 
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
