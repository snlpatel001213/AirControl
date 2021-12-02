using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Engine : MonoBehaviour
    {
        #region Variables
        [Header("Engine Properties")]
        // Max force the engine can produce
        public float maxForce = 200f;
        // Max RPM the engine can produce
        public float maxRPM = 2550f;
        [Header("Animation curve for to set engine efficiency")]
        public AnimationCurve powerCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
        [Header("Hook up propeller GameObject Here")]
        public AC_Airplane_Propeller propeller;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion

        #region Custom Method
        public Vector3 calculateForce(float throttle){
            float finalThrottle = Mathf.Clamp01(throttle);
            // Applying power curve
            finalThrottle =  powerCurve.Evaluate(finalThrottle);
            float finalPower =  finalThrottle * maxForce;
            // Power is non direction, converting it to force with forward vector
            // change the vector3 power direction to "up" for vertical takeoff :) 
            Vector3 finalForce =  Vector3.forward*finalPower;

            // Calculating current RPM
            float currentRPM = maxRPM * finalThrottle;
            //rotate the propeller
            if(propeller){
                propeller.HandlePropeller(currentRPM);
            }
                   
            return finalForce;

        }
        #endregion

    }

}
