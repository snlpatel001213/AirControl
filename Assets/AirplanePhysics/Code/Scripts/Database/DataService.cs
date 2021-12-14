﻿// https://github.com/codemaker2015/sqlite-unity-demo


using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

		#if UNITY_EDITOR
					var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
				// check if file exists in Application.persistentDataPath
				var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

				if (!File.Exists(filepath))
				{
					Debug.Log("Database not in Persistent path");
					// if it doesn't ->
					// open StreamingAssets directory and load the db ->

		#if UNITY_ANDROID 
					var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
					while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
					// then save to Application.persistentDataPath
					File.WriteAllBytes(filepath, loadDb.bytes);
		#elif UNITY_IOS
						var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
						// then save to Application.persistentDataPath
						File.Copy(loadDb, filepath);
		#elif UNITY_WP8
						var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
						// then save to Application.persistentDataPath
						File.Copy(loadDb, filepath);

		#elif UNITY_WINRT
				var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
				// then save to Application.persistentDataPath
				File.Copy(loadDb, filepath);
				
		#elif UNITY_STANDALONE_OSX
				var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
				// then save to Application.persistentDataPath
				File.Copy(loadDb, filepath);
		#else
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);

		#endif

					Debug.Log("Database written");
				}

				var dbPath = filepath;
		#endif
					_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
				Debug.Log("Final PATH: " + dbPath);     

	}

	public void CreateDB(){
		_connection.DropTable<Person> ();
		_connection.CreateTable<Person> ();
		_connection.Insert(new Person{
				Id = 1,
				Name = "Janki",
				Surname = "Patel",
				Age = 27
			});
		_connection.InsertAll (new[]{
			new Person{
				Id = 1,
				Name = "Sunil",
				Surname = "Sivan",
				Age = 27
			},
			new Person{
				Id = 2,
				Name = "Hema",
				Surname = "Dileep",
				Age = 26
			},
			new Person{
				Id = 3,
				Name = "Aiswarya",
				Surname = "Sethu",
				Age = 25
			},
			new Person{
				Id = 4,
				Name = "Jithin",
				Surname = "B",
				Age = 25
			}
		});
	}

	public IEnumerable<Person> GetPersons(){
		return _connection.Table<Person>();
	}

	public IEnumerable<Person> GetPersonsNamedVishnu(){
		return _connection.Table<Person>().Where(x => x.Name == "Vishnu");
	}

	public Person GetVishnu(){
		return _connection.Table<Person>().Where(x => x.Name == "Vishnu").FirstOrDefault();
	}

	public Person CreatePerson(){
		var p = new Person{
				Name = "Arun",
				Surname = "Kumar",
				Age = 30
		};
		_connection.Insert (p);
		return p;
	}
}
