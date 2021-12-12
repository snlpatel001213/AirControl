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

            AC_Airplane_Setuptools.buildNewAirplane("New Airplane");         
        }
    }
}

