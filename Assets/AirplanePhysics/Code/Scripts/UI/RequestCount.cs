using UnityEngine;
using TMPro;
using Commons;
 
/// <summary>
/// It updates the text of the request count text object with the value of the counter variable in the 
/// CommonFunctions class
/// </summary>
public class RequestCount : MonoBehaviour
{
    public TextMeshProUGUI requestCountText;
 
    void Update () {
        requestCountText.text = "REQUEST COUNT : "+ CommonFunctions.Counter;
    }
}