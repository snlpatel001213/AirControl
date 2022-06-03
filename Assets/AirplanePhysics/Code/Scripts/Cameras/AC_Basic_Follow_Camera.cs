using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;

namespace  AirControl
{
    /// <summary>
    /// Setup follow camera
    /// </summary>
    public class AC_Basic_Follow_Camera : MonoBehaviour
    {
        #region Variables
        [Header("Basic follow camera properties")]
        [Tooltip("Drag and drop entire airplane group over here")]
        public Transform airplane;
        public float cameraDistance = 10f;
        public float cameraHeight = 6f;
        public float cameraMovementSpeed = 0.5f;

        private Vector3 smoothVelocity;

        protected float originalCamraHeight;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            originalCamraHeight = cameraHeight;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(airplane){
                HandleCamera();
            }// handel else
            
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Setup follow camera height and distance from the Airplane
        /// </summary>
        protected virtual void HandleCamera()
        {
            // Camera that follow the target
            // -airplane.forward*cameraDistance Negative to bring the camera back of the airplane
            Vector3 wantedPosition =  airplane.position + (-airplane.forward*cameraDistance) + Vector3.up*cameraHeight;
            // Debug.DrawLine(airplane.position, wantedPosition, Color.blue);
            transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref smoothVelocity, cameraMovementSpeed);
            // Watch the airplane
            transform.LookAt(airplane);
        }
    #endregion
    }

    
}

