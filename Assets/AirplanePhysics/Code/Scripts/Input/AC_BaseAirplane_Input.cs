using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using SqliteDB;
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

        protected KeyCode cameraKey = KeyCode.C;
        protected bool camerSwitch = false;

        // Slowly move the throttle
        [Header("Sticky throttle value control how the throttle can be moved")]
        public float throttleSpeed = 0.5f;
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
        public float NormalizedFlaps{
            get{
                return (float)flaps / maxFlapIncrements;
            }
        }
        public float Brake{
            get{return brake;}
        }
        public float StickyThrottle {
            get{return stickyThrottle;}
        }
        public bool CameraSwitch{
            get{return camerSwitch;}
        }
        #endregion
        
        
        #region Builtin Methods
        // Update is called once per frame
        void Update()
        {
            HandleInput();
            StickyThrottleControl();
            ClampInputs();

             // Keeping Get connection in the update loop is essential to avoid the lag
            SQLiteConnection connection = DB_Init.GetConnection();
            DBGetter(connection);
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

            //camera switch key
            camerSwitch = Input.GetKeyDown(cameraKey);

        }

        void StickyThrottleControl(){
            stickyThrottle = stickyThrottle + (throttle*throttleSpeed*Time.deltaTime);
            stickyThrottle =  Mathf.Clamp01(stickyThrottle);

        }
        protected void ClampInputs()
        {
            pitch = Mathf.Clamp(pitch,-1f,1f);
            roll = Mathf.Clamp(roll,-1f,1f);
            yaw = Mathf.Clamp(yaw,-1f,1f);
            throttle = Mathf.Clamp(throttle,-1f,1f);
            brake = Mathf.Clamp(brake,0f,1f);
            flaps = Mathf.Clamp(flaps, 0, maxFlapIncrements);
        }

        protected void DBGetter(SQLiteConnection connection)
        {
            string DBInputControlType = DB_Functions.getInputControType(connection);
            // if control type is code then lock the controls and fly it
            // else let user fly manually
             if(DBInputControlType == "Code")
             {
                throttle = DB_Functions.getThrottle(connection);
                stickyThrottle = DB_Functions.getStickyThrottle(connection);
                pitch = DB_Functions.getPitch(connection);
                roll = DB_Functions.getRoll(connection);
                yaw = DB_Functions.getYaw(connection);
                brake = DB_Functions.getBrake(connection);
                flaps = DB_Functions.getFlaps(connection);
             }
        }
           
    }

        #endregion
}



