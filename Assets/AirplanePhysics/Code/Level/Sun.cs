using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using UnityEngine.UI;

namespace AirControl
{
    
    /// <summary>
    /// Function to change sun location and create Day and Night effects.
    /// </summary>
    [RequireComponent(typeof(Light))]
    [ExecuteInEditMode]
    public class Sun : MonoBehaviour
    {
        //UI slider
        public GameObject sliderAndText;
        public Toggle sunLocationControl;
        public Slider longitudeSlider;
        public Slider latitudeSlider;
        public Slider sunHourSlider;
        public Slider sunMinuteSlider;

        [SerializeField]
        float longitude;

        [SerializeField]
        float latitude;

        [SerializeField]
        [Range(0, 24)]
        int hour;

        [SerializeField]
        [Range(0, 60)]
        int minutes;

        DateTime time;
        new Light light;

        [SerializeField]
        float timeSpeed = 1;

        [SerializeField]
        int frameSteps = 1;
        int frameStep;

        [SerializeField]
        DateTime date;
        /// <summary>
        /// set dun location as per Hour and Minute
        /// </summary>
        /// <param name="hour">Hour, limit 0-24</param>
        /// <param name="minutes">Minutes, limit 0-60</param>
        public void SetTime(int hour, int minutes) {
            this.hour = hour;
            this.minutes = minutes;            
            OnValidate();
        }
        /// <summary>
        /// Set sun location as per the Longitude and latitude
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public void SetLocation(float longitude, float latitude){
          this.longitude = longitude;
          this.latitude = latitude;
        }

        // public void SetDate(DateTime dateTime){
        //  this.hour = dateTime.Hour;
        //  this.minutes = dateTime.Minute;
        //  this.date = dateTime.Date;
        //  OnValidate();
        // }

        public void SetUpdateSteps(int i) {
            frameSteps = i;
        }

        public void SetTimeSpeed(float speed) {
            timeSpeed = speed;
        }
        /// <summary>
        /// Set datetime as soon as the game starts
        /// </summary>
        private void Awake()
        {

            light = GetComponent<Light>();
            time = DateTime.Now;
            hour = time.Hour;
            minutes = time.Minute;
            date = time.Date;
        }

        private void OnValidate()
        {
            time = date + new TimeSpan(hour, minutes, 0);
        }

        private void FixedUpdate()
        {   

            /// <summary>
            /// Switching sun location as per the Input/Output from communicator
            /// </summary>
            /// <returns></returns>
            

            float sunLatitude=-66f;
            float sunLongitude=-45f;
            int sunHour=1;
            int sunMinute=1;
            bool isActive =  sunLocationControl.isOn;
            sliderAndText.SetActive(isActive);
            if(isActive)
            {  
                /// from UI slider
                sunLatitude = longitudeSlider.value;
                sunLongitude = latitudeSlider.value;
                sunHour = (int)sunHourSlider.value;
                sunMinute = (int)sunMinuteSlider.value;
                SetTime(sunHour, sunMinute);
                SetLocation(sunLatitude, sunLongitude);
            }
            #region IOSwitch
            if(StaticTODSchema.IsActive )
            {   
                sunLocationControl.isOn = false;
                Debug.Log("UI Control is set to " + sunLocationControl.isOn);
                sunLatitude = StaticTODSchema.SunLatitude;
                sunLongitude = StaticTODSchema.SunLongitude;
                sunHour = StaticTODSchema.Hour;
                sunMinute = StaticTODSchema.Minute;
                SetTime(sunHour, sunMinute);
                SetLocation(sunLatitude, sunLongitude);
                // set is active to false to not let this loop run for all the updates
                StaticTODSchema.IsActive =false;
                
            }
            #endregion


            time = time.AddSeconds(timeSpeed * Time.deltaTime);
            if (frameStep==0) {
                SetPosition();
            }
            frameStep = (frameStep + 1) % frameSteps;


        }
        /// <summary>
        /// Set sun location as per the provided input
        /// </summary>
        void SetPosition()
        {

            Vector3 angles = new Vector3();
            double alt;
            double azi;
            SunPosition.CalculateSunPosition(time, (double)latitude, (double)longitude, out azi, out alt);
            angles.x = (float)alt * Mathf.Rad2Deg;
            angles.y = (float)azi * Mathf.Rad2Deg;
            //UnityEngine.Debug.Log(angles);
            transform.localRotation = Quaternion.Euler(angles);
            light.intensity = Mathf.InverseLerp(-12, 0, angles.x);
        }

        
    }

    /// <summary>
    /// Setting sun position
    /// The following source came from this blog:
    /// http://guideving.blogspot.co.uk/2010/08/sun-position-in-c.html
    /// </summary>
    public static class SunPosition
    {
        private const double Deg2Rad = Math.PI / 180.0;
        private const double Rad2Deg = 180.0 / Math.PI;

       /// <summary>
       ///  Calculates the sun light.   
       ///  CalcSunPosition calculates the suns "position" based on a 
       ///  given date and time in local time, latitude and longitude 
       ///  expressed in decimal degrees. It is based on the method 
       ///  found here: 
       ///  http://www.astro.uio.no/~bgranslo/aares/calculate.html 
       ///  The calculation is only satisfiably correct for dates in 
       ///  the range March 1 1900 to February 28 2100. 
       /// </summary>
       /// <param name="dateTime"> dateTime Time and date in local time.</param>
       /// <param name="latitude">latitude Latitude expressed in decimal degrees. </param>
       /// <param name="longitude">longitude Longitude expressed in decimal degrees. </param>
       /// <param name="outAzimuth"></param>
       /// <param name="outAltitude"></param>
        public static void CalculateSunPosition(
            DateTime dateTime, double latitude, double longitude, out double outAzimuth, out double outAltitude)
        {
            // Convert to UTC  
            dateTime = dateTime.ToUniversalTime();

            // Number of days from J2000.0.  
            double julianDate = 367 * dateTime.Year -
                (int)((7.0 / 4.0) * (dateTime.Year +
                (int)((dateTime.Month + 9.0) / 12.0))) +
                (int)((275.0 * dateTime.Month) / 9.0) +
                dateTime.Day - 730531.5;

            double julianCenturies = julianDate / 36525.0;

            // Sidereal Time  
            double siderealTimeHours = 6.6974 + 2400.0513 * julianCenturies;

            double siderealTimeUT = siderealTimeHours +
                (366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

            double siderealTime = siderealTimeUT * 15 + longitude;

            // Refine to number of days (fractional) to specific time.  
            julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;
            julianCenturies = julianDate / 36525.0;

            // Solar Coordinates  
            double meanLongitude = CorrectAngle(Deg2Rad *
                (280.466 + 36000.77 * julianCenturies));

            double meanAnomaly = CorrectAngle(Deg2Rad *
                (357.529 + 35999.05 * julianCenturies));

            double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) *
                Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));

            double elipticalLongitude =
                CorrectAngle(meanLongitude + equationOfCenter);

            double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

            // Right Ascension  
            double rightAscension = Math.Atan2(
                Math.Cos(obliquity) * Math.Sin(elipticalLongitude),
                Math.Cos(elipticalLongitude));

            double declination = Math.Asin(
                Math.Sin(rightAscension) * Math.Sin(obliquity));

            // Horizontal Coordinates  
            double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;

            if (hourAngle > Math.PI)
            {
                hourAngle -= 2 * Math.PI;
            }

            double altitude = Math.Asin(Math.Sin(latitude * Deg2Rad) *
                Math.Sin(declination) + Math.Cos(latitude * Deg2Rad) *
                Math.Cos(declination) * Math.Cos(hourAngle));

            // Nominator and denominator for calculating Azimuth  
            // angle. Needed to test which quadrant the angle is in.  
            double aziNom = -Math.Sin(hourAngle);
            double aziDenom =
                Math.Tan(declination) * Math.Cos(latitude * Deg2Rad) -
                Math.Sin(latitude * Deg2Rad) * Math.Cos(hourAngle);

            double azimuth = Math.Atan(aziNom / aziDenom);

            if (aziDenom < 0) // In 2nd or 3rd quadrant  
            {
                azimuth += Math.PI;
            }
            else if (aziNom < 0) // In 4th quadrant  
            {
                azimuth += 2 * Math.PI;
            }

            outAltitude = altitude;
            outAzimuth = azimuth;
        }

        /// <summary>
        /// Corrects an angle. 
        /// </summary>
        /// <param name="angleInRadians">An angle expressed in radians. </param>
        /// <returns> An angle in the range 0 to 2*PI.</returns>
        private static double CorrectAngle(double angleInRadians)
        {
            if (angleInRadians < 0)
            {
                return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
            }
            else if (angleInRadians > 2 * Math.PI)
            {
                return angleInRadians % (2 * Math.PI);
            }
            else
            {
                return angleInRadians;
            }
        }
    }

}