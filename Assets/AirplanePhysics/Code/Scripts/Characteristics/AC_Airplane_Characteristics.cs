using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Characteristics : MonoBehaviour 
    {
        #region Varaibles
        [Header("Characteristics Properties")]
        public float maxMPH = 110f;
        public float rbLerpSpeed = 0.01f;


        [Header("Lift Properties")]
        public float maxLiftPower = 4000f ;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        public float flapLiftPower = 100f;


        [Header("Drag Properties")]
        public float dragFactor = 0.01f;
        public float flapDragFactor = 0.005f;


        [Header("Control Properties")]
        public float pitchSpeed = 1000f;
        public float rollSpeed = 1000f;
        public float yawSpeed = 1000f;
        public AnimationCurve controlSurfaceEfficiency = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public float forwardSpeed;
        public float ForwardSpeed
        {
            get{return forwardSpeed;}
        }

        private float mph;
        public float MPH
        {
            get{return mph;}
        }

        private AC_BaseAirplane_Input input;
        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag;

        private float maxMPS;
        private float normalizeMPH;

        private float angleOfAttack;
        private float pitchAngle;
        private float rollAngle;

        private float csEfficiencyValue;
        #endregion
        
        #region Constants
        const float mpsToMph = 2.23694f;
        #endregion

        #region Custom Methods
        public void InitCharacteristics(Rigidbody curRB, AC_BaseAirplane_Input curInput)
        {
            //Basic Initialization
            input = curInput;
            rb = curRB;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;

            //Find the max Meters Per Second
            maxMPS = maxMPH / mpsToMph;
        }

        //Update all the Flight Characteristics methods
        public void UpdateCharacteristics()
        {
            if(rb)
            {
                //Process the Flight Physics
                CalculateForwardSpeed();
                CalculateLift();
                CalculateDrag();

                //Process Control
                HandleControlSurfaceEfficiency();
                HandleYaw();
                HandlePitch();
                HandleRoll();
                HandleBanking();

                //Handle Rigidbody
                HandleRigidbodyTransform();
            }
        }



        //Get the local forward speed in Meters per second and convert it to Miles Per Hour
        void CalculateForwardSpeed()
        {
            //Transform the Rigidbody velocity vector from world space to local space
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            forwardSpeed = Mathf.Max(0f, localVelocity.z);
            //  forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);

            //find the Miles Per Hour from Meters Per Second
            mph = forwardSpeed * mpsToMph;
            //  mph = Mathf.Clamp(mph, 0f, maxMPH);
            normalizeMPH = Mathf.InverseLerp(0f, maxMPH, mph);
        }


        //Build a lift force strong enough to lift he plane off the ground
        void CalculateLift()
        {
            //Get the angle of Attack
            angleOfAttack = Vector3.Dot(rb.velocity.normalized, transform.forward);
            angleOfAttack *= angleOfAttack;

            //Create the Lift Direction
            Vector3 liftDir = transform.up;
            float liftPower = liftCurve.Evaluate(normalizeMPH) * maxLiftPower;

            // //Add Flap Lift
            // float finalLiftPower = flapLiftPower * input.NormalizedFlaps;

            //Apply the final Lift Force to the Rigidbody
            Vector3 finalLiftForce = liftDir * (liftPower) * angleOfAttack;
            rb.AddForce(finalLiftForce);
        }



        //Get a Drag force to keep the plane relatively stable in the air
        void CalculateDrag()
        {
            //Speed Drag
            float speedDrag = forwardSpeed * dragFactor;

            //Flap Drag
            float flapDrag = input.Flaps * flapDragFactor;

            //add it all together!
            float finalDrag = startDrag + speedDrag + flapDrag;

            rb.drag = finalDrag;
            rb.angularDrag = startAngularDrag * forwardSpeed;
        }


        void HandleRigidbodyTransform()
        {
            if(rb.velocity.magnitude > 1f)
            {
                Vector3 updatedVelocity = Vector3.Lerp(rb.velocity, transform.forward * forwardSpeed, forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
                rb.velocity = updatedVelocity;


                Quaternion updatedRotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity, transform.up), Time.deltaTime * rbLerpSpeed);
                rb.MoveRotation(updatedRotation);
            }
        }


        void HandleControlSurfaceEfficiency()
        {
            csEfficiencyValue = controlSurfaceEfficiency.Evaluate(normalizeMPH);
        }

        void HandlePitch()
        {
            Vector3 flatForward = transform.forward;
            flatForward.y = 0f;
            flatForward = flatForward.normalized;
            pitchAngle = Vector3.Angle(transform.forward, flatForward);
            // Debug.Log(pitchAngle);

            Vector3 pitchTorque = input.Pitch * pitchSpeed * transform.right * csEfficiencyValue;
            rb.AddTorque(pitchTorque);
        }

        void HandleRoll()
        {
            Vector3 flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            rollAngle = Vector3.SignedAngle(transform.right, flatRight, transform.forward);
            // Debug.Log(rollAngle);

            Vector3 rollTorque = -input.Roll * rollSpeed * transform.forward * csEfficiencyValue;
            rb.AddTorque(rollTorque);
        }

        void HandleYaw()
        {
            Vector3 yawTorque = input.Yaw * yawSpeed * transform.up * csEfficiencyValue;
            rb.AddTorque(yawTorque);
        }

        void HandleBanking()
        {
            float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
            float bankAmount = Mathf.Lerp(-1f, 1f, bankSide);
            Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
            rb.AddTorque(bankTorque);
        }
        #endregion
    }
}
