using System.Collections;
using System;
using System.Collections.Generic;
using Commons;
namespace Communicator
{
	#region ControlInput
	public class ControlSchema 
	{

		public string MsgType  { get; set; } = "ControlInput";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
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

	public class StaticControlSchema 
	{
		/// <summary>
		/// Just [PrimaryKey] is added to the Id Attribute as we only want the updated value and dont want to accumulate it
		/// if one need to auto increment it the Id fielsd can be configured as [PrimaryKey, AutoIncrement]
		/// In this case it will fill up the database. [Not supported]
		/// </summary>
		/// <value></value>
		public static string MsgType  { get; set; } = "ControlInput";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public static string InputControlType {get;set;} = "Other";
		//Control Pitch
		public static float Pitch  {get; set;}=0f;
		//Control Roll
		public static float Roll  {get; set;} = 0f;
		//Control Yaw
		public static float Yaw  {get; set;}=0f;
		//Control Throttle
		public static float Throttle  {get; set;}=0f;
		//Control Sticky Throttle
		public static float StickyThrottle  {get; set;}=0f;
		//Control Brake
		public static float Brake  {get; set;}=0f;
		//Control Flaps
		public static int Flaps  {get; set;}=0;

	}
	#endregion

	#region Output
	public static class StaticOutputSchema
	{
		public static string MsgType  { get; set; } = "Output";
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
		public static float [] LidarPointCloud;
		public static string log;
	}
	public class OutputSchema
	{
		public string MsgType  { get; set; } = "Output";
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
		public float [] LidarPointCloud;
		public static string log;
	}
	#endregion

	#region TOD
	public class TODSchema
	{
		public string MsgType  { get; set; } = "TOD";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//set active
		public bool IsActive{get; set;} = false;
		// sun location
		public float SunLatitude { get; set; } = -826.39f;
		public float SunLongitude { get; set; } = -1605.4f;
		public int Hour { get; set; } = 10;
		public int Minute { get; set; } = 5;
	}

	public static class StaticTODSchema
	{
		public static string MsgType  { get; set; } = "TOD";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//set active
		public static bool IsActive{get; set;} = false;
		// sun location
		public static float SunLatitude { get; set; } = -826.39f;
		public static float SunLongitude { get; set; } = -1605.4f;
		public static int Hour { get; set; } = 10;
		public static int Minute { get; set; } = 5;

	}
	#endregion

	#region  Camera
	public class CameraSchema
	{	
		public string MsgType  { get; set; } = "Transcation";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		// Which camera is active
		public int ActiveCamera { get; set; } = 0;
		public bool IsCapture {get; set;} = false;
		public int CaptureCamera {get; set;} = 0;
		public int CaptureType {get; set;} = 0;
		public int CaptureWidth {get; set;} = 256;
		public int CaptureHeight {get; set;} = 256;

	}

	public static class StaticCameraSchema
	{	
		public static string MsgType  { get; set; } = "Camera";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public static string InputControlType {get;set;} = "Other";
		// Which camera is active
		public static int ActiveCamera { get; set; } = 0;
		public static bool IsCapture {get; set;} = false;
		public static int CaptureCamera {get; set;} = 0;
		public static int CaptureType {get; set;} = 0;
		public static int CaptureWidth {get; set;} = 256;
		public static int CaptureHeight {get; set;} = 256;

	}
	#endregion

	#region Level
	public class LevelSchema
	{	
		public string MsgType  { get; set; } = "Level";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		// if transaction schema is active or not this is to reduce computation load in the update loops
		public bool IsActive {get; set;} = false;
		//reload the level if this is set true
		public bool LevelReload {get; set;} = false; 

	}

	public static class StaticLevelSchema
	{	
		public static string MsgType  { get; set; } = "Level";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public static string InputControlType {get;set;} = "Other";
		// // if transaction schema is active or not this is to reduce computation load in the update loops
		public static bool IsActive {get; set;} = false;
		//reload the level if this is set true
		public static bool LevelReload {get; set;} = false; 

	}
	#endregion

	#region WeatherSchema
	public class WeatherSchema
	{	
		public string MsgType  { get; set; } = "Weather";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		// if transaction schema is active or not this is to reduce computation load in the update loops
		public bool IsClouds {get; set;} = false;
		//reload the level if this is set true
		public bool IsFog {get; set;} = false; 

	}

	public static class StaticWeatherSchema
	{	
		public static string MsgType  { get; set; } = "Weather";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public static string InputControlType {get;set;} = "Other";
		// // if transaction schema is active or not this is to reduce computation load in the update loops
		public static bool IsClouds {get; set;} = false;
		//reload the level if this is set true
		public static bool IsFog {get; set;} = false; 

	}
	#endregion

	#region UIAudioSchema
	public class UIAudioSchema
	{	
		public string MsgType  { get; set; } = "UIAudio";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		// if transaction schema is active or not this is to reduce computation load in the update loops
		public bool ShowUIElements {get; set;} = false;
		//reload the level if this is set true
		public float AudioVolume {get; set;} = 1f; 

	}

	public static class StaticUIAudioSchema
	{	
		public static string MsgType  { get; set; } = "UIAudio";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public static string InputControlType {get;set;} = "Other";
		// // if transaction schema is active or not this is to reduce computation load in the update loops
		public static bool ShowUIElements {get; set;} = false;
		//reload the level if this is set true
		public static float AudioVolume {get; set;} = 1f; 

	}
	#endregion

	#region Lidar
	public class LidarSchema
	{	
		public string MsgType  { get; set; } = "Lidar";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		// if transaction schema is active or not this is to reduce computation load in the update loops
		public float Range {get; set;} = 100000f;
		//reload the level if this is set true
		public int Density {get; set;} = 360; 

	}

	public static class StaticLidarSchema
	{	
		public static string MsgType  { get; set; } = "Lidar";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public static string InputControlType {get;set;} = "Other";
		// // if transaction schema is active or not this is to reduce computation load in the update loops
		public static float Range {get; set;} = 100000f;
		//reload the level if this is set true
		public static int Density {get; set;} =  360; 

	}
	#endregion

	#region Fuel
	public class FuelSchema
	{	
		public string MsgType  { get; set; } = "Lidar";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public string InputControlType {get;set;} = "Other";
		// if transaction schema is active or not this is to reduce computation load in the update loops


	}

	public static class StaticFuelSchema
	{	
		public static string MsgType  { get; set; } = "Lidar";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		//Control type can be one out of "Comminocator","Other". Other methods means Keyboard or Joystick
		public static string InputControlType {get;set;} = "Other";
		// // if transaction schema is active or not this is to reduce computation load in the update loops

	}
	#endregion

	#region Preset
	public class PresetSchema
	{	
		public string MsgType  { get; set; } = "StartUp";
		//Version of the sceme, IT will be same as the release version
		public string Version {get;set;} = CommonFunctions.GET_VERSION();
		// //set Airplane masss
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
	public static class StaticStartUpSchema
	{	
		public static string MsgType  { get; set; } = "StartUp";
		//Version of the sceme, IT will be same as the release version
		public static string Version {get;set;} = CommonFunctions.GET_VERSION();
		// //set Airplane masss
		public static float AirplaneMass {get; set;} = 1000f; 
		//set Airplane fuel
		public static float Fuel {get; set;} = 61f;
		//set Airplane fuel burnrate
		public static float FuelBurnRate {get; set;} = 6.1f;  
		//set Airplane start Location
		public static float StartLocation_x {get; set;} = 6.1f;  
		public static float StartLocation_y {get; set;} = 6.1f;  
		public static float StartLocation_z {get; set;} = 6.1f;  
		//set Airplane start Rotation
		public static float StartRotation_x {get; set;} = 6.1f;  
		public static float StartRotation_y {get; set;} = 6.1f;  
		public static float StartRotation_z {get; set;} = 6.1f; 
	}
	#endregion
	

}
