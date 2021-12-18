using System.Collections;
using SQLite4Unity3d;
using System;
using System.Collections.Generic;
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
		public string Direction  { get; set; } = "Incoming";
		// Which camera is active
		public int ActiveCamera { get; set; } = 0;
		// if sun is present
		public bool sunPresent  { get; set; } = true;
		public float sunLocation_x { get; set; } = 0f;
		public float sunLocation_y { get; set; } = 0f;
		public float sunLocation_z { get; set; } = 0f;
		public float sunRotation_x { get; set; } = 0f;
		public float sunRotation_y { get; set; } = 0f;
		public float sunRotation_z { get; set; } = 0f;
		// Airplane weight - to simulate the effect of weight on airplane
		public float AirplaneMass {get; set;} = 1200f;
		// Airplane drag 
		public float AirplaneDrag {get; set;} = 0.01f;
		// Airplane Angular Drag
		public float AirplaneAngularDrag {get; set;} = 0.1f;
		//Airplane collison
		public bool AirplaneCollison {get; set;} = false;
		//Airplane Properties 
		public float AirplanemaxMPH {get; set;} = 150f;
		//Airplane Properties 
		public float maxLiftPower {get; set;} = 200f;
		//Airplane Properties 
		//Control Pitch
		public float Pitch  {get; set;}=0f;
		//Control Roll
		public float Roll  {get; set;} = 0f;
		//Control Yaw
		public float Yaw  {get; set;}=0f;
		//Control Throttle
		public float Throttle  {get; set;}=0f;
		//Control Brake
		public float Brake  {get; set;}=0f;
		//Control Flaps
		public float Flaps  {get; set;}=0f;

		public override string ToString ()
		{
			return string.Format ("[Active Camera={0}]", ActiveCamera);
		}
		
	}
}
