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

	}

	/// <summary>
	/// Class to output to external program
	/// </summary>
	public static class DB_StaticEternalOutput
	{
		public static string MsgType  { get; set; } = "Outgoing";
		//Version of the sceme, IT will be same as the release version
		public static  string Version {get;set;} = CommonFunctions.GET_VERSION();
		public static float AGL;
		public static float MSL;
		public static float CurrentRPM;
		public static float MaxRPM;
		public static float MaxPower;
		public static float CurrentPower;
		public static float CurrentFuel;
		public static float CurrentSpeed;
		public static float BankAngle;
		public static float PitchAngle;
		public static byte [] ScreenCapture;
		public static float [] lidarPointCloud;
	}
	public class DB_EternalOutput
	{
		public string MsgType  { get; set; } = "Outgoing";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		public float AGL;
		public float MSL;
		public float CurrentRPM;
		public float MaxRPM;
		public float MaxPower;
		public float CurrentPower;
		public float CurrentFuel;
		public float CurrentSpeed;
		public float BankAngle;
		public float PitchAngle;
		public byte [] ScreenCapture;
		public static float [] lidarPointCloud;
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
		// if transaction schema is active or not this is to reduce computation load in the update loops
		public bool IsActive {get; set;} = false;
		//reload the level if this is set true
		public bool LevelReload {get; set;} = false; 
		// Which camera is active
		public int ActiveCamera { get; set; } = 0;
		// if sun is present
		public float SunLatitude { get; set; } = -826.39f;
		public float SunLongitude { get; set; } = -1605.4f;
		public int SunHour { get; set; } = 10;
		public int SunMinute { get; set; } = 5;
		public bool CaptureScreen {get; set;} = false;
		public int ScreenType {get; set;} = 0;

	}

	/// <summary>
	/// Properties that can be initialized at startup
	/// </summary>
	public class DB_StartUpSchema
	{	
		[PrimaryKey]
		public string MsgType  { get; set; } = "StartUp";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		//set Airplane masss
		public float AirplaneMass {get; set;} = 1000f; 
		//set Airplane fuel
		public float Fuel {get; set;} = 61f;
		//set Airplane fuel burnrate
		public float FuelBurnRate {get; set;} = 6.1f;  
		//set Airplane start Location
		public float StartLocation_x {get; set;} = 6.1f;  
		public float StartLocation_y {get; set;} = 6.1f;  
		public float StartLocation_z {get; set;} = 6.1f;  
		//set Airplane start Rotation
		public float StartRotation_x {get; set;} = 6.1f;  
		public float StartRotation_y {get; set;} = 6.1f;  
		public float StartRotation_z {get; set;} = 6.1f; 
	}
	

}
