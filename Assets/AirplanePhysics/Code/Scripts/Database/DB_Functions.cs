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
            public static string getInputControType(DB_InputSchema DBRow)
            {
                return DBRow.InputControlType;
            }
            #endregion
            
            #region Camera
            public static int getCameraStatus(DB_InputSchema DBRow)
            {
                return DBRow.ActiveCamera;
            }
            #endregion

            #region Airplane Properties
            public static float getPitch(DB_InputSchema DBRow)
            {
                return DBRow.Pitch;
            }
            public static float getRoll(DB_InputSchema DBRow)
            {
                return DBRow.Roll;
            }
            public static float getYaw(DB_InputSchema DBRow)
            {
                return DBRow.Yaw;
            }
            public static float getThrottle(DB_InputSchema DBRow)
            {
                return DBRow.Throttle;
            }
            public static float getStickyThrottle(DB_InputSchema DBRow)
            {
                return DBRow.StickyThrottle;
            }
            public static float getBrake(DB_InputSchema DBRow)
            {
                return DBRow.Brake;
            }
            public static int getFlaps (DB_InputSchema DBRow)
            {
                return DBRow.Flaps;
            }
            public static float getAirplaneDrag (DB_InputSchema DBRow)
            {
                return DBRow.AirplaneDrag;
            }
            public static float getAirplaneAngularDrag (DB_InputSchema DBRow)
            {
                return DBRow.AirplaneAngularDrag;
            }
            public static float getAirplanemaxMPH (DB_InputSchema DBRow)
            {
                return DBRow.AirplanemaxMPH;
            }
            public static float getMaxLiftPower (DB_InputSchema DBRow)
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
                    DB_Transactions currentSchema = connection.Table<DB_Transactions>().Where(x => x.Direction == "Transcation").FirstOrDefault();
                    return currentSchema.LevelReload;
                }
            /// <summary>
            /// Reset the variable `LevelReset` to false to prevent repeated firing
            /// </summary>
            /// <param name="connection"></param>
            public static void resetLevelReset(SQLiteConnection connection)
                {
                    connection.InsertOrReplace(new DB_Transactions{
                    Direction = "Incoming",
                    LevelReload = false
                    });

                }
        #endregion
    }
}