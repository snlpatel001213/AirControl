using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using SqliteDB;


namespace AirControl
{
    public class AC_Airplane_CameraController : MonoBehaviour
    {
        #region Variables
        [Header("Camera Controller Properties")]
        public AC_BaseAirplane_Input input;

        public List<Camera> cameras = new List<Camera>();
        public int startCameraIndex = 0;
        private int curentCameraIndex=0;

        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            if (startCameraIndex >=0 && startCameraIndex < cameras.Count)
            {
                DisableAllCameras();
                cameras[startCameraIndex].enabled = true;
                cameras[startCameraIndex].GetComponent<AudioListener>().enabled = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //switching camera as per the database
            #region DBSwitch
            
            SQLiteConnection connection = DB_Init.GetConnection();
            DB_Schema x = DB_Functions.getcamera_status(connection);
            // Debug.Log(x.ToString());
            if (x.ActiveCamera != curentCameraIndex)
            {
                selectCamera(x.ActiveCamera);
            }

            #endregion

            if(input.CameraSwitch){
                SwitchCamera();

            }
        }
        #endregion

        #region Custom Methods
        protected virtual void SwitchCamera()
        {
            // chnage the camera index as we chaneg the camera
            DisableAllCameras();
            curentCameraIndex++;
            //circular index 
            if (curentCameraIndex >= cameras.Count){
                curentCameraIndex = 0;
            }
            cameras[curentCameraIndex].enabled = true;
            cameras[curentCameraIndex].GetComponent<AudioListener>().enabled = true;
        }

        public void selectCamera(int cameraId)
        {
            DisableAllCameras();
            
            if (cameraId <= cameras.Count){
                cameras[cameraId].enabled = true;
                cameras[cameraId].GetComponent<AudioListener>().enabled = true;
                curentCameraIndex = cameraId;
            }
        }

        void DisableAllCameras()
        {
            if(cameras.Count > 0)
            {
                foreach(Camera cam in cameras)
                {
                    cam.enabled = false;
                    cam.GetComponent<AudioListener>().enabled = false;
                }
            }
        }
        #endregion
    }

}
