using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Communicator;
namespace AirControl
{
    public class LevelControl:MonoBehaviour
    {
        void Update()
        {
            if(StaticTransactionSchema.LevelReload){
                RestartLevel();
                //set back the transaction to deault to avoid multiple firing
                StaticTransactionSchema.LevelReload = false;
            }
        }
        public void RestartLevel() //Restarts the level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}

