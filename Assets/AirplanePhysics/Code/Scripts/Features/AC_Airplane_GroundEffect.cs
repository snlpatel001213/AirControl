using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
namespace AirControl
{
    public class AC_Airplane_GroundEffect : MonoBehaviour
    {
        // Start is called before the first frame update
        #region Variables
        [Tooltip("Distance from ground when ground effect ends")]
        private float groundDistance;
        [Tooltip("Max list force")]
        private float liftForce;
        [Tooltip("Max speed for max round effect")]
        private float maxSpeed;

        private Rigidbody rb;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            groundDistance = (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/groundDistance"];
            liftForce =  (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/liftForce"];
            maxSpeed =   (float)CommonFunctions.airplanePreset[CommonFunctions.ActiveAirplane+"/maxSpeed"];
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(rb)
            {   
                HandleGroundEffect();
            }
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Creates the Ground effect - https://en.wikipedia.org/wiki/Ground_effect_(aerodynamics)
        /// </summary>
        protected virtual void HandleGroundEffect()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if(hit.distance < groundDistance && hit.transform.tag=="Ground")
                {
                    
                    float currentSpeed = rb.velocity.magnitude;
                    float normalizedSpeed = currentSpeed/maxSpeed;
                    normalizedSpeed = Mathf.Clamp01(normalizedSpeed);

                    float distance = groundDistance - hit.distance;
                    float finalForce = liftForce *distance *normalizedSpeed;
                    rb.AddForce(Vector3.up*finalForce);
                }
            }

        }
        #endregion
    }

}
