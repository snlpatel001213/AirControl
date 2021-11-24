using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace AirControl
{
    [CustomEditor(typeof(AC_XboxAirplane_Input))]
    public class AC_XboxAirplane_Input_Editor : Editor
    {
       #region  Variables
        private AC_XboxAirplane_Input targetInput;
        #endregion

        #region Builtin Methods
        void OnEnable(){
            targetInput = (AC_XboxAirplane_Input)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // Debugger display
            string debugInfo = "";
            debugInfo += "Pitch : "+ targetInput.Pitch + "\n";
            debugInfo += "Roll : "+ targetInput.Roll+ "\n";
            debugInfo += "Yaw : "+ targetInput.Yaw+ "\n";
            debugInfo += "Throttle : "+ targetInput.Throttle+ "\n";
            debugInfo += "Brake : "+ targetInput.Brake+ "\n";
            debugInfo += "Flaps : "+ targetInput.Flaps+ "\n";

            // custom editor code
            GUILayout.Space(20);
            EditorGUILayout.TextArea(debugInfo, GUILayout.Height(120));
            GUILayout.Space(20);
            Repaint();
        }
        #endregion
    }
 
}
