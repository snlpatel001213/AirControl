using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Propeller : MonoBehaviour
    {
        #region Variables
        // Drag and drop this scrip to the propeller Gameobject in the Hierarchy

        #endregion

        #region  Builtin Methods
        // Start is called before the first frame update
        #endregion

        #region Custom Methods
        public void HandlePropeller(float currentRPM){
            // get degrees per second
            float degreesPerSecond =  ((currentRPM * 360)/60)*Time.deltaTime;
            //rotate the propeller
            transform.Rotate(Vector3.forward, degreesPerSecond);
        }

        #endregion
    }

}
