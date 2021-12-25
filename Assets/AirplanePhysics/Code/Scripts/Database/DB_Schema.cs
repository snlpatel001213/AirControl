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
	public class DB_EternalOutput
	{
		[PrimaryKey]
		public string MsgType  { get; set; } = "Outgoing";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		public float AGL{get;set;}  = 0.0f;
		public float MSL{get;set;}  = 0.0f;
		public float CurrentRPM{get;set;}  = 0.0f;
		public float MaxRPM{get;set;}  = 0.0f;
		public float MaxPower{get;set;}  = 0.0f;
		public float CurrentPower{get;set;}  = 0.0f;
		public float CurrentFuel{get;set;}  = 0.0f;
		public float CurrentSpeed{get;set;}  = 0.0f;
		public float BankAngle{get;set;}  = 0.0f;
		public float PitchAngle{get;set;}  = 0.0f;
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
		// Force outoput
		public bool GetOutput {get; set;} = false;
		// if sun is present
		public float SunLatitude { get; set; } = -826.39f;
		public float SunLongitude { get; set; } = -1605.4f;
		public int SunHour { get; set; } = 10;
		public int SunMinute { get; set; } = 5;

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
