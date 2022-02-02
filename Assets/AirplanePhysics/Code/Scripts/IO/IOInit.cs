using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AirControl;
using Commons;

namespace Communicator
{
    /// <summary>
    /// Input Output init
    /// Writting schema files to the disk for reference
    /// </summary>
    public class IOInit
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
            string json = JsonConvert.SerializeObject(new T(),Formatting.Indented, new PrimitiveToStringConverter());
            string schemaFilePath = System.IO.Path.Combine(persistentDataPath,schemaName);
            File.WriteAllText(schemaFilePath, json);
            // Debug.Log("Writting Default schema file to : "+ schemaFilePath);
        }

        /// <summary>
        /// Create new schem if the *schema.json if it exists or not.
        /// These schema can be used for to format python API 
        /// </summary>
        public static void CreateSchema()
        {
            // Write schema to external file for reference
            createSchema<ControlSchema>("ControlSchema.json");
            createSchema<OutputSchema>("OutputSchema.json");
            createSchema<TODSchema>("TOD.json");
            createSchema<CameraSchema>("CameraSchema.json");
            createSchema<LevelSchema>("LevelSchema.json");
            createSchema<WeatherSchema>("WeatherSchema.json");
            createSchema<UISchema>("UISchema.json");
            createSchema<AudioSchema>("AudioSchema.json");
            createSchema<LidarSchema>("LidarSchema.json");
            createSchema<FuelSchema>("FuelSchema.json");
            createSchema<PresetSchema>("PresetSchema.json");

        }    

        

        #endregion

    }
    /// <summary>
    /// Converting boolena to string while serializing
    /// </summary>
    class PrimitiveToStringConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(bool);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString().ToLower());
            }

            public override bool CanRead
            {
                get { return false; }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

}
