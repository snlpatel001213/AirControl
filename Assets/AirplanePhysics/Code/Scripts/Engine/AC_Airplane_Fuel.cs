using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace AirControl
{
    /// <summary>
    /// Control fuel consumption
    /// </summary>
    public class AC_Airplane_Fuel : MonoBehaviour
    {
        #region Variables
        [Header("Fuel Properties")]
        [Tooltip("The total number of gallons in the fuel tank.")]
        public float fuelCapacity = 26f;
        [Tooltip("The average fuel burn per hour")]
        public float fuelBurnRate = 6.1f;

        [Header("Events")]
        public UnityEvent onFuelFull = new UnityEvent();
        #endregion


        #region Properties
        private float currentFuel;
        public float CurrentFuel
        {
            get{return currentFuel;}
        }

        private float normalizeFuel;
        public float NormalizedFuel
        {
            get{return normalizeFuel;}
        }
        #endregion


        #region Custom Methods
        /// <summary>
        /// Initialize full to full capacity
        /// </summary>
        public void InitFuel()
        {
            currentFuel = fuelCapacity;
        }
        /// <summary>
        /// Add fuel in case of mid airfueling facility
        /// </summary>
        /// <param name="aFuelAmount"></param>
        public void AddFuel(float aFuelAmount)
        {
            currentFuel += aFuelAmount;
            currentFuel = Mathf.Clamp(currentFuel, 0f, fuelCapacity);

            if(currentFuel >= fuelCapacity)
            {
                if(onFuelFull != null)
                {
                    onFuelFull.Invoke();
                }
            }
        }
        /// <summary>
        /// Reset fuel in case of mid airfueling facility
        /// </summary>
        public void ResetFuel()
        {
            currentFuel = fuelCapacity;
        }
        /// <summary>
        /// Fuel burnout calculations
        /// </summary>
        /// <param name="aPrecentage"></param>
        public void UpdateFuel(float aPrecentage)
        {
            float currentBurn = ((fuelBurnRate * aPrecentage) / 3600f) * Time.deltaTime;
            currentFuel -= currentBurn;
            currentFuel = Mathf.Clamp(currentFuel, 0f, fuelCapacity);
            // Debug.Log("currentFuel   -- "+currentFuel);

            normalizeFuel = currentFuel / fuelCapacity;
        }
        #endregion
    }
}
