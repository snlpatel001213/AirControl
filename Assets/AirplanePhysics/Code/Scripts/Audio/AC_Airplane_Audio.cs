using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;

namespace AirControl{

    /// <summary>
    /// Setup airplane audio component
    /// </summary>
    public class AC_Airplane_Audio : MonoBehaviour
    {
        #region Variables
        [Header("Airplane Audio Properties")]
        [Tooltip("Attach base input script to audio group; Drag and drop that script component on input")]
        public AC_BaseAirplane_Input input;
        [Tooltip("Create empty Gameobject - idleSource, add audiosource to it; add audio idleSource file to audiosource; Drag and drop that game object on idleSource")]
        public AudioSource idleSource;
        [Tooltip("Create empty Gameobject - fullThrottleSource, add audiosource to it; add audio fullThrottleSource file to audiosource; Drag and drop that game object on fullThrottleSource")]
        public AudioSource fullThrottleSource;
        private float maxPitchValue = 1.5f;

        private float fullVolumeValue;
        private bool isShutOff = false;
        private float finalPitchValue;
        // Decrease the volume gradually when engine cutoff;
        private float fadeVolumeRate = 0.005f;
        private bool currentEnableAudio = true;
        #endregion

        #region Propeties
        // property that listens to the engine shutoff script events
        public bool ShutEngineOff
        {
            set{isShutOff = value;}
        }
        
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            if(fullThrottleSource)
            {
                fullThrottleSource.volume = 0f;

            }
            
        }

        // Update is called once per frame
        void Update()
        {
            /// <summary>
            /// Switching audio as per the Input/Output from communicator
            /// </summary>
            #region IOSwitch
            
            bool isActive = StaticAudioSchema.IsActive;
            if(isActive)
            {
                bool enableAudio = StaticAudioSchema.EnableAudio;
                idleSource.enabled = enableAudio;
                fullThrottleSource.enabled = enableAudio;
                currentEnableAudio = enableAudio;
                //logging
                string logString = " Audio set to : "+enableAudio;
                StaticLogger.Log = logString;
                Debug.unityLogger.Log(logString);
                StaticAudioSchema.IsActive = false;
            }
            #endregion

            if(input)
            {
                if(!isShutOff)
                {
                    HandleAudio();
                }
                else
                {
                    //cutting off audio when engine cutoff
                    fullThrottleSource.volume -= fullThrottleSource.volume * fadeVolumeRate;
                    idleSource.volume -=  idleSource.volume * fadeVolumeRate;
                }
                
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleAudio()
        {
            

            fullVolumeValue = Mathf.Lerp(0f,1f,input.StickyThrottle);
            finalPitchValue = Mathf.Lerp(1f,maxPitchValue,input.StickyThrottle);
            if(fullThrottleSource)
            {
                fullThrottleSource.volume = fullVolumeValue;
                fullThrottleSource.pitch = finalPitchValue ;
            }
        }
        #endregion
    }
}

