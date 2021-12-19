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

        /// <summary>
        /// To ge the connection to the SQLite database
        /// Connection can be used to Interact with Database
        /// </summary>
        /// <returns>connection - instance of SQLiteConnection</returns>
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

        /// <summary>
        /// Delete existing DB and create new SQLite DB
        /// Insert default paramter values to DB to prevent null point exception
        /// Write parameter schema file (json). these parameters can be externally modified
        /// </summary>
        public static void CreateDB()
        {
                        
            if(File.Exists(dbPath))
            {
                //delete the older file
                CommonConfigs.DeleteFile(dbPath);
                connection = GetConnection();
                CreateTable<DB_InputSchema>(connection);
                CreateTable<DB_OutputSchema>(connection);
                CreateTable<DB_Transactions>(connection);
                // write default vaues to database to avoid the null pointer exeption
                connection.Insert(new DB_InputSchema());
                connection.Insert(new DB_OutputSchema());
                connection.Insert(new DB_Transactions());
                // Write  default json to Streaming asset folder for reference
                WriteParamSet();
                
            }
            else
            {
                connection = GetConnection();
                CreateTable<DB_InputSchema>(connection);
                CreateTable<DB_OutputSchema>(connection);
                CreateTable<DB_Transactions>(connection);
                Console.WriteLine("Database {DatabaseName} Created");
                // write default vaues to database to avoid the null pointer exeption
                connection.Insert(new DB_InputSchema());
                connection.Insert(new DB_OutputSchema());
                connection.Insert(new DB_Transactions());
               // Write  default json to Streaming asset folder for reference
                WriteParamSet();
            }
            
            
        }
        /// <summary>
        /// Write Json with all the parmaters
        /// These parameters can be modified from python
        /// </summary>
        private static void WriteParamSet()
        {
            string inputJson = JsonConvert.SerializeObject(new DB_InputSchema(),Formatting.Indented);
            string inputSchemaFilePath = System.IO.Path.Combine(persistentDataPath,"inputSchema.json");
            Debug.Log("Writting Default schema file to : "+ inputSchemaFilePath);
            File.WriteAllText(inputSchemaFilePath, inputJson);
            string outputJson = JsonConvert.SerializeObject(new DB_OutputSchema(),Formatting.Indented);
            string outputSchemaFilePath = System.IO.Path.Combine(persistentDataPath,"outputSchema.json");
            Debug.Log("Writting Default schema file to : "+ outputSchemaFilePath);
            File.WriteAllText(outputSchemaFilePath, outputJson);
            string transactionJson = JsonConvert.SerializeObject(new DB_Transactions(),Formatting.Indented);
            string transactionSchemaFilePath = System.IO.Path.Combine(persistentDataPath,"transactionSchema.json");
            Debug.Log("Writting Default schema file to : "+ transactionSchemaFilePath);
            File.WriteAllText(transactionSchemaFilePath, transactionJson);
        }

         /// <summary>
        /// Creates default table in the Database
        /// Call the function as connection.CreateTable<Person> ();
        /// </summary>
        /// <param name="_connection"></param>
        /// <typeparam name="T"></typeparam>
        public static void CreateTable<T>(SQLiteConnection connection)
        {
            connection.CreateTable<T> ();
        }   

        #endregion

    }

}
