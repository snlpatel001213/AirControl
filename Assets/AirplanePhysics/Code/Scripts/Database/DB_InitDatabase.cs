using SQLite4Unity3d;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public static class DB_InitDatabase
{
	public static SQLiteConnection GetConnection(string persistentDataPath, string DatabaseName){

		#if UNITY_EDITOR
					var filepath = System.IO.Path.Combine(persistentDataPath, DatabaseName);
		#else
				// check if file exists in Application.persistentDataPath
				var filepath = System.IO.Path.Combine(persistentDataPath, DatabaseName);
		#endif

		SQLiteConnection _connection = new SQLiteConnection(filepath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        return _connection; 

	}
    /// <summary>
    /// Call the functopn as _connection.CreateTable<Person> ();
    /// </summary>
    /// <param name="_connection"></param>
    /// <typeparam name="T"></typeparam>
   public static void CreateTable<T>(SQLiteConnection _connection){
       _connection.CreateTable<T> ();
       Debug.Log("Table creeated");
   } 
    // path for other platform
   // 		if (!File.Exists(filepath))
		// 		{
		// 			Debug.Log("Database not in Persistent path - initializing new database");
                    
		// 			// if it doesn't ->
		// 			// open StreamingAssets directory and load the db ->

		// #if UNITY_ANDROID 
		// 			var loadDb = new WWW("jar:file://" + persistentDataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
		// 			while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		// 			// then save to Application.persistentDataPath
		// 			File.WriteAllBytes(filepath, loadDb.bytes);
		// #elif UNITY_IOS
		// 				var loadDb =  System.IO.Path.Combine(persistentDataPath , "Raw", DatabaseName);  // this is the path to your StreamingAssets in iOS
		// 				// then save to Application.persistentDataPath
		// 				File.Copy(loadDb, filepath);
		// #elif UNITY_WP8
		// 				var loadDb = System.IO.Path.Combine(persistentDataPath , "StreamingAssets", DatabaseName);  // this is the path to your StreamingAssets in iOS
		// 				// then save to Application.persistentDataPath
		// 				File.Copy(loadDb, filepath);

		// #elif UNITY_WINRT
		// 		var loadDb = System.IO.Path.Combine(persistentDataPath, "StreamingAssets" ,DatabaseName);  // this is the path to your StreamingAssets in iOS
		// 		// then save to Application.persistentDataPath
		// 		File.Copy(loadDb, filepath);
				
		// #elif UNITY_STANDALONE_OSX
		// 		var loadDb = System.IO.Path.Combine(persistentDataPath,"Resources/Data/StreamingAssets", DatabaseName);  // this is the path to your StreamingAssets in iOS
		// 		// then save to Application.persistentDataPath
		// 		File.Copy(loadDb, filepath);
                
		// #else
		// 	var loadDb = System.IO.Path.Combine(persistentDataPath , "StreamingAssets", DatabaseName);  // this is the path to your StreamingAssets in iOS
		// 	// then save to Application.persistentDataPath
		// 	File.Copy(loadDb, filepath);

		// #endif

		// 			Debug.Log("Database written");
		// 		}
}
