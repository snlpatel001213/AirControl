using System.Collections;
using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace SqliteDB
{
	public class DB_Schema 
	{
		/// <summary>
		/// Just [PrimaryKey] is added to the Id Attribute as we only want the updated value and dont want to accumulate it
		/// if one need to auto increment it the Id fielsd can be configured as [PrimaryKey, AutoIncrement]
		/// In this case it will fill up the database. [Not supported]
		/// </summary>
		/// <value></value>
		[PrimaryKey]
		public string Direction  { get; set; }
		// Which camera is active
		public int ActiveCamera { get; set; }
		// if sun is present 
		public bool sunLocation { get; set; }
		// Airplane weight - to simulate the effect of weight on airplane
		public float AirplaneMass {get; set;}
		// Airplane drag
		public float AirplaneDrag {get; set;}
		// Airplane Angular Drag
		public float AirplaneAngularDrag {get; set;}
		//Airplane collison
		public bool AirplaneCollison {get; set;}

		//Control Pitch
		public float Pitch  {get; set;}
		//Control Roll
		public float Roll  {get; set;}
		//Control Yaw
		public float Yaw  {get; set;}
		//Control Throttle
		public float Throttle  {get; set;}
		//Control Brake
		public float Brake  {get; set;}
		//Control Flaps
		public float Flaps  {get; set;}

		public override string ToString ()
		{
			return string.Format ("[Active Camera={0}]", ActiveCamera);
		}
		
	}
}
