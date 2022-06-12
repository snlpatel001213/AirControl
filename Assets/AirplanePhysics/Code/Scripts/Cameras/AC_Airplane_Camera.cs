using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
namespace  AirControl
{   
    /// <summary>
    /// Basic follow camera implementation
    /// </summary>
    public class AC_Airplane_Camera : AC_Basic_Follow_Camera
    {
        #region Variables
        // [Header("Airplane Camera Properties")]
        // public float minHeaightFromGround = 8;

        #endregion

        #region Builtin Methods
        // void Start(){
        //     airplane = GameObject.Find(CommonFunctions.ActiveAirplane+"/FollowCamera").GetComponent<Transform>();
        // }
        // void Update(){
            
        //     transform.LookAt(airplane.transform.position);
        // }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Handle follow camera  
        /// </summary>
        // protected override void HandleCamera(){
        //     // Airplane specifc camera
        //     // Ray cast hit the ground
            
        //     RaycastHit hit;
        //     if(Physics.Raycast(transform.position, Vector3.down, out hit)){
        //         if(hit.distance < minHeaightFromGround && hit.transform.tag == "Ground"){
        //             float wantedHeight =  originalCamraHeight + (minHeaightFromGround - hit.distance);
        //             cameraHeight = wantedHeight;
        //         }
        //     }
        //     base.HandleCamera();
        // }
        #endregion
    }

}
