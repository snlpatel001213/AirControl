using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;

namespace AirControl
{
    /// <summary>
    /// Creates and controls Lidar
    /// </summary>
    public class Lidar : MonoBehaviour
    {
        public int numberOfIncrements = 360;
        public float maxRange = 100000f;

        [HideInInspector]
        public static float[] distances;
    
        // Use this for initialization
        void Start () {
            distances = new float[numberOfIncrements];
        }

        // Update is called once per frame
        void FixedUpdate () {
            Vector3 fwd = new Vector3(0, 0, 1);
            Vector3 dir;
            RaycastHit hit;
            int indx = 0;

            #region IOSwitch
            
            bool isActive = StaticLidarSchema.IsActive;

            if (isActive)
            {
                numberOfIncrements = StaticLidarSchema.Density;;
                maxRange = StaticLidarSchema.Range;
                StaticLidarSchema.IsActive = false;
            }
            #endregion

            for (int incr = 0; incr < numberOfIncrements; incr++)
            {
                
                indx = incr;
                dir = transform.rotation * Quaternion.Euler(0, incr, 0)*fwd;
            
                
                if (Physics.Raycast(transform.position, dir, out hit, maxRange))
                {
                    distances[indx] = (float)hit.distance;
                }
                else
                {
                    distances[indx] = maxRange;
                }
                Debug.DrawRay(transform.position, dir * distances[indx], Color.red);
                StaticOutputSchema.LidarPointCloud = distances;
            }

        }
    }

}
