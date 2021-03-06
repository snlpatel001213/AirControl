using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;

namespace AirControl
{   public enum ControlSurfaceType{
    Rudder, 
    Elevator, 
    Flaps, 
    Alerons
    }
    /// <summary>
    /// Handle control surfaces including Rudder, Elevator, Flaps,and Alerons
    /// </summary>
    public class AC_Airplane_ControlSurface : MonoBehaviour
{
    #region Variables
    [Header("Control surface Properties")]
    public ControlSurfaceType surfacetype = ControlSurfaceType.Rudder;
    private float maxAngle; 
    public Vector3 axis = Vector3.right;
    public Transform controlSurfaceGraphic;
    // smoothly rotate
    private float smoothSpeed; 
    private float wantedAngle;
    #endregion

    #region Builtin Methods

    void Start(){
        maxAngle = (float)CommonFunctions.airplanePreset[CommonFunctions.activeAirplane+"/maxAngle"]; 
        smoothSpeed= (float)CommonFunctions.airplanePreset[CommonFunctions.activeAirplane+"/smoothSpeed"]; 

    }

    // Update is called once per frame
    void Update()
    {
        if(controlSurfaceGraphic){
            // rotate the control surface
            Vector3 effectiveRotationAxis = axis*wantedAngle;
            controlSurfaceGraphic.localRotation = Quaternion.Slerp(controlSurfaceGraphic.localRotation, Quaternion.Euler(effectiveRotationAxis), Time.deltaTime*smoothSpeed);
        }
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Main function to handle Control Surface
    /// </summary>
    /// <param name="input">Airplane Input</param>
    public void HandleControlSurface(AC_BaseAirplane_Input input)
    {
        float inputValue = 0f;
        switch(surfacetype)
        {
            case ControlSurfaceType.Rudder:
                inputValue = input.Yaw;
                break;
            case ControlSurfaceType.Elevator:
                inputValue = input.Pitch;
                break;
            case ControlSurfaceType.Flaps:
                inputValue = input.Flaps;
                break;
            case ControlSurfaceType.Alerons:
                inputValue = input.Roll;
                break;
            default:
                // Throw error here
                break;
        }
        wantedAngle = maxAngle*inputValue;
    }
    #endregion
}

}
