using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using Newtonsoft.Json;
using AirControl;
using Commons;

namespace SqliteDB
{
    public class DB_Init
    {
        #region Variables
        static SQLiteConnection connection ;
        static string persistentDataPath = Application.streamingAssetsPath;
        static string airControlVersion = CommonConfigs.GET_VERSION();
        static string DatabaseName =  "AirControl-"+airControlVersion+".sqlite";
        static string dbPath = System.IO.Path.Combine(persistentDataPath, DatabaseName);
        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods

        public static SQLiteConnection GetConnection()
        {

            #if UNITY_EDITOR
                var filepath = System.IO.Path.Combine(persistentDataPath, DatabaseName);
            #else
                // check if file exists in Application.persistentDataPath
                var filepath = System.IO.Path.Combine(persistentDataPath, DatabaseName);
            #endif

            SQLiteConnection connection = new SQLiteConnection(filepath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            return connection; 

	    }

        public static void CreateDB()
        {
                        
            if(File.Exists(dbPath))
            {
                //delete the older file
                CommonConfigs.DeleteFile(dbPath);
                connection = GetConnection();
                CreateTable<DB_Schema>(connection);
                connection.InsertOrReplace(new DB_Schema());
                // Write  default json to Streaming asset folder for reference
                WriteParamSet();
                
            }
            else
            {
                connection = GetConnection();
                CreateTable<DB_Schema>(connection);
                Console.WriteLine("Database {DatabaseName} Created");
                // write default vaues to database to avoid the null pointer exeption
                connection.InsertOrReplace(new DB_Schema());
               
                WriteParamSet();
            }
            
            
        }
        private static void WriteParamSet()
        {
            string json = JsonConvert.SerializeObject(new DB_Schema(),Formatting.Indented);
            string schemaFilePath = System.IO.Path.Combine(persistentDataPath,"schema.json");
            Debug.Log(schemaFilePath);
            File.WriteAllText(schemaFilePath, json);
        }

         /// <summary>
        /// Call the functopn as _connection.CreateTable<Person> ();
        /// </summary>
        /// <param name="_connection"></param>
        /// <typeparam name="T"></typeparam>
        public static void CreateTable<T>(SQLiteConnection connection)
        {
            connection.CreateTable<T> ();
            Debug.Log("Table created");
        }   

        #endregion

    }

}
