using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Commons;

namespace AirControl
{
    public class autopilot : MonoBehaviour
    {
        #region Variables
        public Rigidbody airplane;
        public Transform target;
        public float speed =10f;

        #endregion

        #region InbuildMethod
        // Start is called before the first frame update
        void Awake(){
            airplane = GameObject.Find(CommonFunctions.ActiveAirplane).GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (airplane.transform.position.y > 100)
            {
                Debug.Log("Looking at target");
                
                Vector3 relativePos =  target.position - airplane.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                airplane.transform.localRotation = rotation;
            }
            
        }
        #endregion
    }
}

