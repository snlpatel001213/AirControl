using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
namespace SqliteDB
{
    public static class DB_Functions
    {
        public static DB_Schema getcamera_status(SQLiteConnection connection)
        {
            return connection.Table<DB_Schema>().Where(x => x.Direction == "Incoming").FirstOrDefault();
        }
    }
}