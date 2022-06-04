using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;

namespace AirControl {
    public class AirplaneSelector : MonoBehaviour
    {
        public enum AirplaneSeelctor { Cessna152, F4UCorsair };
        public AirplaneSeelctor activeAirplane;

        void Awake()
        {
            CommonFunctions.ActiveAirplane =  activeAirplane.ToString();
            Debug.Log(CommonFunctions.ActiveAirplane + " | "+ activeAirplane );
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}


