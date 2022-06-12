﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
namespace AirControl
{
    /// <summary>
    /// Monitor and updates the flap position  to UI 
    /// </summary>
    public class AC_Airplane_FlapLever : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Flap Lever Properties")]
        [SerializeField]
        private AC_BaseAirplane_Input input;
        public RectTransform parentRect;
        public RectTransform handleRect;
        private float handleSpeed = 2f;
        #endregion

        #region Builtin methods
        void Start(){
            input = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<AC_BaseAirplane_Input>();
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Updates to UI 
        /// </summary>
        public void HandleAirplaneUI()
        {
            if(input && parentRect && handleRect)
            {
                float height = parentRect.rect.height;
                Vector2 wantedHandlePosition = new Vector2(0f, -height * input.NormalizedFlaps);
                handleRect.anchoredPosition = Vector2.Lerp(handleRect.anchoredPosition, wantedHandlePosition, Time.deltaTime * handleSpeed);
            }
        }
        #endregion
    }
}
