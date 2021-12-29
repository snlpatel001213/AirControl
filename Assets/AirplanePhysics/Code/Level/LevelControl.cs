using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SqliteDB;
namespace AirControl
{
    public class LevelControl:MonoBehaviour
    {
        void Update()
        {
            if(DB_StaticTransactions.LevelReload){
                RestartLevel();
                //set back the transaction to deault to avoid multiple firing
                DB_StaticTransactions.LevelReload = false;
            }
        }
        public void RestartLevel() //Restarts the level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}

