using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using AirControl;


public class DB_Test : MonoBehaviour
{
    // Start is called before the first frame update
   public SQLiteConnection _connection ;
    void Start()
    {
        string persistentDataPath = Application.streamingAssetsPath;
        string airControlVersion = CommonConfigs.GET_VERSION();
        string DatabaseName =  "AirControl-"+airControlVersion+".sqlite";
        string dbPath = System.IO.Path.Combine(persistentDataPath, DatabaseName);
        
        if(File.Exists(dbPath))
        {
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        }
        else
        {
            _connection = DB_InitDatabase.GetConnection(persistentDataPath,DatabaseName);
            DB_InitDatabase.CreateTable<DB_InputClassDefinitions>( _connection);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        DB_InputClassDefinitions temp = new DB_InputClassDefinitions{
				direction = "incoming",
                // date = DateTime.Now,
				ActiveCamera = 1,
				// Surname = "Sivan",
				// Age = 27
                };
        _connection.InsertOrReplace(temp);
    }
    public DB_InputClassDefinitions getcamera_status(SQLiteConnection _connection)
    {
        return _connection.Table<DB_InputClassDefinitions>().Where(x => x.direction == "incoming").FirstOrDefault();
    } 
}
