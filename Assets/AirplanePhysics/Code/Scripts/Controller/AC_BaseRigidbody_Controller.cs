using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Commons;
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
        private bool hasEntered;

                
        #endregion

        #region Builtin Methods
        // Methods to be called before start goes here
        public  virtual void Awake()
        {
#if UNITY_EDITOR
            // init DB
            // not applicable to unity webGL deployment as this is not supported
            IOInit.CreateSchema();
#endif
            
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
            }
            
        }

        void OnCollisionStay(Collision col)
        {
            //Allow collision penality once
            if (StaticOutputSchema.IfCollision == false)
            {
                //reward function
                StaticOutputSchema.Reward -= 100f;
                // Collision detection
                DateTime now = DateTime.Now;
                // Debug.LogFormat(now +" - Collided with : {0} , Counter : {1}, reward : {2}",col.gameObject.tag, StaticOutputSchema.Counter, StaticOutputSchema.Reward);  
                StaticOutputSchema.IfCollision=true;
                StaticOutputSchema.CollisionObject = col.gameObject.tag;
            }
        
            
        }

        // void OnTriggerStay(Collider col)
        // {
        //     if (StaticOutputSchema.IfCollision == false)
        //     {
        //       if(col.CompareTag("Fence"))
        //         {
        //             //reward function
        //             StaticOutputSchema.Reward -= 100f;
        //             // Collision detection
        //             DateTime now = DateTime.Now;
        //             Debug.LogFormat(now +" - Collided with : {0} , Counter :{1}, reward : {2}",col.gameObject.tag, StaticOutputSchema.Counter, StaticOutputSchema.Reward);  
        //             StaticOutputSchema.IfCollision=true;
        //             StaticOutputSchema.CollisionObject = col.gameObject.tag;
        //         }
        //     }
           
        // }      
        #endregion

        protected virtual void HandlePhysics(){

        }

        
        
    }

}
