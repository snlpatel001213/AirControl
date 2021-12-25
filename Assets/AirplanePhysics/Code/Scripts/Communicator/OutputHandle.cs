using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using SqliteDB;
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
            string output = JsonConvert.SerializeObject(new DB_EternalOutput{
                BankAngle = DB_StaticEternalOutput.BankAngle,
                PitchAngle = DB_StaticEternalOutput.PitchAngle,
                AGL = DB_StaticEternalOutput.AGL,
                MSL = DB_StaticEternalOutput.MSL,
                CurrentRPM = DB_StaticEternalOutput.CurrentRPM,
                MaxRPM = DB_StaticEternalOutput.MaxRPM,
                MaxPower = DB_StaticEternalOutput.MaxPower,
                CurrentPower = DB_StaticEternalOutput.CurrentPower,
                CurrentSpeed = DB_StaticEternalOutput.CurrentSpeed
            });
            
            return output;
        }
    }

}
