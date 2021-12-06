using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Wheel : MonoBehaviour
    {
        #region Variables 
        [Header("Wheel Properties")]
        public Transform wheelGraphic;
        public bool isBraking=false;
        public float brakePower = 500f;
        public bool isSteering=false;
        public float steerAngle=20f;

        private WheelCollider wheelCol;
        private Vector3 worldPos;
        private Quaternion worldRot;
        private float slowlyBrake;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            wheelCol = GetComponent<WheelCollider>();
        }

        // Update is called once per frame
        #endregion

        #region Custom Method
        public void initWheel(){

            if (wheelCol){
                //setting wheel torque to really small, this way wheel dont exert ant force or get locked
                wheelCol.motorTorque = 0.0000000001f;
            }
        }
        public void HandleWheel(AC_BaseAirplane_Input input)
        {
            if(wheelCol)
            {

                wheelCol.GetWorldPose(out worldPos, out worldRot);
                if(wheelGraphic)
                {   //updating wheel rotation 
                    wheelGraphic.rotation = worldRot;
                    wheelGraphic.position = worldPos;
                }// handle Else
                if (isBraking)
                {
                    if(input.Brake > 0.1f)
                    {
                        //slowly apply brake
                        slowlyBrake = Mathf.Lerp(slowlyBrake,input.Brake*brakePower, Time.deltaTime );
                        wheelCol.brakeTorque = slowlyBrake;
                        // Release motor torque to move forward
                        wheelCol.motorTorque = 0.0f;
                    }
                    else
                    {
                        //relase the brake
                        slowlyBrake = 0f;
                        wheelCol.brakeTorque = 0.0f;
                        // small motor torque not to allow airplane roll backward
                        wheelCol.motorTorque = 0.00001f;
                    }
                }
                if(isSteering)
                {
                    wheelCol.steerAngle = -input.Yaw * steerAngle;
                }
                
            }
        }

        #endregion

    }

}