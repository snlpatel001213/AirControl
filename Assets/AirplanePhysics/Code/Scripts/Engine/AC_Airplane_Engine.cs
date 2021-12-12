using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{   
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
        public float maxForce = 3000f;
        public float maxRPM = 3000f;
        
        public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Header("Propellers")]
        public AC_Airplane_Propeller propeller;

        private float shutOffSpeed = 2f; // engine slowdone rate on shutoff
        private bool isShutOff = false;
        private float lastThrottleValue;
        private float finalShutoffThrottleValue;

        private AC_Airplane_Fuel fuel;
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
        void Start()
        {
            if(!fuel)
            {
                fuel = GetComponent<AC_Airplane_Fuel>();
                if(fuel)
                {
                    fuel.InitFuel();
                }
            }
        }
        #endregion

        #region Custom Methods
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


            //Create Force
            float finalPower = finalThrottle * maxForce;
            Vector3 finalForce = transform.forward * finalPower;
//            Debug.Log(finalForce.magnitude * 0.727f);

            return finalForce;
        }


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
