using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using SqliteDB;

namespace AirControl
{
    public class AC_Airplane_Attitude : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Attitude Indicator Properties")]
        public AC_Airplane_Controller airplane;
        public RectTransform bgRect;
        public RectTransform arrowRect;
        #endregion


        #region Interface Methods
        public void HandleAirplaneUI(SQLiteConnection connection)
        {
            if(airplane)
            {
                //Create Angles
                float bankAngle = Vector3.Dot(airplane.transform.right, Vector3.up) * Mathf.Rad2Deg;
                float pitchAngle = Vector3.Dot(airplane.transform.forward, Vector3.up) * Mathf.Rad2Deg;

                //Handle UI Elements
                if(bgRect)
                {
                    Quaternion bankRotation  = Quaternion.Euler(0f, 0f, bankAngle);
                    bgRect.transform.rotation = bankRotation;

                    Vector3 wantedPosition = new Vector3(0f, -pitchAngle, 0f);
                    bgRect.anchoredPosition = wantedPosition;

                    if(arrowRect)
                    {
                        arrowRect.transform.rotation = bankRotation;
                    }
                }
                #region DBArea
                //Set value of AGL and MSL to DB
                DB_Functions.SetAirplaneAngle(connection, bankAngle, pitchAngle);
                #endregion 

            }
        }
        #endregion
    }
}
