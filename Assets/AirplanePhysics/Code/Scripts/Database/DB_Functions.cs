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
            public static string getInputControType(SQLiteConnection connection)
            {
                DB_InputSchema controlType = connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").First();
                return controlType.InputControlType;
            }
            #endregion
            
            #region Camera
            public static int getCameraStatus(SQLiteConnection connection)
            {
                DB_InputSchema currentSchema = connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
                return currentSchema.ActiveCamera;

            }
            #endregion

            #region Airplane Properties
            public static float getPitch(SQLiteConnection connection)
            {
                DB_InputSchema currentSchema =  connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
                return currentSchema.Pitch;
            }
            public static float getRoll(SQLiteConnection connection)
            {
                DB_InputSchema currentSchema =  connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
                return currentSchema.Roll;
            }
            public static float getYaw(SQLiteConnection connection)
            {
                DB_InputSchema currentSchema =  connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
                return currentSchema.Yaw;
            }
            public static float getThrottle(SQLiteConnection connection)
            {
                return connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").First().Throttle;
            }
            public static float getStickyThrottle(SQLiteConnection connection)
            {
                DB_InputSchema currentSchema =  connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
                return currentSchema.StickyThrottle;
            }
            public static float getBrake(SQLiteConnection connection)
            {
                DB_InputSchema currentSchema =  connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
                return currentSchema.Brake;
            }
            public static int getFlaps (SQLiteConnection connection)
            {
                DB_InputSchema currentSchema =  connection.Table<DB_InputSchema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
                return currentSchema.Flaps;
            }
            #endregion
        #endregion


        #region Transaction
            public static bool getLevelReset(SQLiteConnection connection)
                {
                    DB_Transactions currentSchema = connection.Table<DB_Transactions>().Where(x => x.Direction == "Transcation").FirstOrDefault();
                    return currentSchema.LevelReload;

                }
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