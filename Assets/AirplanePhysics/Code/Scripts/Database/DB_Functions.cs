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
    }
}