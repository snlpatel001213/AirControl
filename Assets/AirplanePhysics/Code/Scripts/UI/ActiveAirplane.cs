using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

using Commons;
 
public class ActiveAirplane : MonoBehaviour
{
    public TextMeshProUGUI activeAirplaneText;
    void Start () {
        activeAirplaneText.text = CommonFunctions.activeAirplane;
     }
}