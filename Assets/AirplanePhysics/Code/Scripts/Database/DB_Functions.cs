using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
namespace SqliteDB
{
    public static class DB_Functions
    {
        #region Input
            #region InputControlType
            /// <summary>
            /// Get the control type
            /// The InputControltype can be "Code" or "Other"
            /// "Code" meansd it can be controlled throught any external program
            /// "Other" indicates Keyboard or Joystick
            /// </summary>
            /// <param name="connection"></param>
            /// <returns>Control type stored in DB</returns>
            public static string getInputControType(DB_EternalInput DBRow)
            {
                return DBRow.InputControlType;
            }
            #endregion
            
            #region Camera
            public static int getCameraStatus(DB_Transactions DBRow)
            {
                return DBRow.ActiveCamera;
            }
            #endregion

            #region Airplane Properties
            public static float getPitch(DB_EternalInput DBRow)
            {
                return DBRow.Pitch;
            }
            public static float getRoll(DB_EternalInput DBRow)
            {
                return DBRow.Roll;
            }
            public static float getYaw(DB_EternalInput DBRow)
            {
                return DBRow.Yaw;
            }
            public static float getThrottle(DB_EternalInput DBRow)
            {
                return DBRow.Throttle;
            }
            public static float getStickyThrottle(DB_EternalInput DBRow)
            {
                return DBRow.StickyThrottle;
            }
            public static float getBrake(DB_EternalInput DBRow)
            {
                return DBRow.Brake;
            }
            public static int getFlaps (DB_EternalInput DBRow)
            {
                return DBRow.Flaps;
            }
            public static float getAirplaneDrag (DB_EternalInput DBRow)
            {
                return DBRow.AirplaneDrag;
            }
            public static float getAirplaneAngularDrag (DB_EternalInput DBRow)
            {
                return DBRow.AirplaneAngularDrag;
            }
            public static float getAirplanemaxMPH (DB_EternalInput DBRow)
            {
                return DBRow.AirplanemaxMPH;
            }
            public static float getMaxLiftPower (DB_EternalInput DBRow)
            {
                return DBRow.MaxLiftPower;
            }
            #endregion
        #endregion

        #region Transaction
            /// <summary>
            /// Trigger Level Reset
            /// </summary>
            /// <param name="connection"></param>
            /// <returns></returns>
            public static bool getLevelReset(SQLiteConnection connection)
                {
                    DB_Transactions currentSchema = connection.Table<DB_Transactions>().Where(x => x.MsgType == "Transcation").FirstOrDefault();
                    return currentSchema.LevelReload;
                }
            /// <summary>
            /// Get the control type
            /// The InputControltype can be "Code" or "Other"
            /// "Code" meansd it can be controlled throught any external program
            /// "Other" indicates Keyboard or Joystick
            /// </summary>
            /// <param name="connection"></param>
            /// <returns>Control type stored in DB</returns>
            public static string getInputControType(DB_Transactions DBRow)
            {
                return DBRow.InputControlType;
            }
            /// <summary>
            /// Reset the variable `LevelReset` to false to prevent repeated firing
            /// </summary>
            /// <param name="connection"></param>
            public static void resetLevelReset(SQLiteConnection connection)
                {
                    DB_Transactions currentSchema = connection.Table<DB_Transactions>().Where(x => x.MsgType == "Transcation").FirstOrDefault();
                    currentSchema.LevelReload =false;
                    connection.InsertOrReplace(currentSchema);

                }
            /// <summary>
            /// Set active to false to decrese the computational load
            /// </summary>
            /// <param name="connection"></param>
            public static void UnsetActive(SQLiteConnection connection){
                DB_Transactions currentSchema = connection.Table<DB_Transactions>().Where(x => x.MsgType == "Transcation").FirstOrDefault();
                currentSchema.IsActive =false;
                connection.InsertOrReplace(currentSchema);
            }
        #endregion

        #region Output
            public static void SetMSLAndAGL(SQLiteConnection connection, float msl, float agl){

                DB_EternalOutput currentSchema = connection.Table<DB_EternalOutput>().Where(x => x.MsgType == "Outgoing").FirstOrDefault();
                currentSchema.MSL = msl;
                currentSchema.AGL = agl;
                connection.InsertOrReplace(currentSchema);
                
            }
            public static void SetEngineVariables(SQLiteConnection connection, float maxForce, float finalPower, float maxRPM, float currentRPM)
            {
                DB_EternalOutput currentSchema = connection.Table<DB_EternalOutput>().Where(x => x.MsgType == "Outgoing").FirstOrDefault();
                currentSchema.MaxRPM = maxRPM;
                currentSchema.MaxPower = maxForce;
                currentSchema.CurrentRPM =currentRPM;
                currentSchema.CurrentPower = finalPower;
                connection.InsertOrReplace(currentSchema);
            }
            public static void SetSpeed(SQLiteConnection connection, float airplaneSpeed)
            {
                DB_EternalOutput currentSchema = connection.Table<DB_EternalOutput>().Where(x => x.MsgType == "Outgoing").FirstOrDefault();
                currentSchema.CurrentSpeed = airplaneSpeed;
                connection.InsertOrReplace(currentSchema);
            }
            public static void SetAirplaneAngle(SQLiteConnection connection, float bankAngle, float pitchAngle)
            {
                DB_EternalOutput currentSchema = connection.Table<DB_EternalOutput>().Where(x => x.MsgType == "Outgoing").FirstOrDefault();
                currentSchema.BankAngle = bankAngle;
                currentSchema.PitchAngle = pitchAngle;
                connection.InsertOrReplace(currentSchema);
            }
        #endregion
    }
}