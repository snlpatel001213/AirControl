using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Engine : MonoBehaviour
    {
        #region Variables
        // Max force the engine can produce
        public float maxForce = 200f;
        // Max RPM the engine can produce
        public float mazRPM = 2550f;
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
            float finalPower =  finalThrottle * maxForce;
            Vector3 finalForce =  Vector3.forward*finalPower;
                                    
            return finalForce;

        }
        #endregion

    }

}
