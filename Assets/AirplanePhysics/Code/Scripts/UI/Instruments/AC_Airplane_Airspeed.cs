using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;

namespace AirControl
{
    public class AC_Airplane_Airspeed : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Airspeed Indicator Properties")]
        public AC_Airplane_Characteristics characteristics;
        public RectTransform pointer;
        public float maxIndicatedKnots = 200f;
        float currentKnots;
        #endregion


        public const float mphToKnts = 0.868976f;


        #region Interface Methods
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
        #endregion
    }
}
