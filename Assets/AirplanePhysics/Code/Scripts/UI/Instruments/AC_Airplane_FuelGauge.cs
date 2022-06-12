using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
namespace AirControl
{
    /// <summary>
    /// Monitor and updates the Fuel level to UI 
    /// </summary>
    public class AC_Airplane_FuelGauge : MonoBehaviour, IAirplaneUI 
    {
        #region Variables
        [Header("Fuel Guage Properties")]
        [SerializeField]
        private AC_Airplane_Fuel fuel;
        public RectTransform pointer;
        public Vector2 minMaxRotation = new Vector2(-90f, 90f);
        #endregion

        #region Builtin methods
        void Start(){
            fuel = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<AC_Airplane_Fuel>();
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Updates to UI 
        /// </summary>
        public void HandleAirplaneUI()
        {
            if(fuel && pointer)
            {
                float wantedRotation = Mathf.Lerp(minMaxRotation.x, minMaxRotation.y, fuel.NormalizedFuel);
                pointer.rotation = Quaternion.Euler(0f, 0f, -wantedRotation);
            }
        }
        #endregion

    }
}
