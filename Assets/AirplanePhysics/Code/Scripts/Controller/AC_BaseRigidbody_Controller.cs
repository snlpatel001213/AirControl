using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Communicator;

namespace AirControl
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class AC_BaseRigidbody_Controller : MonoBehaviour
    {
        #region Variable
        protected Rigidbody rb;
        protected AudioSource aSource;
        
        #endregion

        #region Builtin Methods
        // Methods to be called before start goes here
        public  virtual void Awake()
        {
            // init DB
            IOInit.CreateSchema();
            
        }
        // Start is called before the first frame update
        public virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            
            aSource = GetComponent<AudioSource>();
            // Dont allow audio to play on start 
            if(aSource){
                aSource.playOnAwake = false;
            }

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(rb){
                HandlePhysics();
                // HandleLocation();
                
            }
            // DB based operations
            
        }
        
        #endregion

        protected virtual void HandlePhysics(){

        }
        
    }

}
