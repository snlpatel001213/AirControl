using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        GameObject[] runways = GameObject.FindGameObjectsWithTag("Runway");
        foreach (GameObject i in runways){
            // i.transform.parent;
            
        }
        
        List<string> items =  new List<string>();
        items.Add(runways[0].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
