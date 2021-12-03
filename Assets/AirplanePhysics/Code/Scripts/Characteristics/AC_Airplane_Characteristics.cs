using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AirControl{
    public class AC_Airplane_Characteristics : MonoBehaviour
    {
        #region Variables
        [Header("Charatceristics")]
        public float forwardSpeed;
        public float milesPerHours;
        [Header("Max flying speed for the Airplane")]
        public float maxMilesPerHours = 110f;

        [Header("Lift Properties")]
        [Tooltip("For Cesna its 2500")]
        public float maxLiftPower = 2500f;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);

        [Header("Drag Properties")]
        public float dragFactor = 0.01f;

        [Header("Current Power")]
        public float currentPower;


        private Rigidbody rb;
        private float startDrag=0.01f;
        private float startAngularDrag=0.025f;
        private float maxMetersPerSecond;
        private float normalizedMilesPerHour;
        private float angleOfAttack;

    
        #endregion

        #region Constants
        const float mpsTomph = 2.23694f;
        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods
        public void InitCharacteristics(Rigidbody currentRigidBody){
            //Basic Initialization
            rb = currentRigidBody;
    
            //max meters per second that we can fly with this plane
            maxMetersPerSecond =  maxMilesPerHours/mpsTomph;
        }

        public void UpdateCharacteristics(){
            // Processs the flight 
            if(rb){
                CalculateForwardSpeed();
                CalculateLift();
                CalculateDrag();
                HandleRigidbodyTransform();
                
            }
        }
        public void CalculateForwardSpeed()
        {
            //Calculating velocity of airplane with respect to itself
            Vector3 localVelocity =  transform.InverseTransformDirection(rb.velocity);
            forwardSpeed = Mathf.Max(0f,localVelocity.z);
            forwardSpeed = Mathf.Clamp(forwardSpeed, 0f,maxMetersPerSecond);

            milesPerHours = forwardSpeed*mpsTomph;
            milesPerHours = Mathf.Clamp(milesPerHours, 0f, maxMilesPerHours);

            normalizedMilesPerHour = Mathf.InverseLerp(0f, maxMilesPerHours, milesPerHours);
            // for to debug the velocity vector
            // Debug.DrawRay(transform.position, transform.position+localVelocity, Color.green );

        }

        // using bernoulis principle to generate lift
        public void CalculateLift()
        {

            // calculate angle of attck
            angleOfAttack =  Vector3.Dot(rb.velocity.normalized, transform.forward);
            // square the angleOfAttack to get exponential curve
            angleOfAttack =  angleOfAttack*angleOfAttack;

            // Generate the lift in upward direction
            Vector3 liftDirection = transform.up;
            float liftPower = liftCurve.Evaluate(normalizedMilesPerHour)* maxLiftPower;
            // just for visualization purpose
            currentPower = liftPower;
                
            Vector3 finalLiftForce =  liftDirection* liftPower*angleOfAttack;
            rb.AddForce(finalLiftForce);
                
            // for to debug the lift vector
            // Debug.DrawRay(transform.position, transform.position+finalLiftForce, Color.magenta );
        }

        // counting the Air friction
        public void CalculateDrag()
        {
            float speedDrag =  forwardSpeed*dragFactor;
            float finalDrag = speedDrag+startDrag;
            rb.drag = finalDrag;
            rb.angularDrag = startAngularDrag * forwardSpeed;
        }

        void HandleRigidbodyTransform()
        {
            if(rb.velocity.magnitude > 1f)
            {
                // More power we add we want the plane to be pointed in that direction
                // angleOfAttack is added, as if  angleOfAttack==0 Airplane is at 90 degree to ground then no more lift and the motion only depends on power of engine
                Vector3 updatedVelocity = Vector3.Lerp(rb.velocity, transform.forward*forwardSpeed, forwardSpeed*angleOfAttack*Time.deltaTime);
                rb.velocity = updatedVelocity;

                // correct airplane body as per the force direction=
                // When powered, nose up
                // Stay horizontal with medium power 
                // When engin cut nose down
                Quaternion updateRottion = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity.normalized, transform.up), Time.deltaTime);
                rb.MoveRotation(updateRottion);
                
            }


        }
        #endregion
    }

}
