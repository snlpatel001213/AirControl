using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  AirControl
{
    public class AC_BaseAirplane_Input : MonoBehaviour
    {
        #region Variable

        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;
        protected float throttle = 0f;
        protected float brake = 0f;
        public int maxFlapIncrements=2; // maximum increment allowed to flaps
        protected int flaps = 0;

        // Slowly move the throttle
        [Header("Sticky throttle value control how the throttle can be moved")]
        public float throttleSpeed = 0.8f;
        protected float stickyThrottle;

        #endregion

        #region Properties
        public float Pitch{
            get{return pitch;}
        }
        public float Roll{
            get{return roll;}
        }
        public float Yaw{
            get{return yaw;}
        }
        public float Throttle{
            get{return throttle;}
        }
        public int Flaps{
            get{return flaps;}
        }
        public float Brake{
            get{return brake;}
        }
        public float StickyThrottle {
            get{return stickyThrottle;}
        }
        #endregion
        
        
        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }
        #endregion
        
        #region Custom Methods
        protected virtual void HandleInput(){
            // Process pitch, roll, yaw and throttle
            pitch =  Input.GetAxis("Vertical");
            roll =  Input.GetAxis("Horizontal");
            yaw =  Input.GetAxis("yaw");
            throttle =  Input.GetAxis("throttle");
            StickyThrottleControl();
            // Process brakes bool
            brake =  Input.GetKey(KeyCode.Space)?1f:0f;
            // Process flaps
            // get GetKeyDown is used because it fires only once when key pressed. GetKey constantly fire events
            if(Input.GetKeyDown(KeyCode.F)){
                flaps+=1;
            }
            if(Input.GetKeyDown(KeyCode.G)){
                flaps-=1;
            }
            flaps =   Mathf.Clamp(flaps, 0,maxFlapIncrements);

        }

        void StickyThrottleControl(){
            stickyThrottle = stickyThrottle + (throttle*throttleSpeed*Time.deltaTime);
            stickyThrottle =  Mathf.Clamp01(stickyThrottle);

        }
        #endregion
    }


}
