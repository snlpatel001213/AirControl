using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using Commons;
namespace AirControl
{
    /// <summary>
    /// Handle wheel braking and steering
    /// </summary>
    public class AC_Airplane_Wheel : MonoBehaviour
    {
        #region Properties
        public bool isGrounded = false;
        public bool IsGrounded
        {
            get{ return isGrounded; }
        }
        #endregion
        
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

        // void OnCollisionExit(Collision other) {

        //     string colliderObjectTag = other.gameObject.tag;
        //     Debug.Log("Wheel collided with : " + colliderObjectTag);
        //     if ( colliderObjectTag != "Runway") {
        //         StaticOutputSchema.IfCollision = true;
        //         StaticOutputSchema.CollisionObject = colliderObjectTag;
        //     }
        // }

        /// <summary>
        /// This is checking if the wheel is grounded. If it is grounded, 
        /// it will check to see if the collider is not the runway. If it is not the runway, it will set the IfCollision to true and
        /// set the CollisionObject to the tag of the collider.
        /// </summary>
        void Update(){
            WheelHit hit;
            if (wheelCol.GetGroundHit(out hit)) {
                string surface = hit.collider.tag;
                float wheelPenalty = 10.0f;
                if(hit.collider.tag != "Runway"){
                    CommonFunctions.MaxR -= wheelPenalty;
                    StaticOutputSchema.IfCollision = true;
                    StaticOutputSchema.CollisionObject = surface;
                }
            }
        }

        // Update is called once per frame
        #endregion

        #region Custom Method
        /// <summary>
        /// Init wheel set the motor torque to very small nuber to allow it to roll freely
        /// </summary>
        public void initWheel(){

            if (wheelCol){
                //setting wheel torque to really small, this way wheel dont exert ant force or get locked
                wheelCol.motorTorque = 0.0000000001f;
            }
        }
        /// <summary>
        /// Handle whele graphics, and steering 
        /// </summary>
        /// <param name="input">Airplane Input</param>
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

                // check to see if the wheels are grounded
                isGrounded = wheelCol.isGrounded;
                
            }
        }
        #endregion

    }

}
