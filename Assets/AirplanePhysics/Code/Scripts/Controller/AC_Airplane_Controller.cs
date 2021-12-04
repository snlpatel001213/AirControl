using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    [RequireComponent(typeof(AC_Airplane_Characteristics))]
    [RequireComponent(typeof(AC_BaseAirplane_Input))]
    [RequireComponent(typeof(AC_XboxAirplane_Input))]
    public class AC_Airplane_Controller : AC_BaseRigidbody_Controller
    {
        #region variables
        [Header("Base Airplane Properties")]
        [Tooltip("Drag and drop here the  AC_BaseAirplane_Input.cs OR AC_XboxAirplane_Input.cs")]
        public AC_BaseAirplane_Input input;

        [Header("Airplane Characteristics")]
        [Tooltip("Drag and drop here the  AC_Airplane_Characteristics.cs")]
        public AC_Airplane_Characteristics characteristics;

        [Tooltip("Weight is in pounds")]
        public float airplaneWeight = 1200f;

        [Tooltip("Initialize an empty object and set it in  airplane body. That position is Center of Gravity of the Airplane. Hook that object here")]
        public Transform centerOfGravity;

        [Header("Engines")]
        [Tooltip("Initialize an empty object and set it in  airplane body at the place of Engine. Add AC_Airplane_Engine script to that object. Hook that engine object here")]
        public List<AC_Airplane_Engine> engines =  new List<AC_Airplane_Engine>();

        [Header("Wheels")]
        [Tooltip("Initialize wheel colliders and set it in  airplane body at the place of wheel. Add AC_Airplane_Wheel script to that object. Hook those wheels object here")]
        public List<AC_Airplane_Wheel> wheels =  new List<AC_Airplane_Wheel>();
        
        #endregion

        #region Constants
        const float poundToKilos = 0.453592f;
        #endregion

        #region Builtin Methods
        public override void Start()
        {
            base.Start();

            //calculate final mass in kilos
            float finalMass =  airplaneWeight * poundToKilos;
            
            // if rigid body added then add center of mass
            if (rb){
                rb.mass = finalMass;
                if(centerOfGravity){
                    rb.centerOfMass = centerOfGravity.localPosition;
                } // handel exception 

                // Initialize Airplane characteristics  
                characteristics = GetComponent<AC_Airplane_Characteristics>();  
                if(characteristics){
                    characteristics.InitCharacteristics(rb, input);
                }
            }
            
            // Initialize Wheels
            if (wheels != null){
                if(wheels.Count>0){
                    foreach(AC_Airplane_Wheel wheel in wheels){
                        wheel.initWheel();
                    }
                }
            }  
        }
        #endregion

        #region Custom Methods
        protected override void HandlePhysics()
        {
            if(input){
                HandleEngines();
                HandleCharacteristics();
                HandleSteering();
                HandleBrakes();
                HandleAltitude();
            }// handle else
            
        }

        void HandleEngines(){
            if(engines != null){
                if(engines.Count > 0 ){
                    foreach(AC_Airplane_Engine engine in engines){
                        rb.AddForce(engine.CalculateForce(input.StickyThrottle));
                    }
                }
            }

        }
        void HandleCharacteristics(){
            if (characteristics){
                characteristics.UpdateCharacteristics();
            }
            
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
