using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AirControl
{
    public class AC_XboxAirplane_Input : AC_BaseAirplane_Input
    {
        #region Variable

        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion

        #region Custom Methods
        /// make sure you update input settings in Unity
        /// Then Only Xbox will work properly 
        /// Refer to resources for setup
        // 1. https://answers.unity.com/questions/1350081/xbox-one-controller-mapping-solved.html
        // 2. https://www.udemy.com/course/intro-to-airplane-physics-in-unity-3d/learn/lecture/10348654#questions
        protected override void HandleInput()
        {
            // Process pitch, roll, yaw and throttle
            pitch =  Input.GetAxis("Vertical");
            roll =  Input.GetAxis("Horizontal");
            yaw =  Input.GetAxis("X_RH_Stick");
            throttle =  Input.GetAxis("X_RV_Stick");
            // Process brakes bool
            brake =  Input.GetAxis("Fire1");
            // Process flaps
            // get GetKeyDown is used because it fires only once when key pressed. GetKey constantly fire events
            if(Input.GetButtonDown("X_R_Bumper")){
                flaps+=1;
            }
            if(Input.GetButtonDown("X_L_Bumper")){
                flaps-=1;
            }
            flaps =   Mathf.Clamp(flaps, 0,maxFlapIncrements);

        }
        #endregion

    }

}
