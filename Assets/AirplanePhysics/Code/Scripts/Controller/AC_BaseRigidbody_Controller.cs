using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SQLite4Unity3d;
using Commons;
using SqliteDB;

namespace AirControl
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class AC_BaseRigidbody_Controller : MonoBehaviour
    {
        #region Variable
        protected Rigidbody rb;
        protected AudioSource aSource;
        protected SQLiteConnection DB_connection ;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        public virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            aSource = GetComponent<AudioSource>();
            // Dont allow audio to play on start 
            if(aSource){
                aSource.playOnAwake = false;
            }
            // invoking db connection
            // DB_connection = DB_Init();

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(rb){
                HandlePhysics();
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void HandlePhysics(){

        }
        #endregion
    }

}
