using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using Commons;
using JetBrains.Annotations;
using System;
using UnityEngine.TextCore.LowLevel;

namespace AirControl
{   
    /// <summary>
    /// Engine controls
    /// </summary>
    [RequireComponent(typeof(AC_Airplane_Propeller))]
    [RequireComponent(typeof(AC_Airplane_Fuel))]
    [RequireComponent(typeof(AC_Airplane_Audio))]
    [RequireComponent(typeof(AC_BaseAirplane_Input))]
    [RequireComponent(typeof(AC_XboxAirplane_Input))]
    [RequireComponent(typeof(AC_Airplane_Audio))]
    public class AC_Airplane_Engine : MonoBehaviour
    {
         #region Variables
        [Header("Engine Properties")]
        private float maxForce;
        private float maxRPM ;

        private Vector3 rotationDelta;
        private Vector3 rotationLast;
        
        public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve liftOff = AnimationCurve.Linear(0f, 0f, 1000f, 1000f);

        [Header("Propellers")]
        public AC_Airplane_Propeller propeller;

        private float shutOffSpeed;// engine slowdone rate on shutoff
        private bool isShutOff = false;
        private float lastThrottleValue;
        private float finalShutoffThrottleValue;
        private float wingSpan;


        private AC_Airplane_Fuel fuel;
        #endregion

        #region constants
        private const float engineEfficiency = 1.6f; //  What percentage of propellers actions get converted to actual force 
        private const float airDensity = 0.07967f;
        #endregion

        #region Properties
        // property that listens to the engine shutoff script events
        public bool ShutEngineOff
        {
            set{isShutOff = value;}
        }

        private float currentRPM;
        public float CurrentRPM
        {
            get{return currentRPM;}
        }
        #endregion



        #region BuiltIn Methods
        /// <summary>
        /// init fuel
        /// </summary>
        void Start()
        {
            rotationLast = propeller.transform.eulerAngles;
            // maxForce =  //(float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/maxForce"];
            maxRPM = (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/maxRPM"];
            shutOffSpeed = (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/shutOffSpeed"];
            wingSpan = (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/wingSpan"];
            if(!fuel)
            {
                fuel = GetComponent<AC_Airplane_Fuel>();
                if(fuel)
                {
                    fuel.InitFuel();
                }
            }
        }

        void Update(){
            getAngularVelocityPro();
        }
        #endregion


        #region Custom Methods

        /// <summary>
        /// Calculate angular velocity of the propellar
        /// </summary>
        public void getAngularVelocityPro()
        {
            rotationDelta = propeller.transform.eulerAngles - rotationLast;
            rotationLast = propeller.transform.eulerAngles;
        }
        /// <summary>
        /// Calculate the force created by engine
        /// Calculate Engine RPM 
        /// Calculate fuel consumption
        /// </summary>
        /// <param name="throttle">Input throttle value</param>
        /// <returns>Final Engine force</returns>
        public Vector3 CalculateForce(float throttle)
        {
            //Calcualte Power
            float finalThrottle = Mathf.Clamp01(throttle);


            if(!isShutOff)
            {
                finalThrottle = powerCurve.Evaluate(finalThrottle);
                //keep eye on last throttle value, In case of engine cutoff we can use this value to slowly decrease the engine power
                lastThrottleValue = finalThrottle;
            }
            else
            {
                lastThrottleValue -= Time.deltaTime * shutOffSpeed;
                lastThrottleValue = Mathf.Clamp01(lastThrottleValue);
                finalThrottle = powerCurve.Evaluate(lastThrottleValue);
            }


            //Calculate RPM's
            currentRPM = finalThrottle * maxRPM;
            if(propeller)
            {
                propeller.HandlePropeller(currentRPM);
            }


            // Process the Fuel
            HandleFuel(finalThrottle);

            // maxForce = 0.6f*0.07967f* (float)Math.Pow(1.6,4)* (float)Math.Pow(rotationDelta.magnitude,2); //cessna
            maxForce = engineEfficiency*airDensity* (float)Math.Pow(wingSpan,4)* (float)Math.Pow(rotationDelta.magnitude,2);            
            //Create Force
            float finalPower = finalThrottle * maxForce;
            Vector3 finalForce = transform.forward * finalPower;

            #region DBArea
            //Setting Current engine paramters to DB
            // DB_Functions.SetEngineVariables(connection, maxForce, finalPower,  maxRPM, CurrentRPM);
            StaticOutputSchema.MaxPower = maxForce; // can be moved to start
            StaticOutputSchema.CurrentPower = finalPower;
            StaticOutputSchema.MaxRPM = maxRPM; // can be moved to start
            StaticOutputSchema.CurrentRPM = currentRPM;
            #endregion

            return finalForce;
        }

        /// <summary>
        /// Shutdown the engine if fuel not avialble
        /// </summary>
        /// <param name="throttleValue">input throttle</param>
        void HandleFuel(float throttleValue)
        {
            //Handle Fuel
            if(fuel)
            {
                fuel.UpdateFuel(throttleValue);
                if(fuel.CurrentFuel <= 0f)
                {
                    isShutOff = false;
                }
            }
        }
        #endregion

    }

}
