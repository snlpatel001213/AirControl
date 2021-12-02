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

        [Header("Lift Properties")]
        public float maxLiftPower = 800f;

        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag;
    
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
            startDrag = rb.drag;
            startAngularDrag =  rb.angularDrag;
        }

        public void UpdateCharacteristics(){
            // Processs the flight 
            if(rb){
                CalculateForwardSpeed();
                CalculateLift();
                CalculateDrag();
            }
            

        }
        public void CalculateForwardSpeed(){
            //Calculating velocity of airplane with respect to itself
            Vector3 localVelocity =  transform.InverseTransformDirection(rb.velocity);
            forwardSpeed = Mathf.Max(0f,localVelocity.z);
            milesPerHours = forwardSpeed*mpsTomph;
            // for to debug the velocity vector
            Debug.DrawRay(transform.position, transform.position+localVelocity, Color.green );

        }
        // using bernoulis principle to generate lift
        public void CalculateLift(){
                // Generate the lift in upward direction
                Vector3 liftDirection = Vector3.up;
                float liftPower =  forwardSpeed * maxLiftPower;

                Vector3 finalLiftForce =  liftDirection* liftPower;
                rb.AddForce(finalLiftForce);
                Debug.DrawRay(transform.position, transform.position+finalLiftForce, Color.magenta );
            
            

        }
        public void CalculateDrag(){

        }
        #endregion
    }

}
