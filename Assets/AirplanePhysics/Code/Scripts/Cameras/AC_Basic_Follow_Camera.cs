using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  AirControl
{
    public class AC_Basic_Follow_Camera : MonoBehaviour
    {
        #region Variables
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            HandleCamera()
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleCamera()
        {
            //Camera that follow the target
        
        }
    #endregion
    }

    
}

