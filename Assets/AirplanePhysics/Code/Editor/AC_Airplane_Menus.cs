using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AirControl
{
    /// <summary>
    /// creates "Create New Airplane" menu in the menubar
    /// This helps in easy setup of new airplane
    /// </summary>
    public static class AC_Airplane_Menus
    {
        [MenuItem("Air Control/Create New Airplane")]
        public static void CreateNewAirplane(){

            AC_Airplane_Setuptools.buildNewAirplane("New Airplane");         
        }
    }
}

