using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
using System.Dynamic;
using System.ComponentModel;

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
        private float cameraDistance;
        private float cameraHeight; //AirplaneProperties.getFloat(CommonFunctions.ActiveAirplane, "cameraHeight");
        private float cameraMovementSpeed = 0.5f;
        private float minHeaightFromGround;

        private Vector3 smoothVelocity;
        protected float originalCamraHeight;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        
        void Start()
        {   
            cameraDistance = (float)CommonFunctions.airplanePreset[CommonFunctions.activeAirplane+"/cameraDistance"];
            cameraHeight = (float)CommonFunctions.airplanePreset[CommonFunctions.activeAirplane+"/cameraHeight"]; //AirplaneProperties.getFloat(CommonFunctions.ActiveAirplane, "cameraHeight");
            cameraMovementSpeed = 0.5f;
            minHeaightFromGround = (float)CommonFunctions.airplanePreset[CommonFunctions.activeAirplane+"/minHeaightFromGround"];
            airplane = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<Transform>();
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
            // transform.LookAt(airplane.transform.position, Vector3.up);
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit)){
                if(hit.distance < minHeaightFromGround && hit.transform.tag == "Ground"){
                    float wantedHeight =  originalCamraHeight + (minHeaightFromGround - hit.distance);
                    cameraHeight = wantedHeight;
                }
            }
        }
    #endregion
    }

    
}

