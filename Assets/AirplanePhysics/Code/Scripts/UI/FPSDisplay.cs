using UnityEngine;
using UnityEngine.UI;
 
public class FPSDisplay : MonoBehaviour
{
    public Text fpsText;
    private float deltaTime;
 
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS :"+ Mathf.Ceil (fps).ToString ();
     }
}