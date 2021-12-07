using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AirControl{
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
        private float finalPitchValue;
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
            if(input)
            {
                HandleAudio();
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
                fullThrottleSource.pitch = finalPitchValue;
            }
        }
        #endregion
    }
}

