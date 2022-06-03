using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;
using UnityEngine.UI;

public class KeyManual : MonoBehaviour
{
    public Toggle Switch;
    public GameObject Image;
    void Start(){
        Switch.isOn = false;
    } 
    // Update is called once per frame
    void Update()
    {
        bool isActive =  Switch.isOn;
        Image.SetActive(isActive);
            
    }
}
