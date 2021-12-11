using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_CameraController : MonoBehaviour
    {
        #region Variables
        [Header("Camera Controller Properties")]
        public AC_BaseAirplane_Input input;
        public List<Camera> cameras = new List<Camera>();

        private int curentCameraIndex=0;

        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(input.CameraSwitch){
                SwitchCamera();

            }
        }
        #endregion

        #region Custom Methods
        protected virtual void SwitchCamera()
        {
            // chnage the camera index as we chaneg the camera
            cameras[curentCameraIndex].enabled = false;
            curentCameraIndex++;
            //circular index 
            if (curentCameraIndex >= cameras.Count){
                curentCameraIndex = 0;
            }
            cameras[curentCameraIndex].enabled = true;
        }
        #endregion
    }

}
