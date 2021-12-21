using System.Collections;
using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using Commons;
namespace SqliteDB
{
	/// <summary>
	/// Class to get input from external program
	/// </summary>
	public class DB_EternalInput 
	{
		/// <summary>
		/// Just [PrimaryKey] is added to the Id Attribute as we only want the updated value and dont want to accumulate it
		/// if one need to auto increment it the Id fielsd can be configured as [PrimaryKey, AutoIncrement]
		/// In this case it will fill up the database. [Not supported]
		/// </summary>
		/// <value></value>
		[PrimaryKey]
		public string MsgType  { get; set; } = "Incoming";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		// if sun is present
		public bool sunPresent  { get; set; } = true;
		public float sunLocation_x { get; set; } = 0f;
		public float sunLocation_y { get; set; } = 0f;
		public float sunLocation_z { get; set; } = 0f;
		public float sunRotation_x { get; set; } = 0f;
		public float sunRotation_y { get; set; } = 0f;
		public float sunRotation_z { get; set; } = 0f;
		// Airplane drag 
		public float AirplaneDrag {get; set;} = 0.01f;
		// Airplane Angular Drag
		public float AirplaneAngularDrag {get; set;} = 0.1f;
		//Airplane Properties 
		public float AirplanemaxMPH {get; set;} = 150f;
		//Airplane Properties 
		public float MaxLiftPower {get; set;} = 200f;
		//Airplane Properties 
		//Control Pitch
		public float Pitch  {get; set;}=0f;
		//Control Roll
		public float Roll  {get; set;} = 0f;
		//Control Yaw
		public float Yaw  {get; set;}=0f;
		//Control Throttle
		public float Throttle  {get; set;}=0f;
		//Control Sticky Throttle
		public float StickyThrottle  {get; set;}=0f;
		//Control Brake
		public float Brake  {get; set;}=0f;
		//Control Flaps
		public int Flaps  {get; set;}=0;

		// public override string ToString ()
		// {
		// 	return string.Format ("[Active Camera={0}]", ActiveCamera);
		// }

	}

	/// <summary>
	/// Class to output to external program
	/// </summary>
	public class DB_EternalOutput
	{
		[PrimaryKey]
		public string MsgType  { get; set; } = "Outgoing";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
	}
	/// <summary>
	/// Class to just transact once to the game engine
	/// This can be used for level releoad or setting gameplay volume etc 
	/// </summary>
	public class DB_Transactions
	{	
		[PrimaryKey]
		public string MsgType  { get; set; } = "Transcation";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		//reload the level if this is set true
		public bool LevelReload {get; set;} = false; 
		// Which camera is active
		public int ActiveCamera { get; set; } = 0;
	}

	public class DB_StartUpSchema
	{	
		[PrimaryKey]
		public string MsgType  { get; set; } = "StartUp";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//set Airplane masss
		public float airplaneMass {get; set;} = 1000f; 
		//set Airplane fuel
		public float fuel {get; set;} = 61f;
		//set Airplane fuel burnrate
		public float fuelBurnRate {get; set;} = 6.1f;  
		//set Airplane start Location
		public float startLocation_x {get; set;} = 6.1f;  
		public float startLocation_y {get; set;} = 6.1f;  
		public float startLocation_z {get; set;} = 6.1f;  
		//set Airplane start Rotation
		public float startRotation_x {get; set;} = 6.1f;  
		public float startRotation_y {get; set;} = 6.1f;  
		public float startRotation_z {get; set;} = 6.1f; 
	}
	

}
