using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Communicator;
using System.IO;
using Commons;

namespace AirControl
{
    /// <summary>
    /// Camera switching and capture functionality
    /// </summary>
    public class AC_Airplane_CameraController : MonoBehaviour
    {
        #region Variables
        [Header("Camera Controller Properties")]
        public AC_BaseAirplane_Input input;

        public List<Camera> cameras = new List<Camera>();
        public List<CapturePass> CapturePassList = new List<CapturePass>();
        public int startCameraIndex =1;
        private int curentCameraIndex=1;
        private int currentCaprtureCamera=1;
        [Header("Shader Setup")]
        public Shader uberReplacementShader;
        public Shader opticalFlowShader;
        public float opticalFlowSensitivity = 1.0f;

        [Header("Save Image Capture")]
        public bool saveImage = true;
        public bool saveIdSegmentation = true;
        public bool saveLayerSegmentation = true;
        public bool saveDepth = true;
        public bool saveNormals = true;
        public bool saveOpticalFlow=true;
        // camera pass configuration
        public CapturePass[] capturePasses = new CapturePass[] {
            new CapturePass() { name = "_img" },
            new CapturePass() { name = "_id", supportsAntialiasing = false },
            new CapturePass() { name = "_layer", supportsAntialiasing = false },
            new CapturePass() { name = "_depth" },
            new CapturePass() { name = "_normals" },
            new CapturePass() { name = "_flow", supportsAntialiasing = false, needsRescale = true } // (see issue with Motion Vectors in @KNOWN ISSUES)
        };
        // Capture pass struct
        public struct CapturePass
        {
            // configuration
            public string name;
            public bool supportsAntialiasing;
            public bool needsRescale;
            public CapturePass(string name_) { name = name_; supportsAntialiasing = true; needsRescale = false; camera = null; }

            // impl
            public Camera camera;
        };

        // cached materials
        private Material opticalFlowMaterial;
        // Type of capture
        public enum ReplacementMode
        {
            ObjectId = 0,
            CatergoryId = 1,
            DepthCompressed = 2,
            DepthMultichannel = 3,
            Normals = 4
        };

        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            input = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<AC_BaseAirplane_Input>();
            //Adding cameras
            cameras.Add(GameObject.Find(CommonFunctions.ActiveAirplane+"/FollowCamera").GetComponent<Camera>());
            cameras.Add(GameObject.Find(CommonFunctions.ActiveAirplane+"/CockpitCamera").GetComponent<Camera>());
            if (startCameraIndex >=0 && startCameraIndex < cameras.Count)
            {
                DisableAllCameras();
                cameras[startCameraIndex].enabled = true;
                cameras[startCameraIndex].GetComponent<AudioListener>().enabled = true;
                startCameraIndex =  curentCameraIndex;
            }
            CreateCamera();
        }

        /// <summary>
        /// Create hidden capture camera for each schene camera
        /// </summary>
        public void CreateCamera()
        {
                for (int q = 0; q < capturePasses.Length; q++)
                {
                    capturePasses[q].camera = CreateHiddenCamera(capturePasses[q].name, cameras[currentCaprtureCamera].transform);
                    CapturePassList.Add(capturePasses[q]);
                }    
            
        }
        /// <summary>
        /// When called by the `CreateCamera` function  
        /// </summary>
        /// <param name="name">Capture pass name</param>
        /// <param name="parentCamera">Scene camera to which the hidden camera will be attached</param>
        /// <returns> New hidden camera</returns>
        private Camera CreateHiddenCamera(string name, Transform parentCamera)
        {
            var go = new GameObject(name, typeof(Camera));
            go.hideFlags = HideFlags.HideAndDontSave;
            go.transform.SetParent(parentCamera, false);
            var newCamera = go.GetComponent<Camera>();
            newCamera.enabled = false;
            return newCamera;
        }


        // Update is called once per frame
        void Update()
        {
            //switching camera as per the database
            #region IOSwitch
            // Keeping Get connection in the update loop is essential to avoid the lag
            int activeCamera = StaticCameraSchema.ActiveCamera;
            int captureCamera = StaticCameraSchema.CaptureCamera;
            int captureType = StaticCameraSchema.CaptureType;
            bool isCapture = StaticCameraSchema.IsCapture;
            int captureWidth = StaticCameraSchema.CaptureWidth;
            int captureHeight = StaticCameraSchema.CaptureHeight;
            bool isActive = StaticCameraSchema.IsActive;
            if (isActive)
            {
                curentCameraIndex = activeCamera;
                selectCamera(activeCamera);
                CreateCamera();
                string logString = System.String.Format("Active scene camera - {0} Capture camera - {1} Width - {2}  Height - {3}: ",curentCameraIndex, currentCaprtureCamera, captureWidth, captureHeight);
                StaticLogger.Log = logString;
                Debug.unityLogger.Log(logString);
                StaticCameraSchema.IsActive = false;
            }

            // if (isCapture)
            // {
                //screen capture
                CapturePass passes = CapturePassList[captureType];
                passes.camera.enabled =true;
                ScreenToBytes(passes.camera, cameras[curentCameraIndex] , captureWidth, captureHeight, passes.supportsAntialiasing, passes.needsRescale, ref StaticOutputSchema.ScreenCapture);
                //if the camera is changed then disable all active capture camera
                if(currentCaprtureCamera != captureCamera)
                {  
                    ResetAllCaptureCam(capturePasses);
                    currentCaprtureCamera = captureCamera;
                    string logString = System.String.Format("Active scene camera - {0} Capture camera - {1} Width - {2}  Height - {3}: ",curentCameraIndex, currentCaprtureCamera, captureWidth, captureHeight);
                    StaticLogger.Log = logString;
                    Debug.unityLogger.Log(logString);
                }
                OnCameraChange(cameras[currentCaprtureCamera]);// 1 indicate the outside camera
                OnSceneChange();
    
            // }

            #endregion
            // Manual Camera switch with key c
            if(input.CameraSwitch){
                SwitchCamera();
            }
            // run through all camera and save screenshot - for testing
            if (Input.GetKeyDown(KeyCode.Return))
            {
                int i = 0;
                foreach(CapturePass pass in CapturePassList)
                {
                    pass.camera.enabled =true;
                    Save(pass.camera, cameras[0] ,"Sunil_"+ i + pass.name + ".png", 250, 200, pass.supportsAntialiasing, pass.needsRescale);
                    i++;
                    pass.camera.enabled =false;
                    OnCameraChange(cameras[0]);
                    OnSceneChange();
                }
                foreach(CapturePass pass in CapturePassList)
                {
                    pass.camera.enabled =true;
                    Save(pass.camera, cameras[1] ,"Sunil_"+ i + pass.name + ".png", 250, 200, pass.supportsAntialiasing, pass.needsRescale);
                    i++;
                    pass.camera.enabled =false;
                    OnCameraChange(cameras[1]);
                    OnSceneChange();
                }
            }  

        }
        /// <summary>
        /// Reset all the capture camers to prevent constant capture from released cameras
        /// This is required for the compute optimization
        /// This function restart all cams and set them inactive
        /// </summary>
        /// <param name="CapturePassList">Capture pass struct</param>
        void ResetAllCaptureCam(CapturePass[] CapturePassList){
            foreach(CapturePass pass in CapturePassList)
            {
                pass.camera.enabled =false;
            }   
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Capture and return the screen as byte array
        /// </summary>
        /// <param name="cam">Hidden capture camera</param>
        /// <param name="mainCamera">Any of scene camera</param>
        /// <param name="width">Width of the capture</param>
        /// <param name="height">height of the capture</param>
        /// <param name="supportsAntialiasing">Antialiasing Property</param>
        /// <param name="needsRescale">Rescale property</param>
        /// <param name="screencapture">reference screencapture</param>
        public void ScreenToBytes(Camera cam, Camera mainCamera, int width, int height, bool supportsAntialiasing, bool needsRescale, ref byte[] screencapture)
        {
             if (width <= 0 || height <= 0)
            {
                width = Screen.width;
                height = Screen.height;
            }
            
            var depth = 8;
            var format = RenderTextureFormat.Default;
            var readWrite = RenderTextureReadWrite.Default;
            var antiAliasing = (supportsAntialiasing) ? Mathf.Max(1, QualitySettings.antiAliasing) : 1;

            var finalRT =
                RenderTexture.GetTemporary(width, height, depth, format, readWrite, antiAliasing);
            var renderRT = (!needsRescale) ? finalRT :
                RenderTexture.GetTemporary(mainCamera.pixelWidth, mainCamera.pixelHeight, depth, format, readWrite, antiAliasing);
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            var prevActiveRT = RenderTexture.active;
            var prevCameraRT = cam.targetTexture;

            // render to offscreen texture (readonly from CPU side)
            RenderTexture.active = renderRT;
            cam.targetTexture = renderRT;

            cam.Render();

            if (needsRescale)
            {
                // blit to rescale (see issue with Motion Vectors in @KNOWN ISSUES)
                RenderTexture.active = finalRT;
                Graphics.Blit(renderRT, finalRT);
                RenderTexture.ReleaseTemporary(renderRT);
            }

            // read offsreen texture contents into the CPU readable texture
            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            tex.Apply();

            // encode texture into PNG
            screencapture =  tex.EncodeToPNG();
            
        }

        /// <summary>
        /// Save function just for debugging
        /// </summary>
        /// <param name="cam">Hidden capture camera</param>
        /// <param name="mainCamera">Any of scene camera</param>
        /// <param name="width">Width of the capture</param>
        /// <param name="height">height of the capture</param>
        /// <param name="supportsAntialiasing">Antialiasing Property</param>
        /// <param name="needsRescale">Rescale property</param>
        /// <param name="screencapture">reference screencapture</param>

        private void Save(Camera cam, Camera mainCamera, string filename, int width, int height, bool supportsAntialiasing, bool needsRescale)
        {
            var depth = 24;
            var format = RenderTextureFormat.Default;
            var readWrite = RenderTextureReadWrite.Default;
            var antiAliasing = (supportsAntialiasing) ? Mathf.Max(1, QualitySettings.antiAliasing) : 1;

            var finalRT =
                RenderTexture.GetTemporary(width, height, depth, format, readWrite, antiAliasing);
            var renderRT = (!needsRescale) ? finalRT :
                RenderTexture.GetTemporary(mainCamera.pixelWidth, mainCamera.pixelHeight, depth, format, readWrite, antiAliasing);
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            var prevActiveRT = RenderTexture.active;
            var prevCameraRT = cam.targetTexture;

            // render to offscreen texture (readonly from CPU side)
            RenderTexture.active = renderRT;
            cam.targetTexture = renderRT;

            cam.Render();

            if (needsRescale)
            {
                // blit to rescale (see issue with Motion Vectors in @KNOWN ISSUES)
                RenderTexture.active = finalRT;
                Graphics.Blit(renderRT, finalRT);
                RenderTexture.ReleaseTemporary(renderRT);
            }

            // read offsreen texture contents into the CPU readable texture
            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            tex.Apply();

            // encode texture into PNG
            var bytes = tex.EncodeToPNG();
            File.WriteAllBytes(filename, bytes);

            // restore state and cleanup
            cam.targetTexture = prevCameraRT;
            RenderTexture.active = prevActiveRT;

            Object.Destroy(tex);
            RenderTexture.ReleaseTemporary(finalRT);
        }

        /// <summary>
        /// Switch camera
        /// </summary>
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

        /// <summary>
        /// select scene cameras
        /// </summary>
        /// <param name="cameraId"></param>
        public void selectCamera(int cameraId)
        {
            DisableAllCameras();
            
            if (cameraId <= cameras.Count){
                cameras[cameraId].enabled = true;
                cameras[cameraId].GetComponent<AudioListener>().enabled = true;
                curentCameraIndex = cameraId;
            }
        }

        /// <summary>
        /// Disable camera when scene camera switches
        /// </summary>
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

        /// <summary>
        /// Replacing shader accroding to active capture camera
        /// </summary>
        /// <param name="cam">Capture Camera</param>
        /// <param name="shader">Shader</param>
        /// <param name="mode">Replacement mode</param>
        /// <param name="clearColor">backgroundColor</param>
        static private void SetupCameraWithReplacementShader(Camera cam, Shader shader, ReplacementMode mode, Color clearColor)
        {
            var cb = new CommandBuffer();
            cb.SetGlobalFloat("_OutputMode", (int)mode); // @TODO: CommandBuffer is missing SetGlobalInt() method
            cam.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, cb);
            cam.AddCommandBuffer(CameraEvent.BeforeFinalPass, cb);
            cam.SetReplacementShader(shader, "");
            cam.backgroundColor = clearColor;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.allowHDR = false;
            cam.allowMSAA = false;
        }
        /// <summary>
        /// Deprecated Not used with API
        /// This fucntion was used to switch the screen manually
        /// </summary>
        public void OnSceneChange()
        {
            var renderers = Object.FindObjectsOfType<Renderer>();
            var mpb = new MaterialPropertyBlock();
            foreach (var r in renderers)
            {
                var id = r.gameObject.GetInstanceID();
                var layer = r.gameObject.layer;
                var tag = r.gameObject.tag;

                mpb.SetColor("_ObjectColor", ColorEncoding.EncodeIDAsColor(id));
                mpb.SetColor("_CategoryColor", ColorEncoding.EncodeLayerAsColor(layer));
                r.SetPropertyBlock(mpb);
            }
        }
        /// <summary>
        /// Refresh capture camera when active scene camera switch happens
        /// </summary>
        /// <param name="activeCamera">Active scene camera</param>
        public void OnCameraChange(Camera activeCamera)
        {
            int targetDisplay = 1;
            foreach (var pass in capturePasses)
            {
                if (pass.camera == activeCamera)
                    continue;

                // cleanup capturing camera
                pass.camera.RemoveAllCommandBuffers();

                // copy all "main" camera parameters into capturing camera
                pass.camera.CopyFrom(activeCamera);

                // set targetDisplay here since it gets overriden by CopyFrom()
                pass.camera.targetDisplay = targetDisplay++;
            }

            // cache materials and setup material properties
            if (!opticalFlowMaterial || opticalFlowMaterial.shader != opticalFlowShader)
                opticalFlowMaterial = new Material(opticalFlowShader);
            opticalFlowMaterial.SetFloat("_Sensitivity", opticalFlowSensitivity);

            // setup command buffers and replacement shaders
            SetupCameraWithReplacementShader(capturePasses[1].camera, uberReplacementShader, ReplacementMode.ObjectId, Color.black);
            SetupCameraWithReplacementShader(capturePasses[2].camera, uberReplacementShader, ReplacementMode.CatergoryId, Color.black);
            SetupCameraWithReplacementShader(capturePasses[3].camera, uberReplacementShader, ReplacementMode.DepthCompressed, Color.white);
            SetupCameraWithReplacementShader(capturePasses[4].camera, uberReplacementShader, ReplacementMode.Normals, Color.black);
            SetupCameraWithPostShader(capturePasses[5].camera, opticalFlowMaterial, DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
        }

        /// <summary>
        /// Setup shader for the capture camera
        /// </summary>
        /// <param name="cam">Capture camera</param>
        /// <param name="material">Shader Material</param>
        /// <param name="depthTextureMode">Depth capture setting</param>
        static private void SetupCameraWithPostShader(Camera cam, Material material, DepthTextureMode depthTextureMode = DepthTextureMode.None)
        {
            var cb = new CommandBuffer();
            cb.Blit(null, BuiltinRenderTextureType.CurrentActive, material);
            cam.AddCommandBuffer(CameraEvent.AfterEverything, cb);
            cam.depthTextureMode = depthTextureMode;
        }

        #endregion
    }

}
// ########################################
// depricated code will be here up to next release incase of bug with new code
// ########################################
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Rendering;
// using Communicator;
// using System.IO;
// using Commons;

// namespace AirControl
// {
//     /// <summary>
//     /// Camera switching and capture functionality
//     /// </summary>
//     public class AC_Airplane_CameraController : MonoBehaviour
//     {
//         #region Variables
//         [Header("Camera Controller Properties")]
//         public AC_BaseAirplane_Input input;
//         [SerializeField]
//         private List<Camera> cameras = new List<Camera>();
//         private Transform airplane;
//         public List<CapturePass> CapturePassList = new List<CapturePass>();
//         public int startCameraIndex =1;
//         private int curentCameraIndex=1;
//         private int currentCaprtureCamera=1;
//         [Header("Shader Setup")]
//         public Shader uberReplacementShader;
//         public Shader opticalFlowShader;
//         public float opticalFlowSensitivity = 1.0f;

//         [Header("Save Image Capture")]
//         public bool saveImage = true;
//         public bool saveIdSegmentation = true;
//         public bool saveLayerSegmentation = true;
//         public bool saveDepth = true;
//         public bool saveNormals = true;
//         public bool saveOpticalFlow=true;
//         // camera pass configuration
//         public CapturePass[] capturePasses = new CapturePass[] {
//             new CapturePass() { name = "_img" },
//             new CapturePass() { name = "_id", supportsAntialiasing = false },
//             new CapturePass() { name = "_layer", supportsAntialiasing = false },
//             new CapturePass() { name = "_depth" },
//             new CapturePass() { name = "_normals" },
//             new CapturePass() { name = "_flow", supportsAntialiasing = false, needsRescale = true } // (see issue with Motion Vectors in @KNOWN ISSUES)
//         };
//         // Capture pass struct
//         public struct CapturePass
//         {
//             // configuration
//             public string name;
//             public bool supportsAntialiasing;
//             public bool needsRescale;
//             public CapturePass(string name_) { name = name_; supportsAntialiasing = true; needsRescale = false; camera = null; }

//             // impl
//             public Camera camera;
//         };

//         // cached materials
//         private Material opticalFlowMaterial;
//         // Type of capture
//         public enum ReplacementMode
//         {
//             ObjectId = 0,
//             CatergoryId = 1,
//             DepthCompressed = 2,
//             DepthMultichannel = 3,
//             Normals = 4
//         };

//         #endregion

//         #region Builtin Methods
//         // Start is called before the first frame update


//         void Start()
//         {
//             input = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<AC_BaseAirplane_Input>();
//             //Adding cameras
//             cameras.Add(GameObject.Find(CommonFunctions.ActiveAirplane+"/FollowCamera").GetComponent<Camera>());
//             cameras.Add(GameObject.Find(CommonFunctions.ActiveAirplane+"/CockpitCamera").GetComponent<Camera>());
            

//             if (startCameraIndex >=0 && startCameraIndex < cameras.Count)
//             {
//                 DisableAllCameras();
//                 cameras[startCameraIndex].enabled = true;
//                 cameras[startCameraIndex].GetComponent<AudioListener>().enabled = true;
//                 startCameraIndex =  curentCameraIndex;
//             }
//             CreateCamera();
//         }

//         /// <summary>
//         /// Create hidden capture camera for each schene camera
//         /// </summary>
//         public void CreateCamera()
//         {
//                 for (int q = 0; q < capturePasses.Length; q++)
//                 {
//                     capturePasses[q].camera = CreateHiddenCamera(capturePasses[q].name, cameras[currentCaprtureCamera].transform);
//                     CapturePassList.Add(capturePasses[q]);
//                 }    
            
//         }
//         /// <summary>
//         /// When called by the `CreateCamera` function  
//         /// </summary>
//         /// <param name="name">Capture pass name</param>
//         /// <param name="parentCamera">Scene camera to which the hidden camera will be attached</param>
//         /// <returns> New hidden camera</returns>
//         private Camera CreateHiddenCamera(string name, Transform parentCamera)
//         {
//             var go = new GameObject(name, typeof(Camera));
//             go.hideFlags = HideFlags.HideAndDontSave;
//             go.transform.SetParent(parentCamera, false);
//             var newCamera = go.GetComponent<Camera>();
//             newCamera.enabled = false;
//             return newCamera;
//         }


//         // Update is called once per frame
//         void Update()
//         {
//             //switching camera as per the database
//             #region IOSwitch
//             // Keeping Get connection in the update loop is essential to avoid the lag
//             int activeCamera = StaticCameraSchema.ActiveCamera;
//             int captureCamera = StaticCameraSchema.CaptureCamera;
//             int captureType = StaticCameraSchema.CaptureType;
//             bool isCapture = StaticCameraSchema.IsCapture;
//             int captureWidth = StaticCameraSchema.CaptureWidth;
//             int captureHeight = StaticCameraSchema.CaptureHeight;
//             bool isActive = StaticCameraSchema.IsActive;
//             if (isActive)
//             {
//                 curentCameraIndex = activeCamera;
//                 selectCamera(activeCamera);
//                 CreateCamera();
//                 string logString = System.String.Format("Active scene camera - {0} Capture camera - {1} Width - {2}  Height - {3}: ",curentCameraIndex, currentCaprtureCamera, captureWidth, captureHeight);
//                 StaticLogger.Log = logString;
//                 Debug.unityLogger.Log(logString);
//                 StaticCameraSchema.IsActive = false;
//             }
//             // These statements were removed as these were causing delay in the scrrenshot by one frame 
//             // Capture will be continuous, to send the capture to client is defined by `isCapture` defined in Outputhandle 
//             // if (isCapture)
//             // {
//                 //screen capture
//                 CapturePass pass = CapturePassList[captureType];
//                 pass.camera.enabled =true;
//                 ScreenToBytes(pass.camera, cameras[curentCameraIndex] , captureWidth, captureHeight, pass.supportsAntialiasing, pass.needsRescale, ref StaticOutputSchema.ScreenCapture);
//                 //if the camera is changed then disable all active capture camera
//                 if(currentCaprtureCamera != captureCamera)
//                 {  
//                     ResetAllCaptureCam(capturePasses);
//                     currentCaprtureCamera = captureCamera;
//                     string logString = System.String.Format("Active scene camera - {0} Capture camera - {1} Width - {2}  Height - {3}: ",curentCameraIndex, currentCaprtureCamera, captureWidth, captureHeight);
//                     StaticLogger.Log = logString;
//                     Debug.unityLogger.Log(logString);
//                     OnCameraChange(cameras[currentCaprtureCamera]);// 1 indicate the outside camera
//                     OnSceneChange();
//                 }
                
    
//             // }

//             #endregion
//             // Manual Camera switch with key c
//             if(input.CameraSwitch){
//                 SwitchCamera();
//             }
//             // run through all camera and save screenshot - for testing
//             // if (Input.GetKeyDown(KeyCode.Return))
//             // {
//             //     int i = 0;
//             //     foreach(CapturePass pass in CapturePassList)
//             //     {
//             //         pass.camera.enabled =true;
//             //         Save(pass.camera, cameras[0] ,"Sunil_"+ i + pass.name + ".png", 250, 200, pass.supportsAntialiasing, pass.needsRescale);
//             //         i++;
//             //         pass.camera.enabled =false;
//             //         OnCameraChange(cameras[0]);
//             //         OnSceneChange();
//             //     }
//             //     foreach(CapturePass pass in CapturePassList)
//             //     {
//             //         pass.camera.enabled =true;
//             //         Save(pass.camera, cameras[1] ,"Sunil_"+ i + pass.name + ".png", 250, 200, pass.supportsAntialiasing, pass.needsRescale);
//             //         i++;
//             //         pass.camera.enabled =false;
//             //         OnCameraChange(cameras[1]);
//             //         OnSceneChange();
//             //     }
//             // }  

//         }
//         /// <summary>
//         /// Reset all the capture camers to prevent constant capture from released cameras
//         /// This is required for the compute optimization
//         /// This function restart all cams and set them inactive
//         /// </summary>
//         /// <param name="CapturePassList">Capture pass struct</param>
//         void ResetAllCaptureCam(CapturePass[] CapturePassList){
//             foreach(CapturePass pass in CapturePassList)
//             {
//                 pass.camera.enabled =false;
//             }   
//         }
//         #endregion

//         #region Custom Methods
//         /// <summary>
//         /// Capture and return the screen as byte array
//         /// </summary>
//         /// <param name="cam">Hidden capture camera</param>
//         /// <param name="mainCamera">Any of scene camera</param>
//         /// <param name="width">Width of the capture</param>
//         /// <param name="height">height of the capture</param>
//         /// <param name="supportsAntialiasing">Antialiasing Property</param>
//         /// <param name="needsRescale">Rescale property</param>
//         /// <param name="screencapture">reference screencapture</param>
//         public void ScreenToBytes(Camera cam, Camera mainCamera, int width, int height, bool supportsAntialiasing, bool needsRescale, ref byte[] screencapture)
//         {
//              if (width <= 0 || height <= 0)
//             {
//                 width = Screen.width;
//                 height = Screen.height;
//             }
            
//             var depth = 8;
//             var format = RenderTextureFormat.Default;
//             var readWrite = RenderTextureReadWrite.Default;
//             var antiAliasing = (supportsAntialiasing) ? Mathf.Max(1, QualitySettings.antiAliasing) : 1;

//             var finalRT =
//                 RenderTexture.GetTemporary(width, height, depth, format, readWrite, antiAliasing);
//             var renderRT = (!needsRescale) ? finalRT :
//                 RenderTexture.GetTemporary(mainCamera.pixelWidth, mainCamera.pixelHeight, depth, format, readWrite, antiAliasing);
//             var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

//             var prevActiveRT = RenderTexture.active;
//             var prevCameraRT = cam.targetTexture;

//             // render to offscreen texture (readonly from CPU side)
//             RenderTexture.active = renderRT;
//             cam.targetTexture = renderRT;

//             cam.Render();

//             if (needsRescale)
//             {
//                 // blit to rescale (see issue with Motion Vectors in @KNOWN ISSUES)
//                 RenderTexture.active = finalRT;
//                 Graphics.Blit(renderRT, finalRT);
//                 RenderTexture.ReleaseTemporary(renderRT);
//             }

//             // read offsreen texture contents into the CPU readable texture
//             tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
//             tex.Apply();

//             // encode texture into PNG
//             screencapture =  tex.EncodeToPNG();
            
//         }

//         /// <summary>
//         /// Save function just for debugging
//         /// </summary>
//         /// <param name="cam">Hidden capture camera</param>
//         /// <param name="mainCamera">Any of scene camera</param>
//         /// <param name="width">Width of the capture</param>
//         /// <param name="height">height of the capture</param>
//         /// <param name="supportsAntialiasing">Antialiasing Property</param>
//         /// <param name="needsRescale">Rescale property</param>
//         /// <param name="screencapture">reference screencapture</param>

//         private void Save(Camera cam, Camera mainCamera, string filename, int width, int height, bool supportsAntialiasing, bool needsRescale)
//         {
//             var depth = 24;
//             var format = RenderTextureFormat.Default;
//             var readWrite = RenderTextureReadWrite.Default;
//             var antiAliasing = (supportsAntialiasing) ? Mathf.Max(1, QualitySettings.antiAliasing) : 1;

//             var finalRT =
//                 RenderTexture.GetTemporary(width, height, depth, format, readWrite, antiAliasing);
//             var renderRT = (!needsRescale) ? finalRT :
//                 RenderTexture.GetTemporary(mainCamera.pixelWidth, mainCamera.pixelHeight, depth, format, readWrite, antiAliasing);
//             var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

//             var prevActiveRT = RenderTexture.active;
//             var prevCameraRT = cam.targetTexture;

//             // render to offscreen texture (readonly from CPU side)
//             RenderTexture.active = renderRT;
//             cam.targetTexture = renderRT;

//             cam.Render();

//             if (needsRescale)
//             {
//                 // blit to rescale (see issue with Motion Vectors in @KNOWN ISSUES)
//                 RenderTexture.active = finalRT;
//                 Graphics.Blit(renderRT, finalRT);
//                 RenderTexture.ReleaseTemporary(renderRT);
//             }

//             // read offsreen texture contents into the CPU readable texture
//             tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
//             tex.Apply();

//             // encode texture into PNG
//             var bytes = tex.EncodeToPNG();
//             File.WriteAllBytes(filename, bytes);

//             // restore state and cleanup
//             cam.targetTexture = prevCameraRT;
//             RenderTexture.active = prevActiveRT;

//             Object.Destroy(tex);
//             RenderTexture.ReleaseTemporary(finalRT);
//         }

//         /// <summary>
//         /// Switch camera
//         /// </summary>
//         protected virtual void SwitchCamera()
//         {
//             // chnage the camera index as we chaneg the camera
//             DisableAllCameras();
//             curentCameraIndex++;
//             //circular index 
//             if (curentCameraIndex >= cameras.Count){
//                 curentCameraIndex = 0;
//             }
//             cameras[curentCameraIndex].enabled = true;
//             cameras[curentCameraIndex].GetComponent<AudioListener>().enabled = true;
//         }

//         /// <summary>
//         /// select scene cameras
//         /// </summary>
//         /// <param name="cameraId"></param>
//         public void selectCamera(int cameraId)
//         {
//             DisableAllCameras();
            
//             if (cameraId <= cameras.Count){
//                 cameras[cameraId].enabled = true;
//                 cameras[cameraId].GetComponent<AudioListener>().enabled = true;
//                 curentCameraIndex = cameraId;
//             }
//         }

//         /// <summary>
//         /// Disable camera when scene camera switches
//         /// </summary>
//         void DisableAllCameras()
//         {
//             if(cameras.Count > 0)
//             {
//                 foreach(Camera cam in cameras)
//                 {
//                     cam.enabled = false;
//                     cam.GetComponent<AudioListener>().enabled = false;
//                 }
//             }
//         }

//         /// <summary>
//         /// Replacing shader accroding to active capture camera
//         /// </summary>
//         /// <param name="cam">Capture Camera</param>
//         /// <param name="shader">Shader</param>
//         /// <param name="mode">Replacement mode</param>
//         /// <param name="clearColor">backgroundColor</param>
//         static private void SetupCameraWithReplacementShader(Camera cam, Shader shader, ReplacementMode mode, Color clearColor)
//         {
//             var cb = new CommandBuffer();
//             cb.SetGlobalFloat("_OutputMode", (int)mode); // @TODO: CommandBuffer is missing SetGlobalInt() method
//             cam.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, cb);
//             cam.AddCommandBuffer(CameraEvent.BeforeFinalPass, cb);
//             cam.SetReplacementShader(shader, "");
//             cam.backgroundColor = clearColor;
//             cam.clearFlags = CameraClearFlags.SolidColor;
//             cam.allowHDR = false;
//             cam.allowMSAA = false;
//         }
//         /// <summary>
//         /// Deprecated Not used with API
//         /// This fucntion was used to switch the screen manually
//         /// </summary>
//         public void OnSceneChange()
//         {
//             var renderers = Object.FindObjectsOfType<Renderer>();
//             var mpb = new MaterialPropertyBlock();
//             foreach (var r in renderers)
//             {
//                 var id = r.gameObject.GetInstanceID();
//                 var layer = r.gameObject.layer;
//                 var tag = r.gameObject.tag;

//                 mpb.SetColor("_ObjectColor", ColorEncoding.EncodeIDAsColor(id));
//                 mpb.SetColor("_CategoryColor", ColorEncoding.EncodeLayerAsColor(layer));
//                 r.SetPropertyBlock(mpb);
//             }
//         }
//         /// <summary>
//         /// Refresh capture camera when active scene camera switch happens
//         /// </summary>
//         /// <param name="activeCamera">Active scene camera</param>
//         public void OnCameraChange(Camera activeCamera)
//         {
//             int targetDisplay = 1;
//             foreach (var pass in capturePasses)
//             {
//                 if (pass.camera == activeCamera)
//                     continue;

//                 // cleanup capturing camera
//                 pass.camera.RemoveAllCommandBuffers();

//                 // copy all "main" camera parameters into capturing camera
//                 pass.camera.CopyFrom(activeCamera);

//                 // set targetDisplay here since it gets overriden by CopyFrom()
//                 pass.camera.targetDisplay = targetDisplay++;
//             }

//             // cache materials and setup material properties
//             if (!opticalFlowMaterial || opticalFlowMaterial.shader != opticalFlowShader)
//                 opticalFlowMaterial = new Material(opticalFlowShader);
//             opticalFlowMaterial.SetFloat("_Sensitivity", opticalFlowSensitivity);

//             // setup command buffers and replacement shaders
//             SetupCameraWithReplacementShader(capturePasses[1].camera, uberReplacementShader, ReplacementMode.ObjectId, Color.black);
//             SetupCameraWithReplacementShader(capturePasses[2].camera, uberReplacementShader, ReplacementMode.CatergoryId, Color.black);
//             SetupCameraWithReplacementShader(capturePasses[3].camera, uberReplacementShader, ReplacementMode.DepthCompressed, Color.white);
//             SetupCameraWithReplacementShader(capturePasses[4].camera, uberReplacementShader, ReplacementMode.Normals, Color.black);
//             SetupCameraWithPostShader(capturePasses[5].camera, opticalFlowMaterial, DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
//         }

//         /// <summary>
//         /// Setup shader for the capture camera
//         /// </summary>
//         /// <param name="cam">Capture camera</param>
//         /// <param name="material">Shader Material</param>
//         /// <param name="depthTextureMode">Depth capture setting</param>
//         static private void SetupCameraWithPostShader(Camera cam, Material material, DepthTextureMode depthTextureMode = DepthTextureMode.None)
//         {
//             var cb = new CommandBuffer();
//             cb.Blit(null, BuiltinRenderTextureType.CurrentActive, material);
//             cam.AddCommandBuffer(CameraEvent.AfterEverything, cb);
//             cam.depthTextureMode = depthTextureMode;
//         }

//         #endregion
//     }

// }
