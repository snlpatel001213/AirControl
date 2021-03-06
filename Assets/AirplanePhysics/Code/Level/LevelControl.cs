using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Communicator;
namespace AirControl
{   
    /// <summary>
    /// Level related controls
    /// </summary>
    public class LevelControl:MonoBehaviour
    {
        
        /// <summary>
        /// Constantly check for the reset level trigger
        /// </summary>
        void Update()
        {
            if(StaticLevelSchema.LevelReload){
                RestartLevel();
                //set back the transaction to deault to avoid multiple firing
                StaticLevelSchema.LevelReload = false;
            }
        }
        
        /// <summary>
        /// Reset the level to startover
        /// </summary>
        public void RestartLevel() //Restarts the level
        {
            //resetting rewards before reload
            StaticOutputSchema.Reward=0;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); 
            //resetting rewards after reload
            StaticOutputSchema.Reward=0;
        }

        /// <summary>
        /// Function to quit  the application from Python API 
        /// To be added in v2.0.0
        /// </summary>
        void applicationQuit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
            #else
            Application.Quit();
            #endif
        }

    }
}

