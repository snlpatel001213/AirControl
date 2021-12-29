using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
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
        static string persistentDataPath = Application.streamingAssetsPath;
        static string airControlVersion = CommonFunctions.GET_VERSION();
        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods

        /// <summary>
        /// Create the schema
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="schemaName">name of the Json file to be created</param>
        /// <typeparam name="T"></typeparam>
        public static void createSchema<T>( string schemaName) where T : new()
        {
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
            string existingInSchemaPath = System.IO.Path.Combine(persistentDataPath,"inputSchema.json");
            // Write schema to external file for reference
            createSchema<DB_EternalInput>("inputSchema.json" );

            string existingTransactionSchemaPath = System.IO.Path.Combine(persistentDataPath,"transactionSchema.json");
           // Write schema to external file for reference
            createSchema<DB_Transactions>("transactionSchema.json" );
            
            string existingStartUpSchemaPath = System.IO.Path.Combine(persistentDataPath,"startUpSchema.json");
            // Write schema to external file for reference
            createSchema<DB_StartUpSchema>("startUpSchema.json" );
        }    

        #endregion

    }

}
