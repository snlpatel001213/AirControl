using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  AirControl
{
    public class AC_Airplane_Camera : AC_Basic_Follow_Camera
    {
        #region Variables
        [Header("Airplane Camera Properties")]
        public float minHeaightFromGround = 8;

        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods
        protected override void HandleCamera(){
            // Airplane specifc camera
            // Ray cast hit the ground
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit)){
                if(hit.distance < minHeaightFromGround && hit.transform.tag == "ground"){
                    float wantedHeight =  originalCamraHeight + (minHeaightFromGround - hit.distance);
                    cameraHeight = wantedHeight;
                }
            }
            base.HandleCamera();
        }
        #endregion
    }

}
