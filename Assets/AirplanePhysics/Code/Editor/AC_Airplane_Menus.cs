using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AirControl
{
    public static class AC_Airplane_Menus
    {
        [MenuItem("Air Control/Create New Airplane")]
        public static void CreateNewAirplane(){
            Debug.Log("Creating new airplane");
            GameObject  currentSelected =  Selection.activeGameObject;
            if (currentSelected){
                
                AC_Airplane_Controller currentController = currentSelected.AddComponent<AC_Airplane_Controller>();
                GameObject currentCenterOfGravty =  new GameObject("CenterOfGrvity");
                currentCenterOfGravty.transform.SetParent(currentSelected.transform);
                currentController.centerOfGravity = currentCenterOfGravty.transform;
                GameObject Graphics_GRP = new GameObject("Graphics_GRP");
                GameObject Collision_GRP = new GameObject("Collision_GRP");
                Graphics_GRP.transform.SetParent(currentSelected.transform);
                Collision_GRP.transform.SetParent(currentSelected.transform);

            }

        }
    }
}

