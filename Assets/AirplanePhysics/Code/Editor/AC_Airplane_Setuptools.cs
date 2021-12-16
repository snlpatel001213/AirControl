using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AirControl{
    public static class AC_Airplane_Setuptools
    {
        # region Variables
        #endregion
        
        #region Custom Methods
        public static void buildNewAirplane(string airplaneName)
        {
            //create root object
            GameObject rootGameObject =  new GameObject(airplaneName, typeof(AC_Airplane_Controller), typeof(AC_BaseAirplane_Input));
            //create center of gravity
            GameObject currentCenterOfGravty =  new GameObject("CenterOfGrvity");
            currentCenterOfGravty.transform.SetParent(rootGameObject.transform, worldPositionStays:false);

            // currentController.centerOfGravity = currentCenterOfGravty.transform;
            // AC_Airplane_Controller currentController = rootGameObject.AddComponent<AC_Airplane_Controller>();

            // create base components or find them
            AC_BaseAirplane_Input input = rootGameObject.GetComponent<AC_BaseAirplane_Input>();
            AC_Airplane_Controller controller = rootGameObject.GetComponent<AC_Airplane_Controller>();
            AC_Airplane_Characteristics characteristics = rootGameObject.GetComponent<AC_Airplane_Characteristics>();
            //setup the airplane
            if(controller){

                //Assign core components
                controller.input =  input;
                controller.characteristics = characteristics;
                controller.centerOfGravity = currentCenterOfGravty.transform;

                // Create Hierarchy groups
                GameObject Graphics_GRP = new GameObject("Graphics_GRP");
                GameObject Collision_GRP = new GameObject("Collision_GRP");
                
                Graphics_GRP.transform.SetParent(rootGameObject.transform, false);
                Collision_GRP.transform.SetParent(rootGameObject.transform, false);
                
                // setup collision group
                setupCollisionGRP(Collision_GRP);

                // Create engine
                setupEngine(rootGameObject, controller);

                // Create audio group
                setupAudio(rootGameObject);
                
                // Setup control surfaces
                setupControlSurfaces(rootGameObject);

                // Setup UI GRP
                // No need to call this method as UI grp is common to all
                // setupUIGRP(rootGameObject);

            }
        }
        static void setupCollisionGRP(GameObject Collision_GRP){
            GameObject Airplane = new GameObject("Airplane"); 
            GameObject Wheel_GRP = new GameObject("Wheel_GRP"); 
            Airplane.transform.SetParent(Collision_GRP.transform);
            Wheel_GRP.transform.SetParent(Collision_GRP.transform);
        }
        
        static void setupEngine(GameObject root, AC_Airplane_Controller controller){
            GameObject engineGO = new GameObject("Engine", typeof(AC_Airplane_Engine));
            AC_Airplane_Engine engine = engineGO.GetComponent<AC_Airplane_Engine>();
            controller.engines.Add(engine);
            engineGO.transform.SetParent(root.transform, false);
        }

        static void setupAudio(GameObject root){
            GameObject Audio_GRP = new GameObject("Audio_GRP");
            Audio_GRP.transform.SetParent(root.transform, false);
            GameObject Idle = new GameObject("Idle");
            Idle.AddComponent<AudioSource>();
            Idle.transform.SetParent(Audio_GRP.transform, false);
            GameObject FullThrottle = new GameObject("FullThrottle");
            FullThrottle.AddComponent<AudioSource>();
            FullThrottle.transform.SetParent(Audio_GRP.transform, false);

        }

        static void setupControlSurfaces(GameObject root){
            
            // Create control surfaces
            GameObject Control_Surfaces_GRP = new GameObject("Control_Surfaces_GRP"); 
            Control_Surfaces_GRP.transform.SetParent(root.transform, false);
            // Setup individual control surfaces
            GameObject Elevator = new GameObject("Elevator", typeof(AC_Airplane_ControlSurface));
            Elevator.transform.SetParent(Control_Surfaces_GRP.transform);
            GameObject Rudder = new GameObject("Rudder", typeof(AC_Airplane_ControlSurface));
            Rudder.transform.SetParent(Control_Surfaces_GRP.transform);
            GameObject LeftAilerons = new GameObject("LeftAilerons", typeof(AC_Airplane_ControlSurface));
            LeftAilerons.transform.SetParent(Control_Surfaces_GRP.transform);
            GameObject RightAilerons = new GameObject("RightAilerons", typeof(AC_Airplane_ControlSurface));
            RightAilerons.transform.SetParent(Control_Surfaces_GRP.transform);
            GameObject LeftFlaps = new GameObject("LeftFlaps", typeof(AC_Airplane_ControlSurface));
            LeftFlaps.transform.SetParent(Control_Surfaces_GRP.transform);
            GameObject RightFlaps = new GameObject("RightFlaps", typeof(AC_Airplane_ControlSurface));
            RightFlaps.transform.SetParent(Control_Surfaces_GRP.transform);

        }
        
        static void setupUIGRP(GameObject root){
            //setup UI hierarchy
            GameObject UI_GRP = new GameObject("UI_GRP");
            GameObject Airplane_HUD = new GameObject("Airplane_HUD");
            GameObject Canvas = new GameObject("Canvas", typeof(Canvas));
            UI_GRP.transform.SetParent(root.transform, false);
            Airplane_HUD.transform.SetParent(UI_GRP.transform, false);
            Canvas.transform.SetParent(Canvas.transform, false);
            
            //setup Instruments
            GameObject Altimeter = new GameObject("Altimeter", typeof(AC_Airplane_Altimeter));
            Altimeter.transform.SetParent(Canvas.transform);
            GameObject Tachometer = new GameObject("Tachometer", typeof(AC_Airplane_Tachometer));
            Tachometer.transform.SetParent(Canvas.transform);
            GameObject FuelGauge = new GameObject("FuelGauge", typeof(AC_Airplane_FuelGauge));
            FuelGauge.transform.SetParent(Canvas.transform);
            GameObject ThrottleLever = new GameObject("ThrottleLever", typeof(AC_Airplane_ThrottleLever));
            ThrottleLever.transform.SetParent(Canvas.transform);
            GameObject FlapLever = new GameObject("FlapLever", typeof(AC_Airplane_FlapLever));
            FlapLever.transform.SetParent(Canvas.transform);
            GameObject Airspeed = new GameObject("Airspeed", typeof(AC_Airplane_Airspeed));
            Airspeed.transform.SetParent(Canvas.transform);
            GameObject Attitude = new GameObject("Attitude", typeof(AC_Airplane_Attitude));
            Attitude.transform.SetParent(Canvas.transform);
        }
        #endregion
        
    }
}