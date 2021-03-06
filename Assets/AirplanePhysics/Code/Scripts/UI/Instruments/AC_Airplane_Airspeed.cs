using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using Commons;

namespace AirControl
{
    /// <summary>
    /// Monitor and updates the Airspeed to UI 
    /// </summary>
    public class AC_Airplane_Airspeed : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Airspeed Indicator Properties")]
        [SerializeField]
        private AC_Airplane_Characteristics characteristics;
        public RectTransform pointer;
        public float maxIndicatedKnots = 200f;
        float currentKnots;
        #endregion


        public const float mphToKnts = 0.868976f;

        #region Builtin methods
        void Start(){
            characteristics = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<AC_Airplane_Characteristics>();
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Updates to UI 
        /// </summary>
        public void HandleAirplaneUI()
        {
            if(characteristics && pointer)
            {
                currentKnots = characteristics.MPH * mphToKnts;
                //Debug.Log(currentKnots);

                float normalizedKnots = Mathf.InverseLerp(0f, maxIndicatedKnots, currentKnots);
                float wantedRotation = 360f * normalizedKnots;
                pointer.rotation = Quaternion.Euler(0f, 0f, -wantedRotation);
            }

            #region DBArea
            StaticOutputSchema.CurrentSpeed = currentKnots;
            //Set value of AGL and MSL to DB
            // DB_Functions.SetSpeed(connection, currentKnots);
            #endregion         
        }

        /// <summary>
        /// Detect if the Airplane turn upside down on runway
        /// Then it will be also considered as collision
        /// </summary>
        #endregion
    }
}
