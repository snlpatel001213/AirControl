using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        private double MaxR = 100;
        private bool hasEntered;

                
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

        void OnCollisionExit(Collision col)
        {
            if(col.gameObject.tag!= "Runway" &&  !hasEntered)
            {
                hasEntered = true;
                MaxR -=10f;
                Debug.LogFormat("Collided with {0} {1} , Counter {2}",col.gameObject.name,col.gameObject.tag, CommonFunctions.Counter);  
                StaticOutputSchema.IfCollision=true;
                StaticOutputSchema.collisionObject = col.gameObject.tag;
            }
            return;
        }

        void OnTriggerExit(Collider col)
        {
           if(col.CompareTag("Fence") &&  !hasEntered)
            {
                hasEntered = true;
                MaxR -=10f;
                Debug.LogFormat("Collided with {0} {1} , Counter {2}",col.attachedRigidbody.gameObject.name,col.gameObject.tag, CommonFunctions.Counter);  
                StaticOutputSchema.IfCollision=true;
                StaticOutputSchema.collisionObject = col.gameObject.tag;
            }
            return;
        }
        
        #endregion

        protected virtual void HandlePhysics(){

        }
        
    }

}
