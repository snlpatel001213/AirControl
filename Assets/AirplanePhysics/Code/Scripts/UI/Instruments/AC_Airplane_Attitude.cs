using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using Commons;

namespace AirControl
{
    /// <summary>
    /// Monitor and updates the Attitude to UI 
    /// </summary>
    public class AC_Airplane_Attitude : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Attitude Indicator Properties")]
        [SerializeField]
        private AC_Airplane_Controller airplane;
        public RectTransform bgRect;
        public RectTransform arrowRect;
        protected float bankAngle; 
        protected float pitchAngle;
        protected float bankAngleRad; 
        protected float pitchAngleRad;
        #endregion

        #region Properties
        public float BankAngle{
            get{return bankAngle;}
        }
        public float PitchAngle{
            get{return pitchAngle;}
        }
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
                //Create Angles
                bankAngleRad = Vector3.Dot(airplane.transform.right, Vector3.up); // value from -1 to 1
                bankAngle =  bankAngleRad * Mathf.Rad2Deg;
                pitchAngleRad =  Vector3.Dot(airplane.transform.forward, Vector3.up); // value from -1 to 1
                pitchAngle = pitchAngleRad * Mathf.Rad2Deg;

                //Handle UI Elements
                if(bgRect)
                {
                    Quaternion bankRotation  = Quaternion.Euler(0f, 0f, bankAngle);
                    bgRect.transform.rotation = bankRotation;

                    Vector3 wantedPosition = new Vector3(0f, -pitchAngle, 0f);
                    bgRect.anchoredPosition = wantedPosition;

                    if(arrowRect)
                    {
                        arrowRect.transform.rotation = bankRotation;
                    }
                }
                #region DBArea
                StaticOutputSchema.BankAngle = bankAngleRad;
                StaticOutputSchema.PitchAngle = pitchAngleRad;
                #endregion 

            }
        }
        #endregion
    }
}
