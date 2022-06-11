using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;

namespace AirControl
{
    /// <summary>
    /// Main class defines the Airplane Characteristics
    /// </summary>
    public class AC_Airplane_Characteristics : MonoBehaviour 
    {
        #region Varaibles
        [Header("Characteristics Properties")]
        private  float maxMPH;
        private float rbLerpSpeed;


        [Header("Lift Properties")]
        private float maxLiftPower;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        private float flapLiftPower;


        [Header("Drag Properties")]
        private float dragFactor;
        private float flapDragFactor;


        [Header("Control Properties")]
        private float pitchSpeed;
        private float rollSpeed;
        private float yawSpeed;
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

        #region BuiltIn Methods 
        void Start(){
        maxMPH = (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/maxMPH"];
        rbLerpSpeed = (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/rbLerpSpeed"];

        maxLiftPower =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/maxLiftPower"] ;
        flapLiftPower =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/flapLiftPower"];

        dragFactor =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/dragFactor"];
        flapDragFactor =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/flapDragFactor"];

        pitchSpeed =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/pitchSpeed"];
        rollSpeed =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/rollSpeed"];
        yawSpeed =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/yawSpeed"];
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Initialize Airplane Charatceristics
        /// </summary>
        /// <param name="curRB">Rigid body reference to Airplane</param>
        /// <param name="curInput">Airplane Inputs</param>
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

        /// <summary>
        /// Update all the Flight Characteristics methods
        /// </summary>
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
        /// <summary>
        /// Get the local forward speed in Meters per second and convert it to Miles Per Hour
        /// </summary>
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


        /// <summary>
        /// Build a lift force strong enough to lift he plane off the ground
        /// </summary>
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

        /// <summary>
        /// Get a Drag force to keep the plane relatively stable in the air
        /// </summary>
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

        /// <summary>
        /// Control Airplane stabilization
        /// </summary>
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

        /// <summary>
        /// Control surface effciency mapping
        /// </summary>
        void HandleControlSurfaceEfficiency()
        {
            csEfficiencyValue = controlSurfaceEfficiency.Evaluate(normalizeMPH);
        }

        /// <summary>
        /// Handle Airplane Pitch
        /// </summary>
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

        /// <summary>
        /// Handle Airplane Roll
        /// </summary>
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

        /// <summary>
        /// Handle Airplane Yaw
        /// </summary>
        void HandleYaw()
        {
            Vector3 yawTorque = input.Yaw * yawSpeed * transform.up * csEfficiencyValue;
            rb.AddTorque(yawTorque);
        }

        /// <summary>
        /// Handle Airplane Braking
        /// </summary>
        void HandleBanking()
        {
            float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
            float bankAmount = Mathf.Lerp(-1f, 1f, bankSide);
            Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
            rb.AddTorque(bankTorque);
        }
 
        // /// <summary>
        // /// Get the local forward speed in Meters per second and convert it to Miles Per Hour
        // /// </summary>
        // void CalculateForwardSpeed()
        // {
        //     //Transform the Rigidbody velocity vector from world space to local space
        //     Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        //     forwardSpeed = Mathf.Max(0f, localVelocity.z);
        //     //  forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);

        //     //find the Miles Per Hour from Meters Per Second
        //     mph = forwardSpeed * mpsToMph;
        //     //  mph = Mathf.Clamp(mph, 0f, maxMPH);
        //     normalizeMPH = Mathf.InverseLerp(0f, maxMPH, mph);
        // }


        // /// <summary>
        // /// Build a lift force strong enough to lift he plane off the ground
        // /// </summary>
        // void CalculateLift()
        // {
        //     //Get the angle of Attack
        //     angleOfAttack = Vector3.Dot(rb.velocity.normalized, transform.forward);
        //     angleOfAttack *= angleOfAttack;

        //     //Create the Lift Direction
        //     Vector3 liftDir = transform.up;
        //     float liftPower = liftCurve.Evaluate(normalizeMPH) * maxLiftPower;

        //     //Add Flap Lift
        //     float finalLiftPower = flapLiftPower * input.NormalizedFlaps;
        //     //Apply the final Lift Force to the Rigidbody
        //     Vector3 finalLiftForce = liftDir * ((liftPower * angleOfAttack) + finalLiftPower);
        //     rb.AddForce(finalLiftForce);
        // }

        // /// <summary>
        // /// Get a Drag force to keep the plane relatively stable in the air
        // /// </summary>
        // void CalculateDrag()
        // {
        //     //Speed Drag
        //     float speedDrag = forwardSpeed * dragFactor;

        //     //Flap Drag
        //     float flapDrag = input.Flaps * flapDragFactor;

        //     //add it all together!
        //     float finalDrag = startDrag + speedDrag + (flapDrag*input.NormalizedFlaps);

        //     rb.drag = finalDrag;
        //     rb.angularDrag = startAngularDrag * forwardSpeed;
        // }

        // /// <summary>
        // /// Control Airplane stabilization
        // /// </summary>
        // void HandleRigidbodyTransform()
        // {
        //     if(rb.velocity.magnitude > 1f)
        //     {
        //         Vector3 updatedVelocity = Vector3.Lerp(rb.velocity, transform.forward * forwardSpeed, forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
        //         rb.velocity = updatedVelocity;


        //         Quaternion updatedRotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity, transform.up), Time.deltaTime * rbLerpSpeed);
        //         rb.MoveRotation(updatedRotation);
        //     }
        // }

        // /// <summary>
        // /// Control surface effciency mapping
        // /// </summary>
        // void HandleControlSurfaceEfficiency()
        // {
        //     csEfficiencyValue = controlSurfaceEfficiency.Evaluate(normalizeMPH);
        // }

        // /// <summary>
        // /// Handle Airplane Pitch
        // /// </summary>
        // void HandlePitch()
        // {
        //     Vector3 flatForward = transform.forward;
        //     flatForward.y = 0f;
        //     flatForward = flatForward.normalized;
        //     pitchAngle = Vector3.Angle(transform.forward, flatForward);
        //     // Debug.Log(pitchAngle);

        //     Vector3 pitchTorque = input.Pitch * pitchSpeed * transform.right * csEfficiencyValue;
        //     Debug.Log(pitchTorque);
        // }

        // /// <summary>
        // /// Handle Airplane Roll
        // /// </summary>
        // void HandleRoll()
        // {
        //     Vector3 flatRight = transform.right;
        //     flatRight.y = 0f;
        //     flatRight = flatRight.normalized;
        //     rollAngle = Vector3.SignedAngle(transform.right, flatRight, transform.forward);
        //     // Debug.Log(rollAngle);

        //     Vector3 rollTorque = -input.Roll * rollSpeed * transform.forward * csEfficiencyValue;
        //     rb.AddTorque(rollTorque);
        // }

        // /// <summary>
        // /// Handle Airplane Yaw
        // /// </summary>
        // void HandleYaw()
        // {
        //     Vector3 yawTorque = input.Yaw * yawSpeed * transform.up * csEfficiencyValue;
        //     rb.AddTorque(yawTorque);
        // }

        // /// <summary>
        // /// Handle Airplane Braking
        // /// </summary>
        // void HandleBanking()
        // {
        //     float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
        //     float bankAmount = Mathf.Lerp(-1f, 1f, bankSide);
        //     Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
        //     rb.AddTorque(bankTorque);
        // }
        #endregion
    }
}
