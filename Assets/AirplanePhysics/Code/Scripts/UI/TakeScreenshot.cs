using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;

public class TakeScreenshot: MonoBehaviour {
    
 
    public static string ScreenShotName(int width, int height) {
         return string.Format("{0}/screen_{1}x{2}_{3}.png", 
                              Application.dataPath, 
                              width, height, 
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
     }
 
    public void CaptureIt() {
            int count = Camera.allCameras.Length;
            print("We've got " + count + " cameras");

            int resWidth = 1920; 
            int resHeight = 1200;

            Camera camera = Camera.allCameras[0];
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
     }
 }