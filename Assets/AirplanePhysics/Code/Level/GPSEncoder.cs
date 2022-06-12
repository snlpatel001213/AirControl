//Copyright 2013 MichaelTaylor3D
//www.michaeltaylor3d.com
using UnityEngine;
using Communicator;
using Commons;
namespace AirControl
{   
    
    //Copyright 2013 MichaelTaylor3D
    //www.michaeltaylor3d.com
    //https://github.com/MichaelTaylor3D/UnityGPSConverter/
    
    public class GPSEncoder : MonoBehaviour
    {
        
        #region Instance Variables
        public Rigidbody rb;

        private Vector2 _localOrigin = Vector2.zero;
        private float _LatOrigin { get{ return _localOrigin.x; }}	
        private float _LonOrigin { get{ return _localOrigin.y; }}
        private float MaxLongitude = 180f;
        private float MaxLatitude = 90f;

        private float metersPerLat;
        private float metersPerLon;
        #endregion
        
        void Update(){
            HandleLocation();
        }

        void Start()
        {   
            rb = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<Rigidbody>();
        }

        #region Instance Functions
        public void FindMetersPerLat(float lat) // Compute lengths of degrees
        {
            float m1 = 1000f;   // reducing the gloabe equatorail diameter, original : 111132.92f;    // latitude calculation term 1
            float m2 = -559.82f;        // latitude calculation term 2
            float m3 = 1.175f;      // latitude calculation term 3
            float m4 = -0.0023f;        // latitude calculation term 4
            float p1 = 1000f; // reducing the globe polar diameter, original : 111412.84f;    // longitude calculation term 1
            float p2 = -93.5f;      // longitude calculation term 2
            float p3 = 0.118f;      // longitude calculation term 3
            
            lat = lat * Mathf.Deg2Rad;
        
            // Calculate the length of a degree of latitude and longitude in meters
            metersPerLat = m1 + (m2 * Mathf.Cos(2 * (float)lat)) + (m3 * Mathf.Cos(4 * (float)lat)) + (m4 * Mathf.Cos(6 * (float)lat));
            metersPerLon = (p1 * Mathf.Cos((float)lat)) + (p2 * Mathf.Cos(3 * (float)lat)) + (p3 * Mathf.Cos(5 * (float)lat));	   
        }

        public Vector3 ConvertGPStoUCS(Vector2 gps)  
        {
            FindMetersPerLat(_LatOrigin);
            float zPosition  = metersPerLat * (gps.x - _LatOrigin); //Calc current lat
            float xPosition  = metersPerLon * (gps.y - _LonOrigin); //Calc current lat
            return new Vector3((float)xPosition, 0, (float)zPosition);
        }
        
        public Vector2 ConvertUCStoGPS(Vector3 position)
        {
            FindMetersPerLat(_LatOrigin);
            Vector2 geoLocation = new Vector2(0,0);
            geoLocation.x = (_LatOrigin + (position.z)/metersPerLat); //Calc current lat
            geoLocation.y = (_LonOrigin + (position.x)/metersPerLon); //Calc current lon
            return geoLocation;
        }


        /// <summary>
        /// Provides the location of the Aircraft in 3D coordinate system
        /// </summary>
        void HandleLocation()
        {
            //future functionality to provide the xyz location of the plane
            Vector3 currentLocation = rb.position;
            Vector2 latlong =  ConvertUCStoGPS(currentLocation);
            // Debug.Log(latlong);
            float normalizedLatitude = latlong.x/MaxLatitude;
            float normalizedLongitude = latlong.y/MaxLongitude;
            // Add location to static function
            StaticOutputSchema.Latitude = normalizedLatitude;
            StaticOutputSchema.Longitude = normalizedLongitude;
        }
        #endregion
    }
}