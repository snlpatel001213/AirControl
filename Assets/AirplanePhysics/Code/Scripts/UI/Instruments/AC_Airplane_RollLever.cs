﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirControl
{
    /// <summary>
    /// Monitor and updates the Roll lever to UI 
    /// </summary>
    public class AC_Airplane_RollLever : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Throttle Lever Properties")]
        public AC_BaseAirplane_Input input;
        public RectTransform parentRect;
        public RectTransform handleRect;
        private float handleSpeed = 2f;
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
                Vector2 wantedHandlePosition = new Vector2(0f, height/2 * input.Roll);
                handleRect.anchoredPosition = Vector2.Lerp(handleRect.anchoredPosition, wantedHandlePosition, Time.deltaTime * handleSpeed);
            }
        }
        #endregion
    }
}