using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    /// <summary>
    /// Monitor and updates the tachometer to UI 
    /// </summary>
    public class AC_Airplane_Tachometer : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Tachometer  Properties")]
        [Tooltip("Drag and drop Engine Here")]
        public AC_Airplane_Engine engine;
        [Tooltip("Drag and drop pointer Here")]
        public RectTransform pointer; 
        public float maxRPMIntachometer = 3500f;

        private float smoothrotation;
        #endregion

        #region Builtin Methods
        #endregion

        #region Interface Methods
        /// <summary>
        /// Updates to UI 
        /// </summary>
        public void HandleAirplaneUI()
        {
            if(engine && pointer)
            {
                float normalizedRPM = Mathf.InverseLerp(0f, engine.maxRPM, engine.CurrentRPM);
                float radialspeed = 360f * normalizedRPM;
                //smoothly move tachmeter
                smoothrotation = Mathf.Lerp(smoothrotation, radialspeed, Time.deltaTime*0.1f);
                pointer.rotation = Quaternion.Euler(0f,0f,-smoothrotation);
            }
            

        }
        #endregion
    }

}
