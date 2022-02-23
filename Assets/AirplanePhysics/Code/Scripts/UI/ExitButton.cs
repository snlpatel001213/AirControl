using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // control through API
        if (StaticUIControlsSchema.ifExit){
            Exit();
        }
    }
    public void Exit(){
        Debug.Log("Exit Triggered");
        Application.Quit();
#if UNITY_EDITOR	
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}
