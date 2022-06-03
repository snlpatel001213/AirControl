using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime;
 
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS :"+ Mathf.Ceil (fps).ToString ();
     }
}