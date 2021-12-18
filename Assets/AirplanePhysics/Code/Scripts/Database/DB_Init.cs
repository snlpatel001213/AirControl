using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using AirControl;
using Commons;

namespace SqliteDB
{
    public class DB_Init : MonoBehaviour
    {
        #region Variables
        static SQLiteConnection connection ;
        static string persistentDataPath = Application.streamingAssetsPath;
        static string airControlVersion = CommonConfigs.GET_VERSION();
        static string DatabaseName =  "AirControl-"+airControlVersion+".sqlite";
        static string dbPath = System.IO.Path.Combine(persistentDataPath, DatabaseName);
        #endregion

        #region Builtin Methods
        static void Awake()
        {
            Debug.Log("Called Awake");
             CreateDB();
        }
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

        static void CreateDB()
        {
                        
            if(File.Exists(dbPath))
            {
                //delete the older file
                CommonConfigs.DeleteFile(dbPath);
                // connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
                connection = GetConnection();
                CreateTable<DB_Schema>(connection);
                
            }
            else
            {
                connection = GetConnection();
                CreateTable<DB_Schema>(connection);
                Console.WriteLine("Database {DatabaseName} Created");
            }
            
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
