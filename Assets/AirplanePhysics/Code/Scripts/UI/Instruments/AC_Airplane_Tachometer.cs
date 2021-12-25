using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

namespace AirControl
{
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
        public void HandleAirplaneUI(SQLiteConnection connection)
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
