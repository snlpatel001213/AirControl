using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Wheel : MonoBehaviour
    {
        #region Variables 
        private WheelCollider wheelCol;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            wheelCol = GetComponent<WheelCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion

        #region Custom Method
        public void initWheel(){

            if (wheelCol){
                //setting wheel torque to really small, this way wheel dont exert ant force or get locked
                wheelCol.motorTorque = 0.0000000001f;
            }
        }

        #endregion

    }

}
