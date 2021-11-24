using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    public class AC_Airplane_Controller : AC_BaseRigidbody_Controller
    {
        #region variables
        [Header("Base Airplane Properties")]
        [Tooltip("Drag and drop here the  AC_BaseAirplane_Input.cs OR AC_XboxAirplane_Input.cs")]
        public AC_BaseAirplane_Input input;

        [Tooltip("Weight is in pounds")]
        public float airplaneWeight = 800f;

        [Tooltip("Initialize an empty object and set it in  airplane body. That position is Center of Gravity of the Airplane. Hook that object here")]
        public Transform centerOfGravity;
        
        #endregion

        #region Constants
        const float poundToKilos = 0.453592f;
        #endregion

        #region Builtin Methods
        public override void Start()
        {
            base.Start();
            float finalMass =  airplaneWeight * poundToKilos;
            if (rb){
                rb.mass = finalMass;
                if(centerOfGravity){
                    rb.centerOfMass = centerOfGravity.localPosition;
                } // handel exception 
            }
                
        }
        #endregion

        #region Custom Methods
        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleAerodynamics();
            HandleSteering();
            HandleBrakes();
            HandleAltitude();
        }

        void HandleEngines(){

        }
        void HandleAerodynamics(){

        }
        void HandleSteering(){

        }
        void HandleBrakes(){

        }
        void HandleAltitude(){

        }
        #endregion
    }

}
