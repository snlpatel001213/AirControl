using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Communicator;
using Commons;
using UnityEngine.UI;
using System;
using UnityEditor;

namespace AirControl
{

    public enum AirplaneState{
        Taxiing, 
        GROUNDED, 
        FLYING,
    }
    /// <summary>
    /// Master Controller, controls the entire Airplane
    /// it implements function to Handle Engines,   Handle Characteristics,  Handle ControlSurfaces,  Handle Wheel and  Handle Altitude
    /// </summary>
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
        
        [Header("Control Surfaces")]
        [Tooltip("Initialize empty control surfaces. Add AC_Airplane_ControlSurface script to that object. Hook wheels object here")]
        public List<AC_Airplane_ControlSurface> controlSurfaces = new List<AC_Airplane_ControlSurface>();

        // Starting from ground
        private AirplaneState airplaneState =  AirplaneState.Taxiing;
        [SerializeField] private bool isGrounded =  true;
        [SerializeField] private bool isTaxiing =  true;
        [SerializeField] private bool isFlying =  false;

        [Header("Airplane States")]
        [Tooltip("Attach Airplane state gameobject from UI canvas here")]
        public Toggle IsGroundedObject;
        public Toggle IsFlyingObject;
        public Toggle IsTaxiingObject;
        

        // Meadian sea level
        private float currentMSL;
        // Above Ground Level
        private float currentAGL;

        // To detect if the Airplane is stuck 
        private Vector3 lastAirplanePosition;
        private Vector3 currAirplanePosition;

        // private int lastCommCounter=0;
        // private int currCommCounter=0;

        
        #endregion

        #region Properties
        public float CurrentMSL{
            get{return currentMSL;}
        }
        public float CurrentAGL{
            get{return currentAGL;}
        }
        #endregion

        #region Constants
        const float poundToKilos = 0.453592f;
        const float metersToFeets = 3.28084f;
        private float start_x; 
        private float start_y;
        private float start_z;
        #endregion

        #region Builtin Methods
        /// <summary>
        /// Regulate initilaization like mass of the Vehicle, Gravity, Wheels and Characteristics
        /// </summary>
        public override void Start()
        {
            base.Start();

            //calculate final mass in kilos
            float finalMass =  airplaneWeight * poundToKilos;
            start_x = rb.position.x;
            start_y = rb.position.y; 
            start_z = rb.position.z;
            Debug.LogFormat("Starting Position  x : {0} y: {1} z: {2} ",start_x, start_y, start_z );
            
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
            InvokeRepeating("CheckGrounded", 1f, 1f); 
            InvokeRepeating("DetectAirplaneStuck", 5f, 5f); 
        }
        void Update()
        {
            rewardCalculator();
            UpdatePosition();
        }

        /// <summary>
        /// calculate abolute and relative position of the Airplane. 
        /// Absolute -  w.r.t. world
        /// Relative - w.r.t initila position
        /// </summary>
        void UpdatePosition(){
            Vector3 absolutePosition = rb.position;
            StaticOutputSchema.PosXAbs = absolutePosition.x;
            StaticOutputSchema.PosYAbs = absolutePosition.y;
            StaticOutputSchema.PosZAbs = absolutePosition.z;
            Vector3 relativePosition = rb.position - new Vector3(start_x, start_y, start_z);
            StaticOutputSchema.PosXRel =  relativePosition.x;
            StaticOutputSchema.PosYRel =  relativePosition.x;
            StaticOutputSchema.PosZRel =  relativePosition.x;
        }

        /// <summary>
        /// Calculates reward for RL optimization
        /// </summary>
        // void rewardCalculator(){
        //     float Height = 100f;
        //     float Base = start_y;
        //     float RateOfInclination = 230f;
        //     float Angle = 3f;
        //     float ideal_height= Height+((Base-Height)/(1.0f+ (float)Math.Pow(rb.position.z/RateOfInclination,Angle)));
        //     float Penalty = (float)Math.Pow(ideal_height-rb.position.y, 2);
        //     CommonFunctions.MaxR -= Penalty;
        //     Debug.Log("Reward : "+CommonFunctions.MaxR );
        //     StaticOutputSchema.Reward = CommonFunctions.MaxR;
        //     // Debug.LogFormat( "Ideal Height : {0} |  Position Up (y) : {1} | Position Forward (z) : {2} ",ideal_height, rb.position.y, rb.position.z);
        // }
        void rewardCalculator(){
            // Wait for airplane to reach 100 mtr
            // if ((rb.position.y - start_y) >= 100){
            //     CommonFunctions.MaxR += 100;
            // }
            // Debug.Log("Reward : "+CommonFunctions.MaxR );
            if (isTaxiing){
                Debug.DrawLine(rb.transform.forward,rb.velocity);
                float forward_velocity =  Vector3.Dot(rb.velocity,rb.transform.forward);
                // Debug.LogFormat("Forward Velocity : {0}, Direction : {1}, Forward Vector {2}",rb.velocity, rb.transform.forward,forward_velocity);
                float l2_side = Mathf.Pow(Vector3.Dot(rb.velocity,rb.transform.right),2);
                float l2_up = Mathf.Pow(Vector3.Dot(rb.velocity,rb.transform.up),2);
                // Debug.LogFormat("other Side penalty : {0}, up reward : {1}",l2_side, l2_up);
                CommonFunctions.MaxR = forward_velocity+l2_up-l2_side;
            }
            if (isFlying){
                Debug.DrawLine(rb.transform.forward,rb.velocity);
                float forward_velocity =  Vector3.Dot(rb.velocity,rb.transform.forward);
                // Debug.LogFormat("Forward Velocity : {0}, Direction : {1}, Forward Vector {2}",rb.velocity, rb.transform.forward,forward_velocity);
                float l2_side = Mathf.Pow(Vector3.Dot(rb.velocity,rb.transform.right),2);
                float l2_up = Mathf.Pow(Vector3.Dot(rb.velocity,rb.transform.up),2);
                // Debug.LogFormat("other Side penalty : {0}, up reward : {1}",l2_side, l2_up);
                CommonFunctions.MaxR = forward_velocity+currentMSL-l2_side; ;
            }
            
        
        }

        #endregion

        #region Custom Methods
        /// <summary>
        /// Handles physics related to Engine, Characteristics, Control surfaces wheel and Altitude
        /// </summary>
        protected override void HandlePhysics()
        {
            if(input)
            {
                HandleEngines();
                HandleCharacteristics();
                HandleControlSurfaces();
                HandleWheel();
                HandleAltitude();
            }// handle else

            // DB based update
            
        }

        /// <summary>
        /// Handle all the Engine
        /// </summary>
        void HandleEngines()
        {
            if(engines != null)
            {
                if(engines.Count > 0 )
                {
                    foreach(AC_Airplane_Engine engine in engines)
                    {
                        rb.AddForce(engine.CalculateForce(input.StickyThrottle));
                    }
                }
            }

        }
        /// <summary>
        /// Handle Airplane characteristics
        /// </summary>
        void HandleCharacteristics( )
        {
            if (characteristics)
            {
                characteristics.UpdateCharacteristics();
            }
            
        }
        /// <summary>
        /// Handle control surfaces such as rudder, alurone, Flaps etc
        /// </summary>
        void HandleControlSurfaces( )
        {
            if(controlSurfaces.Count > 0)
            {
                foreach (AC_Airplane_ControlSurface controlSurface in controlSurfaces){
                    controlSurface.HandleControlSurface(input);
                }
            }
        }
        /// <summary>
        /// Handle all the wheels
        /// </summary>
        void HandleWheel( ){
            if (wheels.Count>0)
            {
                foreach (AC_Airplane_Wheel wheel in wheels)
                {
                    wheel.HandleWheel(input);
                }
            }

        }
        /// <summary>
        /// Calculate altitude of the Airplane
        /// </summary>
        void HandleAltitude(){
            currentMSL  =  transform.position.y * metersToFeets;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if(hit.transform.tag == "Ground" || hit.transform.tag == "Building")
                {
                    currentAGL = (hit.distance) *metersToFeets;
                }
            }

            #region DBArea
            StaticOutputSchema.MSL = currentMSL;
            StaticOutputSchema.AGL = currentAGL;
            #endregion
        }

        /// <summary>
        /// Check if all the Airplane wheel are grounded and determine the current state
        /// </summary>
        void CheckGrounded()
        {
            if(wheels.Count > 0){
                int groundedCount = 0;
                foreach(AC_Airplane_Wheel wheel in wheels)
                {
                    if(wheel.isGrounded)
                    {
                        groundedCount++;
                    }
                }
                if(groundedCount ==  wheels.Count)
                {
                    if(rb.velocity.magnitude > 1f){
                        isTaxiing = true;
                        isGrounded = false;
                        isFlying = false;
                        // update to API and UI
                        IsGroundedObject.isOn = StaticOutputSchema.IsGrounded = isGrounded;
                        IsTaxiingObject.isOn = StaticOutputSchema.IsTaxiing = isTaxiing;
                        IsFlyingObject.isOn = StaticOutputSchema.IsFlying = isFlying;
                    }
                    else{
                        isTaxiing = false;
                        isGrounded = true;
                        isFlying = false;
                        // update to API and UI
                        IsGroundedObject.isOn = StaticOutputSchema.IsGrounded = isGrounded;
                        IsTaxiingObject.isOn = StaticOutputSchema.IsTaxiing = isTaxiing;
                        IsFlyingObject.isOn = StaticOutputSchema.IsFlying = isFlying;
                    }
                }
                else
                {
                    isTaxiing = false;
                    isGrounded = false;
                    isFlying = true;
                    // update to API and UI
                    IsGroundedObject.isOn = StaticOutputSchema.IsGrounded = isGrounded;
                    IsTaxiingObject.isOn = StaticOutputSchema.IsTaxiing = isTaxiing;
                    IsFlyingObject.isOn = StaticOutputSchema.IsFlying = isFlying;
                }

            }
        }
        /// <summary>
        /// Checking if the airplane is stuck in the same position. If it is stuck, it will reload the level.
        /// </summary>
        private void DetectAirplaneStuck()
        {
            currAirplanePosition = rb.transform.localPosition;
            if(currAirplanePosition == lastAirplanePosition)
            {
                StaticOutputSchema.log = "Airplane was stuck";
                Debug.LogError("Airplane was stuck");
                
                // Relaod the level 
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); 
                StaticOutputSchema.IfCollision = true;
                StaticOutputSchema.CollisionObject = "Stuck";
            }
            lastAirplanePosition = currAirplanePosition;
            
        }

      
        #endregion
    }

}
