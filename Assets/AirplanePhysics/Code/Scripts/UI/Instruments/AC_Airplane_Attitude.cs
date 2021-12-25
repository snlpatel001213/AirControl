﻿using System.Collections;
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
        protected float bankAngle; 
        protected float pitchAngle;
        #endregion

        #region Properties
        public float BankAngle{
            get{return bankAngle;}
        }
        public float PitchAngle{
            get{return pitchAngle;}
        }
        #endregion


        #region Interface Methods
        public void HandleAirplaneUI(SQLiteConnection connection)
        {
            if(airplane)
            {
                //Create Angles
                bankAngle = Vector3.Dot(airplane.transform.right, Vector3.up) * Mathf.Rad2Deg;
                pitchAngle = Vector3.Dot(airplane.transform.forward, Vector3.up) * Mathf.Rad2Deg;

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
                DB_StaticEternalOutput.BankAngle = BankAngle;
                DB_StaticEternalOutput.PitchAngle = PitchAngle;

                #endregion 

            }
        }
        #endregion
    }
}
