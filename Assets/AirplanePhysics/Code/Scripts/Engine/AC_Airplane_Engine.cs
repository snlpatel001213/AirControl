using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Engine : MonoBehaviour
    {
         #region Variables
        [Header("Engine Properties")]
        public float maxForce = 3000f;
        private float maxRPM = 2500f;
        public float shutOffSpeed = 2f;
        public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Header("Propellers")]
        public AC_Airplane_Propeller propeller;


        private bool isShutOff = false;
        private float lastThrottleValue;
        private float finalShutoffThrottleValue;

        // private AC_Airplane_Fuel fuel;
        #endregion

        #region Properties
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
            // if(!fuel)
            // {
            //     fuel = GetComponent<IP_Airplane_Fuel>();
            //     if(fuel)
            //     {
            //         fuel.InitFuel();
            //     }
            // }
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


            //Process the Fuel
            // HandleFuel(finalThrottle);


            //Create Force
            float finalPower = finalThrottle * maxForce;
            Vector3 finalForce = transform.forward * finalPower;
//            Debug.Log(finalForce.magnitude * 0.727f);

            return finalForce;
        }


        // void HandleFuel(float throttleValue)
        // {
        //     //Handle Fuel
        //     if(fuel)
        //     {
        //         fuel.UpdateFuel(throttleValue);
        //         if(fuel.CurrentFuel <= 0f)
        //         {
        //             isShutOff = false;
        //         }
        //     }
        // }
        #endregion

    }

}
