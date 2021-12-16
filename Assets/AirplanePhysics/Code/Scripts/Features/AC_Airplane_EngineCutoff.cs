using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Aircontrol
{
    public class AC_Airplane_EngineCutoff : MonoBehaviour
    {
        // Start is called before the first frame update
        # region Variables
        [Header("Engine cutoff properties")]
        public KeyCode cutoffKey = KeyCode.O;
        public UnityEvent onEngineCutoff =  new UnityEvent();
        #endregion

        #region Builtin Methods
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(cutoffKey))
            {
                HandleEngineCutoff();
            }
        }
        #endregion

        #region Custom Methods
        void HandleEngineCutoff()
        {   if(onEngineCutoff != null)
            {
                onEngineCutoff.Invoke();
            }
        }
        #endregion
    } 
}
