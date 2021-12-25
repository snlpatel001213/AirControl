using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using Newtonsoft.Json.Linq;
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
        static string airControlVersion = CommonFunctions.GET_VERSION();
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
        /// Create the schema
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="schemaName">name of the Json file to be created</param>
        /// <typeparam name="T"></typeparam>
        public static void createSchema<T>( SQLiteConnection connection, string schemaName) where T : new()
        {
            DropTable<T>(connection);
            CreateTable<T>(connection);
            connection.InsertOrReplace(new T());
            string json = JsonConvert.SerializeObject(new T(),Formatting.Indented);
            string schemaFilePath = System.IO.Path.Combine(persistentDataPath,schemaName);
            File.WriteAllText(schemaFilePath, json);
            Debug.Log("Writting Default schema file to : "+ schemaFilePath);
        }

        /// <summary>
        /// Create new schem if the *schema.json dont exists. 
        /// if *schema.json exist then read the paramters and insert in the db as default parameters
        /// if version of *schema.json and version of release dont match then existing files will be deleted and new *schema.json will be created.
        /// </summary>
        public static void CreateDB()
        {
            #region DB_EternalInput
                string existingInSchemaPath = System.IO.Path.Combine(persistentDataPath,"inputSchema.json");
                // if inputschema file exists
                connection = GetConnection();
                if(File.Exists(existingInSchemaPath))
                {
                    // if version of the schema matches the release version
                    DB_EternalInput existingSchema = CommonFunctions.DeserializeJson<DB_EternalInput>(existingInSchemaPath);
                    if(existingSchema.Version.ToString() == CommonFunctions.GET_VERSION())
                    {
                        try //if schema is cchnaged then it may throw exception 
                        {
                            CreateTable<DB_EternalInput>(connection);
                            connection.InsertOrReplace(existingSchema);
                        }
                        catch{ // so when this occurs, generate new schema 
                            createSchema<DB_EternalInput>(connection,"outputSchema.json" );
                        }
                    }
                    else // if schema file dont match then use the internal defaults values and write to file
                    {
                        createSchema<DB_EternalInput>(connection,"inputSchema.json" );
                    }
                }    
                else // if schema file dont exist then use the internal defaults values and write to file
                {
                    createSchema<DB_EternalInput>(connection,"inputSchema.json" );
                }
            #endregion
            
            #region DB_EternalOutput

            #endregion

            #region DB_Transactions
                string existingTransactionSchemaPath = System.IO.Path.Combine(persistentDataPath,"transactionSchema.json");
                // if inputschema file exists
                if(File.Exists(existingTransactionSchemaPath))
                {
                    // if version of the schema matches the release version
                    DB_Transactions existingTransactionSchema = CommonFunctions.DeserializeJson<DB_Transactions>(existingTransactionSchemaPath);
                    if(existingTransactionSchema.Version.ToString() == CommonFunctions.GET_VERSION())
                    {
                        try //if schema is cchnaged then it may throw exception 
                        {
                            CreateTable<DB_Transactions>(connection);
                            connection.InsertOrReplace(existingTransactionSchema);
                        }
                        catch{ // so when this occurs, generate new schema 
                            createSchema<DB_Transactions>(connection,"outputSchema.json" );
                        }
                    }
                    else // if schema file dont match then use the internal defaults values and write to file
                    {
                        createSchema<DB_Transactions>(connection,"transactionSchema.json" );
                    }
                }    
                else // if schema file dont exist then use the internal defaults values and write to file
                {
                    createSchema<DB_Transactions>(connection,"transactionSchema.json" );
                }
            #endregion
            
            #region DB_StartUpSchema
                string existingStartUpSchemaPath = System.IO.Path.Combine(persistentDataPath,"startUpSchema.json");
                // if inputschema file exists
                if(File.Exists(existingStartUpSchemaPath))
                {
                    // if version of the schema matches the release version
                    DB_StartUpSchema existingStartUpSchema = CommonFunctions.DeserializeJson<DB_StartUpSchema>(existingTransactionSchemaPath);
                    if(existingStartUpSchema.Version.ToString() == CommonFunctions.GET_VERSION())
                    {
                        try //if schema is cchnaged then it may throw exception 
                        {
                            CreateTable<DB_StartUpSchema>(connection);
                            connection.InsertOrReplace(existingStartUpSchema);
                        }
                        catch{ // so when this occurs, generate new schema 
                            createSchema<DB_StartUpSchema>(connection,"outputSchema.json" );
                        }
                    }
                    else // if schema file dont match then use the internal defaults values and write to file
                    {
                        createSchema<DB_StartUpSchema>(connection,"startUpSchema.json" );
                    }
                }    
                else // if schema file dont exist then use the internal defaults values and write to file
                {
                    createSchema<DB_StartUpSchema>(connection,"startUpSchema.json" );
                }
            #endregion
        }    
            

         /// <summary>
        /// Creates default table in the Database
        /// Call the function as connection.CreateTable<Person> ();
        /// </summary>
        /// <param name="_connection"></param>
        /// <typeparam name="T"></typeparam>
        public static void CreateTable<T>(SQLiteConnection connection)
        {
            Debug.Log("Table Created" + typeof(T));
            connection.CreateTable<T>();
        }
        public static void DropTable<T>(SQLiteConnection connection)
        {
            Debug.Log("Table Created" + typeof(T));
            connection.DropTable<T>();
        }   

        #endregion

    }

}
