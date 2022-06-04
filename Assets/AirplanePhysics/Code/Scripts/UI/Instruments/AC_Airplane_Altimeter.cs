using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;

namespace AirControl
{
    /// <summary>
    /// Monitor and updates the Altimeter to UI 
    /// </summary>
    public class AC_Airplane_Altimeter : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Altimeter Properties")]
        [Tooltip("Drag and drop Airplane Here")]
        [SerializeField]
        private AC_Airplane_Controller airplane;
        [Tooltip("Drag and drop Hundreds pointer Here")]
        [SerializeField]
        private RectTransform hundredspointer; 
        [Tooltip("Drag and drop Thousands Pointer Here")]
        [SerializeField]
        private RectTransform thousandsPointer;
        #endregion

        #region Builtin methods
        void Start(){
            airplane = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<AC_Airplane_Controller>();
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Updates to UI 
        /// </summary>
        public void HandleAirplaneUI()
        {
            if(airplane)
            {
                float currentAltitude =  airplane.CurrentMSL;
                float currentThousands = currentAltitude/1000f;
                currentThousands = Mathf.Clamp(currentThousands,0,10);

                float currentHundreds =  currentAltitude - (Mathf.Floor(currentThousands)*1000f);
                currentHundreds =  Mathf.Clamp(currentHundreds, 0f,1000f);

                if(thousandsPointer)
                {
                    // Calcualting degrees from the current height
                    float radialThousands = Mathf.InverseLerp(0,10,currentThousands);
                    radialThousands = 360f * radialThousands;
                    thousandsPointer.rotation = Quaternion.Euler(0f,0f,-radialThousands);
                }
                if(hundredspointer)
                {
                    // Calcualting degrees from the current height
                    float radialHundreds = Mathf.InverseLerp(0f,1000f,currentHundreds);
                    radialHundreds = 360f * radialHundreds;
                    hundredspointer.rotation = Quaternion.Euler(0f,0f,-radialHundreds);
                }
            }
        }
        #endregion
    }

}
