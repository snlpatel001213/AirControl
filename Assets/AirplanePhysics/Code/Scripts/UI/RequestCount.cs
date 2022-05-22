using UnityEngine;
using TMPro;
using Commons;
 
public class RequestCount : MonoBehaviour
{
    public TextMeshProUGUI requestCountText;
 
    void Update () {
        requestCountText.text = "REQUEST COUNT : "+ CommonFunctions.Counter;
    }
}